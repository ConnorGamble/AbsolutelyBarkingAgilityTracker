using System;

namespace AbsolutelyBarkingAgilityTracking.Data
{
    public class Dog
    {
        public string Id = "";
        public string RunTime { get; set; } = "TBR";
        public int Faults { get; set; } = 0;
        public string HandlerName { get; set; }
        public string DogName { get; set; }
        public bool HasRan { get; set; } = false;
        public DogClass Class { get; set; }
        public DogHeight Height { get; set; }
        public DogLevel Level { get; set; }
        public bool IsGunDog { get; set; } = false;
        public bool IsRescue { get; set; } = false;

        public Dog()
        {
        }

        public Dog(string runnerName, string dogName, DogClass dogClass, DogHeight dogHeight, DogLevel dogLevel, bool isGunDog, bool isRescueDog)
        {
            Id = Guid.NewGuid().ToString();
            HandlerName = runnerName;
            DogName = dogName;
            Class = dogClass;
            Height = dogHeight;
            Level = dogLevel;
            IsGunDog = isGunDog;
            IsRescue = isRescueDog;
        }

        public Dog(string runnerName, string dogName, DogClass dogClass, DogHeight dogHeight, DogLevel dogLevel, bool isGunDog, bool isRescueDog, Guid id)
        {
            Id = id.ToString();
            HandlerName = runnerName;
            DogName = dogName;
            Class = dogClass;
            Height = dogHeight;
            Level = dogLevel;
            IsGunDog = isGunDog;
            IsRescue = isRescueDog;
        }

        public void UpdateTime(string timeToUpdateTo)
        {
            RunTime = timeToUpdateTo;
            HasRan = true;
        }
    }
}
