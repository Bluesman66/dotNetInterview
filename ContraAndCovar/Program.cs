using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAndCovar
{
    class Automobile
    {

    }

    class Car : Automobile
    {

    }

    class Truck : Automobile
    {

    }

    class MonsterTruck : Truck
    {

    }

    class Program
    {
        static void Main(string[] args)
        {         
            // Covariant example
            IEnumerable<Automobile> covariant = new List<Truck>();

            // Contravariant example
            Action<Truck> contravariant = DescribeAutomobile;
            contravariant(new Truck());
            contravariant(new MonsterTruck());
            //contravariant(new Car());

            Action<Truck> contravariant2 = new Action<Automobile>((Automobile automobile) => { });

            Console.ReadKey();
        }        

        static void DescribeAutomobile(Automobile automobile)
        {
            Console.WriteLine($"This automobile is a {automobile.GetType().Name}");
        }
    }
}
