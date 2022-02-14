using System;
using System.Collections;

namespace Countries1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Варіант 11
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            IAdmUnit Earth = new Region("Земля");
            IAdmUnit Europe = Earth.AddRegion("Європа");

            var ukr = Europe.AddRegion("Україна");

            var kr = ukr.AddRegion("Дніпропетровська область").AddRegion("Криворізький район");
            kr.AddLocality("Авангард", 1522); kr.AddLocality("Кривий Ріг", 624_579); kr.AddLocality("Новопілля", 2047);

            var ods = ukr.AddRegion("Одеська область").AddRegion("Одеський район");
            ods.AddLocality("Одеса", 1_015_826); ods.AddLocality("Авангард", 5554);

            var south = Earth.AddRegion("Південна Америка");
            var gnd = south.AddRegion("Гондурас");
            gnd.AddLocality("Тегусігальпа", 894_000);
            gnd.AddLocality("Сан-Педро-Сула", 15_000);

            Console.WriteLine("\tВСІ АДМІНІСТРАТИВНІ ОДИНИЦІ:");
            foreach(IAdmUnit adm in Earth)
                PrintAdmData(adm);

            Console.WriteLine("\tТІЛЬКИ НАСЕЛЕНІ ПУНКТИ:");
            foreach (IAdmUnit adm in ((Region)Europe).LocalitiesOnly)
                PrintAdmData(adm);

            Console.WriteLine("\tПОШУК ЕЛЕМЕНТІВ ЗА ЗНАЧЕННЯМ");
            foreach (IAdmUnit adm in Europe.Search("Авангард"))
                PrintAdmData(adm);

        }

        static void PrintAdmData(IAdmUnit adm)
        {
            string data = adm.Name + '.';
            IEnumerable c = adm.Containing;
            while (c != null && c is IAdmUnit cadm)
            {
                data = cadm.Name + " -> " + data;
                c = cadm.Containing;
            }
            Console.WriteLine(data);
            Console.WriteLine("Населення: " + adm.Accept(new CalcPopVisitor()));
        }
    }

    static class Ext
    {
        public static IAdmUnit AddRegion(this IAdmUnit comp, string name)
        {
            if (comp is Region adm)
                return adm.AddRegion(name);
            throw new Exception("Zachem...");
        }
        public static IAdmUnit AddLocality(this IAdmUnit comp, string name, ulong population)
        {
            if (comp is Region adm)
                return adm.AddLocality(name, population);
            throw new Exception("Ne nado");
        }
    }
}
