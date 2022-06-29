using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium.DTOs.Requests
{
    public class FireTruckDtoRequest
    {
        public int IdAction { get; set; }
        public int IdFireTruck { get; set; }
        public Byte NeedSpecialEquipment { get; set; }
    }
}
