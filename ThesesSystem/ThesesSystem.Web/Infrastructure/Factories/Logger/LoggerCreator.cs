namespace ThesesSystem.Web.Infrastructure.Factories.Logger
{
    using ThesesSystem.Data;

    public abstract class LoggerCreator
    {
        public abstract Logger Create(IThesesSystemData data);
    }
}