namespace AbsolutelyBarkingAgilityTracking.Data
{
    public class RunResult
    {
        public Dog Dog;
        public RunStats Round1;
        public RunStats Round2; // if it's odds'n'bobs or vet then not needed
        public RunStats Overall;
        public string Place { get; set;} = "-";

        public RunResult(Dog dog, RunStats round1Results)
        {
            Dog = dog;
            Round1 = round1Results;
            Overall = new RunStats(round1Results.Time, round1Results.Faults);
        }

        public RunResult(Dog dog, RunStats round1Results, RunStats round2Results)
        {
            Dog = dog;
            Round1 = round1Results;
            Round2 = round2Results;
            Overall = DetermineOverallResult(round1Results, round2Results);
        }

        public void RefreshOverallTime()
        {
            Overall = DetermineOverallResult();
        }

        private RunStats DetermineOverallResult()
        {
            return DetermineOverallResult(this.Round1, this.Round2);
        }

        private RunStats DetermineOverallResult(RunStats round1Results, RunStats round2Results)
        {
            // TODO: what about if both are elims?

            // if one or the other is eliminated, return the opposite 
            if (round1Results.Eliminated)
                return round2Results;
            else if (round2Results.Eliminated)
                return round1Results;

            // If one has more faults than the other, then use the one which has less faults
            if (round1Results.Faults < round2Results.Faults)
                return round1Results;
            else if (round2Results.Faults < round1Results.Faults)
                return round2Results;

            // Otherwise, return whichever has the fastest time
            if (round1Results.Time < round2Results.Time)
                return round1Results;
            else
                return round2Results;
        }
    }
}