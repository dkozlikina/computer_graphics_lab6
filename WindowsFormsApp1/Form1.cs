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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Bitmap map;

        class point
        {
            int x;
            int y;
            Color color;

            point(int x, int y, Color color)
            {
                this.x = x;
                this.y = y;
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
            point a;
            point b;
            Color color;

            edge(point a, point b, Color color)
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
