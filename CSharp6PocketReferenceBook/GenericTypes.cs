using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp6PocketReferenceBook
{
    public class Stack<T> : IPoppable<T>, IPushable<T>
    {
        int position;
        T[] data = new T[100];
        public void Push(T obj) => data[position++] = obj;
        public T Pop() => data[--position];
        public T this[int index] { get { return data[index]; } }
    }

    class G { }
    class G<T> { }
    class G<T1, T2> { }

    class Generics
    {

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void Zap<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = default(T);
            }
        }

        public static void Initialize<T>(T[] array) where T : new()
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = new T();
        }
    }

    public class SpecialStack<T> : Stack<T> { }
    public class IntStack : Stack<int> { }
    public class List<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class KeyedList<T, TKey> : List<T> { }

    public interface IEquatable<T>
    {
        bool Equals(T obj);
    }

    public class Balloon : IEquatable<Balloon>
    {
        public bool Equals(Balloon b) { return true; }
    }

    class Foo<T> where T : IComparable<T> { }
    class Bar<T> where T : Bar<T> { }

    public class Bob<T> { public static int Count; }

    public interface IPushable<in T> { void Push(T obj); }
    public interface IPoppable<out T> { T Pop(); }
}
