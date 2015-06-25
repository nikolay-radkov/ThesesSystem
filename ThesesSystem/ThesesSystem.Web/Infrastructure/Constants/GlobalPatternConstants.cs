namespace ThesesSystem.Web.Infrastructure.Constants
{
    public static class GlobalPatternConstants
    {
        public const string FORWARD_MESSAGE_URL = "/Message?friendId={0}";
        public const string FORWARD_URL = "/{0}/{1}";
        public const string FORWARD_URL_WITH_ID = "/{0}/{1}/{2}";
        public const string VERSION_NAME = "{0}+{1}";
        public const string CREATE_THESIS = "{0} създаде дипломната работа";
        public const string USER_COMMENTED = "{0} коментира дипломната работа";
        public const string USER_ADDED_VERSION = "{0} добави нова версия";
        public const string USER_ADDED_PART = "{0} добави нова част към дипломната работа";
        public const string USER_ADDED_REVIEW = "{0} добави рецензия";
        public const string USER_ADDED_FINAL_EVALUATION = "{0} добави крайна оценка";
        public const string USER_COMPLEDED_THESIS = "{0} завърши дипломната работа";
        public const string USER_FULL_NAME = "{0} {1}";

        public const string NOTIFICATION_INVITATION = "{0} Ви добави към дипломна работа {1}";
        public const string NOTIFICATION_ADDED_VERSION = "{0} добави нова версия към {1}";
        public const string NOTIFICATION_COMMENTED = "{0} коментира {1}";
        public const string NOTIFICATION_ADDED_PART = "{0} добави нова част към {1}";
        public const string NOTIFICATION_ADDED_REVIEW = "{0} добави рецензия към {1}";
        public const string NOTIFICATION_ADDED_FINAL_EVALUATION = "{0} добави крайна оценка за {1}";
        public const string NOTIFICATION_COMPLEDED_THESIS = "{0} завърши {1}";
        public const string NOTIFICATION_USER_VERIFIED = "{0} потвърди вашите данни";
        public const string NOTIFICATION_USER_COMPLETE = "Поздравления! Вече можете да използвате системата";
        public const string NOTIFICATION_NEW_FRIEND = "{0} Ви добави като приятел";
        public const string NOTIFICATION_LOST_FRIEND = "{0} Ви премахна от приятели";
        public const string NOTIFICATION_NEW_MESSAGE = "{0} Ви изпрати съобщение";
        public const string NOTIFICATION_REVIEW_PART = "{0} маркира за проверка частта {1}";
    }
}