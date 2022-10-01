namespace AbsolutelyBarkingAgilityTracking.Data
{
    public class RunStats
    {
        public decimal Time;
        public int Faults;
        public bool Eliminated;
        public RunStats()
        {
            Eliminated = true;
        }

        public RunStats(decimal time, int faults)
        {
            this.Time = time;
            this.Faults = faults;
            this.Eliminated = false;
        }
    }
}
