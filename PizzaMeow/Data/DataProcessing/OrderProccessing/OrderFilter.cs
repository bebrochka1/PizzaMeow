using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.DataProcessing.OrderProccessing
{
    public class OrderFilter
    {
        public DateTime? DateTime { get; set; }
        public DateDirection? Direction { get; set; }
        public LessOrMore LessOrTMore { get; set; }
        public decimal? TotalPrice { get; set; }
        public Status? Status { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }

    public enum DateDirection
    {
        Before,
        After
    }

    public enum LessOrMore
    {
        Less,
        More
    }
}
