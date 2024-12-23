namespace PizzaMeow.Data.DataProcessing.OrderProccessing
{
    public class OrderSort
    {
        public string? Property { get; set; }
        public OrderDirection? Direction {  get; set; } 
    }

    public enum OrderDirection
    {
        Ascending, Descending
    }
}
