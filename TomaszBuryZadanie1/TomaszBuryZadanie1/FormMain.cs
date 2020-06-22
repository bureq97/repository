using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomaszBuryZadanie1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            // timer odmierza czas, 1000ms = 1 sekunda
            timerResources.Interval = 2000;
            // wywołanie startu timera odpowiadający za przyrost zasobów
            timerResources.Start();
            // timer negatywnych wydarzeń odmierza czas
            timerNegativeEvents.Interval = 1200000;
            // wywołanie timera odpowiadający za negatywne wydarzenia
            timerNegativeEvents.Start();
            // timer złodziejów odmierza czas
            timerThiefs.Interval = 90000;
            // wywołanie timera odpowiadający za złodziejów
            timerThiefs.Start();

        }
        private void timerResources_Tick(object sender, EventArgs e)
        {
            // wyswietla i aktualizuje ilośc złota
            Resources.ResourcesGold += Resources.GoldGrowth();
            if(Resources.ResourcesGold>Resources.ResourcesMax*(Buildings.LevelOfWareHouse + 1))
            {
                Resources.ResourcesGold = Resources.ResourcesMax * (Buildings.LevelOfWareHouse+1);
            }
            labelResourcesGold.Text = Resources.ResourcesGold.ToString();
            // wyswietla i aktualizuje ilość drewna
            Resources.ResourcesWood += Resources.WoodGrowth();
            if (Resources.ResourcesWood > Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1))
            {
                Resources.ResourcesWood = Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1);
            }
            labelResourcesWood.Text = Resources.ResourcesWood.ToString();
            // wyswietla i aktualizuje ilość kamienia
            Resources.ResourcesRock += Resources.RockGrowth();
            if (Resources.ResourcesRock > Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1))
            {
                Resources.ResourcesRock = Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1);
            }
            labelResourcesRock.Text = Resources.ResourcesRock.ToString();
            // wyswietla i aktualizuje ilość jedzenia
            Resources.ResourcesFood += Resources.FoodGrowth();
            if (Resources.ResourcesFood > Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1))
            {
                Resources.ResourcesFood = Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1);
            }
            labelResourcesFood.Text = Resources.ResourcesFood.ToString();
            // wyswietla i aktualizuje ilosc żelaza
            Resources.ResourcesIron += Resources.IronGrowth();
            if (Resources.ResourcesIron > Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1))
            {
                Resources.ResourcesIron = Resources.ResourcesMax * (Buildings.LevelOfWareHouse + 1);
            }
            labelResourcesIron.Text = Resources.ResourcesIron.ToString();

        }
        /// <summary>
        /// Przycisk zamykający program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            //Zamyka program
            Close();
        }

        /// <summary>
        /// Przycisk otwierający okno z ulepszeniami budynków
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenBuildings_Click(object sender, EventArgs e)
        {
            FormBuildings windowBuilding = new FormBuildings();
            windowBuilding.Show();
        }
        /// <summary>
        /// Timer odpowiadający za losowe wypadki w budynkach
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerNegativeEvents_Tick(object sender, EventArgs e)
        {
            Random randomNumber = new Random();
            //tworzenia zmiennej której przypisujemy wynik losowania gdzie ma nastapic wypadek
            int accidentPlace = randomNumber.Next(1, 5);
               
                if(accidentPlace == 1)
                {
                    MessageBox.Show("Wypadek w kopalni złota!!!Poziom Budynku obniżył się o jeden poziom ");
                    //obniża poziom kopalni złota 
                    Buildings.LevelOfGoldMine -= 1;
                }
                if (accidentPlace == 2)
                {
                    MessageBox.Show("Wypadek w tartaku!!!Poziom Budynku obniżył się o jeden poziom ");
                    //obniża poziom tartaku
                    Buildings.LevelOfSawmill -= 1;
                }
                if (accidentPlace == 3)
                {
                    MessageBox.Show("Wypadek w kopalni kamienia!!!Poziom Budynku obniżył się o jeden poziom ");
                    //obniża poziom kopalni kamienia
                    Buildings.LevelOfRockMine -= 1;
                }
                if (accidentPlace == 4)
                {
                    MessageBox.Show("Wypadek w chacie myśliwskiej!!!Poziom Budynku obniżył się o jeden poziom ");
                    //obniża poziom chaty myśliwskiej
                    Buildings.LevelOfHuntingBuilding -= 1;
                }
                if (accidentPlace == 5)
                {
                    MessageBox.Show("Wypadek w kopalni żelaza!!!Poziom Budynku obniżył się o jeden poziom ");
                    //obniża poziom kopalni żelaza
                    Buildings.LevelOfIronMine -= 1;
                }
                
            
        }

        /// <summary>
        /// Funkcja symulująca przybycie bandytów do miasta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerThiefs_Tick(object sender, EventArgs e)
        {
            //zmienna określająca liczbe ataków złodzieji na miasto
            int thiefsAttackCounter = 1;
            Random randomNumber = new Random();
            //tworzenia zmiennej której przypisujemy wynik losowania gdzie ma nastapic atak bandytów
            int ThiefsPlace = randomNumber.Next(1, 6);
            if (Buildings.LevelOfWatchtower <= thiefsAttackCounter)
            {
                if (ThiefsPlace == 1)
                {

                    MessageBox.Show("Złodzieje! Złodzieje kradną część pieniądzy w mieście!Aby uniknąć następnych rabunków zaleca się ulepszenie wieży strażniczej");
                    Resources.ResourcesGold -= Convert.ToInt32(Math.Round(Resources.ResourcesGold * 0.70));
                    Resources.ResourcesGold -= 50;
                    if(Resources.ResourcesGold < 0)
                    {
                        Resources.ResourcesGold = 0;
                    }
                }
                if (ThiefsPlace == 2)
                {
                    MessageBox.Show("Złodzieje! Złodzieje kradną część drewna w mieśce!Aby uniknąć następnych rabunków zaleca się ulepszenie wieży strażniczej");
                    Resources.ResourcesWood -= Convert.ToInt32(Math.Round(Resources.ResourcesWood * 0.70));
                    Resources.ResourcesWood -= 50 ;
                    if (Resources.ResourcesWood < 0)
                    {
                        Resources.ResourcesWood = 0;
                    }
                }
                if (ThiefsPlace == 3)
                {
                    MessageBox.Show("Złodzieje! Złodzieje kradną część kamieni w mieśce!Aby uniknąć następnych rabunków zaleca się ulepszenie wieży strażniczej");
                    Resources.ResourcesRock -= Convert.ToInt32(Math.Round(Resources.ResourcesRock * 0.70));
                    Resources.ResourcesRock -= 50 ;
                    if (Resources.ResourcesRock < 0)
                    {
                        Resources.ResourcesRock = 0;
                    }
                }
                if (ThiefsPlace == 4)
                {
                    MessageBox.Show("Złodzieje! Złodzieje kradną jedzenie w mieśce!Aby uniknąć następnych rabunków zaleca się ulepszenie wieży strażniczej");
                    Resources.ResourcesFood -= Convert.ToInt32(Math.Round(Resources.ResourcesFood * 0.70));
                    Resources.ResourcesFood -= 50 ;
                    if (Resources.ResourcesFood < 0)
                    {
                        Resources.ResourcesFood = 0;
                    }
                }
                if (ThiefsPlace == 5)
                {
                    MessageBox.Show("Złodzieje! Złodzieje ukradli część sztabek żelaza w mieśce!Aby uniknąć następnych rabunków zaleca się ulepszenie wieży strażniczej");
                    Resources.ResourcesIron -= Convert.ToInt32(Math.Round(Resources.ResourcesIron * 0.70));
                    Resources.ResourcesIron -= 50 ;
                    if (Resources.ResourcesIron < 0)
                    {
                        Resources.ResourcesIron = 0;
                    }
                }

            }
            thiefsAttackCounter++;
        }
    }
}
