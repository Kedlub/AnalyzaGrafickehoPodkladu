using System.Collections.Generic;
using System.Numerics;

namespace AnalyzaGrafickehoPodkladu
{
    public partial class Form1 : Form
    {
        readonly Point badPoint = new Point(-1, -1);
        List<Point> points = new List<Point>();

        MapModes currentMode = MapModes.None;
        string debugText = string.Empty;
        decimal polygonPerimeter = 0;
        Bitmap pictureBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var lastPoint = new Point();
            foreach (Point p in points)
            {
                graphics.FillEllipse(Brushes.Red, new Rectangle(p.X - 5, p.Y - 5, 10, 10));
                if (lastPoint != Point.Empty)
                {
                    graphics.DrawLine(Pens.Red, lastPoint, p);
                }
                lastPoint = p;
            }

            if (points.Count > 0)
            {
                // Also connect lastPoint and firstPoint
                graphics.DrawLine(Pens.Red, points[0], lastPoint);
            }

            foreach (Point p in debugPoints)
            {
                graphics.FillEllipse(Brushes.Fuchsia, new Rectangle(p.X - 2, p.Y - 2, 4, 4));
            }

            Pen linePen = new Pen(Color.LightBlue, 2);

            foreach (Line line in debugLines)
            {
                graphics.DrawLine(linePen, line.p1, line.p2);
            }

            if (tempPoint2 != Point.Empty)
            {
                graphics.DrawLine(Pens.Red, tempPoint, tempPoint2);
            }

            if (debugText != string.Empty)
            {
                graphics.DrawString(debugText, DefaultFont, Brushes.Red, new PointF(10, 10));
            }
        }

        private void recalculatePolygon()
        {
            decimal total = 0;
            var lastPoint = new Point();
            foreach (Point point in points)
            {
                if (lastPoint != Point.Empty)
                {
                    var distance = Convert.ToDecimal(Vector2.Distance(new Vector2(lastPoint.X, lastPoint.Y), new Vector2(point.X, point.Y)));
                    total += distance / mapScale;
                }
                lastPoint = point;
            }

            polygonPerimeter = total;
            polygonSizeLabel.Text = $"Velikost polygonu: {Math.Floor(polygonPerimeter)}";
            debugText += $"\n{Math.Floor(polygonPerimeter)}";
        }

        Point tempPoint = new Point();
        Point tempPoint2 = new Point();
        decimal mapScale = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            switch (currentMode)
            {
                case MapModes.None:
                    break;
                case MapModes.ScaleSelect:
                    tempPoint = pictureBox1.PointToClient(Cursor.Position);
                    currentMode = MapModes.ScaleSelect2;
                    break;
                case MapModes.ScaleSelect2:
                    tempPoint2 = pictureBox1.PointToClient(Cursor.Position);
                    setMeritko();
                    createMeritko.Enabled = true;
                    currentMode = MapModes.PolygonSelect;
                    break;
                case MapModes.PolygonSelect:

                    //var point = pictureBox1.PointToClient(Cursor.Position);
                    points.Add(CheckIntersection(pictureBox1.PointToClient(Cursor.Position)));
                    recalculatePolygon();
                    break;
                default:
                    MessageBox.Show("Nìco se šerednì pokazilo");
                    break;
            }

            pictureBox1.Invalidate();
        }

        private void createMeritko_Click(object sender, EventArgs e)
        {
            tempPoint = new Point();
            tempPoint2 = new Point();
            currentMode = MapModes.ScaleSelect;
            createMeritko.Enabled = false;

            Bitmap bmp = new Bitmap(pictureBox1.Image);
        }

        private void setMeritko()
        {
            using (TextInput textInput = new TextInput("Jak velká je tato èára ve skuteèných metrech?"))
            {
                if (textInput.ShowDialog() == DialogResult.OK)
                {
                    var meters = textInput.inputText.Text;
                    var metersDecimal = Convert.ToDecimal(meters);
                    var pixelDistance = Convert.ToDecimal(Vector2.Distance(new Vector2(tempPoint.X, tempPoint.Y), new Vector2(tempPoint2.X, tempPoint2.Y)));

                    mapScale = pixelDistance / metersDecimal;
                    var nìco = pixelDistance / mapScale;
                    debugText = mapScale.ToString();
                    debugText += $"\n{nìco}";
                }
            }
        }

        List<Point> debugPoints = new();
        List<Line> debugLines = new();

        private Point CheckIntersection(Point point)
        {
            debugLines.Clear();
            debugPoints.Clear();
            Point right = CheckIntersection(point, new Vector2(1, 0)); // Right
            Point left = CheckIntersection(point, new Vector2(-1, 0)); // Left
            Point down = CheckIntersection(point, new Vector2(0, 1)); // Down
            Point up = CheckIntersection(point, new Vector2(0, -1)); // Up

            var subPoint = point;
            var closestX = 9999;
            var closestY = 9999;

            if (right != badPoint)
            {
                var X = GetCenterBetween(point, right).X;
                var distance = Math.Abs(X - point.X);
                if (distance < closestX)
                {
                    subPoint.X = X;
                    closestX = distance;
                }
            }

            if (left != badPoint)
            {
                var X = GetCenterBetween(point, left).X;
                var distance = Math.Abs(X - point.X);
                if (distance < closestX)
                {
                    subPoint.X = X;
                    closestX = distance;
                }
            }

            if (down != badPoint)
            {
                var Y = GetCenterBetween(point, down).Y;
                var distance = Math.Abs(Y - point.Y);
                if (distance < closestY)
                {
                    subPoint.Y = Y;
                    closestY = distance;
                }
            }

            if (up != badPoint)
            {
                var Y = GetCenterBetween(point, up).Y;
                var distance = Math.Abs(Y - point.Y);
                if (distance < closestY)
                {
                    subPoint.Y = Y;
                    closestY = distance;
                }
            }

            debugPoints.Add(subPoint);

            debugPoints.Add(right);
            debugPoints.Add(left);
            debugPoints.Add(down);
            debugPoints.Add(up);

            var subscanResult = CheckIntersectionSubscan(subPoint);

            foreach (Point sub in subscanResult)
            {
                debugPoints.Add(sub);
            }

            Point horizontalPoint = Point.Empty;
            Point verticalPoint = Point.Empty;

            Vector2 normal = Vector2.Zero;

            bool rightSide = point.X < subPoint.X ? true : false;

            if (right != badPoint && subscanResult[0] != badPoint)
            {
                var vector = GetVector(right, subscanResult[0]);
                debugLines.Add(new Line() { p1 = right, p2 = subscanResult[0] });
                //verticalPoint = ToPoint(ToVector2(subscanResult[0]) + vector);
                normal = GetNormal(vector);
            }

            if (left != badPoint && subscanResult[1] != badPoint)
            {
                var vector = GetVector(left, subscanResult[1]);
                debugLines.Add(new Line() { p1 = left, p2 = subscanResult[1] });
                //verticalPoint = ToPoint(ToVector2(subscanResult[1]) + vector);
                normal = GetNormal(vector);
            }

            if (!rightSide)
            {
                normal = -normal;
            }

            float closestDistance = 99999;

            if (down != badPoint && subscanResult[2] != badPoint)
            {
                var vector = GetVector(down, subscanResult[2]);
                debugLines.Add(new Line() { p1 = down, p2 = subscanResult[2] });
                horizontalPoint = ToPoint(ToVector2(subscanResult[2]) - normal);
                closestDistance = Vector2.Distance(ToVector2(point), ToVector2(horizontalPoint));
            }

            if (up != badPoint && subscanResult[3] != badPoint)
            {
                var vector = GetVector(up, subscanResult[3]);
                debugLines.Add(new Line() { p1 = up, p2 = subscanResult[3] });
                var hPoint = ToPoint(ToVector2(subscanResult[3]) + normal);
                var distance = Vector2.Distance(ToVector2(point), ToVector2(hPoint));
                if(distance < closestDistance)
                {
                    horizontalPoint = hPoint;
                }
            }


            return horizontalPoint;

            if (verticalPoint == horizontalPoint)
            {

            }

            return new Point(-1, -1);
        }

        private Point ToPoint(Vector2 vector)
        {
            return new Point(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y));
        }

        private Vector2 ToVector2(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        private Vector2 GetNormal(Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        private Point GetVectorIntersection(Point point1, Vector2 vector1, Point point2, Vector2 vector2)
        {
            Vector2 n = new Vector2(CrossProduct(vector1, vector2), Vector2.Dot(vector1, vector2));
            float distance = CrossProduct(n, vector2 - vector1);

            if (distance == 0)
            {
                Console.WriteLine("The lines are parallel and do not intersect.");
                return Point.Empty;
            }
            else
            {
                Vector2 intersection = new Vector2(point1.X, point1.Y) + (distance / CrossProduct(vector1, vector2)) * vector1;
                Console.WriteLine("The intersection point is ({0}, {1}).", intersection.X, intersection.Y);
                return new Point((int)intersection.X, (int)intersection.Y);
            }
        }

        private float CrossProduct(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        private List<Point> CheckIntersectionSubscan(Point point)
        {
            List<Point> results = new()
            {
                CheckIntersection(point, new Vector2(1, 0)), // Right
                CheckIntersection(point, new Vector2(-1, 0)), // Left
                CheckIntersection(point, new Vector2(0, 1)), // Down
                CheckIntersection(point, new Vector2(0, -1)) // Up
            };

            return results;
        }

        private Point CheckIntersection(Point point, Vector2 direction)
        {
            var current = point;
            var moveCount = 0;
            while (true)
            {
                if (moveCount > 40)
                {
                    break;
                }
                current = new Point((int)(current.X + direction.X), (int)(current.Y + direction.Y));

                if (current.X < 0 || current.X > pictureBitmap.Width || current.Y < 0 || current.Y > pictureBitmap.Height)
                {
                    break;
                }

                var color = pictureBitmap.GetPixel(current.X, current.Y);

                if (color.ToArgb() != Color.White.ToArgb())
                {
                    return current;
                }

                moveCount++;
            }

            return new Point(-1, -1);
        }

        private Point GetCenterBetween(Point point1, Point point2)
        {
            var diffX = point2.X - point1.X;
            var diffY = point2.Y - point1.Y;

            return new Point(point1.X + (diffX / 2), point1.Y + (diffY / 2));
        }

        private Vector2 GetVector(Point from, Point to)
        {
            var diffX = to.X - from.X;
            var diffY = to.Y - from.Y;
            return new Vector2(diffX, diffY);
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openFileDialog.FileName;
                    pictureBox1.Load();
                    debugText = "Prosím naètìte mìøítko";

                    pictureBitmap = new(pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.DrawToBitmap(pictureBitmap, pictureBox1.ClientRectangle);
                }
            }
        }

        private void newSelectionButton_Click(object sender, EventArgs e)
        {
            points.Clear();
            debugPoints.Clear();
            debugLines.Clear();
            polygonPerimeter = 0;
            polygonSizeLabel.Text = "Velikost polygonu: 0";
            pictureBox1.Invalidate();
        }
    }

    enum MapModes
    {
        None,
        ScaleSelect,
        ScaleSelect2,
        PolygonSelect,
    }

    struct Line
    {
        public Point p1;
        public Point p2;
    }
}