using System;
using Shop;

namespace Gameplay.Shop
{
    public static class Inventory
    {
        public static int Money { get; set; }
        public static int Gems { get; set; }

        public static float ElectricStamina { get; set; }

        public static float MaxElectricStamina =>
            Tank.CurrentTank.baseHealth * LevelToMultiplier(Turret.CurrentTurret.mainCanonDamagesUpgrade.level);

        public static float Health { get; set; }

        public static float MaxHealth =>
            Tank.CurrentTank.baseHealth * LevelToMultiplier(Tank.CurrentTank.healthUpgrade.level);

        public static float Speed =>
            Tank.CurrentTank.baseSpeed * LevelToMultiplier(Tank.CurrentTank.speedUpgrade.level, 1.01f);

        private static float LevelToMultiplier(int level, float @base = 1.1f) => (float)Math.Pow(@base, level);
        // todo : getter for flamethrower range, flamethrower damage, and flamethrower cooldown, main canon damages,
        //  secondary canon damages and shoot frequency and then speed
    }
}