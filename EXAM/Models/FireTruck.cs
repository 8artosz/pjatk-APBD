using System;
using System.Collections.Generic;

#nullable disable

namespace kolokwium.Models
{
    public partial class FireTruck
    {
        public FireTruck()
        {
            FireTruckActions = new HashSet<FireTruckAction>();
        }

        public int IdFireTruck { get; set; }
        public string OperationalNumber { get; set; }
        public bool SpecialEquipment { get; set; }

        public virtual ICollection<FireTruckAction> FireTruckActions { get; set; }
    }
}
