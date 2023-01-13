namespace ET.Logging.Core.Concrete
{
    public class LogDetailWithException : LogDetail
    {
        public LogDetailWithException()
        {
        }

        public LogDetailWithException(string fullName, string methodName, string user, string exceptionMessage)
            : base(fullName, methodName, user)
        {
            ExceptionMessage = exceptionMessage;
        }

        public string ExceptionMessage { get; set; }

        public static LogDetailWithException Create(string fullName, string methodName, string user, string exceptionMessage)
        {
            return new LogDetailWithException(fullName, methodName, user, exceptionMessage);
        }
    }
}
