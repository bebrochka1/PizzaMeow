namespace PizzaMeow.Data.DataProcessing.OrderProccessing
{
    public class OrderSort
    {
        public string? Property { get; set; }
        public OrderSortDirection? OrderSortDirection {  get; set; } 
    }

    public enum OrderSortDirection
    {
        Ascending, Descending
    }
}
