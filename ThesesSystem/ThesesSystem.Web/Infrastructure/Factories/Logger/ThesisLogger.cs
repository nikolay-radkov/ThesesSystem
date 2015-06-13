namespace ThesesSystem.Web.Infrastructure.Factories.Logger
{
    using System;
    using ThesesSystem.Data;
    using ThesesSystem.Models;

    public class ThesisLogger : Logger
    {
        public ThesisLogger(IThesesSystemData data)
            : base(data)
        {
        }

        public override void Log(object message)
        {
            var messageToLog = message as ThesisLog;

            if (messageToLog == null)
            {
                throw new ArgumentNullException("message", "Message must be of type ThesisLog!");
            }

            this.Data.ThesisLogs.Add(messageToLog);
            this.Data.SaveChanges();
        }
    }
}