using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public abstract class JObjectCompare<VType> : IObjectCompare<JObject, JObject>
    {
        public JObjectCompare(
            CompareGates gate,
            ObjectChooseOptions objectChoose,
            Func<JObject, VType> selectorA,
            Func<JObject, VType> selectorB)
        {
            this.SelectorA = selectorA;
            this.SelectorB = selectorB;
            this.CompareGate = gate;
            this.Option = objectChoose;
            
        }

        public ObjectChooseOptions Option { get; private set; }

        public string OutStr { get; protected set; }

        
        protected CompareGates CompareGate { get; set; }

        protected Func<JObject, VType> SelectorA { get; set; }
        protected Func<JObject, VType> SelectorB { get; set; }


        public abstract bool Compare(JObject a, JObject b);

        public bool Compare(object a, object b)
        {
            return this.Compare(a as JObject, b as JObject);
        }

        
    }
}
