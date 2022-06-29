using System;
using System.Collections.Generic;

#nullable disable

namespace kolokwium.Models
{
    public partial class FireTruckAction
    {
        public int IdFireTruckAction { get; set; }
        public int IdFireTruck { get; set; }
        public int IdAction { get; set; }
        public DateTime AssignmentDate { get; set; }

        public virtual Action IdActionNavigation { get; set; }
        public virtual FireTruck IdFireTruckNavigation { get; set; }
    }
}
