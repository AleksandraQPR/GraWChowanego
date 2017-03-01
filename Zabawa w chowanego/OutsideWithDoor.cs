using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDescription) 
            : base(name, hot)
        {
            this.doorDescription = doorDescription;
        }

        private string doorDescription;

        public string DoorDescription
        {
            get
            {
                return doorDescription;
            }
        }

        public Location DoorLocation { get; set; }

        public override string Description
        {
            get
            {
                return base.Description + " Widzisz teraz " + DoorDescription + "." ;
            }
        }
    }
}
