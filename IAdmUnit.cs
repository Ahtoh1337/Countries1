using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Countries1
{
    interface IAdmUnit : IEnumerable<IAdmUnit>
    {
        string Name { get; }
        IEnumerable Containing { get; }
        IEnumerable<IAdmUnit> Search(string name);
        T Accept<T>(IAdmVisitor<T> visitor);
    }
}
