using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJsonPars
{
    public class JsonPars
    {
        private static Dictionary<string, string> dictionaryJson = new Dictionary<string, string>();
        public static Dictionary<string, string> getJsonValid(JToken token, out string Error)
        {
            dictionaryJson.Clear();
            Validate(token, out Error);
            return recursivJson(token);
        }
        private static Dictionary<string, string> recursivJson(JToken jToken)
        {
            foreach (JToken token in jToken.Children())
            {
                if (token is JProperty)
                {
                    var prop = token as JProperty;
                    if (prop.Value.ToString()[0] != '{')
                    {
                        dictionaryJson.Add(prop.Path.Replace('.', ':'), prop.Value.ToString());
                    }
                }

                if (token.Children().ToList().Count > 0)
                {
                    recursivJson(token);
                }
            }
            return dictionaryJson;
        }

        private static bool Validate(JToken jsonString, out string Error)
        {
            try
            {
                JObject.Parse(jsonString.Parent.ToString());
                Error = null;
                return true;
            }
            catch (Exception ex)
            {
                Error = $"Неверный формат JSON строки: {ex.Message}";
                return false;
            }
        }
    }
}

