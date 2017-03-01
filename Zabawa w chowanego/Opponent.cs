using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zabawa_w_chowanego
{
    public class Opponent
    {
        public Opponent (Location myLocation, Random random)
        {
            this.myLocation = myLocation;
            this.random = random;
        }

        private Location myLocation;
        private Random random;

        public void Move()
        {
            int arrayExitsIndex;
            do
            {
                if (this.myLocation is IHasExteriorDoor)    // Jeżeli pomieszczenie mam drzwi zewnętrzne rzuć monetą
                {
                    if (random.Next(2) == 1)   // Rzut monetą. Jeżeli wartość jest równa 1 przechodzę przez drzwi
                    {
                        IHasExteriorDoor locationWithDoor;
                        locationWithDoor = this.myLocation as IHasExteriorDoor;
                        this.myLocation = locationWithDoor.DoorLocation;
                    }
                    else   // Jeżeli wartość na monecie jest równa 0 wybieram losowe z dostępnych wyjść
                    {
                        arrayExitsIndex = random.Next(this.myLocation.Exits.Length);     // Wybór losowego pomieszczenia z dostępnych
                        this.myLocation = this.myLocation.Exits[arrayExitsIndex];
                    }
                }
                else    /// Jeżeli pomieszczenie, w którym jest przeciwnik nie ma drzwy wyjściowych 
                {
                    arrayExitsIndex = random.Next(this.myLocation.Exits.Length);     // Wybór losowego pomieszczenia z dostępnych
                    this.myLocation = this.myLocation.Exits[arrayExitsIndex];
                }
            } while (!(this.myLocation is IHidingPlace));  // Lokazlizacja przeciwnika ma kryjówkę
        }

        public bool Check(Location location)
        {
            if (location == this.myLocation)
                return true;
            else
                return false;
        } 
    }
}
