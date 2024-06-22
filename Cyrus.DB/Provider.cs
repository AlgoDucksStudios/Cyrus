using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotenv.net;

namespace Cyrus.DB
{
    internal class Provider
    {
        public static string env(string name, string _default = null)
        {
            try
            {
                DotEnv.Load(); // Load environment variables from .env file

                var val = Environment.GetEnvironmentVariable(name);
                if (val == null)
                {
                    return _default;
                }
                return val;
            }
            catch (Exception e)
            {
                throw new Exception($"[X] {e.Message}");
            }
        }
        public static T env<T>(string name, object _default = null)
        {

            try
            {
                DotEnv.Load(); // Load environment variables from .env file

                var val = Environment.GetEnvironmentVariable(name);
                if (val == null)
                {
                    return (T)_default;
                }

                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception e)
            {
                throw new Exception($"[X] {e.Message}");
            }
        }
    }
}
