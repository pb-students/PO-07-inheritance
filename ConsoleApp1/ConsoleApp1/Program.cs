
// Author: Daniel Pietrzeniuk

// Zadanie 7 - figury

using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();

            shapes.Add(new Tris("red", new Vector2[] { Vector2.Zero, new Vector2(2, 0), new Vector2(1, 2) }));

            Console.WriteLine("\nTrójkąt: " + shapes[0] + "\n");


            shapes.Add(new Square("blue", new Vector2(-5, 7), 3));

            Console.WriteLine("\nKwadrat przed zmianami: " + shapes[1] + "\n");

            shapes[1].Move(new Vector2(10, -2));
            shapes[1].color = "yellow";

            Console.WriteLine("\nKwadrat po zmianach: " + shapes[1] + "\n");

            Quad quad = (Quad)shapes[1];

            Console.WriteLine("\nJedna z lini kwadratu: " + quad.lines[0] + "\n");

            Console.WriteLine("\nZawartość kolekcji:");

            foreach (Shape shape in shapes)
                Console.WriteLine(shape);
        }
    }


    /// <summary>
    /// Klasa Kwadrat ze specyfikacji zadania
    /// </summary>
    class Square : Rectangle
    {
        public Square(string color, Vector2 pointLeftUp, int sideLenght)
            : base(color, pointLeftUp, pointLeftUp + new Vector2(sideLenght, -sideLenght))
        { Console.WriteLine("Skonstruowałem kwadrat -> " + this); }
    }

    /// <summary>
    /// Klasa Prostokąt ze specyfikacji zadania
    /// </summary>
    class Rectangle : Quad
    {
        public Rectangle(string color, Vector2 pointLeftUp, Vector2 pointRightDown)
            : base(color, new Vector2[] { pointLeftUp, new Vector2(pointLeftUp.x, pointRightDown.y), pointRightDown, new Vector2(pointRightDown.x, pointLeftUp.y) })
        { Console.WriteLine("Skonstruowałem prostokąt -> " + this); }
    }

    /// <summary>
    /// Klasa Czworokąt ze specyfikacji zadania
    /// </summary>
    class Quad : Shape
    {
        public Line[] lines = new Line[4];

        public Quad(string color, Vector2[] points) 
            : base(color)
        {
            if(points.Length < 4)
            {
                Console.WriteLine("Quads(): Too low amount of points passed! ({0})", points.Length);
                return;
            }

            lines[0] = new Line(points[0], points[1]);
            lines[1] = new Line(points[1], points[2]);
            lines[2] = new Line(points[2], points[3]);
            lines[3] = new Line(points[3], points[0]);

            Console.WriteLine("Skonstruowałem czworokąt -> " + this);
        }

        public override void Move(Vector2 vector)
        {
            // for() bo w foreach() nie można modyfikować bezpośrednio zmiennej enumeratora (czy jakoś tak to działa)
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] += vector;
            }
        }

        public override string ToString()
        {
            string geometryText = "";
            foreach (Line line in lines)
                geometryText += " " + line;

            return $"(Type: {this.GetType().Name}; Color: {color}; Geometry: {geometryText})";
        }
    }

    /// <summary>
    /// Klasa Trójkąt ze specyfikacji zadania
    /// </summary>
    class Tris : Shape
    {
        public Line[] lines = new Line[3];

        public Tris(string color, Vector2[] points) 
            : base(color)
        {
            lines[0] = new Line(points[0], points[1]);
            lines[1] = new Line(points[1], points[2]);
            lines[2] = new Line(points[2], points[0]);

            Console.WriteLine("Skonstruowałem trójkąt -> " + this);
        }

        public override void Move(Vector2 vector)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] += vector;
            }
        }

        public override string ToString()
        {
            string geometryText = "";
            foreach (Line line in lines)
                geometryText += " " + line;

            return $"(Type: {this.GetType().Name}; Color: {color}; Geometry:{geometryText})";
        }
    }

    /// <summary>
    /// Klasa Figura ze specyfikacji zadania
    /// </summary>
    class Shape
    {
        protected string color_;

        public string color { get { return color_; } set { color_ = value; } }

        public Shape(string color)
        {
            this.color = color;
            Console.WriteLine("Skonstruowałem figurę -> " + this);
        }
        
        public virtual void Move(Vector2 vector)
        {
            Console.WriteLine("It's just color holder, what do you expect? smh");
        }

        public override string ToString()
        {
            return $"(Type: {this.GetType()}; Color: {color})";
        }
    }

    struct Line
    {
        public Vector2 point0;
        public Vector2 point1;

        public Line(Vector2 point0, Vector2 point1)
        {
            this.point0 = point0;
            this.point1 = point1;
        }

        public override string ToString()
        {
            return $"<{point0},{point1}>";
        }
        public static Line operator +(Line line, Vector2 vector)
        {
            return new Line(line.point0 + vector, line.point1 + vector);
        }
    }

    /// <summary>
    /// Wiem że istnieje cała przestrzeń nazw w .NET z tego typu rzeczami ale jest mi nie potrzebna obecnie 
    /// (a tak naprawde to dopiero później spostrzegłem się że wymagam od tej struktury czegoś więcej niż przechowywanie 2 integerów)
    /// </summary>
    struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public override string ToString()
        {
            return $"[{x},{y}]";
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
    }
}
