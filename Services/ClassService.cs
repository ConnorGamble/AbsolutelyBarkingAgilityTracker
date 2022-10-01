using System;
using System.Collections.Generic;
using System.Linq;

namespace AbsolutelyBarkingAgilityTracking.Data
{
    public class ClassService
    {
        // List<Class>
        public List<Class> Classes;
        // Bunch of methods like get class, get all classes
        // Method for get results of class

        public ClassService()
        {
            Classes = new List<Class>();

            //// TODO: REMOVE ME FOR DEBUG ONLY
            //Classes.Add(new Class(new DogEnums(DogClass.Agility, DogHeight.Large, DogLevel.Elementary)));
        }

        public Class GetClassForType(string dogClass, string dogHeight, string dogLevel)
        {
            var dogInfo = Helpers.ToDogEnums(dogClass, dogHeight, dogLevel);
            return Classes.Where(x => x.DogClass == dogInfo.Class && x.DogHeight == dogInfo.Height && x.DogLevel == dogInfo.Level).FirstOrDefault();
        }

        public Class GetClassForType(DogClass dogClass, DogHeight dogHeight, DogLevel dogLevel)
        {
            return Classes.Where(x => x.DogClass == dogClass && x.DogHeight == dogHeight && x.DogLevel == dogLevel).FirstOrDefault();
        }

        public void AddDogToRunningQueue(string newDogClass, string newDogHeight, string newDogLevel, Dog dogToAddToQueue)
        {
            var foundClass = GetClassForType(newDogClass, newDogHeight, newDogLevel);

            if (foundClass == null)
            {
                var dogInfo = Helpers.ToDogEnums(newDogClass, newDogHeight, newDogLevel);
                foundClass = new Class(dogInfo);
                this.Classes.Add(foundClass);
            }

            foundClass.AddToRunningOrder(dogToAddToQueue);

        }

        public void CreateResultForDog(Dog dogResultBelongsTo, string time, int faults, bool elimination = false)
        {
            var foundClass = GetClassForType(dogResultBelongsTo.Class, dogResultBelongsTo.Height, dogResultBelongsTo.Level);
            if (foundClass != null)
            {
                foundClass.AddNewResult(dogResultBelongsTo, time, faults, elimination);
            }
        }

        public void RemoveDogFromRunningQueue(string newDogClass, string newDogHeight, string newDogLevel, string dogToRemoveId)
        {
            var foundClass = GetClassForType(newDogClass, newDogHeight, newDogLevel);
            if (foundClass != null)
            {
                foundClass.RemoveFromRunningOrder(dogToRemoveId);
            }
        }
    }

    public class Class
    {
        // Running order (queue) 
        public List<Dog> RunningOrder = new List<Dog>();
        // All dogs of that class -MAYBE
        // List of results 
         public List<RunResult> Results;

        public DogClass DogClass { get; set; }
        public DogHeight DogHeight { get; set; }
        public DogLevel DogLevel { get; set; }

        bool OnlyCount1Round = false;

        public Class(DogEnums dogInfo)
        {
            RunningOrder = new List<Dog>();
            Results = new List<RunResult>();

            this.DogClass = dogInfo.Class;
            this.DogHeight = dogInfo.Height;
            this.DogLevel = dogInfo.Level;

            if (DogLevel == DogLevel.OddsNBods || DogLevel == DogLevel.Veterans)
                OnlyCount1Round = true;

            //// DEBUG ONLY
            //this.RunningOrder = Helpers.GenerateListOfDogs(15, dogInfo.Class, dogInfo.Level, dogInfo.Height);
        }

        public void AddNewResult(Dog dogResultBelongsTo, string time, int faults, bool elimination)
        {
            if (this.Results == null)
                this.Results = new List<RunResult>();

            var parsedTime = decimal.Parse(time);
            var newStats = new RunStats(parsedTime, faults);

            var currentResultsForDog = this.Results.Where(x => x.Dog.Id == dogResultBelongsTo.Id).FirstOrDefault();
            if(currentResultsForDog != null)
            {
                // We disregard any additional results if we only care about round 1
                if(!OnlyCount1Round)
                    currentResultsForDog.Round2 = newStats;
            }
            else
                this.Results.Add(new RunResult(dogResultBelongsTo, newStats));

        }

        public void AddToRunningOrder(Dog dogToAdd)
        {
            // Should probably check to see if the dog already exists? 
            RunningOrder.Add(dogToAdd);
        }

        public void RemoveFromRunningOrder(string dogId)
        {
            // feels like there has to be a much nicer way to 
            var foundDog = RunningOrder.Where(x => x.Id == dogId).FirstOrDefault();
            //if found dog is null? Should already be gone from the list, outdated pages on others might affect this? 
            RunningOrder.Remove(foundDog);
        }

        public List<RunResult> GetResults()
        {
            var placements = Math.Ceiling(this.Results.Count * 0.4);
            var results = new List<RunResult>();

            // OddsNBods & Vets only count the first round
            if(OnlyCount1Round)
                results = this.Results.Where(x => x.Dog.Height == DogHeight && x.Dog.Class == DogClass && x.Dog.Level == DogLevel).OrderBy(x => x.Round1.Time).ToList();
            else
                results = this.Results.Where(x => x.Dog.Height == DogHeight && x.Dog.Class == DogClass && x.Dog.Level == DogLevel).OrderBy(x => x.Overall.Time).ToList();

            for (int i = 0; i < placements; i++)
                results[i].Place = (i + 1).ToString();

            return results;
        }
    }
}
