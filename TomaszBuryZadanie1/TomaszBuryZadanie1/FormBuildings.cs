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
    public partial class FormBuildings : Form
    {
        int[] cost;
        public FormBuildings()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Funkcja wypełniająca labely kosztami zasobów do ulepszeń
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="costGold"></param>
        /// <param name="costWood"></param>
        /// <param name="costStone"></param>
        /// <param name="costIron"></param>
        static void FillCostLabels(int[] cost, Label costGold ,Label costWood, Label costStone, Label costIron, Label costFood)
        {
            costGold.Text = cost[0].ToString();
            costWood.Text = cost[1].ToString();
            costStone.Text = cost[2].ToString();
            costIron.Text = cost[3].ToString();
            costFood.Text = cost[4].ToString();
        }
        /// <summary>
        /// Funkcja sprawdzająca czy wystarcza zasobów do ulepszania budynku lub wybudowania go
        /// </summary>
        /// <param name="costGold"></param>
        /// <param name="costWood"></param>
        /// <param name="costRock"></param>
        /// <param name="costIron"></param>
        /// <returns></returns>
        private bool EnoughtResources(Label costGold, Label costWood, Label costRock, Label costIron, Label costFood)
        {

            if (Resources.ResourcesGold >= Int32.Parse(costGold.Text) &&
                Resources.ResourcesWood >= Int32.Parse(costWood.Text) &&
                Resources.ResourcesRock >= Int32.Parse(costRock.Text) &&
                Resources.ResourcesIron >= Int32.Parse(costIron.Text) &&
                Resources.ResourcesFood >= Int32.Parse(costFood.Text)
                )
            {   Resources.ResourcesGold -= Int32.Parse(costGold.Text);
                Resources.ResourcesWood -= Int32.Parse(costWood.Text);
                Resources.ResourcesRock -= Int32.Parse(costRock.Text);
                Resources.ResourcesIron -= Int32.Parse(costIron.Text);
                Resources.ResourcesFood -= Int32.Parse(costFood.Text);
                return true;
            }
            else
                return false;
        }

        private void FormBuildings_Load(object sender, EventArgs e)
        {
            // pobranie kosztów ulepszenia kopalni złota z klasy Buildings
            cost = Buildings.GoldMineUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach kopalni złota, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelGoldMineCostGold, labelGoldMineCostWood, labelGoldMineCostRock, labelGoldMineCostIron, labelGoldMineCostFood);
            // pobranie kosztów ulepszenia tartaku z klasy Buildings
            cost = Buildings.SawmillUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach tartaku, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelSawmillCostGold, labelSawmillCostWood, labelSawmillCostRock, labelSawmillCostIron, labelSawmillCostFood);
            // pobranie kosztów ulepszenia kamieniołomu z klasy Buildings
            cost = Buildings.RockMineUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach kamieniołomu, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelRockMineCostGold, labelRockMineCostWood, labelRockMineCostRock, labelRockMineCostIron, labelRockMineCostFood);
            // pobranie kosztów ulepszenia chary myśliwskiej z klasy Buildings
            cost = Buildings.HuntingBuildingUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach chaty myśliwskiej, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelHuntingBuildingCostGold, labelHuntingBuildingCostWood, labelHuntingBuildingCostRock, labelHuntingBuildingCostIron, labelHuntingBuildingCostFood);
            // pobranie kosztów ulepszenia kopalni żelaza z klasy Buildings
            cost = Buildings.IronMineUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach kopalni żelaza, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelIronMineCostGold, labelIronMineCostWood, labelIronMineCostRock, labelIronMineCostIron, labelIronMineCostFood);
            // pobranie kosztów ulepszenia magazynu złota z klasy Buildings
            cost = Buildings.WareHouseUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach magazynu, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelWareHouseCostGold, labelWareHouseCostWood, labelWareHouseCostRock, labelWareHouseCostIron, labelWareHouseCostFood);
            // pobranie kosztów ulepszenia ratuszu z klasy Buildings
            cost = Buildings.TownHallUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach ratuszu, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelTownHallCostGold, labelTownHallCostWood, labelTownHallCostRock, labelTownHallCostIron, labelTownHallCostFood);
            // pobranie kosztów ulepszenia infrastruktury z klasy Buildings
            cost = Buildings.InfrastructureUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach infrastruktury, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelInfrastructureCostGold, labelInfrastructureCostWood, labelInfrastructureCostRock, labelInfrastructureCostIron, labelInfrastructureCostFood);
            // pobranie kosztów ulepszenia wieży strażniczej z klasy Buildings
            cost = Buildings.WatchtowerUpgradeCost();
            // wyswietlenie aktualnej ceny w labelach wieży strażniczej złota, ktore sa argumentami wywolywanej funkcji
            FillCostLabels(cost, labelWatchtowerCostGold, labelWatchtowerCostWood, labelWatchtowerCostRock, labelWatchtowerCostIron, labelWatchtowerCostFood);
        }
        /// <summary>
        /// Funkcja ulepszania budynku kopalni złota
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpgradeGoldMine_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfGoldMine < (Buildings.LevelOfTownHall)+3)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelGoldMineCostGold, labelGoldMineCostWood , labelGoldMineCostRock, labelGoldMineCostIron , labelGoldMineCostFood) == true)
                {
                
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfGoldMine += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfGoldMine.Text = Buildings.LevelOfGoldMine.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.GoldMineUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelGoldMineCostGold, labelGoldMineCostWood, labelGoldMineCostRock, labelGoldMineCostIron, labelGoldMineCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }

            else
            {
                // wyswietlenie wiadomosci
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }
        }
        /// <summary>
        /// Funkcja ulepszania budynku tartaku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpgradeSawmill_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfSawmill < (Buildings.LevelOfTownHall) + 3)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelSawmillCostGold, labelSawmillCostWood, labelSawmillCostRock, labelSawmillCostIron, labelSawmillCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfSawmill += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfSawmill.Text = Buildings.LevelOfSawmill.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.SawmillUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelSawmillCostGold, labelSawmillCostWood, labelSawmillCostRock, labelSawmillCostIron, labelSawmillCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                // wyswietlenie wiadomosci
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }
        }
        /// <summary>
        /// Funkcja ulepszania budynku kamieniołomu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpgradeRockMine_Click(object sender, EventArgs e)
        {
            if(Buildings.LevelOfRockMine < (Buildings.LevelOfTownHall) + 3)
                {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelRockMineCostGold, labelRockMineCostWood, labelRockMineCostRock, labelRockMineCostIron, labelRockMineCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfRockMine += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfRockMine.Text = Buildings.LevelOfRockMine.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.RockMineUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelRockMineCostGold, labelRockMineCostWood, labelRockMineCostRock, labelRockMineCostIron, labelRockMineCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }
        }
        /// <summary>
        /// Funkcja ulepszania budynku Chaty myśliwskiej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpgradeHuntingBuilding_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfHuntingBuilding < (Buildings.LevelOfTownHall) + 3)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelHuntingBuildingCostGold, labelHuntingBuildingCostWood, labelHuntingBuildingCostRock, labelHuntingBuildingCostIron, labelHuntingBuildingCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfHuntingBuilding += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfHuntingBuilding.Text = Buildings.LevelOfHuntingBuilding.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.HuntingBuildingUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelHuntingBuildingCostGold, labelHuntingBuildingCostWood, labelHuntingBuildingCostRock, labelHuntingBuildingCostIron, labelHuntingBuildingCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }
        }
        /// <summary>
        /// Funkcja ulepszania budynku kopalni żelaza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpgradeIronMine_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfRockMine < (Buildings.LevelOfTownHall) + 2)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelIronMineCostGold, labelIronMineCostWood, labelIronMineCostRock, labelIronMineCostIron, labelIronMineCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfIronMine += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfIronMine.Text = Buildings.LevelOfIronMine.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.IronMineUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelIronMineCostGold, labelIronMineCostWood, labelIronMineCostRock, labelIronMineCostIron, labelIronMineCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }
        }

        private void buttonUpgradeWareHouse_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfRockMine < (Buildings.LevelOfTownHall) + 2)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelWareHouseCostGold, labelWareHouseCostWood, labelWareHouseCostRock, labelWareHouseCostIron, labelWareHouseCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfWareHouse += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfWareHouse.Text = Buildings.LevelOfWareHouse.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.WareHouseUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelWareHouseCostGold, labelWareHouseCostWood, labelWareHouseCostRock, labelWareHouseCostIron, labelWareHouseCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }

        }

        private void buttonUpgradeWatchtower_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfRockMine < Buildings.LevelOfTownHall)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelWatchtowerCostGold, labelWatchtowerCostWood, labelWatchtowerCostRock, labelWatchtowerCostIron, labelWatchtowerCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfWatchtower += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfWatchtower.Text = Buildings.LevelOfWatchtower.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.WatchtowerUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelWatchtowerCostGold, labelWatchtowerCostWood, labelWatchtowerCostRock, labelWatchtowerCostIron, labelWatchtowerCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }

        }

        private void buttonUpgradeTownHall_Click(object sender, EventArgs e)
        {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelTownHallCostGold, labelTownHallCostWood, labelTownHallCostRock, labelTownHallCostIron, labelTownHallCostFood) == true)
            {
                // zwiekszenie poziomu budynku
                Buildings.LevelOfTownHall += 1;
                // wyswietlenie aktualnego poziomu budynku
                labelLevelOfTownHall.Text = Buildings.LevelOfTownHall.ToString();
                //pobranie kosztow kolejnego poziomu ulepszenia budynku
                cost = Buildings.TownHallUpgradeCost();
                // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                FillCostLabels(cost, labelTownHallCostGold, labelTownHallCostWood, labelTownHallCostRock, labelTownHallCostIron, labelTownHallCostFood);
            }
            else
            {
                // wyswietlenie wiadomosci
                MessageBox.Show("Niewystarczająca ilość zasobów!");
            }
        }

        private void buttonUpgradeInfrastructure_Click(object sender, EventArgs e)
        {
            if (Buildings.LevelOfRockMine < Buildings.LevelOfTownHall)
            {
                //Wywołanie funkcji sprawdzającej, czy posiadamy wystrczającą ilość zasobów do ulepszenia budynku
                if (EnoughtResources(labelInfrastructureCostGold, labelInfrastructureCostWood, labelInfrastructureCostRock, labelInfrastructureCostIron, labelInfrastructureCostFood) == true)
                {
                    // zwiekszenie poziomu budynku
                    Buildings.LevelOfInfrastructure += 1;
                    // wyswietlenie aktualnego poziomu budynku
                    labelLevelOfInfrastructure.Text = Buildings.LevelOfInfrastructure.ToString();
                    //pobranie kosztow kolejnego poziomu ulepszenia budynku
                    cost = Buildings.InfrastructureUpgradeCost();
                    // wywolanie funkcji, ktora wyswietli koszty rozbudowy na kolejny poziom budynku
                    FillCostLabels(cost, labelInfrastructureCostGold, labelInfrastructureCostWood, labelInfrastructureCostRock, labelInfrastructureCostIron, labelInfrastructureCostFood);
                }
                else
                {
                    // wyswietlenie wiadomosci
                    MessageBox.Show("Niewystarczająca ilość zasobów!");
                }
            }
            else
            {
                MessageBox.Show("Zbyt niski poziom ratusza!");
            }
        }
    }
}
