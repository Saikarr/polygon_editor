using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace polygon_editor
{
    public class Polygon
    {
        public Point recent;
        public Vertex clickedVertex = null;
        //public bool IsMoving = false;
        public bool IsLibrary = true;
        public bool IsEdgeSelected = false;
        public (Vertex, Vertex) clickedEdge;
        public List<Vertex> Vertices { get; set; }
        public List<(Vertex, Vertex)> ControlPoints { get; set; }
        public List<Relation> Relations { get; set; }

        public Polygon()
        {
            Vertices = new List<Vertex>();
            ControlPoints = new List<(Vertex, Vertex)>();
            Relations = new List<Relation>();
        }

        public void AddVertex(Vertex vertex)
        {
            if (Vertices.Count == 0)
            {
                Vertices.Add(vertex);
                return;
            }
            Vertices[Vertices.Count - 1].Next = vertex;
            Vertices[0].Prev = vertex;
            vertex.Prev = Vertices.LastOrDefault();
            vertex.Next = Vertices.FirstOrDefault();
            Vertices.Add(vertex);
        }

        public void RemoveVertex(Vertex vertex)
        {
            if (vertex.IsControlPoint) { return; }
            else if (vertex.ControlPointPrev != null)
            {
                ControlPoints.Remove((vertex.ControlPointPrev.ControlPointPrev, vertex.ControlPointPrev));
                vertex.ControlPointPrev = null;
            }
            else if (vertex.ControlPointNext != null)
            {
                ControlPoints.Remove((vertex.ControlPointNext, vertex.ControlPointNext.ControlPointNext));
                vertex.ControlPointNext = null;
            }
            if (vertex.NextRelation != null)
            {
                vertex.Next.PrevRelation = null;
                Relations.Remove(vertex.NextRelation);
            }
            if (vertex.PrevRelation != null)
            {
                vertex.Prev.NextRelation = null;
                Relations.Remove(vertex.PrevRelation);
            }
            vertex.Prev.Next = vertex.Next;
            vertex.Next.Prev = vertex.Prev;
            Vertices.Remove(vertex);
        }

        public void Bresenham(Bitmap BM, Vertex v1, Vertex v2, Color color)
        {
            int x = v1.X;
            int y = v1.Y;
            int x2 = v2.X;
            int y2 = v2.Y;
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if (x >= 0 && x < BM.Width && y >= 0 && y < BM.Height) BM.SetPixel(x, y, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
        public void Draw(Bitmap BM, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            Pen pen1 = new Pen(Color.Red, 2);
            Pen dottedpen = new Pen(Color.Black, 2);
            dottedpen.DashPattern = [2.0F, 2.0F];
            if (IsLibrary)
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (Vertices[i].ControlPointNext == null)
                    {
                        Vertex v1 = Vertices[i];
                        Vertex v2 = Vertices[(i + 1) % Vertices.Count];
                        e.Graphics.DrawLine(pen, v1.Point, v2.Point);
                    }
                }
                if (IsEdgeSelected)
                {
                    e.Graphics.DrawLine(pen1, clickedEdge.Item1.Point, clickedEdge.Item2.Point);
                }
            }
            else // Bresenham from https://stackoverflow.com/questions/11678693/all-cases-covered-bresenhams-line-algorithm
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (Vertices[i].ControlPointNext == null)
                    {
                        Vertex v1 = Vertices[i];
                        Vertex v2 = Vertices[(i + 1) % Vertices.Count];
                        Bresenham(BM, v1, v2, Color.Black);
                    }
                }
                if (IsEdgeSelected)
                {
                    Bresenham(BM, clickedEdge.Item1, clickedEdge.Item2, Color.Red);
                }
            }


            List<Vertex> FillVertices = new List<Vertex>(Vertices);
            foreach ((Vertex, Vertex) points in ControlPoints)
            {
                Bezier(BM, points.Item1.ControlPointPrev.Point, points.Item1.Point, points.Item2.Point, points.Item2.ControlPointNext.Point, out List<Vertex> FillingBezier);
                FillVertices.InsertRange(FillVertices.IndexOf(points.Item2.ControlPointNext), FillingBezier);
                for (int i = 0; i < FillingBezier.Count - 1; i++)
                {
                    Vertex v1 = FillingBezier[i];
                    Vertex v2 = FillingBezier[i + 1];
                    if (IsLibrary)
                        e.Graphics.DrawLine(pen, v1.Point, v2.Point);
                    else
                        Bresenham(BM, v1, v2, Color.Black);
                }
            }

            e.Graphics.FillPolygon(Brushes.LightGray, FillVertices.Select(v => v.Point).ToArray());

            foreach ((Vertex, Vertex) points in ControlPoints)
            {
                e.Graphics.FillEllipse(Brushes.Gray, points.Item1.X - 5, points.Item1.Y - 5, 10, 10);
                e.Graphics.FillEllipse(Brushes.Gray, points.Item2.X - 5, points.Item2.Y - 5, 10, 10);
                e.Graphics.DrawPolygon(dottedpen, new Point[] { points.Item1.ControlPointPrev.Point, points.Item1.Point,
                    points.Item2.Point, points.Item2.ControlPointNext.Point });
            }

            foreach (Vertex vertex in Vertices)
            {
                e.Graphics.FillEllipse(Brushes.Black, vertex.X - 5, vertex.Y - 5, 10, 10);
            }

            if (clickedVertex != null)
            {
                e.Graphics.FillEllipse(Brushes.Red, clickedVertex.X - 5, clickedVertex.Y - 5, 10, 10);
            }

            foreach (Relation relation in Relations)
            {
                relation.Draw(e);
            }
        }

        private void FillCircleOnBitmap(int cx, int cy, int radius, Color fillColor, Bitmap bitmap)
        {
            for (int y = cy - radius; y <= cy + radius; y++)
            {
                for (int x = cx - radius; x <= cx + radius; x++)
                {
                    // Check if the point (x, y) is inside the circle
                    if ((x - cx) * (x - cx) + (y - cy) * (y - cy) <= radius * radius)
                    {
                        // Ensure we are within the bitmap bounds
                        if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                        {
                            // Set the pixel color inside the circle
                            bitmap.SetPixel(x, y, fillColor);
                        }
                    }
                }
            }
        }

        private void FillPolygonOnBitmap(Point[] polygonPoints, Bitmap bitmap)
        {
            ScanLineFill(polygonPoints, Color.LightGray, bitmap);
        }

        private void ScanLineFill(Point[] polygonPoints, Color fillColor, Bitmap bitmap)
        {
            int minY = int.MaxValue, maxY = int.MinValue;

            // Find the minimum and maximum Y values of the polygon
            foreach (Point p in polygonPoints)
            {
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }

            // For each scan line (Y value), find intersection points with polygon edges
            for (int y = minY; y <= maxY; y++)
            {
                List<int> nodeX = new List<int>();

                // For each edge of the polygon
                for (int i = 0; i < polygonPoints.Length; i++)
                {
                    Point p1 = polygonPoints[i];
                    Point p2 = polygonPoints[(i + 1) % polygonPoints.Length];

                    // Check if the edge crosses the current scan line
                    if (p1.Y < y && p2.Y >= y || p2.Y < y && p1.Y >= y)
                    {
                        // Calculate the X coordinate of the intersection
                        int intersectX = p1.X + (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                        nodeX.Add(intersectX);
                    }
                }

                // Sort the intersection points by X
                nodeX.Sort();

                // Fill the pixels between pairs of intersection points
                for (int i = 0; i < nodeX.Count; i += 2)
                {
                    if (i + 1 < nodeX.Count)
                    {
                        for (int x = nodeX[i]; x < nodeX[i + 1]; x++)
                        {
                            if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                            {
                                bitmap.SetPixel(x, y, fillColor);
                            }
                        }
                    }
                }
            }
        }

        public void Bezier(Bitmap BM, Point point1, Point point2, Point point3, Point point4, out List<Vertex> FillingBezier)
        {
            FillingBezier = new List<Vertex>();
            for (float t = 0; t <= 1; t += 0.01f)
            {
                PointF point = GetBezierPoint(t, point1, point2, point3, point4);
                if (point.X >= 0 && point.X < BM.Width && point.Y >= 0 && point.Y < BM.Height)
                {
                    BM.SetPixel((int)point.X, (int)point.Y, Color.Black);
                    FillingBezier.Add(new Vertex((int)point.X, (int)point.Y));
                }
            }
        }
        private PointF GetBezierPoint(float t, PointF P0, PointF P1, PointF P2, PointF P3)
        {

            float oneMinusT = 1 - t;

            PointF A = new PointF(oneMinusT * P0.X + t * P1.X, oneMinusT * P0.Y + t * P1.Y);
            PointF B = new PointF(oneMinusT * P1.X + t * P2.X, oneMinusT * P1.Y + t * P2.Y);
            PointF C = new PointF(oneMinusT * P2.X + t * P3.X, oneMinusT * P2.Y + t * P3.Y);

            PointF D = new PointF(oneMinusT * A.X + t * B.X, oneMinusT * A.Y + t * B.Y);
            PointF E = new PointF(oneMinusT * B.X + t * C.X, oneMinusT * B.Y + t * C.Y);

            return new PointF(oneMinusT * D.X + t * E.X, oneMinusT * D.Y + t * E.Y);

            //float oneMinusT = 1 - t;
            //float oneMinusTSquared = oneMinusT * oneMinusT;
            //float tSquared = t * t;

            //float x = oneMinusTSquared * oneMinusT * P0.X +
            //          3 * oneMinusTSquared * t * P1.X +
            //          3 * oneMinusT * tSquared * P2.X +
            //          tSquared * t * P3.X;

            //float y = oneMinusTSquared * oneMinusT * P0.Y +
            //          3 * oneMinusTSquared * t * P1.Y +
            //          3 * oneMinusT * tSquared * P2.Y +
            //          tSquared * t * P3.Y;

            //return new PointF(x, y);
        }
        public Vertex GetVertexAt(Point point)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.DistanceTo(point) < 10)
                {
                    return vertex;
                }
            }
            foreach ((Vertex, Vertex) vertices in ControlPoints)
            {
                if (vertices.Item1.DistanceTo(point) < 10)
                {
                    return vertices.Item1;
                }
                if (vertices.Item2.DistanceTo(point) < 10)
                {
                    return vertices.Item2;
                }
            }
            return null;
        }

        public void Move(int dx, int dy)
        {
            foreach (Vertex vertex in Vertices)
            {
                vertex.X += dx;
                vertex.Y += dy;
            }
            foreach ((Vertex, Vertex) points in ControlPoints)
            {
                points.Item1.X += dx;
                points.Item1.Y += dy;
                points.Item2.X += dx;
                points.Item2.Y += dy;
            }
        }

        //public void MoveVertex(Vertex vertex, Point p)
        //{
        //    if (moving)
        //    {
        //        int dx = p.X - recent.X;
        //        int dy = p.Y - recent.Y;
        //        recent = p;
        //        vertex.X += dx;
        //        vertex.Y += dy;
        //    }
        //}

        public double PointToLine(Point v0, Vertex v1, Vertex v2)
        {
            double pX = v2.X - v1.X;
            double pY = v2.Y - v1.Y;
            double pom = ((v0.X - v1.X) * pX + (v0.Y - v1.Y) * pY) / ((pX * pX) + (pY * pY));
            if (pom < 0 || pom > 1) { return -1; }
            int x0 = v0.X;
            int y0 = v0.Y;
            int x1 = v1.X;
            int y1 = v1.Y;
            int x2 = v2.X;
            int y2 = v2.Y;
            double ans = Math.Abs((x2 - x1) * (y0 - y1) - (y2 - y1) * (x0 - x1)) / Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
            return ans;
        }

        public void AddVertexBetween(Vertex v1, Vertex v2)
        {
            int x = (v1.X + v2.X) / 2;
            int y = (v1.Y + v2.Y) / 2;
            Vertex v = new Vertex(x, y);
            v.Prev = v1;
            v.Next = v2;
            v1.Next = v;
            v2.Prev = v;
            if (v1.NextRelation != null)
            {
                Relations.Remove(v1.NextRelation);
                v1.NextRelation = null;
                v2.PrevRelation = null;
            }
            Vertices.Insert(Vertices.IndexOf(v2), v);
        }

        public void AddControlPoints(Vertex v1, Vertex v2)
        {
            Vertex v3 = new Vertex((int)((v1.X - v1.Prev.X) * 0.5 + v1.X), (int)((v1.Y - v1.Prev.Y) * 0.5 + v1.Y));
            Vertex v4 = new Vertex((int)((v2.X - v2.Next.X) * 0.5 + v2.X), (int)((v2.Y - v2.Next.Y) * 0.5 + v2.Y));
            v4.IsControlPoint = true;
            v3.IsControlPoint = true;
            v3.ControlPointPrev = v1;
            v4.ControlPointPrev = v3;
            v3.ControlPointNext = v4;
            v4.ControlPointNext = v2;
            v1.ControlPointNext = v3;
            v2.ControlPointPrev = v4;
            ControlPoints.Add((v3, v4));
        }

        public bool IsInside(Point p)
        {
            List<Vertex> allVertices = new List<Vertex>(Vertices);
            foreach ((Vertex, Vertex) points in ControlPoints)
            {
                allVertices.Insert(allVertices.IndexOf(points.Item2.ControlPointNext), points.Item2);
                allVertices.Insert(allVertices.IndexOf(points.Item2), points.Item1);
            }
            int n = allVertices.Count;
            double[] x = new double[n];
            double[] y = new double[n];
            int i, j;
            for (i = 0; i < n; i++)
            {
                x[i] = allVertices[i].X;
                y[i] = allVertices[i].Y;
            }
            bool c = false;
            for (i = 0, j = n - 1; i < n; j = i++)
            {
                if (((y[i] > p.Y) != (y[j] > p.Y)) && (p.X < (x[j] - x[i]) * (p.Y - y[i]) / (y[j] - y[i]) + x[i]))
                {
                    c = !c;
                }
            }
            return c;
        }

        public void RemoveControlPoints(Vertex v1)
        {
            if (v1.ControlPointPrev != null)
            {
                ControlPoints.Remove((v1.ControlPointPrev, v1));
                v1.ControlPointPrev.ControlPointPrev.ControlPointNext = null;
                v1.ControlPointNext.ControlPointPrev = null;
            }
            else
            {
                ControlPoints.Remove((v1, v1.ControlPointNext));
                v1.ControlPointNext.ControlPointNext.ControlPointPrev = null;
                v1.ControlPointPrev.ControlPointNext = null;
            }
            clickedVertex = null;
        }
    }
}
