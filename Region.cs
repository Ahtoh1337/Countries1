using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Countries1
{
    class Region : IAdmUnit
    {
        public string Name { get; }

        public IEnumerable<IAdmUnit> AdmContents { get; private set; }
        public IEnumerable Containing { get; }

        private Region() { }

        public Region(string name, IAdmUnit containing = null)
        {
            Name = name;
            Containing = containing;
            AdmContents = new List<IAdmUnit>();
        }

        public T Accept<T>(IAdmVisitor<T> v) => v.doForRegion(this);

        public IEnumerable<IAdmUnit> Search(string name = "")
        {
            var match = new List<IAdmUnit>();
            if (this.Name.ToLower().Contains(name.ToLower()))
                match.Add(this);
            foreach (IAdmUnit adm in AdmContents)
            {
                match = match.Concat(adm.Search(name)).ToList();
            }
            return match;
        }

        public IEnumerator<IAdmUnit> GetEnumerator() => Search().GetEnumerator();
        public IEnumerable<IAdmUnit> LocalitiesOnly
        {
            get
            {
                return (from adm in Search() where (adm is Locality) select adm);
            }
        }
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public IAdmUnit AddRegion(string name)
        {
            var r = new Region(name, this);
            AdmContents = AdmContents.Append(r).OrderBy(adm => adm.Name);
            return r;
        }

        public IAdmUnit AddLocality(string name, ulong population)
        {
            var l = new Locality(name, population, this);
            AdmContents = AdmContents.Append(l).OrderBy(adm => adm.Name);
            return l;
        }
    }
}
