using System;
using System.Collections.Generic;

namespace CSharp6PocketReferenceBook
{
    class @class
    {
        public uint X { get; set; }
        public @class(uint x)
        {
            X = x;
        }
    }

    public class Wine
    {
        public readonly int legs = 8, eyes = 2;

        public Wine(decimal price) { }

        public Wine(decimal price, int year) { }
        public Wine(decimal price, DateTime dt) : this(price, dt.Year) { }

        int Foo(int[] numbers) => numbers.Length * 2;

        void Tmp()
        {
            Bunny b1 = new Bunny
            {
                Name = "Bo",
                LikesCarrots = true,
                LikesHumans = false
            };
            Bunny b2 = new Bunny("Bo")
            {
                LikesCarrots = true,
                LikesHumans = false
            };

        }

        public Wine()
        {
            Console.WriteLine("Wine");
        }
        ~Wine()
        {
            Console.WriteLine("Delete object Wine");
        }
    }

    public class Stack
    {
        int position;
        object[] data = new object[10];

        public void Push(object o) { data[position++] = o; }
        public object Pop() { return data[--position]; }
    }

    interface IElement
    {
    }

    class ElementA : IElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Z { get; set; }

        public void powiedzCos()
        {
            Console.WriteLine("ElementA");
        }
    }

    class ElementB : IElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Z { get; set; }

        public void powiedzCos()
        {
            Console.WriteLine("ElementB");
        }
    }

    public class Asset
    {
        public string Name;
        public virtual decimal Liability => 0;
    }

    public class Stock : Asset // inherits from Asset
    {
        public long SharesOwned;
    }

    public class House : Asset // inherits from Asset
    {
        public decimal Mortgage;
        public sealed override decimal Liability => base.Liability + Mortgage;  // "base" keyword access "Asset Liability" property nonvirtually
    }

    public class A { public int Counter = 1; }
    public class B : A { public new int Counter = 2; }  // hiding member (new modifier comunicates your intent to the compiler that isn't an accident

    public class Bunny
    {
        public string Name;
        public bool LikesCarrots, LikesHumans;
        public Bunny() { }
        public Bunny(string n) { Name = n; }
    }

    public class Baseclass
    {
        public int X;
        public Baseclass() { }
        public Baseclass(int x) { this.X = x; }
    }

    public class Subclass : Baseclass
    {
        public Subclass(int x) : base(x) { }
    }

    class Class1 { }    // Class1 is internal (default)
    public class Class2 { }
    class ClassA { int x; } // x is private
    class ClassB { internal int x; }

    public interface IEnumerator
    {
        bool MoveNext();
        object Current { get; }
    }

    internal class Countdown : IEnumerator
    {
        int count = 11;
        public bool MoveNext() => count-- > 0;
        public object Current => count;
    }

    public interface IUndoable { void Undo(); }
    public interface IRedoable : IUndoable { void Redo(); }

    interface I1 { void Foo(); }
    interface I2 { int Foo(); }
    public class Widget : I1, I2
    {
        public void Foo() // Implicit implementation
        {
            Console.Write("Widget's implementation of I1.Foo");
        }
        int I2.Foo() // Explicit implementation of I2.Foo
        {
            Console.Write("Widget's implementation of I2.Foo");
            return 42;
        }
    }

    public class TextBox : IUndoable
    {
        void IUndoable.Undo() => Console.WriteLine("TextBox.Undo");
    }

    public class RichTextBox : TextBox, IUndoable
    {
        public new void Undo() => Console.WriteLine("RichTextBox.Undo");
    }

    public class Animal { }
    public class Bear : Animal { }

    class YieldTestClass
    {
        public static void Execute()
        {
            foreach (int fib in Fibs(6))
            {
                Console.WriteLine(fib + " ");
            }
        }

        static IEnumerable<int> Fibs(int fibCount)
        {
            for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

        static IEnumerable<string> Foo()
        {
            yield return "One";
            yield return "Two";
            yield return "Three";
        }

        static IEnumerable<string> Foo(bool breakEarly)
        {
            yield return "One";
            yield return "Two";
            if (breakEarly) yield break;
            yield return "Three";
        }

        static IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
        {
            foreach (int x in sequence)
            {
                if ((x % 2) == 0)
                    yield return x;
            }
        }
    }

    struct Note
    {
        int value;
        public Note(int semitonesFromA)
        {
            value = semitonesFromA;
        }
        public static Note operator +(Note x, int semitones) => new Note(x.value + semitones);
        public static bool operator ==(Note n1, Note n2) => n1.value == n2.value;
        public static bool operator !=(Note n1, Note n2) => !(n1.value == n2.value);
        public override bool Equals(object otherNote)
        {
            if (!(otherNote is Note)) return false;
            return this == (Note)otherNote;
        }
        // value's hashcode will work for our own hashcode
        public override int GetHashCode() => value.GetHashCode();

        // Convert to hertz
        public static implicit operator double(Note x) => 440 * Math.Pow(2, (double)x.value / 12);

        // Convert from hertz (accurate to nearest semitone)
        public static explicit operator Note(double x) => new Note((int)(0.5 + 12 * (Math.Log(x / 440) / Math.Log(2))));
    }

    public static class StringHelper
    {
        public static bool IsCapitalized(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            return char.IsUpper(s[0]);
        }
        public static T First<T>(this IEnumerable<T> sequence)
        {
            foreach (T element in sequence) return element;
            throw new InvalidOperationException("No elements!");
        }
        public static string Pluralize(this string s) => s;
        public static string Capitalize(this string s) => s;
    }

    public class Purchase
    {
        public int CustomerID { get; }
        public string Description { get; }

        public Purchase(int customerId, string description)
        {
            CustomerID = customerId;
            Description = description;
        }
    }
}
