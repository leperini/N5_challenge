namespace Domain.Helpers
{
    public class EntitiesHelper
    {

        public static string SOFT_DELETE_TAG = "SoftDelete";
        public static string KEY_TOTAL_COUNT = "totalCount";
        public static bool IsNull<T>(T model) => model == null;

    }
}
