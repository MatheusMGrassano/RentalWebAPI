namespace Rental.DAL.Entities.Records
{
    public record Plan
    {
        public static readonly Plan SevenDays = new(7, 30, 0.2);
        public static readonly Plan FifteenDays = new(15, 28, 0.4);
        public static readonly Plan ThirtyDays = new(30, 22, 0.6);

        public int Days { get; }
        public decimal Price { get; }
        public double Fee { get; }

        public Plan(int days, decimal price, double fee)
        {
            Days = days;
            Price = price;
            Fee = fee;
        }

        public static Plan GetPlan(int days)
        {
            return days switch
            {
                7 => SevenDays,
                15 => FifteenDays,
                30 => ThirtyDays,
                _ => throw new Exception("No plans available for this number of days. Please, select one of our currently available plans."),
            };
        }
    }
}
