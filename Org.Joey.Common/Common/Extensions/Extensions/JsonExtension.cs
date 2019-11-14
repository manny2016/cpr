namespace Org.Joey.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class JsonExtension
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(JsonExtension));
        public static T DeserializeToObject<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                //Logger.LogException(ex);
                Logger.Error("DeserializeToObject", ex);
                return default(T);
            }
        }
        public static Dictionary<string, string> DeserializeToDictionary(this string json)
        {
            return DeserializeToObject<Dictionary<string, string>>(json);
        }

        public static string SerializeToJson<T>(this T data)
        {
            try
            {
                return JsonConvert.SerializeObject(data, Formatting.None);
            }
            catch (Exception ex)
            {

                Logger.Error("SerializeToJson", ex);
                return string.Empty;
            }
        }
        public static T DeserializeFromStream<T>(this Stream stream) where T : class
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize(jsonTextReader) as T;
                }
            }
        }

        public static T TryGetValue<T>(this JToken JObject, string jpath)
        {
            try
            {
                var result = JObject.SelectToken(jpath).Value<T>();
                return result;

            }
            catch 
            {
                return default(T);
            }
        }
        public static T TryGetValue<T>(this JObject JObject, string jpath)
        {
            try
            {
                var result = JObject.SelectToken(jpath).Value<T>();
                return result;

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public static IEnumerable<T> TryGetValues<T>(this JObject JObject, string jpath)
        {
            try
            {
                var result = JObject.SelectToken(jpath).Values<T>().ToList();
                return result;

            }
            catch
            {
                return default(IEnumerable<T>);
            }
        }
        public static long? ToUnixTimestamp(this JToken JObject, string jpath)
        {
            var datetime = JObject.TryGetValue<string>(jpath);
            if (string.IsNullOrEmpty(datetime)) return null;
            if (DateTime.TryParse(datetime, out DateTime dt))
                return dt.ToUnixStampDateTime();
            return null;
        }
        public static bool TryCastLogicalExpression(this JObject jObject, out ILogicalExpression expression)
        {
            var text = jObject.ToString();

            expression = null;
            if (Enum.TryParse<CompareGates>(jObject.TryGetValue<string>("$.compare"), out CompareGates compare))
            {
                var filedName = jObject.TryGetValue<string>("$.fieldName");


                var numValues = jObject.TryGetValues<decimal>("$.values");
                if (numValues != null)
                {
                    expression = new NumericalLogicalExpression(compare, filedName, numValues.ToArray());
                    return true;
                }
                var strValues = jObject.TryGetValues<string>("$.values");
                if (strValues != null)
                {
                    expression = new StringLogicalExpression(compare, filedName, strValues.ToArray());
                    return true;
                }
                return false;
            }
            else
            {
                throw new OrgJoeyCastException("The enum type CompareGates cast error.");
            }

        }


        public static string GetCustomerID(this string contractHyperlink)
        {
         
            var prefix = "customer/";
            //https://compassone.microsoft.com/Deal/181330/Customer/209003/
            if (string.IsNullOrEmpty(contractHyperlink)) return null;
            contractHyperlink = contractHyperlink.ToLower();
            var startIndex = contractHyperlink.IndexOf(prefix);
            if (startIndex == -1) return null;

            var text = contractHyperlink.Substring(startIndex, contractHyperlink.Length - startIndex)
                .TrimStart("customer/".ToCharArray());
            return text.Split('/')[0];
        }
    }
}