



namespace Org.Joey.Common
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class GivenObjectCompare : IObjectCompare<JObject, IEnumerable<object>>
    {
        public GivenObjectCompare(
            CompareGates gate,
            Func<JObject, object> selector,
            Func<IEnumerable<object>> compareTo,
            ObjectChooseOptions options = ObjectChooseOptions.ChooseA)
        {
            this.funComapreTo = compareTo;
            this.funSelector = selector;
            this.compareGate = gate;
            this.Option = options;
        }
        private Func<JObject, object> funSelector;
        private Func<IEnumerable<object>> funComapreTo;
        private CompareGates compareGate;
        public string OutStr { get; set; }

        public ObjectChooseOptions Option { get; set; }



        public bool Compare(object a, object b)
        {
            var cast = b as IEnumerable<object>;
            if (cast == null) cast = Enumerable.Empty<object>();
            return Compare(a as JObject, cast);
        }

        public bool Compare(JObject a, IEnumerable<object> b)
        {
            var input = this.funSelector(a) ?? string.Empty;

            switch (this.compareGate)
            {
                case CompareGates.EqualTo:
                    return this.funComapreTo().Any((ctx) =>
                    {
                        if (ctx == null)
                        {
                            return string.IsNullOrEmpty(input.ToString());
                        }                       
                        if (ctx.ToString().StartsWith("#Regx:"))
                        {
                            var regx = ctx.ToString().Replace("#Regx:", string.Empty);
                            return !Regex.IsMatch(this.funSelector(a).ToString(), regx);
                        }
                        return object.Equals(ctx, this.funSelector(a));
                    });
                case CompareGates.Unequal:
                    return !this.funComapreTo().Any((ctx) =>
                    {
                        if (ctx == null)
                        {
                            return string.IsNullOrEmpty(input.ToString());
                        }
                        if (ctx.ToString().StartsWith("#Regx:"))
                        {
                            var regx = ctx.ToString().Replace("#Regx:", string.Empty);
                            return Regex.IsMatch(this.funSelector(a).ToString(), regx);
                        }
                        return object.Equals(ctx, this.funSelector(a));
                    });
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
