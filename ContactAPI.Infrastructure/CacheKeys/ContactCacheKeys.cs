namespace ContactAPI.Infrastructure.CacheKeys
{
    public static class ContactCacheKeys
    {
        public static string ListKey => "ContactList";
        public static string SelectListKey => "ContactSelectList";
        public static string GetKey(int contactId) => $"Contact-{contactId}";
        public static string GetKey(string userId) => $"Contact-{userId}";
        public static string GetDetailsKey(int contactId) => $"ContactDetails-{contactId}";
    }
}