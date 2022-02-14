using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Countries1
{
    class Locality : IAdmUnit
    {
        public string Name { get; }
        public IEnumerable Containing { get; }
        public ulong Population { get; private set; }

        private Locality() { }

        public Locality(string name, ulong pop, IAdmUnit containing)
        {
            Name = name;
            Population = pop;
            Containing = containing;
        }

        public T Accept<T>(IAdmVisitor<T> v) => v.doForLocality(this);

        public IEnumerator<IAdmUnit> GetEnumerator() => Search().GetEnumerator();

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public IEnumerable<IAdmUnit> Search(string name = "")
        {
            var match = new List<IAdmUnit>();
            if (this.Name.ToLower().Contains(name.ToLower()))
                match.Add(this);
            return match;
        }
    }
}
