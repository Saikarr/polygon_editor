using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon_editor
{
    class LengthRelation : Relation
    {
        double length;
        public LengthRelation(Vertex vertex1, Vertex vertex2, double length) : base(vertex1, vertex2)
        {
            this.length = length;
        }

        public override void Draw(PaintEventArgs e)
        {
            Point middle = new Point((Vertex1.X + Vertex2.X)/2, (Vertex1.Y + Vertex2.Y) / 2);
            e.Graphics.DrawString("L", new Font("Arial", 12), Brushes.Black, middle.X, middle.Y);
        }
        public override void UpdateRelation(Vertex movingVertex, Stack<(Relation, Vertex)> relationsStack)
        {
            Vertex otherVertex = movingVertex == Vertex1 ? Vertex2 : Vertex1;
            if (movingVertex.DistanceTo(otherVertex) != length)
            {
                double currentDistance = movingVertex.DistanceTo(otherVertex);
                double scale = length / currentDistance;

                otherVertex.X = (int)(movingVertex.X + (otherVertex.X - movingVertex.X) * scale);
                otherVertex.Y = (int)(movingVertex.Y + (otherVertex.Y - movingVertex.Y) * scale);

                if (otherVertex.Next == movingVertex && otherVertex.PrevRelation != null)
                {
                    relationsStack.Push((otherVertex.PrevRelation, otherVertex));
                }
                else if (otherVertex.Prev == movingVertex && otherVertex.NextRelation != null)
                {
                    relationsStack.Push((otherVertex.NextRelation, otherVertex));
                }
            }
        }
    }
}
