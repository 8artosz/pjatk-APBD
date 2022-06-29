using System;
using System.Collections.Generic;

#nullable disable

namespace kolokwium.Models
{
    public partial class Firefighter
    {
        public Firefighter()
        {
            FirefighterActions = new HashSet<FirefighterAction>();
        }

        public int IdFirefighter { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<FirefighterAction> FirefighterActions { get; set; }
    }
}
