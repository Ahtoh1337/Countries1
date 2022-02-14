using System;
using System.Collections.Generic;
using System.Text;

namespace Countries1
{
    interface IAdmVisitor<T>
    {
        T doForRegion(Region r);
        T doForLocality(Locality l);
    }
}
