using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public class RoomWithHidingPlace : Room, IHidingPlace
    {
        public RoomWithHidingPlace(string name, string decoration, string placeToHide) 
            : base(name, decoration)
        {
            this.placeToHide = placeToHide;
        }

        private string placeToHide;

        public string PlaceToHide
        {
            get
            {
                return placeToHide;
            }
        }
    }
}
