namespace LocationAPI.Utils
{
    public interface ILeasingPlanStrategy
    {
        decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate);
    }

    // Implementation of the 7-day leasing plan strategy
    public class SevenDaysLeasingPlan : ILeasingPlanStrategy
    {
        private const decimal DailyRate = 30.00m;
        private const decimal Penalty = 0.20m;
        private const decimal AdditionalDailyRate = 50.00m;

        public decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate)
        {
            TimeSpan duration = returnDate - startDate;
            int leasingDays = (int)Math.Ceiling(duration.TotalDays);

            decimal totalValue = leasingDays * DailyRate;

            if (returnDate < startDate.AddDays(7))
            {
                decimal missingDays = (7 - leasingDays);
                totalValue += (missingDays * DailyRate * Penalty);
            }
            else if (returnDate > startDate.AddDays(7))
            {
                decimal additionalDays = (leasingDays - 7);
                totalValue += (additionalDays * AdditionalDailyRate);
            }

            return totalValue;
        }
    }

    // Implementation of the 15-day leasing plan strategy
    public class FifteenDaysLeasingPlan : ILeasingPlanStrategy
    {
        private const decimal DailyRate = 28.00m;
        private const decimal Penalty = 0.40m;
        private const decimal AdditionalDailyRate = 50.00m;

        public decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate)
        {
            TimeSpan duration = returnDate - startDate;
            int leasingDays = (int)Math.Ceiling(duration.TotalDays);

            decimal totalValue = leasingDays * DailyRate;

            if (returnDate < startDate.AddDays(15))
            {
                decimal missingDays = (15 - leasingDays);
                totalValue += (missingDays * DailyRate * Penalty);
            }
            else if (returnDate > startDate.AddDays(15))
            {
                decimal additionalDays = (leasingDays - 15);
                totalValue += (additionalDays * AdditionalDailyRate);
            }

            return totalValue;
        }
    }

    // Implementation of the 30-day leasing plan strategy
    public class ThirtyDaysLeasingPlan : ILeasingPlanStrategy
    {
        private const decimal DailyRate = 22.00m;
        private const decimal Penalty = 0.60m;
        private const decimal AdditionalDailyRate = 50.00m;

        public decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate)
        {
            TimeSpan duration = returnDate - startDate;
            int leasingDays = (int)Math.Ceiling(duration.TotalDays);

            decimal totalValue = leasingDays * DailyRate;

            if (returnDate < startDate.AddDays(30))
            {
                decimal missingDays = (30 - leasingDays);
                totalValue += (missingDays * DailyRate * Penalty);
            }
            else if (returnDate > startDate.AddDays(30))
            {
                decimal additionalDays = (leasingDays - 30);
                totalValue += (additionalDays * AdditionalDailyRate);
            }

            return totalValue;
        }
    }

    // Context that uses a strategy to calculate the leasing value
    public class LeasingCalculator
    {
        private ILeasingPlanStrategy _strategy;

        public LeasingCalculator(ILeasingPlanStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal CalculateLeasingValue(DateTime startDate, DateTime returnDate)
        {
            return _strategy.CalculateLeasingValue(startDate, returnDate);
        }
    }

}
