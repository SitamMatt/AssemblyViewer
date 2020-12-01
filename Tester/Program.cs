using System.Reflection;
using Model.Converters;

namespace Tester
{
    public class Program
    {
        public int prop;
        
        static void Main(string[] args)
        {
            var asm = Assembly.GetExecutingAssembly();
            var info = new Converter().Convert(asm);
        }
    }

    public class A
    {
        public B b;
    }

    public class B
    {
        public A a;
    }
}
