namespace ContactAPI.Application.Features.Contacts.Queries.GetAllPaged
{
    public class GetAllContactsResponse
    {
        public int Id { get; set; }
        public string UserIdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobilePhoneNumber { get; set; }
    }
}