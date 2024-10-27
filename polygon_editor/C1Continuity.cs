using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon_editor
{
    class C1Continuity : Relation
    {
        public C1Continuity(Vertex vertex1, Vertex vertex2) : base(vertex1, vertex2)
        {
        }

        public override void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawString("C1", new Font("Arial", 12), Brushes.Black, Vertex1.Point);
        }
        public override void UpdateRelation(Vertex movingVertex, Stack<(Relation, Vertex)> relationsStack)
        {
            Vertex otherVertex = movingVertex == Vertex1 ? Vertex2 : Vertex1;
            Vertex controlPoint = Vertex1.ControlPointNext ?? Vertex1.ControlPointPrev;

            if (!IsC1())
            {
                if (movingVertex == Vertex1)
                {
                    int dx = controlPoint.X - movingVertex.X;
                    int dy = controlPoint.Y - movingVertex.Y;
                    otherVertex.X = movingVertex.X - 3 * dx;
                    otherVertex.Y = movingVertex.Y - 3 * dy;
                }
                else
                {
                    int dx = controlPoint.X - movingVertex.X;
                    int dy = controlPoint.Y - movingVertex.Y;
                    otherVertex.X = movingVertex.X + 3 * dx / 4;
                    otherVertex.Y = movingVertex.Y + 3 * dy / 4;
                }

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

        private bool IsC1()
        {
            if (Vertex1.ControlPointNext != null)
            {
                if ((Vertex1.ControlPointNext.X - Vertex1.X) * 3 / (Vertex1.X - Vertex2.X) - 1 < 1e-3 &&
                    (Vertex1.ControlPointNext.Y - Vertex1.Y) * 3 / (Vertex1.Y - Vertex2.Y) - 1 < 1e-3)
                    return true;
                else
                    return false;
            }
            else
            {
                if ((Vertex1.ControlPointPrev.X - Vertex1.X) * 3 / (Vertex1.X - Vertex2.X) < 1e-3 &&
                    (Vertex1.ControlPointPrev.Y - Vertex1.Y) * 3 / (Vertex1.Y - Vertex2.Y) < 1e-3)
                    return true;
                else
                    return false;
            }
        }
    }
}
