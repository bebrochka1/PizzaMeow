namespace PizzaMeow.Data.DataProcessing.PizzaProccessing
{
    public class PizzaSort
    {
        public string? OrderBy { get; set; }
        public OrderDirection? Direction { get; set; }
    }

    public enum OrderDirection
    {
        Ascending, Descending
    }
}
