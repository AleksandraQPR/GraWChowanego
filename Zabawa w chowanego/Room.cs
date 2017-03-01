using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public class Room : Location
    {
        public Room(string name, string decoration) : base(name)
        {
            Decoration = decoration;
        }

        public string Decoration { get; private set; }

        public override string Description
        {
            get
            {
                string description = base.Description;
                description += "Widzisz tutaj " + Decoration;
                return description;
            }
        }
    }
}
