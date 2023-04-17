namespace AnalyzaGrafickehoPodkladu
{
    public partial class Form1 : Form
    {
        List<Point> points = new List<Point>();
        MapModes currentMode = MapModes.None;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            foreach (Point p in points)
            {
                graphics.FillEllipse(Brushes.Red, new Rectangle(p.X, p.Y, 10, 10));
            }
        }

        Point tempPoint = new Point();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            switch(currentMode)
            {
                case MapModes.None:
                    break;
                case MapModes.ScaleSelect:
                    tempPoint = pictureBox1.PointToClient(Cursor.Position);
                    break;
                case MapModes.ScaleSelect2:

                    break;
                case MapModes.PolygonSelect:
                    var point = pictureBox1.PointToClient(Cursor.Position);
                    points.Add(point);
                    break;
                default:
                    MessageBox.Show("Nìco se šerednì pokazilo");
                    break;
            }
            
            pictureBox1.Invalidate();
        }

        private void createMeritko_Click(object sender, EventArgs e)
        {
            currentMode = MapModes.ScaleSelect;
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