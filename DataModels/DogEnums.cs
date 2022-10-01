namespace AbsolutelyBarkingAgilityTracking.Data
{
    public class DogEnums
    {
        public DogClass Class;
        public DogHeight Height;
        public DogLevel Level;

        public DogEnums(DogClass dogClass, DogHeight dogHeight, DogLevel dogLevel)
        {
            Class = dogClass;
            Height = dogHeight;
            Level = dogLevel;
        }
    }

    public enum DogLevel
    {
        Unknown,
        Elementary,
        Starters,
        Novice,
        Senior,
        OddsNBods,
        Veterans
    }

    public enum DogClass
    {
        Unknown,
        Agility,
        Jumping,
    }

    public enum DogHeight
    {
        Unknown,
        Large,
        Standard,
        Medium,
        Small,
        Toy
    }
}
