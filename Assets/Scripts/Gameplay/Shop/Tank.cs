using Save;

namespace Shop
{
    public class Tank : Purchasable
    {
        private Tank(string name,
            int price,
            float baseHealth,
            float baseSpeed,
            float baseDamages,
            float baseShootFrequency) : base(name, price)
        {
            this.baseHealth = baseHealth;
            this.baseSpeed = baseSpeed;
            this.baseDamages = baseDamages;
            this.baseShootFrequency = baseShootFrequency;
        }


        public readonly float baseHealth;
        public readonly float baseSpeed;
        public readonly float baseDamages;
        public readonly float baseShootFrequency;

        public readonly Upgrade healthUpgrade = new("Health", "", 0, 40, 10);
        public readonly Upgrade speedUpgrade = new("Speed", "", 0, 40, 10);
        public readonly Upgrade damageUpgrade = new("Damage", "", 0, 40, 10);
        public readonly Upgrade shootFrequencyUpgrade = new("Shoot frequency", "", 0, 40, 10);

        public Upgrade[] Upgrades => new[] { healthUpgrade, speedUpgrade, damageUpgrade, shootFrequencyUpgrade };

        public static readonly Tank FirstTank =
            new("First Tank", 100, 100, 10, 10, 10);

        public static readonly Tank SecondTank =
            new("Second Tank", 800, 500, 15, 10, 10);

        public static readonly Tank[] Tanks =
        {
            FirstTank,
            SecondTank
        };

        public static Tank CurrentTank { get; set; }

        private static string PathSpeedUpgrade(string path) => $"{path}/speed";
        private static string PathHealthUpgrade(string path) => $"{path}/health";
        private static string PathDamageUpgrade(string path) => $"{path}/damage";
        private static string PathShootFrequencyUpgrade(string path) => $"{path}/shootFrequency";
        public override void Save(string path)
        {
            base.Save(path);
            speedUpgrade.Save(PathSpeedUpgrade(path));
            healthUpgrade.Save(PathHealthUpgrade(path));
            damageUpgrade.Save(PathDamageUpgrade(path));
            shootFrequencyUpgrade.Save(PathShootFrequencyUpgrade(path));
        }

      
        public override bool Load(string path) =>
            base.Load(path) && 
            speedUpgrade.Load(PathSpeedUpgrade(path)) && 
            healthUpgrade.Load(PathHealthUpgrade(path)) && 
            damageUpgrade.Load(PathDamageUpgrade(path)) && 
            shootFrequencyUpgrade.Load(PathShootFrequencyUpgrade(path));
        
    }
}