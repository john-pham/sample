using ebrain.admin.bc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Models
{
    public class Room : HistoricalEntity
    {
        public Guid RoomId { get; set; }

        public string RoomCode { get; set; }

        public string RoomName { get; set; }

        public Guid? BranchId { get; set; }
    }
}