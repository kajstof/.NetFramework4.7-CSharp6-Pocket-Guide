using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using R = System.Reflection;
using static System.Console;
using System.Net;
using System.IO;
using System.Xml;

namespace CSharp6PocketReferenceBook
{
    class Program
    {
        static reaonly int legs = 8, eyes = 2;

        decimal currentPrice, sharesOwned;
        public decimal Worth
        {
            get { return currentPrice * sharesOwned; }
        }

        public decimal Worth2 => currentPrice * sharesOwned;
        public decimal CurrentPrice { get; set; } = 123;
        public int Maximum { get; } = 999;
        private decimal x;
        public decimal X
        {
            get { return x; }
            private set { x = Math.Round(value, 2); }
        }

        string[] words = "The quick brown fox".Split();
        public string this[int wordNum] // indexer
        {
            get { return words[wordNum]; }
            set { words[wordNum] = value; }
        }

        string name = nameof(StringBuilder) + "." + nameof(StringBuilder.Length);

        //public string this[int wordNum] => words[wordNum];
        public const string Message = "Hello World";

        static void Main(string[] args)
        {
            // Simple tests
            SimpleTests();
            // Miscellous test
            MiscTest();
            // Class with keyword name tests
            ClassWithKeywordNameTest();
            // Number parsing tests
            parseNumbersTest();
            // Checked / unchecked tests
            checkedUncheckedTest();
            // Functions tests
            functionTests();
            // Display parameters for embedded types
            TypesTest();
            StringTest();
            // Classes tests
            PointAndLineClassesTest();
            ElementAandElementBclassesTest();
            // Collections tests
            CollectionsTests();
            // Keyboard press tests
            KeypressedTests();
            // Files tests
            FilesTests();
            // Class tests
            ClassTests();
            // Interface tests
            InterfaceTests();
            // Generic types tests
            GenericTypesTests();
            // Delegates tests
            DelegatesTests();
            // Events tests
            EventsTests();
            // Lambda and Anonymous Methods tests
            LambdaAndAnonymousMethods();
            // Exceptions tests
            ExceptionTest();
            // Enumeration and Iterators tests
            EnumerationAndIteratorsTests();
            // Nullable types
            NullableTypesTests();
            // Operator overloading and Extension Method tests
            OperatorOverloadingExtensionMethodAndAnonymousTypesTests();
            // LinqTests
            LinqTests();

            // Ending program
            Console.WriteLine("== END ==============================================================");
            Console.ReadKey();
        }

        private static void SimpleTests()
        {
            Write("");  // using static System.Console
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            int ht = -50;
            Console.WriteLine($"ht: {ht}\t+ht: {+ht}\t-ht: {-ht}");
            int a = 100;
            a += 100;
            string[] x = { "Test", "Test2" };
            x[0] = x[0].Trim().ToUpper() + " liczba: " + a.ToString();

            foreach (var arr in x)
            {
                foreach (var grr in arr)
                {
                    Console.Write(grr);
                }
                Console.WriteLine();
            }
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            char[] vowels2 = { 'a', 'e', 'i', 'o', 'u' };
            for (int i = 0; i < vowels.Length; i++)
            {
                Console.Write(vowels[i]);
            }
            int[,] matrix = new int[,]
            {
                {0,1,2},
                {3,4,5},
                {6,7,8}
            };

            Foo(new char[] { 'a', 'e', 'i', 'o', 'u' });
            Foo(new[] { 'a', 'e', 'i', 'o', 'u' });

            Console.WriteLine();
            Console.WriteLine("Pi number = {0:.00000}", Math.PI);
            Console.WriteLine($"Some value in hex {155:X2}XXX");
            Console.WriteLine($"Some value in hex {155,-20:X4}XXX");
            Console.WriteLine($"Some value in hex {155,-20:X6}XXX");
            Console.WriteLine($"Some value in hex {141255:X}XXX");
            Console.WriteLine(1.0);
            Console.WriteLine("=====================================================================");
        }

        private static void Foo(char[] data) { }

        public enum BorderSide : byte { Left = 1, Right, Top = 10, Bottom }

        [Flags]
        public enum BorderSides
        {
            None = 0,
            Left = 1, Right = 2, Top = 4, Bottom = 8,
            LeftRight = Left | Right,
            TopBottom = Top | Bottom,
            All = LeftRight | TopBottom
        }

        private static void MiscTest()
        {
            string s1 = null;
            string s2 = s1 ?? "nothing"; // s2 evaluates to "nothing"
            System.Text.StringBuilder sb = null;
            string s = sb?.ToString().ToUpper(); // No error
            // Equivalent: string s = (sb == null ? null : sb.ToString());
            // x?.y?.z
            int? length = sb?.ToString().Length; // OK : int? can be null
            s = sb?.ToString() ?? "nothing"; // s evaluates to "nothing"
            foreach (char c in "beer")
                Console.WriteLine(c + " "); // b e e r
            System.Security.Cryptography.RSA rsa = System.Security.Cryptography.RSA.Create();

            int x = 9;
            object obj = x;     // Box the int
            int y = (int)obj;   // Unbox the int

            object obj2 = 9; // 9 is inferred to be of type int
            //long x2 = (long)obj2; // InvalidCastException
            int x2 = (int)obj2; // OK
            object obj3 = 3.5;  // 3.5 inferred to be type double
            int x3 = (int)(double)obj3;   // x3 is now 3

            //int x4 = "5";    // Fail because the compiler enforces static typing
            object y4 = "5";
            //int z4 = (int)y4; // Runtime error, downcast failed

            // System.Type - GetType Method and typeof Operator
            // - GetType - runtime, on instance
            // - typeof - statically at compile time
            int x5 = 3;
            Console.WriteLine(x5.GetType().Name); // Int32
            Console.WriteLine(typeof(int).Name); // Int32
            Console.WriteLine(x5.GetType().FullName); // System.Int32
            Console.WriteLine(x5.GetType() == typeof(int)); // True
            Console.WriteLine();

            // Enum
            BorderSide topSide = BorderSide.Top;
            bool isTop = (topSide == BorderSide.Top);
            int en = (int)BorderSide.Left;
            BorderSide side = (BorderSide)en;
            bool leftOrRight = (int)side <= 2;

            BorderSides leftRight = BorderSides.Left | BorderSides.Right;
            if ((leftRight & BorderSides.Left) != 0)
                Console.WriteLine("Includes Left");     // Includes Left

            string formatted = leftRight.ToString();    // "Left, Right"

            BorderSides sx = BorderSides.Left;
            sx |= BorderSides.Right;
            Console.WriteLine(sx == leftRight);  // True

            Console.WriteLine("=====================================================================");
        }

        private static void ClassWithKeywordNameTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            @class xx = new @class(10);
            Console.WriteLine(xx.X);
            Console.WriteLine("=====================================================================");
        }

        private static void parseNumbersTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            int i;
            int.TryParse("-109", out i);
            Console.WriteLine(i);
            int? y = null;
            if (y == null)
            {
                Console.WriteLine("zmienna y jest nullem!");
            }

            Console.Write("Napisz jakiś tekst zawierający daty: ");
            string textToParse = Console.ReadLine();

            Regex reg = new Regex(@"\d+\.\d+\.\d{2,4}");
            MatchCollection matches = reg.Matches(textToParse);

            Console.WriteLine("Wyodrebnione daty: ");
            foreach (Match m in matches)
            {
                Console.WriteLine("Found {0} at index {1}", m.Value, m.Index);
            }
        }

        private static void checkedUncheckedTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("- checked");
            checked
            {
                int i = int.MaxValue;
                try
                {
                    i++;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(i == int.MinValue);
            }
            Console.WriteLine("- unchecked");
            unchecked
            {
                int i = int.MaxValue;
                try
                {
                    i++;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(i == int.MinValue);
            }
            Console.WriteLine("=====================================================================");
        }

        private static void functionTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            int par1 = 10, par2 = 20, par3 = 30, par4 = 40, par5 = 50;
            string[] arrayPar = new string[] { par1.ToString(), par2.ToString(), par3.ToString(), par4.ToString(), par5.ToString() };
            int? returnPar = null;
            Console.WriteLine("a:{0}\tb:{1}\tc:{2}\td:{3}\te:{4}\treturn:{5}", par1, par2, par3, par4, par5, returnPar == null);
            returnPar = someFnWithParamsAndNullableReturn(10, 20, ref par3, out par1, arrayPar);
            //returnPar = someFnWithParamsAndNullableReturn(y: 20, x: 10, z: ref par3, ooo: out par1, rest: arrayPar);
            Console.WriteLine("a:{0}\tb:{1}\tc:{2}\td:{3}\te:{4}\treturn:{5}", par1, par2, par3, par4, par5, returnPar);
            returnPar = someFnWithParamsAndNullableReturn(par1, par2, ref par5, out par1, par3.ToString(), "1000");
            Console.WriteLine("a:{0}\tb:{1}\tc:{2}\td:{3}\te:{4}\treturn:{5}", par1, par2, par3, par4, par5, returnPar);
            returnPar = someFnWithParamsAndNullableReturn(par1, par2, ref par5, out par1);
            Console.WriteLine("a:{0}\tb:{1}\tc:{2}\td:{3}\te:{4}\treturn:{5}", par1, par2, par3, par4, par5, returnPar == null);
            Console.WriteLine("=====================================================================");
        }

        private static void TypesTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine(1.0.GetType());
            Console.WriteLine(1E06.GetType());
            Console.WriteLine(1.GetType());
            Console.WriteLine(0xF0000000.GetType());
            Console.WriteLine(0x100000000.GetType());
            Console.WriteLine('c'.GetType());
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"byte.MinValue = {byte.MinValue}");
            Console.WriteLine($"byte.MaxValue = {byte.MaxValue}");
            Console.WriteLine($"sbyte.MinValue = {sbyte.MinValue}");
            Console.WriteLine($"sbyte.MaxValue = {sbyte.MaxValue}");
            Console.WriteLine($"ushort.MinValue = {ushort.MinValue}");
            Console.WriteLine($"ushort.MaxValue = {ushort.MaxValue}");
            Console.WriteLine($"short.MinValue = {short.MinValue}");
            Console.WriteLine($"short.MaxValue = {short.MaxValue}");
            Console.WriteLine($"uint.MinValue = {uint.MinValue}");
            Console.WriteLine($"uint.MaxValue = {uint.MaxValue}");
            Console.WriteLine($"int.MinValue = {int.MinValue}");
            Console.WriteLine($"int.MaxValue = {int.MaxValue}");
            Console.WriteLine($"ulong.MinValue = {ulong.MinValue}");
            Console.WriteLine($"ulong.MaxValue = {ulong.MaxValue}");
            Console.WriteLine($"long.MinValue = {long.MinValue}");
            Console.WriteLine($"long.MaxValue = {long.MaxValue}");
            Console.WriteLine($"float.MinValue = {float.MinValue}");
            Console.WriteLine($"float.MaxValue = {float.MaxValue}");
            Console.WriteLine($"double.MinValue = {double.MinValue}");
            Console.WriteLine($"double.MaxValue = {double.MaxValue}");
            Console.WriteLine($"double.Epsilon = {double.Epsilon}");
            Console.WriteLine($"double.NaN = {double.NaN}");
            Console.WriteLine($"double.NegativeInfinity = {double.NegativeInfinity}");
            Console.WriteLine($"double.PositiveInfinity = {double.PositiveInfinity}");
            Console.WriteLine($"decimal.MinValue = {decimal.MinValue}");
            Console.WriteLine($"decimal.MaxValue = {decimal.MaxValue}");
            Console.WriteLine($"decimal.MinusOne = {decimal.MinusOne}");
            Console.WriteLine($"decimal.One = {decimal.One}");
            Console.WriteLine($"decimal.Zero = {decimal.Zero}");
            Console.WriteLine("=====================================================================");
        }

        private static void StringTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            int y = 5;
            string str;
            str = @"d:\OneDrive\";
            str = $@"This spans {
                y} lines";
            Console.WriteLine("Abc");
            string[] x = { "Test", "Test2" };
            string[] z = new string[] { "Test", "Test2" };
            Console.WriteLine($"Compare \"Poland\".CompareTo(\"Poland\")\t{"Poland".CompareTo("Poland")}");
            Console.WriteLine($"Compare \"Poland\".CompareTo(\"Germany\")\t{"Poland".CompareTo("Germany")}");
            Console.WriteLine($"Compare \"Poland\".CompareTo(\"Zimbabwe\")\t{"Poland".CompareTo("Zimbabwe")}");
            string ex = "Some example of string type   ";
            Console.WriteLine($"ex.Substring(6, 10): \"{ex.Substring(6, 10)}\"");
            Console.WriteLine($"ex.Insert(2, \"XXX\"): \"{ex.Insert(2, "XXX")}\"");
            Console.WriteLine($"ex.Remove(5, 7): \"{ex.Remove(5, 7)}\"");
            Console.WriteLine($"ex.PadLeft(30, '+'): \"{ex.PadLeft(30, '+')}\"");
            Console.WriteLine($"ex.Trim(): \"{ex.Trim()}\"");
            Console.WriteLine($"ex.ToUpper(): \"{ ex.ToUpper()}\"");
            Console.WriteLine($"ex.ToLower(): \"{ ex.ToLower()}\"");
            Console.WriteLine($"ex.Split(' '): \"{ex.Split(' ')}\"");
        }

        private static void PointAndLineClassesTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Point pt = new Point(10, 44);
            Console.WriteLine(pt);
            Point ptx = null;
            Console.WriteLine(ptx);
            ptx = pt;
            ptx.X = Math.Abs(-358);

            Point[] points = new Point[10];
            points[3] = new Point(10, 20);
            points[4] = new Point(99, 88);
            points[1] = new Point(0, 0);
            points[7] = new Point(1, 1);
            points[3] = null;

            int i = 0;
            foreach (Point point in points)
            {
                Console.Write($"points[{i}] = ");
                if (point == null)
                {
                    Console.Write("Jestem sobie null'em");
                }
                Console.WriteLine($"{point?.X}\t{point?.Y}");
                i++;
            }
            Point pt2 = new Point(40, 44);
            Line ln = new Line(pt, pt2);
        }

        private static void ElementAandElementBclassesTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            ElementA[] ela = new ElementA[10];
            ElementB[] elb = new ElementB[20];
            for (uint i = 0; i < ela.GetLength(0); i++)
            {
                ela[i] = new ElementA();
                ela[i].X = (int)i;
                ela[i].Y = (int)i * 2;
                ela[i].Z = i.ToString() + "!!!";
            }
            for (uint i = 0; i < elb.Length; i++)
            {
                elb[i] = new ElementB();
                elb[i].X = (int)i + 100;
                elb[i].Y = (int)i + 1000;
                elb[i].Z = (i + 10000).ToString() + "???";
            }

            foreach (var ea in ela)
            {
                Console.WriteLine("Ela.X = {0}\tEla.Y = {1}\tEla.Z = {2}", ea.X, ea.Y, ea.Z);
            }
            foreach (var eb in elb)
            {
                Console.WriteLine("Elb.X = {0}\tElb.Y = {1}\tElb.Z = {2}", eb.X, eb.Y, eb.Z);
            }
        }

        private static void CollectionsTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            ArrayList arlist = new ArrayList();
            arlist.Add(50);
            arlist.Add("sruu");
            arlist.Add("bleeee");
            ElementA ela = new ElementA();
            arlist.Add(ela);
            arlist.Add(ela);
            arlist.Add(new ElementB());
            arlist.Add(new Point(4, 3));
            arlist.Add(new Line(new Point(1, 2), new Point(3, 4)));
            arlist.Add(1.4f);
            arlist.Add(140.52m);
            arlist.Add(-100);
            arlist.Add(50);
            arlist.Add("sruu");
            arlist.Add("bleeee");
            arlist.Add(new ElementA());
            arlist.Add(new ElementB());
            arlist.Add(new Point(114, 344));
            Console.WriteLine(arlist[4]);

            foreach (object tmp in arlist)
            {
                if (tmp is Point)
                    Console.WriteLine($"X:{((Point)tmp).X}\tY:{((Point)tmp).Y}");
            }
            Console.WriteLine("arlist capacity: {0}\tarlist count: {1}", arlist.Capacity, arlist.Count);

            System.Collections.Generic.List<ElementA> list = new System.Collections.Generic.List<ElementA>();
            foreach (ElementA tmp in list)
            {
                Console.WriteLine(tmp.X);
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", "Krzysztof");
            dict.Add("surname", "Krysiak");
            dict.Add("age", 10.ToString());

            Console.WriteLine($"Dictionary first key: {dict.First().Key} => last value: {dict.Last().Value}");
            foreach (KeyValuePair<string, string> tmp in dict)
            {
                Console.WriteLine($"{tmp.Key}: {tmp.Value.ToUpper()}");
            }

            SortedDictionary<string, string> sdict = new SortedDictionary<string, string>(dict);
            Console.WriteLine($"Sorted Dictionary first key: {sdict.First().Key} => last value: {sdict.Last().Value}");
            foreach (KeyValuePair<string, string> tmp in sdict)
            {
                Console.WriteLine($"{tmp.Key}: {tmp.Value}");
            }
        }

        static uint counter = 0;

        private static void KeypressedTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            counter++;
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
                Console.Write(" --- ");
                Console.Write(counter);
                Console.Write(". You pressed ");
                if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
                if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
                Console.WriteLine(cki.Key.ToString());
            } while (cki.Key != ConsoleKey.Escape);
            Console.WriteLine("=====================================================================");
        }

        private static int? someFnWithParamsAndNullableReturn(int x, int y, ref int z, out int ooo, params string[] rest)
        {
            ooo = x + y;
            z = x + y + z;
            int? tmp = null;
            if (rest != null)
            {
                tmp = 0;
            }
            foreach (string str in rest)
            {
                int tmp22;
                int.TryParse(str, out tmp22);
                tmp += tmp22;
            }

            return tmp;
        }

        private static void FilesTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("This programs lists all the files in the directory");
            System.IO.FileInfo dir = new System.IO.FileInfo("C:\\");
            Console.WriteLine("=====================================================================");
        }

        private static void ClassTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Stock msft = new Stock
            {
                Name = "MSFT",
                SharesOwned = 1000
            };
            Console.WriteLine(msft.Name); // MSFT
            Console.WriteLine(msft.SharesOwned); // 1000
            House mansion = new House
            {
                Name = "Mansion",
                Mortgage = 250000
            };
            Console.WriteLine(mansion.Name); // Mansion
            Console.WriteLine(mansion.Mortgage); // 250000

            Asset a = msft; // Upcast

            Console.WriteLine(a == msft); // True
            Console.WriteLine(a.Name); // OK
            //Console.WriteLine(a.SharesOwned); // Error

            Stock s = (Stock)a; // Downcast
            Console.WriteLine(s.SharesOwned); // <No error>
            Console.WriteLine(s == a); // True
            Console.WriteLine(s == msft); // True

            House h = new House();
            Asset aa = h; // Upcast always succeeds
            //Stock sa = (Stock)a; // Downcast fails: a is not a Stock

            Asset ab = new Asset();
            Stock sb = a as Stock; // s is null; no exception thrown

            if (a is Stock) Console.Write(((Stock)a).SharesOwned);

            House mansion2 = new House
            {
                Name = "Mansion",
                Mortgage = 250000
            };
            Asset a2 = mansion2;
            Console.WriteLine(mansion2.Liability); // 250000
            Console.WriteLine(a2.Liability); // 250000

            House h3 = new House();
            Foo(h); // Calls Foo(House)
            Asset a3 = new House();
            Foo(a); // Calls Foo(Asset)

            Wine wine = new Wine(10.3m, new DateTime(1986, 3, 2));

            Stack stack = new Stack();
            stack.Push("sausage");
            string s4 = (string)stack.Pop(); // Downcast
            Console.WriteLine(s4); // sausage
            Console.WriteLine("=====================================================================");
        }

        private static void Foo(Asset a) { }
        private static void Foo(House h) { }

        private static void InterfaceTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            IEnumerator e = new Countdown();
            while (e.MoveNext())
                Console.Write(e.Current);
            Console.WriteLine("\r\n");

            Widget w = new Widget();
            w.Foo(); // Widget's implementation of I1.Foo
            ((I1)w).Foo(); // Widget's implementation of I1.Foo
            ((I2)w).Foo(); // Widget's implementation of I2.Foo

            RichTextBox r = new RichTextBox();
            r.Undo(); // RichTextBox.Undo
            ((IUndoable)r).Undo(); // RichTextBox.Undo
            Console.WriteLine("=====================================================================");
        }

        private static void GenericTypesTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Stack<int> stack = new Stack<int>();
            stack.Push(5);
            stack.Push(10);
            int x = stack.Pop();
            int y = stack.Pop();
            int xx = 5, yy = 10;
            Generics.Swap(b: ref yy, a: ref xx);

            Type g1 = typeof(G<>);
            Type g2 = typeof(G<,>);
            Console.WriteLine(g2.GetGenericArguments().Count());
            Type g3 = typeof(G<int, int>);
            Console.WriteLine(g3.GetGenericArguments().Count());

            Console.WriteLine(++Bob<int>.Count);
            Console.WriteLine(++Bob<int>.Count);
            Console.WriteLine(++Bob<string>.Count);
            Console.WriteLine(++Bob<object>.Count);

            // Covariance
            {
                // Assuming that Bear subclasses Animal:
                var bears = new Stack<Bear>();
                bears.Push(new Bear());
                // Because bears implements IPoppable<Bear>, we can convert it to IPoppable<Animal>:
                IPoppable<Animal> animals = bears;  // Legal
                Animal a = animals.Pop();
            }
            // Contravariance
            {
                IPushable<Animal> animals = new Stack<Animal>();
                IPushable<Bear> bears = animals;    // Legal
                bears.Push(new Bear());
            }
            Console.WriteLine("=====================================================================");
        }

        private static void DelegatesTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            DelegatesAndEventsTestsClass.DelegatesTests();
            Console.WriteLine("=====================================================================");
        }

        private static void EventsTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            DelegatesAndEventsTestsClass.EventsTests();
            Console.WriteLine("=====================================================================");
        }

        delegate int Transformer(int i);
        public event EventHandler Clicked = delegate { };
        private static void LambdaAndAnonymousMethods()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Func<int, int> sqr = x => x * x;
            Func<int, int> sqr2 = (int x) => x * x;
            Func<string, string, int> totalLength = (s1, s2) => s1.Length + s2.Length;
            int total = totalLength("Hello", "world");
            Console.WriteLine($"Total Length (lambda expression): {total}");
            int factor = 2;
            Func<int, int> multiplier = n => n * factor;
            Console.WriteLine("Factor multiplier: {0}", multiplier(3));
            Func<int> natural = Natural();
            Console.WriteLine("natural() = {0}", natural());        // 0
            Console.WriteLine("natural() = {0}", natural());        // 1
            Action[] actions = new Action[6];
            for (int i = 0; i < 3; i++)
            {
                actions[i] = () => Console.Write(i);
            }
            for (int i = 3; i < 6; i++)
            {
                int loopScopedi = i;
                actions[i] = () => Console.Write(loopScopedi);
            }
            foreach (Action a in actions) { a(); }
            Console.WriteLine();
            Transformer sqrdelegate = delegate (int x) { return x * x; };
            Transformer sqrlambda1 = (int x) => { return x * x; };
            Transformer sqrlambda2 = x => x * x;
            //Clicked += delegate { Console.WriteLine("Clicked"); };
            Console.WriteLine("Square delegate: {0}", sqrdelegate(3));
            Console.WriteLine("=====================================================================");
        }

        static Func<int> Natural()
        {
            int seed = 0;
            return () => seed++;    // Retruns a closure
        }

        private static void ExceptionTest()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            int x = 3, y = 0;
            try { Console.WriteLine(x / y); }
            catch (DivideByZeroException ex) { Console.WriteLine("y cannot be zero"); }
            catch (IndexOutOfRangeException ex) { }
            catch (FormatException ex) { }
            catch (OverflowException ex) { }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout) { }
            catch { }
            ReadFile();
            try { }
            catch (Exception ex)
            {
                // Log error
                throw;          // Rethrow same exception
            }
            try
            {
                // Parse a date of birth from XML element data
            }
            catch (FormatException ex)
            {
                throw new XmlException("Invalid date of birth", ex);
            }
            // Key Properties of System.Exception: StackTrace, Message, InnerException
            // Common Exception Types: System. ArgumentException, ArgumentNullException, ArgumentOutOfRangeException, InvalidOperationException, NotSupportedException, NotImplementedException, ObjectDisposedException
            // Code Contracts eliminate the need for ArgumentException (and its subclasses). Code contracts are covered in Chapter 13 of C# 6.0 in a Nutshell.
            Console.WriteLine("=====================================================================");
        }

        static void ReadFile()
        {
            StreamReader reader = null;     // In System.IO namespace
            try
            {
                reader = File.OpenText("file.txt");
                if (reader.EndOfStream) return;
                Console.WriteLine(reader.ReadToEnd());
            }
            finally
            {
                if (reader != null) reader.Dispose();
            }

            using (StreamReader reader2 = File.OpenText("file.txt")) { }
            {
                StreamReader reader2 = File.OpenText("file.txt");
                try { }
                finally { if (reader2 != null) ((IDisposable)reader2).Dispose(); }
            }
        }

        static void Display(string name)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }
            Console.WriteLine(name);
        }

        private static void EnumerationAndIteratorsTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            // High-level way to iterate
            foreach (char c in "beer") Console.WriteLine(c);
            // Low-level way to iterate
            using (var enumerator = "beer".GetEnumerator())
                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    Console.WriteLine(element);
                }

            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>() { 1, 2, 3 };

            var dict = new Dictionary<int, string>()
            {
                { 5,"five" },
                { 10, "ten" }
            };
            var dict2 = new Dictionary<int, string>()
            {
                [3] = "three",
                [10] = "ten"
            };
            Console.WriteLine("=====================================================================");
        }

        private static void NullableTypesTests()
        {
            Console.WriteLine("--- {0} ---", System.Reflection.MethodBase.GetCurrentMethod().Name);
            int? i = null;
            Console.WriteLine(i == null);
            // Translates to
            Nullable<int> j = new Nullable<int>();
            Console.WriteLine(j.HasValue);
            object o = "string";
            int? x = o as int?;
            Console.WriteLine(x.HasValue);
            Console.WriteLine(null == null);                // True
            Console.WriteLine((bool?)null == (bool?)null);  // True
            bool? n = null, f = false, t = true;
            Console.WriteLine("Null | Null  = {0}", n | n);   // (null)
            Console.WriteLine("Null | False = {0}", n | f);   // (null)
            Console.WriteLine("Null | True  = {0}", n | t);   // True
            Console.WriteLine("Null & Null  = {0}", n & n);   // (null)
            Console.WriteLine("Null & False = {0}", n & f);   // False
            Console.WriteLine("Null & True  = {0}", n & t);   // (null)
            int? z = null;
            int y = z ?? 5;         // y is 5
            int? a = null, b = null, c = 123;
            Console.WriteLine("a (null) ?? b (null) ?? c (123) = {0}", a ?? b ?? c);     // 123
            System.Text.StringBuilder sb = null;
            int? length = sb?.ToString().Length;
            int length2 = sb?.ToString().Length ?? 0;
            Console.WriteLine("=====================================================================");
        }

        private static void OperatorOverloadingExtensionMethodAndAnonymousTypesTests()
        {
            Note B = new Note(2);
            Note CSharp = B + 2;
            CSharp += 2;
            Console.WriteLine("Perth".IsCapitalized());
            Console.WriteLine(StringHelper.IsCapitalized("Perth"));
            Console.WriteLine("Seattle".First());       // S
            string x = "sausage".Pluralize().Capitalize();
            string y = StringHelper.Capitalize(StringHelper.Pluralize("sausage"));
            var dude = new { Name = "Bob", Age = 1 };
            int Age = 1;
            var dude2 = new { Name = "Bob", Age };
            var dudes = new[]
            {
                new { Name = "Bob", Age=30 },
                new { Name="Mary", Age=40 }
            };

        }

        private static void LinqTests()
        {
            string[] names = { "Tom", "Dick", "Harry" };
            IEnumerable<string> filteredNames = System.Linq.Enumerable.Where(names, n => n.Length >= 4);
            foreach (string n in filteredNames) Console.Write(n + "|");
            IEnumerable<string> filteredNames2 = names.Where(n => n.Length >= 4);
            // Projecting
            IEnumerable<string> blee = names.Select(n => n.ToUpper());
            var query = names.Select(n => new
            {
                Name = n,
                Length = n.Length
            });
            // Take and Skip
            int[] numbers = { 10, 9, 8, 7, 6 };
            IEnumerable<int> firstThree = numbers.Take(3);      // firstThree is { 10, 9 , 8 }
            IEnumerable<int> lastTwo = numbers.Skip(3);
            // Element operators
            int firstNumber = numbers.First();                      // 10
            int lastNumber = numbers.Last();                        // 6
            int secondNumber = numbers.ElementAt(2);                // 8
            int firstOddNumber = numbers.First(n => n % 2 == 1);    // 9
            // Aggregation operators
            int count = numbers.Count();                            // 5
            int min = numbers.Min();                                // 6
            int max = numbers.Max();                                // 10
            double avg = numbers.Average();                         // 8
            int evenNums = numbers.Count(n => n % 2 == 0);          // 3
            int maxRemainderAfterDivBy5 = numbers.Max(n => n % 5);  // 4
            // Quantitfiers
            bool hasTheNumberNine = numbers.Contains(9);            // true
            bool hasMoreThanZeroElements = numbers.Any();           // true
            bool hasOddNum = numbers.Any(n => n % 2 == 1);          // true
            bool allOddNum = numbers.All(n => n % 2 == 1);          // false
            // Set operators
            int[] seq1 = { 1, 2, 3 }, seq2 = { 3, 4, 5 };
            IEnumerable<int> concat = seq1.Concat(seq2);            // { 1, 2, 3, 3, 4, 5 }
            IEnumerable<int> union = seq1.Union(seq2);              // { 1, 2, 3, 4, 5 }
            IEnumerable<int> commonality = seq1.Intersect(seq2);    // { 3 }
            IEnumerable<int> difference1 = seq1.Except(seq2);       // { 1, 2 }
            IEnumerable<int> difference2 = seq2.Except(seq1);       // { 4, 5 }
            // Deferred Execution
            var numbers2 = new System.Collections.Generic.List<int> { 1 };
            IEnumerable<int> query2 = numbers.Select(n => n * 10);
            numbers2.Add(2);                                        // Sneak in an extra element
            // Deferred / Lazy (evaluation)
            foreach (int n in query2)
            {
                Console.Write(n + "|");
            }
            // Cache/freeze - avoid reexecution
            var numbers3 = new System.Collections.Generic.List<int>() { 1, 2 };
            System.Collections.Generic.List<int> timesTen = numbers.Select(n => n * 10).ToList();   // Executes immediately into a List<int>
            numbers3.Clear();
            Console.WriteLine(timesTen.Count);                      // Still 2
            names.Where(n => n.Length == names.Min(n2 => n2.Length));

            // Chaining Query Operators
            string[] names3 = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            IEnumerable<string> query3 = names3
                .Where(n => n.Contains("a"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToUpper());
            foreach (string name in query3)
                Console.Write(name + "|");                                              // Result: JAY|MARY|HARRY

            // Query Expressions
            IEnumerable<string> query4 = from n in names3
                                         where n.Contains("a")
                                         orderby n.Length
                                         select n.ToUpper();

            IEnumerable<string> query5 = from n in names3
                                         where n.Length == names3.Min(n2 => n2.Length)
                                         select n;

            // The Let keyword
            IEnumerable<string> query6 = from n in names3
                                         let vowelless = Regex.Replace(n, "[aeiou]", "")
                                         where vowelless.Length > 2
                                         orderby vowelless
                                         select n + " - " + vowelless;
            // Translates to
            IEnumerable<string> query6a = names3
                .Select(n => new
                {
                    n = n,
                    vowelless = Regex.Replace(n, "[aeiou]", "")
                })
                .Where(temp0 => (temp0.vowelless.Length > 2))
                .OrderBy(temp0 => temp0.vowelless)
                .Select(temp0 => ((temp0.n + " - ") + temp0.vowelless));

            // Query Continuations
            IEnumerable<string> query7 = from c in "The quick brown tiger".Split()
                                         select c.ToUpper() into upper
                                         where upper.StartsWith("T")
                                         select upper;                                  // Result: "THE", "TIGER"
            // Translates to
            IEnumerable<string> query7a = "The quick brown tiger".Split()
                .Select(c => c.ToUpper())
                .Where(upper => upper.StartsWith("T"));

            // Multiple Generators
            int[] numbersA = { 1, 2, 3 };
            string[] lettersA = { "a", "b" };
            IEnumerable<string> query8 = from n in numbersA
                                         from l in lettersA
                                         select n.ToString() + l;
            IEnumerable<string> query8a = numbers.SelectMany(
                n => lettersA,
                (n, l) => (n.ToString() + l));

            string[] players = { "Tom", "Jay", "Mary" };
            IEnumerable<string> query9 = from name1 in players
                                         from name2 in players
                                         where name1.CompareTo(name2) < 0
                                         orderby name1, name2
                                         select name1 + " vs " + name2;                 // Result: { "Jay vs Mary", "Jay vs Tom", "Mary vs Tom" }

            string[] fullNames = { "Anne Williams", "John Fred Smith", "Sue Green" };
            IEnumerable<string> query10 = from fullName in fullNames
                                          from name in fullName.Split()
                                          select name + " come from " + fullName;

            // Joining
            var customers = new[]
            {
                new { ID = 1, Name = "Tom" },
                new { ID = 2, Name = "Dick" },
                new { ID = 3, Name = "Harry" },
            };
            var purchases = new[]
            {
                new { CustomerID = 1, Product = "House" },
                new { CustomerID = 2, Product = "Boat" },
                new { CustomerID = 3, Product = "Car" },
                new { CustomerID = 4, Product = "Holiday" },
            };
            IEnumerable<string> query11 = from c in customers
                                          join p in purchases on c.ID equals p.CustomerID
                                          select c.Name + " bought a " + p.Product;
            // Translates to
            IEnumerable<string> query11a = customers
                .Join(purchases, c => c.ID, p => p.CustomerID, (c, p) => c.Name + " bought a " + p.Product);

            // GroupJoin
            Purchase[] purchases2 = new Purchase[]
            {
                new Purchase(1, "House"),
                new Purchase(2, "Boat"),
                new Purchase(3, "Car"),
                new Purchase(4, "Holiday")
            };

            IEnumerable<IEnumerable<Purchase>> query12 = from c in customers
                                                         join p in purchases2 on c.ID equals p.CustomerID into custPurchases
                                                         select custPurchases;            // custPurchases is a sequence
            foreach (IEnumerable<Purchase> purchaseSequence in query12)
                foreach (Purchase p in purchaseSequence)
                    Console.WriteLine(p.Description);
            var query13 = from c in customers
                          join p in purchases2 on c.ID equals p.CustomerID into custPurchases
                          select new { CustName = c.Name, custPurchases };
            var query14 = from c in customers
                          select new
                          {
                              CustName = c.Name,
                              custPurchases = purchases2.Where(p => c.ID == p.CustomerID)
                          };

            // Zip
            int[] numbersZip = { 3, 5, 7 };
            string[] words = { "three", "five", "seven", "ignored" };
            IEnumerable<string> zip =
            numbersZip.Zip(words, (n, w) => n + "=" + w);

            // Ordering
            IEnumerable<string> query15 = from n in names
                                          orderby n.Length, n
                                          select n;
            // Translates to
            IEnumerable<string> query15a = names.OrderBy(n => n.Length).ThenBy(n => n);

            IEnumerable<string> query16 = from n in names
                                          orderby n.Length descending, n
                                          select n;
            // Translates to
            IEnumerable<string> query16a = names.OrderByDescending(n => n.Length).ThenBy(n => n);

            // Grouping
            var query17 = from name in names
                          group name by name.Length;
            // Translates to
            IEnumerable<IGrouping<int, string>> query17a = names.GroupBy(name => names.Length);
            foreach (IGrouping<int, string> grouping in query17)
            {
                Console.WriteLine("\r\n Length=" + grouping.Key + ":");
                foreach (string name in grouping)
                    Console.WriteLine(" " + name);
            }
            var query18 = from name in names group name.ToUpper() by name.Length;
            // Translates to
            var query18a = names.GroupBy(
                name => name.Length,
                name => name.ToUpper());

            var query19 = from name in names
                          group name.ToUpper() by name.Length into grouping
                          orderby grouping.Key
                          select grouping;
            var query19b = from name in names
                           group name.ToUpper() by name.Length into grouping
                           where grouping.Count() == 2
                           select grouping;

            // OfType and Cast
            var classicList = new System.Collections.ArrayList();
            classicList.AddRange(new int[] { 3, 4, 5 });
            IEnumerable<int> sequence1 = classicList.Cast<int>();
            var aaa = from int x in classicList select x;
            // Translates to
            var bbb = from x in classicList.Cast<int>() select x;
        }

        public static IEnumerable<TSource> Cast<TSource>(IEnumerable source)
        {
            foreach (object element in source)
                yield return (TSource)element;
        }

        public static void Display(Asset asset)
        {
            System.Console.WriteLine(asset.Name);
        }
    }

    class Program2 { R.PropertyInfo p; }
}
