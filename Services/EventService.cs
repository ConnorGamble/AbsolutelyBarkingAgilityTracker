using System;
using System.Collections.Generic;
using System.Linq;

namespace AbsolutelyBarkingAgilityTracking.Data
{
    public class EventService
    {
        public List<Dog> CompetingDogs { get; set; }

        /// <summary>
        /// ONLY USED TO MAKE FAKE DATA
        /// </summary>
        public Random Rng { get; set; } = new Random();

        public EventService()
        {
            CompetingDogs = Helpers.GenerateListOfDogs(250);
        }

        public List<Dog> GetCompetingDogs()
        {
            return CompetingDogs;
        }

        public List<Dog> GetCompetingDogsOfType(string dogHeight, string dogLevel, string dogClass)
        {
            var dogInfo = Helpers.ToDogEnums(dogClass, dogHeight, dogLevel);

            if (CompetingDogs == null)
                return new List<Dog>();

            return CompetingDogs.Where(x => x.Height == dogInfo.Height && x.Class == dogInfo.Class && x.Level == dogInfo.Level).ToList();
        }

        public void AddNewCompetingDog(string newDogName, string handlerName, string newDogClass, string newDogLevel, string newDogHeight, bool isGunDog, bool isRescueDog)
        {
            if (CompetingDogs == null)
                CompetingDogs = new List<Dog>();

            var dogInfo = Helpers.ToDogEnums(newDogClass, newDogHeight, newDogLevel);

            var dogToAdd = new Dog
            {
                Id = Guid.NewGuid().ToString(),
                HandlerName = newDogName,
                DogName = handlerName,
                Class = dogInfo.Class,
                Height = dogInfo.Height,
                Level = dogInfo.Level,
                IsRescue = isRescueDog, 
                IsGunDog = isGunDog,
                RunTime = "TBR"
            };

            CompetingDogs.Add(dogToAdd);
        }

        public Dog GetDogById(string dogId)
        {
            return this.CompetingDogs.Where(x => x.Id == dogId).FirstOrDefault();
        }
    }
}
