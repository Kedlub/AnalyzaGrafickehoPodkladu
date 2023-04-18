using System.Numerics;

namespace AnalyzaGrafickehoPodkladu
{
    public partial class Form1 : Form
    {
        List<Point> points = new List<Point>();

        MapModes currentMode = MapModes.None;
        string debugText = string.Empty;
        decimal polygonPerimeter = 0;

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
                if(lastPoint != Point.Empty)
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
                if(lastPoint != Point.Empty)
                {
                    var distance = Convert.ToDecimal(Vector2.Distance(new Vector2(lastPoint.X, lastPoint.Y), new Vector2(point.X, point.Y)));
                    total += distance / mapScale;
                }
                lastPoint = point;
            }

            polygonPerimeter = total;
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
                    var point = pictureBox1.PointToClient(Cursor.Position);
                    points.Add(point);
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

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openFileDialog.FileName;
                    pictureBox1.LoadAsync();
                    debugText = "Prosím naètìte mìøítko";
                }
            }
        }
    }

    enum MapModes
    {
        None,
        ScaleSelect,
        ScaleSelect2,
        PolygonSelect,
    }
}