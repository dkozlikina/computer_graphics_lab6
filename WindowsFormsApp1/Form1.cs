using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Threading;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static System.Windows.Forms.AxHost;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Bitmap map;

        public class point
        {
            public double x;
            public double y;
            public double z;
            public Color color;
            //List<int> 

            public point(double x, double y, double z, Color color)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.color = color;
            }

            //void draw()
            //{
            //    for (int x = this.x - 3; x < this.x + 3; x++)
            //    {
            //        for (int y = this.y - 3; y < this.y + 3; y++)
            //        {
            //            map.SetPixel(x, y, this.color);
            //        }
            //    }
            //}
        }

        class edge
        {
            public point a;
            public point b;
            public Color color;

            public edge(point a, point b, Color color)
            {
                this.a = a;
                this.b = b;
                this.color = color;
            }
        }

        class face
        {
            List<edge> edges;
            face(List<edge> edges)
            {
                this.edges = edges;
            }
        }

        class polyhedron
        {
            List<face> faces;
            polyhedron(List<face> faces)
            {
                this.faces = faces;
            }
        }

        List<point> points = new List<point>();
        List<edge> edges = new List<edge>();

        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(pictureBox.Width, pictureBox.Height);
            //Graphics graphics = Graphics.FromImage(map);
            //graphics.Clear(Color.White);
            ////graphics = Graphics.FromImage(map);// mapPrim);
            //pictureBox.Image = map;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // можно оформить как наследник класса фигур, чтобы рисовать разные 3д объекты
        //class cube
        //{
        //    public List<point> points = new List<point>();
        //    public List<edge> edges = new List<edge>();
        //    cube()
        //    {
        //        // верхние вершины 
        //        points.Add(new point(0, 100, 100, Color.AliceBlue));
        //        points.Add(new point(0, 0, 100, Color.AliceBlue));
        //        points.Add(new point(100, 0, 100, Color.AliceBlue));
        //        points.Add(new point(100, 100, 100, Color.AliceBlue));

        //        // нижние вершины 
        //        points.Add(new point(0, 100, 0, Color.HotPink));
        //        points.Add(new point(0, 0, 0, Color.HotPink));
        //        points.Add(new point(100, 0, 0, Color.HotPink));
        //        points.Add(new point(100, 100, 0, Color.HotPink));

        //        // верхние рёбра 
        //        edges.Add(new edge(points[0], points[1], Color.Orange));
        //        edges.Add(new edge(points[1], points[2], Color.Orange));
        //        edges.Add(new edge(points[2], points[3], Color.Orange));
        //        edges.Add(new edge(points[3], points[0], Color.Orange));

        //        // боковые рёбра 
        //        edges.Add(new edge(points[0], points[4], Color.OldLace));
        //        edges.Add(new edge(points[1], points[5], Color.OldLace));
        //        edges.Add(new edge(points[2], points[6], Color.OldLace));
        //        edges.Add(new edge(points[3], points[7], Color.OldLace));

        //        // нижние рёбра 
        //        edges.Add(new edge(points[4], points[5], Color.Silver));
        //        edges.Add(new edge(points[5], points[6], Color.Silver));
        //        edges.Add(new edge(points[6], points[7], Color.Silver));
        //        edges.Add(new edge(points[7], points[4], Color.Silver));
        //    }
        //}
        void makeCube()
        {
            // верхние вершины 
            points.Add(new point(0, 100, 100, Color.AliceBlue));
            points.Add(new point(0, 0, 100, Color.AliceBlue));
            points.Add(new point(100, 0, 100, Color.AliceBlue));
            points.Add(new point(100, 100, 100, Color.AliceBlue));

            // нижние вершины 
            points.Add(new point(0, 100, 0, Color.HotPink));
            points.Add(new point(0, 0, 0, Color.HotPink));
            points.Add(new point(100, 0, 0, Color.HotPink));
            points.Add(new point(100, 100, 0, Color.HotPink));

            // верхние рёбра 
            edges.Add(new edge(points[0], points[1], Color.Orange));
            edges.Add(new edge(points[1], points[2], Color.Orange));
            edges.Add(new edge(points[2], points[3], Color.Orange));
            edges.Add(new edge(points[3], points[0], Color.Orange));

            // боковые рёбра 
            edges.Add(new edge(points[0], points[4], Color.GreenYellow));
            edges.Add(new edge(points[1], points[5], Color.GreenYellow));
            edges.Add(new edge(points[2], points[6], Color.GreenYellow));
            edges.Add(new edge(points[3], points[7], Color.GreenYellow));

            // нижние рёбра 
            edges.Add(new edge(points[4], points[5], Color.BlueViolet));
            edges.Add(new edge(points[5], points[6], Color.BlueViolet));
            edges.Add(new edge(points[6], points[7], Color.BlueViolet));
            edges.Add(new edge(points[7], points[4], Color.BlueViolet));
        }

        void drawEdge(edge e, int delta)
        {
            double x1 = e.a.x + delta;
            double y1 = e.a.y + delta;
            double x2 = e.b.x + delta;
            double y2 = e.b.y + delta;
            Color color = e.color;

            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0 ||
                x1 >= map.Width || y1 >= map.Height ||
                x2 >= map.Width || y2 >= map.Height)
                return;
            double deltaX = Math.Abs(x2 - x1);
            double deltaY = Math.Abs(y2 - y1);
            double signX = x1 < x2 ? 1 : -1;
            double signY = y1 < y2 ? 1 : -1;
            double error = deltaX - deltaY;
            map.SetPixel((int)x2, (int)y2, color);
            while (x1 != x2 || y1 != y2)
            {
                map.SetPixel((int)x1, (int)y1, color);
                double error2 = error * 2;
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }
        }

        void DrawLine(double x1, double y1, double x2, double y2, Color color)
        {
            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0 ||
                x1 >= map.Width || y1 >= map.Height ||
                x2 >= map.Width || y2 >= map.Height)
                return;
            double deltaX = Math.Abs(x2 - x1);
            double deltaY = Math.Abs(y2 - y1);
            double signX = x1 < x2 ? 1 : -1;
            double signY = y1 < y2 ? 1 : -1;
            double error = deltaX - deltaY;
            map.SetPixel((int)x2, (int)y2, color);
            while (x1 != x2 || y1 != y2)
            {
                map.SetPixel((int)x1, (int)y1, color);
                double error2 = error * 2;
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }
        }

        void clean()
        {
            Graphics graphics = Graphics.FromImage(map);
            graphics.Clear(Color.White);
            graphics = Graphics.FromImage(map);// mapPrim);
            graphics.Clear(Color.White);
            //points = new List<Point>();
            //Graphics graphics = Graphics.FromImage(map);// mapPrim);
            graphics.Clear(Color.White);

            map = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = map;

            //pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        void showCube() // List<point> p)
        {
            List<point> p = points;
            try
            {
                for (int i = 0; i < p.Count; i++)
                {
                    Console.Write("!!!!!");
                    Console.Write(p[i].x);
                    Console.Write(" ");
                    Console.WriteLine(p[i].y);
                }


                for (int i = 0; i < p.Count; i++)
                {
                    double temp_x = p[i].x + 200;
                    double temp_y = p[i].y + 200;
                    //map.SetPixel(p[i].x + 200, p[i].y + 200, p[i].color);

                    for (int xx = (int)temp_x - 3; xx < temp_x + 3; xx++)
                    {
                        for (int yy = (int)temp_y - 3; yy < temp_y + 3; yy++)
                        {
                            map.SetPixel(xx, yy, p[i].color);
                        }
                    }
                }

                for (int i = 0; i < edges.Count; i++)
                {
                    drawEdge(edges[i], 200);
                }

                label1.Text = (p[0].x).ToString() + " " + (p[0].y).ToString();
                label2.Text = (p[1].x).ToString() + " " + (p[1].y).ToString();
                label3.Text = (p[2].x).ToString() + " " + (p[2].y).ToString();
                label4.Text = (p[3].x).ToString() + " " + (p[3].y).ToString();
                label5.Text = (p[4].x).ToString() + " " + (p[4].y).ToString();
                label6.Text = (p[5].x).ToString() + " " + (p[5].y).ToString();
                label7.Text = (p[6].x).ToString() + " " + (p[6].y).ToString();
                label8.Text = (p[7].x).ToString() + " " + (p[7].y).ToString();
            }
            catch(Exception e) 
            {
                label1.Text = (p[0].x).ToString() + " " + (p[0].y).ToString();
                label2.Text = (p[1].x).ToString() + " " + (p[1].y).ToString();
                label3.Text = (p[2].x).ToString() + " " + (p[2].y).ToString();
                label4.Text = (p[3].x).ToString() + " " + (p[3].y).ToString();
                label5.Text = (p[4].x).ToString() + " " + (p[4].y).ToString();
                label6.Text = (p[5].x).ToString() + " " + (p[5].y).ToString();
                label7.Text = (p[6].x).ToString() + " " + (p[6].y).ToString();
                label8.Text = (p[7].x).ToString() + " " + (p[7].y).ToString();
            }


            

            pictureBox.Image = map;
            //DrawLine(200, 200, 500, 200, Color.Red); // Y
            //DrawLine(200, 200, 200, 500, Color.Red);
        }

        double[,] getOneMatr(int n)
        {
            double[,] matr = new double[4, 4];
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    if (i == j)
                        matr[i, j] = 1;
                    else
                        matr[i, j] = 0;
                }
            }
            return matr;
        }

        void RotateX(double Angle) // double[,]
        {
            double[,] res = getOneMatr(4);

            res[1, 1] = Math.Cos(Angle);
            res[2, 1] = -Math.Sin(Angle);
            res[1, 2] = Math.Sin(Angle);
            res[2, 2] = Math.Cos(Angle);
            //return res;
            Rotate(res, Angle);
        }

        void RotateY(double Angle) // double[,]
        {
            double[,] res = getOneMatr(4);

            res[0, 0] = Math.Cos(Angle);
            res[2, 0] = -Math.Sin(Angle);
            res[0, 2] = Math.Sin(Angle);
            res[2, 2] = Math.Cos(Angle);
            //return res;
            Rotate(res, Angle);
        }

        void RotateZ(double Angle) // double[,]
        {
            double[,] res = getOneMatr(4);

            res[0, 0] = Math.Cos(Angle);
            res[1, 0] = -Math.Sin(Angle);
            res[0, 1] = Math.Sin(Angle);
            res[1, 1] = Math.Cos(Angle);
            //return res;
            Rotate(res, Angle);
        }

        void Rotate(double[,] T, double Angle)
        {
            //double[,] T = getMatrRotateX(Angle);
            for (int i = 0; i < points.Count; i++)
            {
                double[] new_p = new double[4];
                new_p[0] = points[i].x;
                new_p[1] = points[i].y;
                new_p[2] = points[i].z;
                new_p[3] = 1;

                double[] res = new double[4];
                for (int j = 0; j < 4; j++)
                    res[j] = 0;

                for(int k = 0; k < 4; k++)
                {
                    for(int l = 0; l < 4; l++)
                    {
                        res[k] += new_p[l] * T[l, k];
                    }
                }

                points[i].x = (int)res[0]; // можно убрать инт и преобразовывать только на выводе
                points[i].y = (int)res[1];
                points[i].z = (int)res[2];
            }
        }

        void Translate()
        {
            double[,] T = getOneMatr(4);

            double dx = 0;
            double dy = 0;
            double dz = 0;

            if (textBox6.Text.Length != 0)
                dx = Convert.ToDouble(textBox6.Text);
            if (textBox5.Text.Length != 0)
                dy = Convert.ToDouble(textBox5.Text);
            if (textBox4.Text.Length != 0)
                dz = Convert.ToDouble(textBox4.Text);


            T[3, 0] = dx;
            T[3, 1] = dy;
            T[3, 2] = dz;

            for (int i = 0; i < points.Count; i++)
            {
                double[] new_p = new double[4];
                new_p[0] = points[i].x;
                new_p[1] = points[i].y;
                new_p[2] = points[i].z;
                new_p[3] = 1;

                double[] res = new double[4];
                for (int j = 0; j < 4; j++)
                    res[j] = 0;

                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        res[k] += new_p[l] * T[l, k];
                    }
                }

                points[i].x = res[0]; //(int)res[0];
                points[i].y = res[1]; //(int)res[1];
                points[i].z = res[2]; //(int)res[2];
            }
        }

        void Scale()
        {
            double[,] T = getOneMatr(4);

            double dx = 1;
            double dy = 1;
            double dz = 1;

            if (textBox9.Text.Length != 0)
                dx = Convert.ToDouble(textBox9.Text);
            if (textBox8.Text.Length != 0)
                dy = Convert.ToDouble(textBox8.Text);
            if (textBox7.Text.Length != 0)
                dz = Convert.ToDouble(textBox7.Text);


            T[0, 0] = dx;
            T[1, 1] = dy;
            T[2, 2] = dz;

            for (int i = 0; i < points.Count; i++)
            {
                double[] new_p = new double[4];
                new_p[0] = points[i].x;
                new_p[1] = points[i].y;
                new_p[2] = points[i].z;
                new_p[3] = 1;

                double[] res = new double[4];
                for (int j = 0; j < 4; j++)
                    res[j] = 0;

                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        res[k] += new_p[l] * T[l, k];
                    }
                }

                points[i].x = (int)res[0];
                points[i].y = (int)res[1];
                points[i].z = (int)res[2];
            }
        }

        void MirrorX()
        {
            double[,] res = getOneMatr(4);

            res[0, 0] = -1;
            Mirror(res);
        }

        void MirrorY()
        {
            double[,] res = getOneMatr(4);

            res[1, 1] = -1;
            Mirror(res);
        }

        void MirrorZ()
        {
            double[,] res = getOneMatr(4);

            res[2, 2] = -1;
            Mirror(res);
        }

        void Mirror(double[,] T)
        {
            for (int i = 0; i < points.Count; i++)
            {
                double[] new_p = new double[4];
                new_p[0] = points[i].x;
                new_p[1] = points[i].y;
                new_p[2] = points[i].z;
                new_p[3] = 1;

                double[] res = new double[4];
                for (int j = 0; j < 4; j++)
                    res[j] = 0;

                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        res[k] += new_p[l] * T[l, k];
                    }
                }

                points[i].x = (int)res[0];
                points[i].y = (int)res[1];
                points[i].z = (int)res[2];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            makeCube(); // заполнение точек и ребер
            showCube();  // отрисовка. если другая проекция, то переделать вывод у
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clean();
            if (textBox1.Text.Length == 0)
                RotateX(30);
            else
                RotateX(Convert.ToDouble(textBox1.Text));
            showCube();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clean();
            if (textBox2.Text.Length == 0)
                RotateY(30);
            else
                RotateY(Convert.ToDouble(textBox2.Text));
            showCube();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clean();
            if (textBox3.Text.Length == 0)
                RotateZ(30);
            else
                RotateZ(Convert.ToDouble(textBox3.Text));
            showCube();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clean();
            points = new List<point>();
            edges = new List<edge>();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clean();
            Translate();
            showCube();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clean();
            Translate();
            showCube();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clean();
            Translate();
            showCube();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            clean();
            Scale();
            showCube();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            clean();
            Scale();
            showCube();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clean();
            Scale();
            showCube();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clean();
            MirrorX();
            showCube();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            clean();
            MirrorY();
            showCube();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            clean();
            MirrorZ();
            showCube();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
