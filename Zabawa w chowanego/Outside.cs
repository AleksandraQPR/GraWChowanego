using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zabawa_w_chowanego
{
    public class Outside : Location
    {
        public Outside(string name, bool hot) : base(name)
        {
            Hot = hot;
        }

        public bool Hot { get; private set; }

        public override string Description
        {
            get
            {
                string description = base.Description;
                if (Hot)
                    description += "Tutaj jest bardzo gorąco";
                return description;
            }
        }
    }
}
