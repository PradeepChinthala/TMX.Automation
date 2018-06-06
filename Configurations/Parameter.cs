
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Configurations
{
    public static class Parameter
    {
        private static Dictionary<string, object> _parametersDictionary = new Dictionary<string, object>();
        // NOTE!  Regex.Replace, for string replacement, will be parsed.  Therefore if the replacement string contains special characters
        // such as $, then those need to be doubled escaped such as $$.  
        private static readonly Regex _paramsRegex = new Regex("(?<!%)%[a-zA-Z0-9_-]+?%"); // defined parameter format               

        public static void Add<T>(string key,T value, bool log = false) where T : class
        {
            if (_parametersDictionary.ContainsKey(key))
                _parametersDictionary[key] = value;

            else
                _parametersDictionary.Add(key, value);

            if (log)
            {
                var stringValue = value as string;
                Logger.InfoFormat($"[Parameter Created] - Name: [{key}]. Value: [{stringValue ?? typeof(T).ToString()}].");
            }
               
        }

        public static T Get<T>(string key) where T : class
        {
            object value = null;
            _parametersDictionary.TryGetValue(key, out value);

            if (value != null)
            {
                var stringValue = value as string;
                Logger.InfoFormat($"[Parameter Info] - Name: [{key}]. Value: [{stringValue ?? typeof(T).ToString()}].");

                return value as T;
            }
            else
            {
                Logger.InfoFormat($"[Null Value] - Parameter collection does not contain key: [{key}]");
                throw new Exception($"[Null Value] - Parameter collection does not contain key: [{key}]");
            }
        }

        public static string GetEmbedded(string value, List<string> variableNames = null)
        {
            if (variableNames == null)
            {
                variableNames = new List<string>() { };
            }

            // parse the value to see if we are using existing parameter values
            MatchCollection embeddedParams = _paramsRegex.Matches(value);
            foreach (Match paramsMatch in embeddedParams)
            {
                string paramKey = paramsMatch.Value.Trim();
                var doublePercent = new Regex("^%|%?");
                paramKey = doublePercent.Replace(paramKey, "");

                //have to check if infinite loop will happend
                if (variableNames.Contains(paramKey))
                    throw new Exception(String.Format("Possible infinite loop for value {0} and parameter name {1}", value, paramKey));

                variableNames.Add(paramKey);
                object tmpValue;
                _parametersDictionary.TryGetValue(paramKey, out tmpValue); // support only string type parameter
                string paramValue = tmpValue as string;
                if (!string.IsNullOrEmpty(paramValue) && IsEmbedded(paramValue))
                {
                    paramValue = GetEmbedded(paramValue, variableNames);
                }

                // It is safe to remove key after recursion ended
                variableNames.Remove(paramKey);

                if (paramValue == null)
                    throw new Exception(String.Format("[ERROR] - Parameter [{0}] of type [{1}] has null value", paramKey, typeof(string)));

                value = Regex.Replace(value, paramsMatch.Value, paramValue);
            }
            return value;
        }

        private static bool IsEmbedded(string paramValue)
        {
            if (_paramsRegex.Match(paramValue).Success)
                return true;
            return false;
        }
    }
}
