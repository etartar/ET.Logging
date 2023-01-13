namespace ET.Logging.Core.Concrete
{
    public class LogDetail
    {
        public LogDetail()
        {
            Parameters = new List<LogParameter>();
        }

        public LogDetail(string fullName, string methodName, string user) : this()
        {
            FullName = fullName;
            MethodName = methodName;
            User = user;
        }

        public string FullName { get; set; }
        public string MethodName { get; set; }
        public string User { get; set; }
        public List<LogParameter> Parameters { get; set; }

        public static LogDetail Create(string fullName, string methodName, string user)
        {
            return new LogDetail(fullName, methodName, user);
        }

        public LogDetail AddParameter(string name, object value, string type)
        {
            Parameters.Add(new LogParameter(name, value, type));
            return this;
        }
    }
}
