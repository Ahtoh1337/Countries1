using System;
using System.Collections.Generic;
using System.Text;

namespace Countries1
{
    class CalcPopVisitor : IAdmVisitor<ulong>
    {
        public ulong doForRegion(Region r)
        {
            ulong sum = 0;
            foreach(IAdmUnit adm in r.AdmContents)
            {
                sum += adm.Accept(this);
            }
            return sum;
        }

        public ulong doForLocality(Locality l) => l.Population;
    }
}
