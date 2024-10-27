using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace polygon_editor
{
    abstract class Relation
    {
        public Vertex Vertex1;
        public Vertex Vertex2;

        public Relation(Vertex vertex1, Vertex vertex2)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }

        public abstract void UpdateRelation(Vertex movingVertex, Stack<(Relation, Vertex)> relationsStack);
        public abstract void Draw(PaintEventArgs e);
        public void CreateRelation()
        {
            var relationsStack = new Stack<(Relation, Vertex)>();
            this.UpdateRelation(Vertex1, relationsStack);
            try
            {
                FixRelationsStack(relationsStack);
            }
            catch
            {
                MessageBox.Show("Polygon with this relation cannot exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void FixRelationsStack(Stack<(Relation, Vertex)> relationsStack)
        {
            int Iterations = 50;
            while (relationsStack.Count > 0)
            {
                var (relation, vertex) = relationsStack.Pop();
                relation.UpdateRelation(vertex, relationsStack);
                if (Iterations-- < 0)
                {
                    throw new Exception();
                }
            }
        }
    }
}
