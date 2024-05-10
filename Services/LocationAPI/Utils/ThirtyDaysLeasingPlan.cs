using LocationAPI.Utils.Interfaces;

namespace LocationAPI.Utils
{
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
}
