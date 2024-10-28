using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon_editor
{
    class G1Continuity : Relation
    {
        public G1Continuity(Vertex vertex1, Vertex vertex2) : base(vertex1, vertex2)
        {
        }

        public override void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawString("G1", new Font("Arial", 12), Brushes.Black, Vertex1.Point);
        }
        public override void UpdateRelation(Vertex movingVertex, Stack<(Relation, Vertex)> relationsStack)
        {
            Vertex otherVertex = movingVertex == Vertex1 ? Vertex2 : Vertex1;
            Vertex controlPoint = Vertex1.ControlPointNext ?? Vertex1.ControlPointPrev;

            // Calculate the initial distance between the vertices
            double initialDistance = movingVertex.DistanceTo(otherVertex);

            // Check if the points are collinear
            if (!AreCollinear(otherVertex, movingVertex, controlPoint))
            {
                if (movingVertex == Vertex1)
                {
                    int dx = controlPoint.X - movingVertex.X;
                    int dy = controlPoint.Y - movingVertex.Y;
                    otherVertex.X = movingVertex.X - dx;
                    otherVertex.Y = movingVertex.Y - dy;
                }
                else
                {
                    int dx = controlPoint.X - movingVertex.X;
                    int dy = controlPoint.Y - movingVertex.Y;
                    otherVertex.X = movingVertex.X + dx / 2;
                    otherVertex.Y = movingVertex.Y + dy / 2;
                }

                // Adjust the position of otherVertex to maintain the initial distance
                double currentDistance = movingVertex.DistanceTo(otherVertex);
                double scale = initialDistance / currentDistance;
                otherVertex.X = (int)Math.Round(movingVertex.X + (otherVertex.X - movingVertex.X) * scale);
                otherVertex.Y = (int)Math.Round(movingVertex.Y + (otherVertex.Y - movingVertex.Y) * scale);

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

        private bool AreCollinear(Vertex v1, Vertex v2, Vertex v3)
        {
            // Calculate the area of the triangle formed by (v1, v2, v3)
            double area = v1.X * (v2.Y - v3.Y) + v2.X * (v3.Y - v1.Y) + v3.X * (v1.Y - v2.Y);
            return Math.Abs(area) < 1e-3; // Use a small tolerance to account for floating-point precision
        }

    }
}

