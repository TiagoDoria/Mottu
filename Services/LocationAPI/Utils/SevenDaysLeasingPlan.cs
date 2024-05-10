using LocationAPI.Utils.Interfaces;

namespace LocationAPI.Utils
{
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
}
