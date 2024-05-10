using LocationAPI.Utils.Interfaces;

namespace LocationAPI.Utils
{
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
}
