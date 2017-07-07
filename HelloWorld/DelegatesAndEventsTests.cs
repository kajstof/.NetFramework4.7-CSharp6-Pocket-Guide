using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class DelegatesAndEventsTestsClass
    {
        delegate int Transformer(int x);
        delegate T Transformer<T>(T arg);
        delegate void D1();
        delegate void D2();
        delegate object ObjectRetriever();
        delegate void StringAction(string s);
        delegate TResult Func<out TResult>();
        delegate void Action<in T>(T arg);

        public static void DelegatesTests()
        {
            Transformer t = Square;     // Create delegate instance
            Transformer t2 = new Transformer(Square);
            int result = t(3);          // Invoke delegate
            result = t2.Invoke(3);      // Invoke delegate

            int[] values = { 1, 2, 3 };
            Transform(values, Square);
            Transform2(values, Square);
            Transformer<double> s = Square;
            Console.WriteLine(s(3.3));

            D1 d1 = Method1;
            //D2 d2 = d1         // Compile-time error
            D2 d2 = new D2(d1);

            ObjectRetriever o = new ObjectRetriever(GetString);
            object result2 = o();
            Console.WriteLine(result2);

            StringAction sa = new StringAction(ActOnObject);
            sa("hello");

            Func<string> x = GetString;
            Func<object> y = x;
            Console.WriteLine(y);
            Action<object> x1 = ActOnObject;
            Action<string> y2 = x1;
        }

        static void Transform(int[] values, Transformer t)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
            }
        }

        public static void Transform2<T>(T[] values, Func<T, T> transformer)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = transformer(values[i]);
            }
        }

        static int Square(int x) => x * x;
        static double Square(double x) => x * x;
        static void Method1() { }
        private static string GetString() => "hello";
        private static void ActOnObject(object o) => Console.WriteLine(o);

        public static void EventsTests()
        {
            StockEventClass stock = new StockEventClass("PLN");
            Console.WriteLine("Without handlers");
            Console.WriteLine($"Price after creating object: {stock.Price}");
            stock.Price = 100;
            stock.Price += 50;
            stock.Price = 0;
            Console.WriteLine("With handlers");
            stock.PriceChanged += PriceChangedHandlerConsoleWrite;
            stock.PriceChanged += PriceChangedHandlerDbLogging;
            stock.PriceChanged += (oldPrice, newPrice) => Console.WriteLine("price changed of: {0}", newPrice - oldPrice);
            stock.Price = 100;
            stock.Price += 50;
            stock.Price = 0;
            StockStdEventPatternClass stock2 = new StockStdEventPatternClass("USD");
            stock2.Price = 27.10M;
            stock2.PriceChanged += Stock2_PriceChanged;
            stock2.Price = 37.55M;
        }

        private static void Stock2_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
            {
                Console.WriteLine("Alert, 10% price increase!");
            }
        }

        public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

        private static void PriceChangedHandlerConsoleWrite(decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine($"ConsoleWrite: Old price: {oldPrice}\tNew price: {newPrice}");
        }

        private static void PriceChangedHandlerDbLogging(decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine($"DatabaseLogging: INSERT RECORD({oldPrice}, {newPrice});");
        }

        public class StockEventClass
        {
            string symbol;
            decimal price;

            public event PriceChangedHandler PriceChanged;

            public StockEventClass(string symbol) { this.symbol = symbol; }

            public decimal Price
            {
                get => price;
                set
                {
                    if (price == value) { return; }
                    // Fire event if invocation list isn't empty:
                    if (PriceChanged != null)
                    {
                        PriceChanged(price, value);
                    }
                    price = value;
                }
            }
        }

        public class PriceChangedEventArgs : EventArgs
        {
            public readonly decimal LastPrice, NewPrice;
            public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
            {
                LastPrice = lastPrice;
                NewPrice = newPrice;
            }
        }

        public class StockStdEventPatternClass
        {
            string symbol;
            decimal price;
            public StockStdEventPatternClass(string symbol) { this.symbol = symbol; }

            public event EventHandler<PriceChangedEventArgs> PriceChanged;

            protected virtual void OnPriceChanged(PriceChangedEventArgs e)
            {
                if (PriceChanged != null) { PriceChanged(this, e); }
            }

            EventHandler PriceChangedNonGeneric;
            //EventHandler priceChangedNonGeneric;    // private delegate
            //public event EventHandler PriceChangedNonGeneric
            //{
            //    add { priceChangedNonGeneric += value; }
            //    remove { priceChangedNonGeneric -= value; }
            //}

            protected virtual void OnPriceChangedNonGeneric(EventArgs e)
            {
                PriceChangedNonGeneric?.Invoke(this, e);
            }

            public decimal Price
            {
                get { return price; }
                set
                {
                    if (price == value) { return; }
                    OnPriceChanged(new PriceChangedEventArgs(price, value));
                    OnPriceChangedNonGeneric(EventArgs.Empty);
                    price = value;
                }
            }
        }
    }
}
