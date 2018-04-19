using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAndCovar2
{
    public class Animal
    {
        public void Eat() => Console.WriteLine("Eat");
    }

    public class Bird : Animal
    {
        public void Fly() => Console.WriteLine("Fly");
    }

    // Covariant delegates 
    delegate Animal ReturnAnimalDelegate();
    delegate Bird ReturnBirsDelegate();

    // Contravariant delegates
    delegate void TakeAnimalDelegate(Animal animal);
    delegate void TakeBirdDelegate(Bird bird);

    class Program
    {
        public static Animal GetAnimal() => new Animal();
        public static Bird GetBird() => new Bird();

        public static void Eat(Animal animal) => animal.Eat();
        public static void Fly(Bird bird) => bird.Fly();
        
        static void Main(string[] args)
        {
            // Polymorphism
            Animal a = new Bird();
            // Covariant delegate
            ReturnAnimalDelegate d = GetBird;

            // Contravariant delegate
            TakeBirdDelegate d2 = Eat;
            d2(new Bird());
        }
    }
}
