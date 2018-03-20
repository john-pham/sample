using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class ConfigNumberOfCode : HistoricalEntity
    {
        public long ConfigNumberOfCodeId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string DateFormat4CreateNumber { get; set; }
        public int LenOfNoOfNumber { get; set; }
       
    }
}