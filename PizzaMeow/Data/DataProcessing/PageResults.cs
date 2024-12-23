namespace PizzaMeow.Data.DataProcessing
{
    public class PageResults<T>
    {
        public List<T> Values { get; set; }
        public int TotalCount { get; set; }
        public PageResults(List<T> values, int totalCount) 
        {
            Values = values;
            TotalCount = totalCount;
        }
    }
}
