#include <wiringPi.h>
#include <wiringPiI2C.h>
#include <softPwm.h>

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <ctype.h>
#include <math.h>

#define LAST(k,n) ((k) & ((1<<(n))-1))
#define MID(k,m,n) LAST((k)>>(m),((n)-(m)))


int chan = 0;
int speed = 1000000;
int32_t t_fine;

/* Funkcja zapisuje pojedynczy bajt do komorki pamieci eeprom
 adress - adres komorki pod jaki zapisać
 value -  wartosc do zapisania
 */
int i2cWriteEEPROM(uint16_t adress, uint8_t value, int fd) {
	wiringPiI2CWriteReg16(fd, (adress >> 8), (value << 8) | (adress & 0xff));
	delay(20);
}

/*Funkcja odczytuje jeden bajt z podanego adresu komorki pamieci
 */
uint8_t i2cReadEeeprom(uint16_t adress, int fd) {
	wiringPiI2CWriteReg8(fd, (adress >> 8), (adress & 0xff));
	delay(20);
	return wiringPiI2CRead(fd);
}


enum
{
	BMP280_REGISTER_DIG_T1 = 0x88,
	BMP280_REGISTER_DIG_T2 = 0x8A,
	BMP280_REGISTER_DIG_T3 = 0x8C,
	BMP280_REGISTER_DIG_P1 = 0x8E,
	BMP280_REGISTER_DIG_P2 = 0x90,
	BMP280_REGISTER_DIG_P3 = 0x92,
	BMP280_REGISTER_DIG_P4 = 0x94,
	BMP280_REGISTER_DIG_P5 = 0x96,
	BMP280_REGISTER_DIG_P6 = 0x98,
	BMP280_REGISTER_DIG_P7 = 0x9A,
	BMP280_REGISTER_DIG_P8 = 0x9C,
	BMP280_REGISTER_DIG_P9 = 0x9E,
	BMP280_REGISTER_CHIPID = 0xD0,
	BMP280_REGISTER_VERSION = 0xD1,
	BMP280_REGISTER_SOFTRESET = 0xE0,
	BMP280_REGISTER_CAL26 = 0xE1, /**< R calibration = 0xE1-0xF0 */
	BMP280_REGISTER_STATUS = 0xF3,
	BMP280_REGISTER_CONTROL = 0xF4,
	BMP280_REGISTER_CONFIG = 0xF5,
	BMP280_REGISTER_PRESSUREDATA = 0xF7,
	BMP280_REGISTER_TEMPDATA = 0xFA,
};


typedef struct
{
	uint16_t dig_T1; /**< dig_T1 cal register. */
	int16_t dig_T2;  /**<  dig_T2 cal register. */
	int16_t dig_T3;  /**< dig_T3 cal register. */

	uint16_t dig_P1; /**< dig_P1 cal register. */
	int16_t dig_P2;  /**< dig_P2 cal register. */
	int16_t dig_P3;  /**< dig_P3 cal register. */
	int16_t dig_P4;  /**< dig_P4 cal register. */
	int16_t dig_P5;  /**< dig_P5 cal register. */
	int16_t dig_P6;  /**< dig_P6 cal register. */
	int16_t dig_P7;  /**< dig_P7 cal register. */
	int16_t dig_P8;  /**< dig_P8 cal register. */
	int16_t dig_P9;  /**< dig_P9 cal register. */
} bmp280_calib_data;

bmp280_calib_data calib_data;

int writeRegister(int adress, int data)
{
	unsigned char buff[2];
	buff[0] = (adress & ~0x80);
	buff[1] = data;
	wiringPiSPIDataRW(chan, buff, 2);
	return 1;
}

int readRegister(int adress)
{
	unsigned char buff[2];
	buff[0] = (adress | 0x80);
	wiringPiSPIDataRW(chan, buff, 2);
	return buff[1];
}

int init()
{
	short unsigned wartosc = 0;

	//reset
	wartosc = 0xb6;
	writeRegister(BMP280_REGISTER_SOFTRESET, wartosc);
	delay(1000);

	return 0;
}


int32_t bmp280_compensate_T_int32(int32_t adc_T)
{
	int32_t var1, var2, T;
	var1 = ((((adc_T >> 3) - ((int32_t)calib_data.dig_T1 << 1))) * ((int32_t)calib_data.dig_T2)) >> 11;
	var2 = (((((adc_T >> 4) - ((int32_t)calib_data.dig_T1)) * ((adc_T >> 4) - ((int32_t)calib_data.dig_T1))) >> 12) * ((int32_t)calib_data.dig_T3)) >> 14;
	t_fine = var1 + var2;
	T = (t_fine * 5 + 128) >> 8;
	return T;
}

uint32_t bmp280_compensate_P_int64(int32_t adc_P)
{
	int64_t var1, var2, p;
	var1 = ((int64_t)t_fine) - 128000;
	var2 = var1 * var1 * (int64_t)calib_data.dig_P6;
	var2 = var2 + ((var1 * (int64_t)calib_data.dig_P5) << 17);
	var2 = var2 + (((int64_t)calib_data.dig_P4) << 35);
	var1 = ((var1 * var1 * (int64_t)calib_data.dig_P3) >> 8) + ((var1 * (int64_t)calib_data.dig_P2) << 12);
	var1 = (((((int64_t)1) << 47) + var1)) * ((int64_t)calib_data.dig_P1) >> 33;
	if (var1 == 0) {
		return 0;
	}
	p = 1048576 - adc_P;
	p = (((p << 31) - var2) * 3125) / var1;
	var1 = (((int64_t)calib_data.dig_P9) * (p >> 13) * (p >> 13)) >> 25;
	var2 = (((int64_t)calib_data.dig_P8) * p) >> 19;
	p = ((p + var1 + var2) >> 8) + (((int64_t)calib_data.dig_P7) << 4);
	return (uint32_t)p;
}

float readPreassure()
{
	int32_t rawPreassure = (readRegister(0xF7) << 12) | (readRegister(0xF8) << 4) | (readRegister(0xF9)); //12 empty bits, 8 bits msb, 8 bits lsb, 4 bits, xlsb
	return bmp280_compensate_P_int64(rawPreassure) / 256.0 / 100.0;
}

float readTemperature()
{
	int32_t rawTemperature = (readRegister(0xFA) << 12) | (readRegister(0xFB) << 4) | (readRegister(0xFC)); //12 empty bits, 8 bits msb, 8 bits lsb, 4 bits, xlsb
	return bmp280_compensate_T_int32(rawTemperature) / 100.0;
}

void CalibrationData()
{
	calib_data.dig_P1 = (readRegister(0x8F) << 8) | (readRegister(0x8E));
	calib_data.dig_P2 = (readRegister(0x91) << 8) | (readRegister(0x90));
	calib_data.dig_P3 = (readRegister(0x93) << 8) | (readRegister(0x92));
	calib_data.dig_P4 = (readRegister(0x95) << 8) | (readRegister(0x94));
	calib_data.dig_P5 = (readRegister(0x97) << 8) | (readRegister(0x96));
	calib_data.dig_P6 = (readRegister(0x99) << 8) | (readRegister(0x98));
	calib_data.dig_P7 = (readRegister(0x9B) << 8) | (readRegister(0x9A));
	calib_data.dig_P8 = (readRegister(0x9D) << 8) | (readRegister(0x9C));
	calib_data.dig_P9 = (readRegister(0x9F) << 8) | (readRegister(0x9E));
	calib_data.dig_T1 = (readRegister(0x89) << 8) | (readRegister(0x88));
	calib_data.dig_T2 = (readRegister(0x8B) << 8) | (readRegister(0x8A));
	calib_data.dig_T3 = (readRegister(0x8D) << 8) | (readRegister(0x8C));
}


int main()
{
	printf("\n1 - odczytanie temperatury z modulu MPU6050\n2 - odczytanie wartosci pomiarow z akcelerometru i zyroskopu\n3 - odczytanie aktualnej daty z modulu RTC\n4 - wprowadzenie nowej daty dla modulu RTC\n5 - odczytanie 16 - bitowej wartosci z pamieci EEPROM\n6 - zapisanie 16 - bitowej wartosci do pamieci EEPROM\n7 - BMP280 - odczytanie ID modulu, temperatury i cisnienia atmosferycznego\n8 - zamknij program\n9 - informacja o autorach\n0 - wyswietlenie informacje o dostepnych opcjach do wyboru, wyswietla liste funkcji\n");


int option = 0;


while (option != 8)
{
	printf("Podaj numer polecenia: ");
	scanf("%i", &option);
	printf("\n");
	switch (option)
	{
		//odczytanie temperatury z modułu MPU6050
	case 1:
	{

		int fd;
		int adressMPU6050 = 0x69;

		int dane[2];

		if (wiringPiSetup() == -1)
			exit(1);

		if ((fd = wiringPiI2CSetup(adressMPU6050)) == -1) {
			printf("error initialize I2C");
			exit(1);
		}
		printf("I2C modul MPU6050\r\n");

		//Uruchamia pomiary
		int regPWR = 0x6B; //<<adres rejestru PWR_MGMT_1
		wiringPiI2CWriteReg8(fd, regPWR, 0);

		uint8_t adresRejestru[2] = { 0x41, 0x42 }; 			//Adres rejestru jaki chcemy odczytac

		uint8_t tempOut1 = wiringPiI2CReadReg8(fd, adresRejestru[0]);
		uint8_t tempOut2 = wiringPiI2CReadReg8(fd, adresRejestru[1]);
		int16_t temperatura = (tempOut1 << 8) | tempOut2;	//<<---- dokonczyc wzor na przeliczenie temperatury	
		float temp = (temperatura) / 340 + 36.53;
		printf("Temperatura z modulu MPU6050: %f C\r\n", temp);
	}
	break;
	//odczytanie wartości pomiarów z akcelerometru i żyroskopu
	case 2:
	{
		int fd;
		int adressMPU6050 = 0x69;

		int dane[2];

		if (wiringPiSetup() == -1)
			exit(1);

		if ((fd = wiringPiI2CSetup(adressMPU6050)) == -1) {
			printf("error initialize I2C");
			exit(1);
		}
		printf("I2C modul MPU6050\r\n");

		//Uruchamia pomiary
		int regPWR = 0x6B; //<<adres rejestru PWR_MGMT_1
		wiringPiI2CWriteReg8(fd, regPWR, 0);

		uint8_t tabAH[3] = { 0x3B, 0x3D, 0x3F };
		uint8_t tabAL[3] = { 0x3C, 0x3E, 0x40 };
		uint8_t tabGH[3] = { 0x43, 0x45, 0x47 };
		uint8_t tabGL[3] = { 0x44, 0x46, 0x48 };

		//<<----przekonwertowac na liczby w formacie int16_t
		uint8_t acc_xH = wiringPiI2CReadReg8(fd, tabAH[0]);
		uint8_t acc_yH = wiringPiI2CReadReg8(fd, tabAH[1]);
		uint8_t acc_zH = wiringPiI2CReadReg8(fd, tabAH[2]);
		uint8_t acc_xL = wiringPiI2CReadReg8(fd, tabAL[0]);
		uint8_t acc_yL = wiringPiI2CReadReg8(fd, tabAL[1]);
		uint8_t acc_zL = wiringPiI2CReadReg8(fd, tabAL[2]);

		uint8_t gyr_xH = wiringPiI2CReadReg8(fd, tabGH[0]);
		uint8_t gyr_yH = wiringPiI2CReadReg8(fd, tabGH[1]);
		uint8_t gyr_zH = wiringPiI2CReadReg8(fd, tabGH[2]);
		uint8_t gyr_xL = wiringPiI2CReadReg8(fd, tabGL[0]);
		uint8_t gyr_yL = wiringPiI2CReadReg8(fd, tabGL[1]);
		uint8_t gyr_zL = wiringPiI2CReadReg8(fd, tabGL[2]);

		int16_t facc_x = (acc_xH << 8) | (acc_xL); // 16384.0;
		int16_t facc_y = (acc_yH << 8) | (acc_yL); // 16384.0;
		int16_t facc_z = (acc_zH << 8) | (acc_zL); // 16384.0;

		int16_t fgyr_x = (gyr_xH << 8) | (gyr_xL); // 131;
		int16_t fgyr_y = (gyr_yH << 8) | (gyr_yL); // 131;
		int16_t fgyr_z = (gyr_zH << 8) | (gyr_zL); // 131;


		float poziom1 = atan2(facc_x, facc_z) * (180 / M_PI);
		float poziom2 = atan2(facc_x, facc_y) * (180 / M_PI);
		float poziom3 = atan2(facc_y, facc_z) * (180 / M_PI);


		printf("Akcelerometr:\n");
		printf("1. X: %d \n", facc_x);
		printf("2. Y: %d \n", facc_y);
		printf("3. Z: %d \n", facc_z);

		printf("Zyroskop:\n");
		printf("1. X: %d \n", fgyr_x);
		printf("2. Y: %d \n", fgyr_y);
		printf("3. Z: %d \n", fgyr_z);

		printf("Poziomica 1: %f \n", poziom1);
		printf("Poziomica 2: %f \n", poziom2);
		printf("Poziomica 3: %f \n", poziom3);


	}
	break;
	//odczytanie aktualnej daty z modułu RTC
	case 3:
	{
		int fd;

		int adressDS1307 = 0x68;


		if (wiringPiSetup() == -1)
			exit(1);

		if ((fd = wiringPiI2CSetup(adressDS1307)) == -1) {
			printf("error initialize I2C");
			exit(1);
		}
		printf("I2C modul DS1307\r\n");

		int regPWR = 0x6B;
		wiringPiI2CWriteReg8(fd, regPWR, 0);

		int secondAddress = 0x00, minuteAddress = 0x01, hourAddress = 0x02;
		int dayAddress = 0x04, monthAddress = 0x05, yearAddress = 0x06;

		int timeS = wiringPiI2CReadReg8(fd, secondAddress);
		int timeS_10 = MID(timeS, 4, 7);
		int timeS_1 = MID(timeS, 0, 4);

		int timeM = wiringPiI2CReadReg8(fd, minuteAddress);
		int timeM_10 = MID(timeM, 4, 7);
		int timeM_1 = MID(timeM, 0, 4);

		int timeH = wiringPiI2CReadReg8(fd, hourAddress);
		int timeH_10;
		int timeH_1;

		if (!(MID(timeH, 6, 7)))	timeH_10 = MID(timeH, 4, 6);
		else timeH_10 = MID(timeH, 4, 5);
		timeH_1 = MID(timeH, 0, 4);

		int dateD = wiringPiI2CReadReg8(fd, dayAddress);
		int dateD_10 = MID(dateD, 4, 6);
		int dateD_1 = MID(dateD, 0, 4);

		int dateM = wiringPiI2CReadReg8(fd, monthAddress);
		int dateM_10 = MID(dateM, 4, 5);
		int dateM_1 = MID(dateM, 0, 4);

		int dateY = wiringPiI2CReadReg8(fd, yearAddress);
		int dateY_10 = MID(dateY, 4, 8);
		int dateY_1 = MID(dateY, 0, 4);

		printf("20%d%d-%d%d-%d%d %d%d:%d%d:%d%d\n", dateY_10, dateY_1, dateM_10, dateM_1, dateD_10, dateD_1, timeH_10, timeH_1, timeM_10, timeM_1, timeS_10, timeS_1);


	}
	break;
	//wprowadzenie nowej daty dla modułu RTC
	case 4:
	{
		int fd;

		int adressDS1307 = 0x68;


		if (wiringPiSetup() == -1)
			exit(1);

		if ((fd = wiringPiI2CSetup(adressDS1307)) == -1) {
			printf("error initialize I2C");
			exit(1);
		}
		printf("I2C modul DS1307\r\n");

		int regPWR = 0x6B;
		wiringPiI2CWriteReg8(fd, regPWR, 0);

		int secondAddress = 0x00, minuteAddress = 0x01, hourAddress = 0x02;
		int dayAddress = 0x04, monthAddress = 0x05, yearAddress = 0x06;


		printf("Wprowadz date w formacie:'YY-MM-DD_HH:MM:SS'.");
		char date[17];
		scanf("%s", date);

		int CH = wiringPiI2CReadReg8(fd, secondAddress);
		CH = CH >> 7;

		int timeH_1 = 0;
		if (isdigit(date[10])) { timeH_1 = date[10] - 48; }
		int timeM_1 = 0;
		if (isdigit(date[13])) { timeM_1 = date[13] - 48; }
		int timeS_1 = 0;
		if (isdigit(date[16])) { timeS_1 = date[16] - 48; }
		int timeH_10 = 0;
		if (isdigit(date[9])) { timeH_10 = date[9] - 48; }
		int timeM_10 = 0;
		if (isdigit(date[12])) { timeM_10 = date[12] - 48; }
		int timeS_10 = 0;
		if (isdigit(date[15])) { timeS_10 = date[15] - 48; }

		int timeH = (0 << 6) | (timeH_10 << 4) | timeH_1;
		int timeM = (timeM_10 << 4) | timeM_1;
		int timeS = (CH << 7) | (timeS_10 << 4) | timeS_1;

		wiringPiI2CWriteReg8(fd, hourAddress, timeH);
		wiringPiI2CWriteReg8(fd, minuteAddress, timeM);
		wiringPiI2CWriteReg8(fd, secondAddress, timeS);

		int dateD_1 = 0;
		if (isdigit(date[7])) { dateD_1 = date[7] - 48; }
		int dateM_1 = 0;
		if (isdigit(date[4])) { dateM_1 = date[4] - 48; }
		int dateY_1 = 0;
		if (isdigit(date[1])) { dateY_1 = date[1] - 48; }
		int dateD_10 = 0;
		if (isdigit(date[6])) { dateD_10 = date[6] - 48; }
		int dateM_10 = 0;
		if (isdigit(date[3])) { dateM_10 = date[3] - 48; }
		int dateY_10 = 0;
		if (isdigit(date[0])) { dateY_10 = date[0] - 48; }

		int dateD = (dateD_10 << 4) | dateD_1;
		int dateM = (dateM_10 << 4) | dateM_1;
		int dateY = (dateY_10 << 4) | dateY_1;

		wiringPiI2CWriteReg8(fd, dayAddress, dateD);
		wiringPiI2CWriteReg8(fd, monthAddress, dateM);
		wiringPiI2CWriteReg8(fd, yearAddress, dateY);

	}
	break;
	//odczytanie 16 - bitowej wartości z pamięci EEPROM
	case 5:
	{
		int fd;
		int adress = 0x50;

		int daneM;
		int daneL;
		int dane;

		int adr = 0;

		if (wiringPiSetup() == -1)
			exit(1);

		if ((fd = wiringPiI2CSetup(adress)) == -1) {
			printf("error initialize I2C");
			exit(1);
		}

		printf("Podaj adres komorki:");
		scanf("%d", &adr);

		//Odczytanie z komorki adr wartosc
		daneM = i2cReadEeeprom(adr, fd);
		daneL = i2cReadEeeprom(adr+1, fd);

		dane=(daneM << 8) | daneL;

		printf("Odczytane dane:%d \n", dane);


	}
	break;
	//zapisanie 16 - bitowej wartości do pamięci EEPROM
	case 6:
	{
		int fd;
		int adress = 0x50;

		int dane;

		int adr = 0;

		if (wiringPiSetup() == -1)
			exit(1);

		if ((fd = wiringPiI2CSetup(adress)) == -1) {
			printf("error initialize I2C");
			exit(1);
		}
		printf("Podaj adres komorki:");
		scanf("%d", &adr);
		printf("Podaj dane do zapisania:");
		scanf("%d", &dane);

		int daneM= (dane >> 8) & 0xff;
		int daneL= dane & 0xff;

		//Zapisuje do komorki adr wartosc dane
		i2cWriteEEPROM(adr, daneM, fd);
		i2cWriteEEPROM(adr+1, daneL, fd);

	}
	break;
	//BMP280 – odczytanie ID modułu, temperatury i ciśnienia atmosferycznego

	case 7:
	{
		if (wiringPiSPISetup(chan, speed) == -1)
		{
			printf("Could not initialise SPI\n");
			return 0;
		}

		init();
		delay(600);

		int id = readRegister(0x90);

		printf("Czujnik BMP280 - ChipID: %x\r\n", id);

		writeRegister(BMP280_REGISTER_CONTROL, 0x57); //010 101 11 => 2x temp oversampling, 16x preassure oversampling, mode normal
		int config = readRegister(BMP280_REGISTER_CONTROL);
		printf("config %x\n", config);
		//załadowanie danych kalibracji
		CalibrationData();

		//odczyt
		float temp = readTemperature();
		float press = readPreassure();
		//printf("Temperatura: %f C,  Cisnienie: %f hPa\r\n", temp, press);

		delay(3000);

		temp = readTemperature();
		press = readPreassure();
		printf("Temperatura: %f C,  Cisnienie: %f hPa\r\n", temp, press);

	}
	break;
	//informacja o autorach
	case 9:
	{
		printf("\n _____________________________________________________________\n");
		printf("|Praca studentow: Kamil Bzorski - 241728, Tomasz Bury - 235415|");
		printf("\n _____________________________________________________________\n\n\n");
	}
	break;

	case 0:
	{
		printf("\n1 - odczytanie temperatury z modulu MPU6050\n2 - odczytanie wartosci pomiarow z akcelerometru i zyroskopu\n3 - odczytanie aktualnej daty z modulu RTC\n4 - wprowadzenie nowej daty dla modulu RTC\n5 - odczytanie 16 - bitowej wartosci z pamieci EEPROM\n6 - zapisanie 16 - bitowej wartosci do pamieci EEPROM\n7 - BMP280 - odczytanie ID modulu, temperatury i cisnienia atmosferycznego\n8 - zamknij program\n9 - informacja o autorach\n0 - wyswietlenie informacje o dostepnych opcjach do wyboru, wyswietla liste funkcji\n");

	}
	break;
	default: printf("Wprowadzono niepoprawny numer");

	}

}
return 0;
}
