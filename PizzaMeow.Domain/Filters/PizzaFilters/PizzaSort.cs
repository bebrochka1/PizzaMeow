namespace PizzaMeow.Data.DataProcessing.PizzaProccessing
{
    public class PizzaSort
    {
        public string? OrderBy { get; set; }
        public PizzaSortDirection? PizzaSortDirection { get; set; }
    }

    public enum PizzaSortDirection
    {
        Ascending, Descending
    }
}
