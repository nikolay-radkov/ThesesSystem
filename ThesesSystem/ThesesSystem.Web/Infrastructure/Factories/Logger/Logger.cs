namespace ThesesSystem.Web.Infrastructure.Factories.Logger
{
    using ThesesSystem.Data;

    public abstract class Logger
    {
        private IThesesSystemData data;

        public Logger(IThesesSystemData data)
        {
            this.Data = data;
        }

        public IThesesSystemData Data
        {
            get
            {
                return this.data;
            }

            private set
            {
                this.data = value;
            }
        }

        public abstract void Log(object message);
    }
}