using Radzen;
using System;
using System.Collections.Generic;

namespace AbsolutelyBarkingAgilityTracking.Data
{
    public static class Helpers
    {
        public static DogEnums ToDogEnums(string dogClass, string dogHeight, string dogLevel)
        {
            // probably need some error handling here, what if we can't actually parse it? 
            // Shouldn't really be an issue seeing as it's always coming from dropdowns but something to consider...
            Enum.TryParse(dogClass, out DogClass dogsClass);
            Enum.TryParse(dogHeight, out DogHeight dogsHeight);
            Enum.TryParse(dogLevel, out DogLevel dogsLevel);

            return new DogEnums(dogsClass, dogsHeight, dogsLevel);
        }

        /// <summary>
        /// TODO: WILL HAVE TO REMOVE THIS FOR PRODUCTIONS
        /// Or at least not make it accessible.
        /// </summary>
        public static List<Dog> GenerateListOfDogs(int numberOfDogsToMake, DogClass dogClass = DogClass.Unknown, DogLevel dogLevel = DogLevel.Unknown, DogHeight dogHeight = DogHeight.Unknown)
        {
            var RNG = new Random();
            var competingDogs = new List<Dog>();

            competingDogs.Add(new Dog($"MRRUNNERMANHMMMM", $"DOGNAME", DogClass.Agility, DogHeight.Standard, DogLevel.Elementary, true, true, Guid.Empty));

            for (int i = 0; i < numberOfDogsToMake; i++)
            {
                var gunDog = RNG.Next(0, 1) == 0 ? false : true;
                var rescueDog = RNG.Next(0, 1) == 0 ? false : true;

                var generatedDogClass = (DogClass)RNG.Next(1, 2);
                var generatedDogLevel = (DogLevel)RNG.Next(1, 6);
                var generatedDogHeight = (DogHeight)RNG.Next(1, 5);

                if (dogClass == DogClass.Unknown && dogLevel == DogLevel.Unknown && dogHeight == DogHeight.Unknown)
                {
                    competingDogs.Add(new Dog($"DogName-{i}", $"HandlerName-{i}", generatedDogClass, generatedDogHeight, generatedDogLevel, gunDog, rescueDog));
                }
                else
                {
                    competingDogs.Add(new Dog($"DogName-{i}", $"HandlerName-{i}", dogClass, dogHeight, dogLevel, gunDog, rescueDog));
                }
            }

            return competingDogs;
        }
    }
}
