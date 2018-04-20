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

    public class Human : Animal { }

    // Covariant delegates 
    delegate Animal ReturnAnimalDelegate();
    delegate Bird ReturnBirsDelegate();

    // Contravariant delegates
    delegate void TakeAnimalDelegate(Animal animal);
    delegate void TakeBirdDelegate(Bird bird);

    // Covariant generic interface
    interface IProcess<out T>
    {
        T Process();
    }

    public class AnimalProcessor<T> : IProcess<T> where T : new()
    {
        public T Process()
        {
            return new T();
        }
    }

    // Contravariant generic interface
    interface IZoo<in T>
    {
        void Add(T t);
        int Count { get; }
    }

    public class Zoo<T> : IZoo<T>
    {
        private IList<T> list = new List<T>();
        public void Add(T t) => list.Add(t);
        public int Count => list.Count;
    }

    public class CustomComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return 0;
        }
    }

    class Program
    {
        public static Animal GetAnimal() => new Animal();
        public static Bird GetBird() => new Bird();

        public static void Eat(Animal animal) => animal.Eat();
        public static void Fly(Bird bird) => bird.Fly();

        static void Main(string[] args)
        {
            #region === DELEGATES ===
            Console.WriteLine("=== DELEGATES ===");
            // Covariance with delegates
            ReturnAnimalDelegate d = GetBird;
            Animal a = d();
            Console.WriteLine($"a: {a.GetType().Name}"); // shows Bird type not Animal 

            // Contravariance with delegates
            TakeBirdDelegate d2 = Eat;
            d2(new Bird()); // performs Eat not Fly

            // Contravariance with delegates EXAMPLE
            Action<Bird> contravariant2 = new Action<Animal>((Animal animal) => { });
            #endregion

            #region === ARRAYS ===
            Console.WriteLine("=== ARRAYS ===");
            // Covariance with arrays
            Animal[] animals = new Bird[10];
            //animals[0] = new Human(); throw exception by run
            Console.WriteLine($"animals: {animals.GetType().Name}"); // shows Bird[] type not Animal[]
            #endregion

            #region === GENERICS ===
            Console.WriteLine("=== GENERICS ==="); 
            // Covariance with generics
            IProcess<Animal> animalProcessor = new AnimalProcessor<Animal>();
            IProcess<Bird> birdProcessor = new AnimalProcessor<Bird>();
            animalProcessor = birdProcessor;
            var processType = animalProcessor.Process();
            Console.WriteLine($"processType: {processType.GetType().Name}"); // shows Bird type not Animal

            // Covariance with generics EXAMPLE
            IEnumerable<Animal> animals2 = new List<Bird>();            

            // Contravariance with generics
            IZoo<Animal> animalZoo = new Zoo<Animal>();
            animalZoo.Add(new Animal());
            IZoo<Bird> birdZoo = new Zoo<Bird>();
            Console.WriteLine($"birdZoo.Count: {birdZoo.Count}"); // shows 0
            birdZoo = animalZoo;
            birdZoo.Add(new Bird());
            Console.WriteLine($"birdZoo.Count: {birdZoo.Count}"); // shows 2 not 1, because birdZoo points to animalZoo

            // Contravariance with generics EXAMPLE
            IComparer<Bird> comparer = new CustomComparer<Animal>();
            #endregion

            Console.ReadKey();
        }
    }
}
