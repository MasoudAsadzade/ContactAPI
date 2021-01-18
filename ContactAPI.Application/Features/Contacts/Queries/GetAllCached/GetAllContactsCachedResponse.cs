namespace ContactAPI.Application.Features.Contacts.Queries.GetAllCached
{
    public class GetAllContactsCachedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}