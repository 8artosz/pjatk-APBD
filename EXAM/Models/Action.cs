using System;
using System.Collections.Generic;

#nullable disable

namespace kolokwium.Models
{
    public partial class Action
    {
        public Action()
        {
            FireTruckActions = new HashSet<FireTruckAction>();
            FirefighterActions = new HashSet<FirefighterAction>();
        }

        public int IdAction { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool NeedSpecialEquipment { get; set; }

        public virtual ICollection<FireTruckAction> FireTruckActions { get; set; }
        public virtual ICollection<FirefighterAction> FirefighterActions { get; set; }
    }
}
