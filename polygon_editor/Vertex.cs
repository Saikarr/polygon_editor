using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon_editor
{
    public class Vertex
    {
        public Point Point;
        public bool IsControlPoint = false;
        public Vertex? ControlPointPrev = null;
        public Vertex? ControlPointNext = null;
        public Vertex Prev = null;
        public Vertex Next = null;
        public Relation? NextRelation = null;
        public Relation? PrevRelation = null;

        public int X { get => Point.X; set => this.Point.X = value; }
        public int Y { get => Point.Y; set => this.Point.Y = value; }

        public Vertex(int x, int y)
        {
            Point = new Point(x, y);
        }

        public double DistanceTo(Vertex other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public double DistanceTo(Point point)
        {
            return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
        }
    }
}
