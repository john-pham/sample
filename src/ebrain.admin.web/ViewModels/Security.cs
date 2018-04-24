using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ebrain.ViewModels
{
    [System.AttributeUsage(System.AttributeTargets.Class |System.AttributeTargets.Struct)]
    public class Security : System.Attribute
    {
        public IList<Guid> IDs { get; set; }

        public Security(params string[] ids)
        {
            this.IDs = new List<Guid>();

            foreach (var index in ids)
            {
                if (!string.IsNullOrEmpty(index))
                {
                    this.IDs.Add(new Guid(index));
                }
            }
        }

    }
}
