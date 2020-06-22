#include <iostream>
#include <vector>
#include <windows.h>
#include <conio.h>
#include <fstream>
#include <string>
#include <ctype.h>
#include <cstdlib>
#include <chrono>
#include <stdio.h>


using namespace std;
   
float t_average(float* arr, int size)
{
    float average = arr[1];
    double sum = 0;
    for (int i = 0; i < size; i++)
        sum += arr[i];
    average = sum / size;
    return average;
}
float t_min(float* arr, int size)
{
    float min=arr[1];
    for (int i = 0; i < size; i++)
    {
        if (min > arr[i])
            min = arr[i];
    }

    return min;
}
float t_max(float* arr, int size)
{
    float max = arr[1];
    for (int i = 0; i < size; i++)
    {
        if (max < arr[i])
            max = arr[i];
    }
    return max;
}
float t_average(int* arr, int size)
{
    float average = arr[1];
    long sum = 0;
    for (int i = 0; i < size; i++)
        sum += arr[i];
    average = sum / size;
    return average;
}
int t_min(int* arr, int size)
{
    int min = arr[1];
    for (int i = 0; i < size; i++)
    {
        if (min > arr[i])
            min = arr[i];
    }

    return min;
}
int t_max(int* arr, int size)
{
    int max = arr[1];
    for (int i = 0; i < size; i++)
    {
        if (max < arr[i])
            max = arr[i];
    }
    return max;
}

void menu(vector <string> options, int selected)
{
    cout << "\t\tMENU"<<endl;
    int i = 0;
    for (string option : options)
    {
        if (i == selected) 
            cout<<option<<"   <--"<<endl;
        else
            cout<<option<<endl;       
        i++;
    }
}

void szybkie_sortowanie(int pierwszy, int rozmiar, float* tablica)
{
    int i = 0;
    int j = 0;
    float piwot = tablica[rozmiar];
    for (j = i = pierwszy; i < rozmiar; i++)
        if (tablica[i] < piwot)
        {
            swap(tablica[i], tablica[j]);
            j++;
        }
    swap(tablica[rozmiar], tablica[j]);
    if (pierwszy < j - 1)
        szybkie_sortowanie(pierwszy, j - 1, tablica);
    if (j + 1 < rozmiar)
        szybkie_sortowanie(j + 1, rozmiar, tablica);
}
void szybkie_sortowanie(int pierwszy, int rozmiar, int* tablica)
{
    int i = 0;
    int j = 0;
    int piwot = tablica[rozmiar];
    for (j = i = pierwszy; i < rozmiar; i++)
        if (tablica[i] < piwot)
        {
            swap(tablica[i], tablica[j]);
            j++;
        }
    swap(tablica[rozmiar], tablica[j]);
    if (pierwszy < j - 1)
        szybkie_sortowanie(pierwszy, j - 1, tablica);
    if (j + 1 < rozmiar)
        szybkie_sortowanie(j + 1, rozmiar, tablica);
}
bool sortowanie_bombelkowe(int rozmiar, float* tablica)
{
    bool sorted = true;
    for (int i = 0; i < rozmiar; i++)
        if (tablica[i] > tablica[i + 1])
        {
            if (sorted == true)
                sorted = false;
            swap(tablica[i], tablica[i + 1]);
        }
    if (sorted == false)
    {
        for (int j = 1; j < rozmiar; j++)
            for (int i = 0; i < rozmiar; i++)
                if (tablica[i] > tablica[i + 1])
                    swap(tablica[i], tablica[i + 1]);
    }
    else
        cout << endl << "Plik wejsciowy nie wymaga sortowania!" << endl;
    return sorted;
}
bool sortowanie_bombelkowe(int rozmiar, int* tablica)
{
    bool sorted = true;
    for (int i = 0; i < rozmiar; i++)
        if (tablica[i] > tablica[i + 1])
        {
            if (sorted == true)
                sorted = false;
            swap(tablica[i], tablica[i + 1]);
        }
    if (sorted == false)
    {
        for (int j = 1; j < rozmiar; j++)
            for (int i = 0; i < rozmiar; i++)
                if (tablica[i] > tablica[i + 1])
                    swap(tablica[i], tablica[i + 1]);
    }
    else
        cout << endl << "Plik wejsciowy nie wymaga sortowania!" << endl;
    return sorted;
}
void sortowanie_przez_zliczanie(int rozmiar, int* tablica) 
{
    int L = t_min(tablica, rozmiar);
    int H = t_max(tablica, rozmiar);
    int k = H - L + 1;

    int n = rozmiar;
    //int *T=tablica; // przed sortowaniem
    int *Ttmp = new int[k]; // zawiera liczbę elementów o danej wartości
    int *Tout = new int[n]; // po sortowaniu
    

    int i; // zmienna pomocnicza

    for (i = 0; i < k; ++i)
        Ttmp[i] = 0;

    for (i = 0; i < n; ++i)
        ++Ttmp[tablica[i]-L];

    for (i = 1; i < k; ++i)
        Ttmp[i] += Ttmp[i - 1];   
                                   
    for (i = n - 1; i >= 0; --i)
        Tout[--Ttmp[tablica[i]-L]] = tablica[i];  

    for (int i = 0; i < rozmiar; i++)
    {
        tablica[i] = Tout[i];
    }

    delete[] Tout;
    delete[] Ttmp;

}
void buduj_kopiec(int rozmiar, int* tablica, int root) 
{
    int largest = root; 
    int l = 2 * root + 1;
    int r = 2 * root + 2;

    if (l < rozmiar && tablica[l] > tablica[largest])
        largest = l;

    if (r < rozmiar && tablica[r] > tablica[largest])
        largest = r;

    if (largest != root)
    {
        swap(tablica[root], tablica[largest]);
        buduj_kopiec(rozmiar, tablica,  largest);
    }
}
void buduj_kopiec(int rozmiar, float* tablica, int root) 
{
    int largest = root;
    int l = 2 * root + 1;
    int r = 2 * root + 2;

    if (l < rozmiar && tablica[l] > tablica[largest])
        largest = l;

    if (r < rozmiar && tablica[r] > tablica[largest])
        largest = r;

    if (largest != root)
    {
        swap(tablica[root], tablica[largest]);
        buduj_kopiec(rozmiar, tablica, largest);
    }
}
void sortowanie_przez_kopcowanie(int rozmiar, int* tablica) 
{
    for (int i = rozmiar / 2 - 1; i >= 0; i--)
        buduj_kopiec( rozmiar,tablica, i);
    for (int i = rozmiar - 1; i >= 0; i--)
    {
        swap(tablica[0], tablica[i]);
        buduj_kopiec( i,tablica, 0);
    }
}
void sortowanie_przez_kopcowanie(int rozmiar, float* tablica) 
{
    for (int i = rozmiar / 2 - 1; i >= 0; i--)
        buduj_kopiec(rozmiar, tablica, i);
    for (int i = rozmiar - 1; i >= 0; i--)
    {
        swap(tablica[0], tablica[i]);
        buduj_kopiec(i, tablica, 0);
    }
}


int main()
{
    vector <string> options;
    options.push_back("1. Wgraj plik wejsciowy");
    options.push_back("2. Zapisz plik");
    options.push_back("3. Sortowanie algorytmem babelkowym");
    options.push_back("4. Sortowanie algorytmem szybkim");
    options.push_back("5. Sortowanie przez zliczanie");
    options.push_back("6. Sortowanie kopcowe");
    options.push_back("7. Zamknij program");
    options.push_back("8. OBLICZENIA TESTOWE");

    int selected = 0;
    char pressed = '0';
    string file_path = "";
    bool float_type = false;
    float* float_array = new float[1];
    int* int_array = new int[1];
    float* input_table_f = new float[1];
    int* input_table_i = new int[1];
    int number_of_lines = 0;
    string tmp_str[6] = { "","","","","","" };
    string time_str[5] = { "Babelkowe;Szybkie;Przez zliczanie;Przez Kopcowanie","","","","" };

    int* kopiec_i = new int[1];
    float* kopiec_f = new float[1];

    bool data = false;
    bool convert = false;

    chrono::duration<double, std::milli> Time_Bombelkowe;
    chrono::duration<double, std::milli> Time_Quick;
    chrono::duration<double, std::milli> Time_Counting;
    chrono::duration<double, std::milli> Time_Heapsort;

    while ((selected == 6 && pressed == 13) == false)
    {
        system("cls");
        menu(options, selected);

        pressed = _getch();
        if (pressed == 'w')
        {
            if (selected > 0)
                selected--;
            else
                selected = options.size() - 1;
        }
        else if (pressed == 's')
        {
            if (selected < options.size() - 1)
                selected++;
            else
                selected = 0;
        }
        else if (pressed == 13)
        {
            system("cls");
            switch (selected) {
            case 0:
            {

                cout << "Wybrano opcje: 1. Wgraj plik wejsciowy" << endl;
                cout << "Wprowadz sciezke do pliku:";
                cin >> file_path;
                fstream myfile;
                myfile.open(file_path, ios::in);
                if (myfile.good() == false)
                {
                    cout << "Nieprawidlowa sciezka do pliku";
                }
                else
                {
                    data = true;
                    string line = "";
                    getline(myfile, line);
                    tmp_str[0] = line;
                    getline(myfile, line);
                    tmp_str[1] = line;
                    getline(myfile, line);
                    tmp_str[2] = line;
                    string tmp_line = "";
                    for (int i = 13; i < sizeof(line); i++)
                    {
                        if (isdigit(line[i]))
                            tmp_line += line[i];
                        else break;
                    }
                    line = tmp_line;
                    number_of_lines = atoi(line.c_str());
                    getline(myfile, line);
                    tmp_str[3] = line;

                    if (line[8] == 'f' && line[9] == 'l' && line[10] == 'o' && line[11] == 'a' && line[12] == 't')
                    {
                        delete[] float_array;
                        delete[] input_table_f;
                        float_type = true;
                        float_array = new float[number_of_lines];
                    }
                    else if (line[8] == 'i' && line[9] == 'n' && line[10] == 't')
                    {
                        delete[] int_array;
                        delete[] input_table_i;
                        float_type = false;
                        int_array = new int[number_of_lines];
                    }
                    getline(myfile, line);
                    tmp_str[4] = line;

                    if (float_type == true)
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            getline(myfile, line);
                            float_array[i] = stof(line);
                        }
                    else if (float_type == false)
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            getline(myfile, line);
                            int_array[i] = stoi(line);
                        }
                    if (float_type == false)
                    {
                        input_table_i = new int[number_of_lines];
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            input_table_i[i] = int_array[i];
                        }
                    }
                    else
                    {
                        input_table_f = new float[number_of_lines];
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            input_table_f[i] = float_array[i];
                        }
                    }

                    cout << "Wczytano plik" << endl;
                }
                cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                _getch();
                myfile.close();



                break;
            };
            case 1:
            {
                cout << "Wybrano opcje: 2. Zapisz plik" << endl;
                if (data == true)
                {
                    cout << "Wprowadz sciezke do pliku wraz z formatem .csv: ";
                    cin >> file_path;
                    fstream myfile;
                    myfile.open(file_path, ios::out);
                    myfile << tmp_str[0] << endl;
                    myfile << tmp_str[1] << endl;
                    myfile << tmp_str[2] << endl;
                    myfile << tmp_str[3] << endl;
                    myfile << tmp_str[4] << endl;

                    if (float_type && convert == false)
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            if (i == 0)
                                myfile << float_array[i] << tmp_str[5] << endl;
                            else if (i == 1)
                                myfile << float_array[i] <<";"+ time_str[0] << endl;
                            else if (i == 2)
                                myfile << float_array[i] << ";" + time_str[1] + ";" + time_str[2] + ";" + time_str[3] + ";" + time_str[4] << endl;
                            else
                                myfile << float_array[i] << endl;
                        }
                    else if (float_type == false || convert == true)
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            if (i == 0)
                                myfile << int_array[i] << tmp_str[5] << endl;
                            else if (i==1)
                                myfile << int_array[i] << ";" + time_str[0] << endl;
                            else if (i==2)
                                myfile << int_array[i] << ";" + time_str[1]+";"+ time_str[2] + ";" + time_str[3] + ";" + time_str[4] << endl;
                            else
                                myfile << int_array[i] << endl;
                        }

                    myfile.close();
                    cout << "Zapisano plik";
                    cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                    _getch();
                }
                else
                {
                    cout << "Nie wczytano danych" << endl;
                    cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                    _getch();
                }
                break;
            }
            case 2:
            {
                cout << "Wybrano opcje: 3. Sortowanie algorytmem babelkowym" << endl;
                bool sorted = false;
                convert = false;
                if (data == true)
                {
                    cout << "Trwa sortowanie...";
                    if (float_type == false)
                    {
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            int_array[i] = input_table_i[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            float_array[i] = input_table_f[i];
                        }
                    }
                    auto start = chrono::high_resolution_clock::now();
                    if (float_type == true)
                        sorted = sortowanie_bombelkowe(number_of_lines - 1, float_array);
                    else
                        sorted = sortowanie_bombelkowe(number_of_lines - 1, int_array);
                    auto finish = chrono::high_resolution_clock::now();
                    Time_Bombelkowe = finish - start;
                    if (sorted == false)
                    {
                        cout << endl << "Sortowanie zakonczone";
                        cout << endl << "Czas sortowania:" << Time_Bombelkowe.count() << "ms";
                        time_str[1]=to_string(Time_Bombelkowe.count())+ " ms";

                    }
                }
                else
                    cout << endl << "Nie wczytano danych.";


                cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                _getch();
                break;
            }
            case 3:
            {
                cout << "Wybrano opcje: 4. Sortowanie algorytmem szybkim" << endl;
                convert = false;
                if (data == true)
                {
                    cout << "Trwa sortowanie...";
                    if (float_type == false)
                    {
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            int_array[i] = input_table_i[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            float_array[i] = input_table_f[i];
                        }
                    }
                    bool check = false;
                    for (int i = 0; i < number_of_lines - 1; i++)
                        if (float_type == true)
                        {
                            if (float_array[i] > float_array[i + 1])
                            {
                                check = true;
                                break;
                            }
                        }
                        else
                        {
                            if (int_array[i] > int_array[i + 1])
                            {
                                check = true;
                                break;
                            }
                        }
                    if (check == true)
                    {
                        auto start = chrono::high_resolution_clock::now();
                        if (float_type == true)
                            szybkie_sortowanie(0, number_of_lines - 1, float_array);
                        else
                            szybkie_sortowanie(0, number_of_lines - 1, int_array);
                        auto finish = chrono::high_resolution_clock::now();
                        Time_Quick = finish - start;
                        cout << endl << "Sortowanie zakonczone";
                        cout << endl << "Czas sortowania:" << Time_Quick.count() << "ms";
                        time_str[2] = to_string(Time_Quick.count()) + " ms";

                    }
                    else
                        cout << endl << "Plik wejsciowy nie wymaga sortowania!" << endl;
                }
                else
                    cout << endl << "Nie wczytano danych.";

                cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                _getch();
                break;
            }
            case 4:
            {
                cout << "Wybrano opcje: 5. Sortowanie przez zliczanie" << endl;
                if (data == true)
                {

                    if (float_type == true)
                    {
                        cout << "Dane nie sa typu calkowitego. Przekonwertowac dane na typ int i wykonac sortowanie?\t[T/N]" << endl;

                        char decision = _getch();
                        if (decision == 'T' || decision == 't')
                        {
                            convert = true;
                            cout << "Wybrano: TAK. Sortowanie zostanie wykonane." << endl;
                        }
                        else if (decision == 'N' || decision == 'n')
                        {
                            convert = false;
                            cout << "Wybrano: NIE. Sortowanie nie zostanie wykonane." << endl;
                        }
                        else
                            cout << "Wybrano nieprawidowa odpowiedz. Sortowanie nie zostanie wykonane." << endl;
                    }
                    if (float_type == false || convert == true)
                    {
                        if (convert == false)
                            for (int i = 0; i < number_of_lines; i++)
                            {
                                int_array[i] = input_table_i[i];
                            }
                        else
                        {
                            delete[] int_array;
                            int_array = new int[number_of_lines];
                            for (int i = 0; i < number_of_lines; i++)
                            {
                                int_array[i] = (int)input_table_f[i];
                            }
                        }
                        bool check = false;
                        for (int i = 0; i < number_of_lines - 1; i++)

                            if (int_array[i] > int_array[i + 1])
                            {
                                check = true;
                                break;
                            }

                        if (check == true)
                        {
                            cout << endl << "Trwa sortowanie...";
                            auto start = chrono::high_resolution_clock::now();
                            sortowanie_przez_zliczanie(number_of_lines, int_array);
                            auto finish = chrono::high_resolution_clock::now();
                            Time_Counting = finish - start;
                            cout << endl << "Sortowanie zakonczone";
                            cout << endl << "Czas sortowania:" << Time_Counting.count() << "ms";
                            time_str[3] = to_string(Time_Counting.count()) + " ms";

                        }
                        else
                            cout << endl << "Plik wejsciowy nie wymaga sortowania!" << endl;
                    }
                }
                else
                    cout << endl << "Nie wczytano danych.";
                cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                _getch();
                break;
            }
            case 5:
            {
                cout << "Wybrano opcje: 6. Sortowanie kopcowe" << endl;

                convert = false;
                if (data == true)
                {
                    cout << "Trwa sortowanie...";
                    if (float_type == false)
                    {
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            int_array[i] = input_table_i[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < number_of_lines; i++)
                        {
                            float_array[i] = input_table_f[i];
                        }
                    }
                    bool check = false;
                    for (int i = 0; i < number_of_lines - 1; i++)
                        if (float_type == true)
                        {
                            if (float_array[i] > float_array[i + 1])
                            {
                                check = true;
                                break;
                            }
                        }
                        else
                        {
                            if (int_array[i] > int_array[i + 1])
                            {
                                check = true;
                                break;
                            }
                        }
                    if (check == true)
                    {
                        auto start = chrono::high_resolution_clock::now();
                        if (float_type == true)
                            sortowanie_przez_kopcowanie(number_of_lines, float_array);
                        else
                            sortowanie_przez_kopcowanie(number_of_lines, int_array);
                        auto finish = chrono::high_resolution_clock::now();
                        Time_Heapsort = finish - start;
                        cout << endl << "Sortowanie zakonczone";
                        cout << endl << "Czas sortowania:" << Time_Heapsort.count() << "ms";
                        time_str[4] = to_string(Time_Heapsort.count()) + " ms";

                    }
                    else
                        cout << endl << "Plik wejsciowy nie wymaga sortowania!" << endl;
                    // }
                    // else
                     //    cout << endl << "Do sortowania przez kopcowanie wymagana jest budowa kopca!" << endl;

                }
                else
                    cout << endl << "Nie wczytano danych.";

                cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                _getch();
                break;
            }
            case 7:
            {
                cout << "Wybrano opcje: 8. OBLICZENIA TESTOWE";
                if (data == true)
                {

                    tmp_str[4] += ";MIN;MAX;SREDNIA";

                    if (float_type)
                        tmp_str[5] = ";" + to_string(t_min(float_array, number_of_lines)) + ";" + to_string(t_max(float_array, number_of_lines)) + ";" + to_string(t_average(float_array, number_of_lines));
                    else
                        tmp_str[5] = ";" + to_string(t_min(int_array, number_of_lines)) + ";" + to_string(t_max(int_array, number_of_lines)) + ";" + to_string(t_average(int_array, number_of_lines));
                    cout << endl << "Obliczono wartosc minimalna, maksymalna, oraz srednia dla wczytanych danych.";
                }
                else
                    cout << endl << "Nie wczytano danych" << endl;
                cout << endl << "Wcisnij dowolny klawisz aby powrocic do menu" << endl;
                _getch();
                break;
            }
            }
        }
    }
    delete[] int_array;
    delete[] float_array;
    delete[] input_table_f;
    delete[] input_table_i;
    delete[] kopiec_f;
    delete[] kopiec_i;
}
