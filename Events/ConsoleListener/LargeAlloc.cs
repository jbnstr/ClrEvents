using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleListener
{
    class LargeAlloc
    {
        const int _arraySize = 1000;
        static object[] staticArray = new object[_arraySize];

        public event EventHandler Ready;

        public void Go()
        {
            object[] localArray = new object[_arraySize];

            Random rand = new Random();
            for (int i = 0; i < _arraySize; i++)
            {
                staticArray[i] = GetNewObject(rand.Next(0, 4));
                localArray[i] = GetNewObject(rand.Next(0, 4));
            }

            Ready?.Invoke(this, new EventArgs());
            
            //Console.WriteLine("Use PerfView to examine heap now. Press any key to exit...");
            //Console.ReadKey();

            // This will prevent localArray from being garbage collected before you take the snapshot
            Console.WriteLine(staticArray.Length);
            Console.WriteLine(localArray.Length);
        }

        private static Base GetNewObject(int type)
        {
            Base obj = null;
            switch (type)
            {
                case 0: obj = new A(); break;
                case 1: obj = new B(); break;
                case 2: obj = new C(); break;
                case 3: obj = new D(); break;
            }
            return obj;
        }
    }

    class Base
    {
        private byte[] memory;
        protected Base(int size) { this.memory = new byte[size]; }
    }

    class A : Base { public A() : base(1000) { } }
    class B : Base { public B() : base(10000) { } }
    class C : Base { public C() : base(100000) { } }
    class D : Base { public D() : base(1000000) { } }
}
