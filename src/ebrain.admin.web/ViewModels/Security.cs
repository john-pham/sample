using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ebrain.ViewModels
{
    [System.AttributeUsage(System.AttributeTargets.Class |System.AttributeTargets.Struct)]
    public class Security : System.Attribute
    {
        public Guid ID { get; set; }

        public Security(string index)
        {
            if (!string.IsNullOrEmpty(index))
            {
                this.ID = new Guid(index);
            }
        }

    }
}
