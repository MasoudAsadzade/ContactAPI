namespace ContactAPI.Infrastructure.CacheKeys
{
    public static class ContactSkillCacheKeys
    {
        public static string ListKey => "ContactSkillList";
        public static string SelectListKey => "ContactSkillSelectList";
        public static string GetKey(string UserIdentityId, int SkillId) => $"ContactSkill-{UserIdentityId}-{SkillId}";
        public static string GetKey(string UserIdentityId) => $"ContactSkill-{UserIdentityId}";
        public static string GetDetailsKey(string UserIdentityId, int SkillId) => $"ContactSkill-{UserIdentityId}-{SkillId}";
    }
}