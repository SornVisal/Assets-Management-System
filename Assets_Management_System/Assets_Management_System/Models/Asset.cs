namespace Assets_Management_System.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalValue()
        {
            return Quantity * Price;
        }
    }
}
