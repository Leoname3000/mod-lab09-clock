using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Width = radius * 3;
            Height = radius * 3;
            Text = "Clock";

            origin_x = Width / 2 - 9;
            origin_y = Height / 2 - 26;

            AddLabels(12);

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            this.Paint += new PaintEventHandler(Form1_Paint);

            timer.Start();
        }

        private void AddLabels(int labels)
        {
            for (int i = 0; i < labels; i++)
            {
                int sizeUnit = radius / 20;
                Label label = new Label();
                label.ForeColor = mainColor;
                label.Size = new Size(sizeUnit * 4, sizeUnit * 5 / 2);
                label.TextAlign = ContentAlignment.MiddleCenter;
                int part = 12 / labels;
                label.Text = (12 - i * part).ToString();

                Font font = new Font("Arial", (float) sizeUnit * 4 / 3, FontStyle.Bold);
                label.Font = font;

                double angle = 2 * Math.PI / labels * i;
                int label_radius = radius * 7 / 8;
                int x = (int) (label_radius * -Math.Sin(angle)) + origin_x - label.Size.Width / 2;
                int y = (int) (label_radius * -Math.Cos(angle)) + origin_y - label.Size.Height / 2;
                label.Left = x;
                label.Top = y;
                Controls.Add(label);
            }
        }

        int radius = 180;
        int origin_x;
        int origin_y;
        Color mainColor = Color.Brown;
        Timer timer = new Timer();

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;

            Graphics g = e.Graphics;
            g.TranslateTransform(origin_x, origin_y);

            Pen cir_pen = new Pen(mainColor, 3);
            g.DrawEllipse(cir_pen, -radius, -radius, radius * 2, radius * 2);


            double second_angle = Math.PI / 30 * dt.Second;
            int second_radius = radius * 3 / 4;
            int second_x = (int) (second_radius * Math.Sin(second_angle));
            int second_y = (int) (second_radius * -Math.Cos(second_angle));
            Pen second_pen = new Pen(Color.Orange, 3);
            g.DrawLine(second_pen, 0, 0, second_x, second_y);

            double minute_angle = Math.PI / 30 * dt.Minute + Math.PI / 30 / 60 * dt.Second;
            int minute_radius = radius * 7 / 12;
            int minute_x = (int) (minute_radius * Math.Sin(minute_angle));
            int minute_y = (int) (minute_radius * -Math.Cos(minute_angle));
            Pen minute_pen = new Pen(Color.OrangeRed, 4);
            g.DrawLine(minute_pen, 0, 0, minute_x, minute_y);

            double hour_angle = Math.PI / 6 * dt.Hour + Math.PI / 6 / 60 * dt.Minute + Math.PI / 6 / 60 /60 * dt.Second;
            int hour_radius = radius * 5 / 12;
            int hour_x = (int) (hour_radius * Math.Sin(hour_angle));
            int hour_y = (int) (hour_radius * -Math.Cos(hour_angle));
            Pen hour_pen = new Pen(Color.Red, 4);
            g.DrawLine(hour_pen, 0, 0, hour_x, hour_y);

            int center_radius = 5;
            g.FillEllipse(cir_pen.Brush, -center_radius, -center_radius, center_radius * 2, center_radius * 2);
        }
    }
}
