namespace ThesesSystem.Web.Infrastructure.Factories.Logger
{
    using Ninject;
    using System.Reflection;
    using ThesesSystem.Data;  

    public class ThesisLoggerCreator : LoggerCreator
    {
        public override Logger Create(IThesesSystemData data)
        {
            return new ThesisLogger(data);
        }
    }
}