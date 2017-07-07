namespace HelloWorld
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            string s = string.Format("Point: x: {0}, y: {1}", this.X, this.Y);
            return s;
        }
    }

    class Line
    {
        private Point[] points;

        public Line(Point pt1, Point pt2)
        {
            points = new Point[2];
            points[0] = pt1;
            points[1] = pt2;
        }

        public override string ToString()
        {
            //string str = "Line with 2 points" + "\r\n" + " pt1 -> " + points[0].ToString() + "\r\n" + " pt2 -> " + points[1].ToString();
            //string str = String.Format("Line with 2 points\r\npt1 -> {0}\r\npt2 -> {1}", points[0].ToString(), points[1].ToString());
            string str = $"Line with 2 points\r\npt1-> {points[0].ToString()}\r\npt2-> {points[1].ToString()}";
            return str;
        }
    }
}