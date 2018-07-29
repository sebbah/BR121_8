using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akhn_8
{
    class GearTable
    {
        public string namePos { get; set; }
        public int sumPos { get; set; }

        public GearTable(string snamePos, int nsumPos)
        {
            namePos = snamePos;
            sumPos = nsumPos;
        }
    }
}
