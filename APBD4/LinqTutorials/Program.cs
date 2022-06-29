using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = LinqTasks.Task12();
            foreach(var result in t)
            {
               Console.WriteLine(result);
            }
            //var tt = LinqTasks.Task13(new int [] { 1, 1, 1, 1, 1, 1, 10,10,10,10,10,1, 1, 1,1 });
            //Console.WriteLine(tt);

        }
    }
}
