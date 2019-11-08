using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Org.Joey.Common
{
    public class StringFormatCompare : IObjectCompare<JObject, string>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(StringFormatCompare));
        public StringFormatCompare(Func<JObject, string> selector)
        {
            this.Selector = selector;
        }

        public ObjectChooseOptions Option { get; set; }

        public string OutStr { get; private set; }

        private Func<JObject, string> Selector
        {
            get; set;
        }

        public bool Compare(JObject a, string regex)
        {
            var input = this.Selector(a);
            if (string.IsNullOrEmpty(input))
            {

                this.OutStr = $"Compare String Value; Object A or B is null return default false value.";
                Logger.Warn(this.OutStr);
                return false;
            }
            bool result = !Regex.IsMatch(input, regex);
            this.OutStr = $"Compare String Format A={input??"(Blank)"};Regex={regex??"(Blank)"};";
            Logger.Warn(this.OutStr);
            return result;
        }

        public bool Compare(object a, object b)
        {
            return Compare(a as JObject, b as string);
        }


    }
}
