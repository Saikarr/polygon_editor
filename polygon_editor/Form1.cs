using System.ComponentModel.DataAnnotations;

namespace polygon_editor
{
    public partial class Form1 : Form
    {
        Polygon? polygon = null;
        //Vertex clickedVertex;
        //(Vertex, Vertex) clickedEdge;
        bool IsMovingVertex = false;
        bool IsMovingPolygon = false;
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            polygon = new Polygon();
            libraryButton.Checked = true;

            polygon.AddVertex(new Vertex(100, 100));
            polygon.AddVertex(new Vertex(200, 100));
            polygon.AddVertex(new Vertex(200, 200));
            polygon.AddVertex(new Vertex(100, 200));
        }

        private void picture_Paint(object sender, PaintEventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            Bitmap BM = new Bitmap(rect.Width, rect.Height);
            polygon.Draw(BM, e);
            e.Graphics.DrawImage(BM, 0, 0);
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                polygon.clickedVertex = polygon.GetVertexAt(e.Location);
                if (polygon.clickedVertex != null)
                {
                    IsMovingVertex = true;
                    polygon.IsEdgeSelected = false;
                }
                else if (polygon.IsInside(e.Location))
                {
                    polygon.recent = e.Location;
                    IsMovingPolygon = true;
                }
            }
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (IsMovingVertex == true) //&& clickedVertex != null)
            {
                polygon.clickedVertex.X = e.X;
                polygon.clickedVertex.Y = e.Y;
                picture.Invalidate();
            }
            else if (IsMovingPolygon == true)
            {
                int dx = e.X - polygon.recent.X;
                int dy = e.Y - polygon.recent.Y;
                polygon.Move(dx, dy);
                polygon.recent = e.Location;
                picture.Invalidate();
            }

        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                IsMovingVertex = false;
                IsMovingPolygon = false;
                //clickedVertex = null;
            }
        }

        private void picture_MouseClick(object sender, MouseEventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                polygon.clickedVertex = polygon.GetVertexAt(e.Location);
                if (polygon.clickedVertex != null)
                {
                    IsMovingVertex = true;
                    picture.Invalidate();
                    return;
                }
                for (int i = 0; i < polygon.Vertices.Count; i++)
                {
                    double dist = polygon.PointToLine(e.Location, polygon.Vertices[i], polygon.Vertices[(i + 1) % polygon.Vertices.Count]);
                    if (dist == -1 || polygon.Vertices[i].ControlPointNext != null) continue;
                    if (dist < 10)
                    {
                        polygon.clickedEdge = (polygon.Vertices[i], polygon.Vertices[(i + 1) % polygon.Vertices.Count]);
                        //polygon.clickedEdge = clickedEdge;
                        polygon.IsEdgeSelected = true;
                        picture.Invalidate();
                        return;
                    }
                }
                polygon.IsEdgeSelected = false;
                picture.Invalidate();
            }

        }

        private void libraryButton_CheckedChanged(object sender, EventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            polygon.IsLibrary = libraryButton.Checked;
            picture.Invalidate();
        }

        private void removePolygonButton_Click(object sender, EventArgs e)
        {
            polygon = null;
            picture.Invalidate();
        }

        private void addPolygonButton_Click(object sender, EventArgs e)
        {
            if (polygon == null)
            {
                if (!int.TryParse(verticesTextBox.Text, out int n))
                {
                    MessageBox.Show("Invalid number of vertices");
                    return;
                }
                polygon = new Polygon();
                for (int i = 0; i < n; i++)
                {
                    polygon.AddVertex(new Vertex((int)(300 + 100 * Math.Cos(2 * Math.PI * i / n)), (int)(300 + 100 * Math.Sin(2 * Math.PI * i / n))));
                }
                picture.Invalidate();
            }
        }

        private void addVertexButton_Click(object sender, EventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (polygon.IsEdgeSelected)
            {
                polygon.AddVertexBetween(polygon.clickedEdge.Item1, polygon.clickedEdge.Item2);
                polygon.IsEdgeSelected = false;
                picture.Invalidate();
            }
        }

        private void removeVertexButton_Click(object sender, EventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (polygon.clickedVertex != null)
            {
                if (polygon.Vertices.Count <= 3)
                {
                    MessageBox.Show("Polygon must have at least 3 vertices");
                    return;
                }
                polygon.RemoveVertex(polygon.clickedVertex);
                polygon.clickedVertex = null;
                picture.Invalidate();
            }
        }

        private void bezierButton_Click(object sender, EventArgs e)
        {
            if (polygon == null)
            {
                return;
            }
            if (polygon.IsEdgeSelected)
            {
                polygon.AddControlPoints(polygon.clickedEdge.Item1, polygon.clickedEdge.Item2);
                polygon.IsEdgeSelected = false;
                picture.Invalidate();
            }
        }
    }
}
