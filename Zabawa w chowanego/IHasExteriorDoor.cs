using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public interface IHasExteriorDoor
    {
        string DoorDescription { get; }
        Location DoorLocation { get; }
    }
}
