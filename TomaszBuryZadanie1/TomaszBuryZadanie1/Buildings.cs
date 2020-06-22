using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomaszBuryZadanie1
{
    static class Buildings
    {
        //poziom budynku kopalni złota
        static public int LevelOfGoldMine = 1;
        //poziom budynku tartaku
        static public int LevelOfSawmill = 1;
        //poziom budynku kopalni kamienia
        static public int LevelOfRockMine = 1;
        //poziom budynku chaty myśliwskiej
        static public int LevelOfHuntingBuilding = 1;
        //poziom budynku kopalni żelaza
        static public int LevelOfIronMine = 0;
        //poziom budynku ratuszu
        static public int LevelOfTownHall = 0;
        //poziom budynku strażnicy
        static public int LevelOfWatchtower = 0;
        //poziom budynku infrastruktura
        static public int LevelOfInfrastructure = 0;
        //poziom budynku magazynu
        static public int LevelOfWareHouse = 0;



        /// <summary>
        /// Funkcja zwracajaca tablice kosztow budowy/ulepszenia kopalni złota
        /// </summary>
        /// <returns></returns>
        public static int[] GoldMineUpgradeCost()
        {
            int gold = LevelOfGoldMine * 50;
            int wood = LevelOfGoldMine * 30;
            int stone = (LevelOfGoldMine + 1) * 10;
            int food = LevelOfGoldMine * 35;
            int iron = 0;
            if (LevelOfGoldMine >= 5)
                iron = LevelOfGoldMine * 2;
            int[] cost = { gold, wood, stone, iron, food };
            return cost;
        }


        /// <summary>
        /// Funkcja zwracajaca tablice kosztow budowy/ulepszenia tartaku
        /// </summary>
        /// <returns></returns>
        public static int[] SawmillUpgradeCost()
        {
            int gold = LevelOfSawmill * 35;
            int wood = LevelOfSawmill * 30;
            int stone = (LevelOfSawmill + 1) * 10;
            int food = LevelOfSawmill * 30;
            int iron = 0;
            if (LevelOfSawmill >= 5)
                iron = LevelOfSawmill * 2;
            int[] cost = { gold, wood, stone, iron, food};
            return cost;
        }

        /// <summary>
        /// Funckja zwracajaca koszty budowy/ulepszenia kamieniolomu
        /// </summary>
        /// <returns></returns>
        public static int[] RockMineUpgradeCost()
        {
            int gold = LevelOfRockMine * 40;
            int wood = LevelOfRockMine * 25;
            int stone = (LevelOfRockMine + 1) * 5;
            int food = LevelOfRockMine * 20;
            int iron = 0;
            if (LevelOfRockMine >= 5)
                iron = LevelOfRockMine * 5;
            int[] cost = { gold, wood, stone, iron, food};
            return cost;
        }

        /// <summary>
        /// Funkcja zwracajaca koszty budowy/ulepszenia chaty mysliwskiej
        /// </summary>
        /// <returns></returns>
        public static int[] HuntingBuildingUpgradeCost()
        {
            int gold = LevelOfHuntingBuilding * 30;
            int wood = LevelOfHuntingBuilding * 20;
            int stone = (LevelOfHuntingBuilding + 1) * 10;
            int food = LevelOfHuntingBuilding * 15;
            int iron = 0;
            if (LevelOfHuntingBuilding >= 5)
                iron = LevelOfHuntingBuilding * 4;
            int[] cost = { gold, wood, stone, iron, food};
            return cost;
        }

        /// <summary>
        /// Funckja zwracajaca koszty budowy/ulepszenia kopalni żelaza
        /// </summary>
        /// <returns></returns>
        public static int[] IronMineUpgradeCost()
        {
            int gold = LevelOfIronMine * 70 + 70;
            int wood = LevelOfIronMine * 40 + 60;
            int stone = (LevelOfIronMine + 1) * 55;
            int iron = LevelOfIronMine * 30;
            int food = LevelOfIronMine * 35+ 50;
            int[] cost = { gold, wood, stone, iron, food };
            return cost;
        }

        /// <summary>
        /// Funckja zwracajaca tablice kosztow budowy/ulepszenia ratuszu
        /// </summary>
        /// <returns></returns>
        public static int[] TownHallUpgradeCost()
        {
            int gold = (LevelOfTownHall+1) * 130;
            int wood = (LevelOfTownHall+1) * 90;
            int stone = (LevelOfTownHall + 1) * 80;
            int food = (LevelOfTownHall+1) * 150;
            int iron = 0;
            if (LevelOfTownHall >= 3)
                iron = LevelOfTownHall * 60;
            int[] cost = { gold, wood, stone, iron, food };
            return cost;
        }

        /// <summary>
        /// Funckja zwracajaca tablice kosztow budowy/ulepszenia wieży strażniczej
        /// </summary>
        /// <returns></returns>
        public static int[] WatchtowerUpgradeCost()
        {
            int gold = (LevelOfWatchtower + 1) * 80;
            int wood = (LevelOfWatchtower + 1) * 60 ;
            int stone = (LevelOfWatchtower + 1) * 65;
            int food = (LevelOfWatchtower + 1) * 75;
            int iron = 0;
            if (LevelOfWatchtower >= 3)
                iron = LevelOfWatchtower * 50;
            int[] cost = { gold, wood, stone, iron, food};
            return cost;
        }

        /// <summary>
        /// Funckja zwracajaca tablice kosztow budowy/ulepszenia infrastruktury
        /// </summary>
        /// <returns></returns>
        public static int[] InfrastructureUpgradeCost()
        {
            int gold = (LevelOfInfrastructure + 1) * 250;
            int wood = (LevelOfInfrastructure + 1) * 170;
            int stone = (LevelOfInfrastructure + 1) * 150;
            int food = (LevelOfWareHouse+1) * 100;
            int iron = 0;
            if (LevelOfInfrastructure >= 3)
                iron = LevelOfInfrastructure * 120;
            int[] cost = { gold, wood, stone, iron, food };
            return cost;
        }

        /// <summary>
        /// Funckja zwracajaca tablice kosztow budowy/ulepszenia magazynu
        /// </summary>
        /// <returns></returns>
        public static int[] WareHouseUpgradeCost()
        {
            int gold = LevelOfWareHouse * 100 + 75;
            int wood = LevelOfWareHouse * 40 + 50;
            int stone = (LevelOfWareHouse + 1) * 40;
            int food = LevelOfWareHouse * 30 + 35;
            int iron = 0;
            if (LevelOfWareHouse >= 3)
                iron = LevelOfWareHouse * 100;
            int[] cost = { gold, wood, stone, iron, food };
            return cost;
        }
    }
}
