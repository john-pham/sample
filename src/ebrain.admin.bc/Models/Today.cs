using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Today : HistoricalEntity
    {
        public Guid TodayId { get; set; }
        [MaxLength(256)]
        public string TodayCode { get; set; }
        [MaxLength(256)]
        public string TodayName { get; set; }
        public string TodayNameEn { get; set; }
    }
}