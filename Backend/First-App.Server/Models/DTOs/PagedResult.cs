namespace First_App.Server.Models.DTOs
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
