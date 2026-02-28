namespace Hardship.Domain.Entities
{
    public class HardshipApplication
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; } = default!;
        public DateTime DateOfBirth { get; private set; }
        public decimal Income { get; private set; }
        public decimal Expenses { get; private set; }
        public string? HardshipReason { get; private set; }

        private HardshipApplication() { }

        public HardshipApplication(
            string customerName,
            DateTime dateOfBirth,
            decimal income,
            decimal expenses,
            string? hardshipReason)
        {
            Id = Guid.NewGuid();
            Update(customerName, dateOfBirth, income, expenses, hardshipReason);
        }

        public void Update(
            string customerName,
            DateTime dateOfBirth,
            decimal income,
            decimal expenses,
            string? hardshipReason)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required.");

            if (dateOfBirth >= DateTime.UtcNow)
                throw new ArgumentException("Date of birth must be in the past.");

            if (income < 0 || expenses < 0)
                throw new ArgumentException("Income and expenses cannot be negative.");

            CustomerName = customerName;
            DateOfBirth = dateOfBirth;
            Income = income;
            Expenses = expenses;
            HardshipReason = hardshipReason;
        }
    }
}
