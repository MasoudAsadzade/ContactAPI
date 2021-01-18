namespace ContactAPI.Infrastructure.CacheKeys
{
    public static class SkillCacheKeys
    {
        public static string ListKey => "SkillList";

        public static string SelectListKey => "SkillSelectList";

        public static string GetKey(int skillId) => $"Skill-{skillId}";

        public static string GetDetailsKey(int skillId) => $"SkillDetails-{skillId}";
    }
}