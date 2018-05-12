using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAndCovar3
{
    // classes
    class A
    {

    }

    class B : A
    {

    }

    // delegates
    delegate A Covar();
    delegate void Contravar(B b);

    // interfaces
    interface ICovar<out T>
    {
        T Method();
    }

    interface IContravar<in T>
    {
        void Method(T t);
        int Count { get; }
    }

    // classes that use interfaces
    class ClassCovar<T> : ICovar<T> where T : new()
    {
        public T Method() => new T();
    }

    class ClassContravar<T> : IContravar<T> where T : A
    {
        private int _count;

        public void Method(T t) => ++_count;
        public int Count { get => _count; }
    }

    class Program
    {
        static void TakeA(A a)
        {
            Console.WriteLine("TakeA");
        }

        static void Main(string[] args)
        {
            // covariance delegate
            Covar covar = () => new B();
            var result = covar();
            Console.WriteLine($"{result.GetType().Name}"); // shows B instead of A

            // contravariance delegate
            Contravar contravar = TakeA;
            contravar(new B()); // shows "TakeA"

            // covariance interfaces
            ICovar<A> classCovarA = new ClassCovar<A>();
            ICovar<B> classCovarB = new ClassCovar<B>();

            result = classCovarA.Method();
            Console.WriteLine($"{result.GetType().Name}"); // shows A

            classCovarA = classCovarB;
            result = classCovarA.Method();
            Console.WriteLine($"{result.GetType().Name}"); // shows B instead of A

            // contravariance interfaces
            IContravar<A> contravarA = new ClassContravar<A>();
            IContravar<B> contravarB = new ClassContravar<B>();

            contravarA.Method(new A());
            Console.WriteLine($"{contravarA.Count}"); // shows 1
            contravarB = contravarA;
            contravarB.Method(new B());
            Console.WriteLine($"{contravarB.Count}"); // shows 2 instead of 1

            Console.ReadKey();
        }
    }
}
