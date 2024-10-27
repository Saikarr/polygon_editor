using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon_editor
{
    class HorizontalRelation : Relation
    {
        public HorizontalRelation(Vertex vertex1, Vertex vertex2) : base(vertex1, vertex2)
        {
        }

        public override void Draw(PaintEventArgs e)
        {
            Point middle = new Point((Vertex1.X + Vertex2.X) / 2, Vertex1.Y);
            e.Graphics.DrawString("H", new Font("Arial", 12), Brushes.Black, middle.X, middle.Y);
        }
        public override void UpdateRelation(Vertex movingVertex, Stack<(Relation, Vertex)> relationsStack)
        {
            Vertex otherVertex = movingVertex == Vertex1 ? Vertex2 : Vertex1;
            if (otherVertex.Y != movingVertex.Y)
            {
                otherVertex.Y = movingVertex.Y;
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
