using System;

namespace LogParser.Infrastructure.Validation
{
    public static class Require
    {
        public static void IsValid(DateTimeOffset? start,DateTimeOffset? end, Func<string> message)
        {
            IsTrue(start != null && end != null && start <= end, message);
        }
        public static void IsValid(DateTimeOffset start, DateTimeOffset end, Func<string> message)
        {
            IsTrue(start <= end, message);
        }
        public static void IsTrue(bool value, Func<string> message)
        {
            InnerCheck<ArgumentException, bool>(value, (i) => i == false, message);
        }

        public static void IsTrue(bool value, string message)
        {
            InnerCheck<ArgumentException, bool>(value, (i) => i == false, message);
        }

        public static void NotNull(object value, Func<string> message)
        {
            InnerCheck<ArgumentNullException, object>(value, i => i == null, message);
        }

        public static void NotNull(object value, string message)
        {
            InnerCheck<ArgumentNullException, object>(value, i => i == null, message);
        }
        public static void NotEmpty(string value, Func<string> message)
        {
            InnerCheck<ArgumentNullException, string>(value, string.IsNullOrEmpty, message);
        }

        public static void NotEmpty(string value, string message)
        {
            InnerCheck<ArgumentNullException, string>(value, string.IsNullOrEmpty, message);
        }

        public static void NotEmpty<TE>(string value, Func<string> message) where TE : Exception
        {
            InnerCheck<TE, string>(value, string.IsNullOrEmpty, message);
        }
        public static void NotEmpty<TE>(string value, string message) where TE : Exception
        {
            InnerCheck<TE, string>(value, string.IsNullOrEmpty, message);
        }

        private static void InnerCheck<TE, TIn>(TIn data, Func<TIn, bool> operation, Func<string> message)
            where TE : Exception
        {
            if (operation(data))
            {
                CreateInstanceException<TE>(message());
            }
        }

        private static void InnerCheck<TE, TIn>(TIn data, Func<TIn, bool> operation, string message)
            where TE : Exception
        {
            if (operation(data))
            {
                CreateInstanceException<TE>(message);
            }
        }

        private static void CreateInstanceException<TE>(string message) where TE : Exception
        {
            throw (TE)Activator.CreateInstance(typeof(TE), message);
        }
    }
}