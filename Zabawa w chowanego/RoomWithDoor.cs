using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
    {
        private string doorDescription;

        public RoomWithDoor(string name, string decoration, string placeToHide, string doorDescription) 
            : base(name, decoration, placeToHide)
        {
            this.doorDescription = doorDescription;
        }

        public string DoorDescription
        {
            get
            {
                return doorDescription;
            }
        }

        public Location DoorLocation { get; set; }

    }
}
