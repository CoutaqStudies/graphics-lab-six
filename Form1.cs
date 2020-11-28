using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphics_lab_six
{
    public partial class Form1 : Form
    {
        Graphics g; 
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            drawPicture(g);
        }
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            drawPicture(g);
        }
        
        private void drawPicture(Graphics g)
        {
            g.Clear(Color.SkyBlue);
            var width = pictureBox1.Size.Width;
            var height = pictureBox1.Size.Height;
            
            PointF[] grassBezier =
            {
                new PointF(0, height/2),
                new PointF(width/4, height*3/4),
                new PointF(width/2, height/2),
                new PointF(width, height*3/4),
            };

            PointF[] grassLines =
            {
                new PointF(width, height * 3 / 4),
                new PointF(width, height),
                new PointF(0, height),
                new PointF(0, height / 2)
            };
            var grassPen = new Pen(Color.ForestGreen, 1f);
            var grass = new GraphicsPath();
            grass.AddBeziers((PointF[]) grassBezier);
            grass.AddLines(grassLines);
            // Draw arc to screen.
            // g.FillRegion(Brushes.Black, grass);
            g.FillPath(new LinearGradientBrush(new Point(0, height/2), new Point(width, height),  Color.DarkGreen, Color.ForestGreen), grass);
            drawTree(grassBezier[1], g);
            drawTree(new PointF(grassBezier[1].X+120, grassBezier[1].Y-30), g);
            drawTree(new PointF(grassBezier[1].X-150, grassBezier[1].Y-5), g);
            g.FillEllipse(Brushes.Gold, width-90, 0+20, 50, 50);
        }

        private void drawTree(PointF location, Graphics g, float trunkCurve = 0.4f, int trunkWidth = 50, int innerTrunkWidth = 30, int lowerTrunkHeight = 50, int totalTrunkHeight = 130)
        {
            float x = location.X;
            float y = location.Y-20;
            var trunkPen = new Pen(Color.SaddleBrown, 1.5f);
            //lower trunk
            PointF[] leftTrunk =
            {
            new PointF(x-trunkWidth/2, y),
            new PointF(x-innerTrunkWidth/2, y-lowerTrunkHeight*trunkCurve),
            new PointF(x-innerTrunkWidth/2, y-lowerTrunkHeight)
            };
            PointF[] rightTrunk =
            {
                new PointF(x+innerTrunkWidth/2, y-lowerTrunkHeight),
                new PointF(x+innerTrunkWidth/2, y-lowerTrunkHeight*trunkCurve),
                new PointF(x+trunkWidth/2, y)
            };

            var lowerTrunk = new GraphicsPath();
            lowerTrunk.AddCurve(leftTrunk);
            lowerTrunk.AddLine(rightTrunk[0], leftTrunk[leftTrunk.Length-1]);
            lowerTrunk.AddCurve(rightTrunk);
            lowerTrunk.AddLine(rightTrunk[rightTrunk.Length-1], leftTrunk[0]);

            //upper trunk
            var upperTrunk = new GraphicsPath();
            upperTrunk.AddLine(leftTrunk[leftTrunk.Length-1], new PointF(leftTrunk[leftTrunk.Length-1].X, leftTrunk[leftTrunk.Length-1].Y-totalTrunkHeight));
            upperTrunk.AddLine(leftTrunk[leftTrunk.Length-1].X, rightTrunk[0].Y-totalTrunkHeight, rightTrunk[0].X, rightTrunk[0].Y-totalTrunkHeight);
            upperTrunk.AddLine(new PointF(rightTrunk[0].X, rightTrunk[0].Y-totalTrunkHeight), rightTrunk[0]);

            //trunk
            var trunk = new GraphicsPath();
            trunk.AddPath(lowerTrunk, false);
            trunk.AddPath(upperTrunk, false);
            g.DrawPath(trunkPen, trunk);
            g.FillPath(Brushes.SaddleBrown, trunk);


            SizeF circle = new SizeF(100, 75);
            SizeF circle1 = new SizeF(75, 50);
            SizeF circle2 = new SizeF(75, 50);

            g.FillEllipse(Brushes.GreenYellow, x-circle.Width/2, y-totalTrunkHeight-circle.Height, circle.Width, circle.Height);
            g.FillEllipse(Brushes.GreenYellow, x-circle1.Width/2-30, y-totalTrunkHeight-circle1.Height, circle1.Width, circle1.Height);
            g.FillEllipse(Brushes.GreenYellow, x-circle2.Width/2+20, y-totalTrunkHeight-circle2.Height-7, circle2.Width, circle2.Height);

            drawBird(new PointF(30, 50), g);
        }
        private void drawBird(PointF location, Graphics g)
        {
            float sizeMultiplier = 10f;
            // PointF[] bodyPoints =
            // {
            //     new PointF(4*sizeMultiplier, 2*sizeMultiplier),
            //     new PointF(3*sizeMultiplier, 3*sizeMultiplier),
            //     new PointF(2*sizeMultiplier, 4*sizeMultiplier),
            //     new PointF(1*sizeMultiplier, 5*sizeMultiplier),
            //     new PointF(1*sizeMultiplier, 7*sizeMultiplier),
            //     new PointF(2*sizeMultiplier, 10*sizeMultiplier),
            //     new PointF(4*sizeMultiplier, 13*sizeMultiplier),
            //     new PointF(7*sizeMultiplier, 15*sizeMultiplier),
            //     new PointF(10*sizeMultiplier, 15*sizeMultiplier),
            //     new PointF(12*sizeMultiplier, 15*sizeMultiplier),
            //     new PointF(14*sizeMultiplier, 14*sizeMultiplier),
            //     new PointF(16*sizeMultiplier, 12*sizeMultiplier),
            //     new PointF(10*sizeMultiplier, 18*sizeMultiplier),
            //     new PointF(8*sizeMultiplier, 19*sizeMultiplier),
            //     new PointF(9*sizeMultiplier, 18*sizeMultiplier),
            //     new PointF(8*sizeMultiplier, 15*sizeMultiplier),
            //     new PointF(11*sizeMultiplier, 5*sizeMultiplier),
            //     new PointF(3*sizeMultiplier, 8*sizeMultiplier),
            //     new PointF(6*sizeMultiplier, 2*sizeMultiplier),
            //     new PointF(4*sizeMultiplier, 2*sizeMultiplier)
            // };

            GraphicsPath bird = new GraphicsPath();
            // bird.AddCurve (bodyPoints);

            // Draw the path to the screen.
            Pen myPen = new Pen(Color.Black, 2);
            Bitmap bitmap = new Bitmap(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height);
            var gBmp = Graphics.FromImage(bitmap);
            // gBmp.DrawPath(myPen, bird);
            gBmp.TranslateTransform(400f, 150.0F);

            gBmp.FillEllipse(Brushes.Honeydew, 0f*sizeMultiplier, 6.5f*sizeMultiplier, 8.5f*sizeMultiplier, 4.5f*sizeMultiplier);
            
          

            gBmp.RotateTransform(40f, 0f);
            gBmp.FillEllipse(Brushes.Honeydew, 3f*sizeMultiplier, 2*sizeMultiplier, 7*sizeMultiplier, 5*sizeMultiplier);

            gBmp.FillEllipse(Brushes.Black, 3.2f * sizeMultiplier, 4f * sizeMultiplier, 1 * sizeMultiplier, 1 * sizeMultiplier);

            gBmp.RotateTransform(-40f, 0f);
            gBmp.TranslateTransform(70.5F, 70.5F);
            gBmp.ScaleTransform(0.5f, 0.5f);
            PointF[] wing =
            {
                new PointF(1.5f*sizeMultiplier, 1.5f*sizeMultiplier),
                new PointF(5f*sizeMultiplier, 1.1f*sizeMultiplier),
                new PointF(1f*sizeMultiplier, 6f*sizeMultiplier),
              
            };

            gBmp.FillPolygon(Brushes.Honeydew, wing);


            gBmp.TranslateTransform(-200F, -45F);
            gBmp.ScaleTransform(1.5f, 1.5f);
            PointF[] beak =
            {
                new PointF(0.5f*sizeMultiplier, 1f*sizeMultiplier),
                new PointF(3.2f*sizeMultiplier, 0.7f*sizeMultiplier),
                new PointF(2.7f*sizeMultiplier, 2f*sizeMultiplier),

            };

            gBmp.FillPolygon(Brushes.Yellow, beak);


            //pictureBox1.Image = bitmap;
            g.DrawImage(bitmap, location);
        }
    }
}