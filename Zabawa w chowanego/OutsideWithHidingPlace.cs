using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot, string placeToHide) 
            : base(name, hot)
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
