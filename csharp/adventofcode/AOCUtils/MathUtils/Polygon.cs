﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.MathUtils
{
    public class Polygon
    {
        public List<Coordinate> Points { get; set; } = new List<Coordinate>();

        public void AddPoint(long x, long y)
        {
            AddPoint(new Coordinate(x, y));
        }

        public void AddPoint(Coordinate coord)
        {
            Points.Add(coord);
        }

        public double CalculateInclusiveArea()
        {
            var area = CalculateArea();
            var perm = CalculatePerimeter();

            return area + (perm / 2) + 1;
        }

        public double CalculateArea()
        {
            int n = Points.Count;
            double a = 0.0;
            for (int i = 0; i < n - 1; i++)
            {
                a += Points[i].X * Points[i + 1].Y - Points[i + 1].X * Points[i].Y;
            }
            return Math.Abs(a + Points[n - 1].X * Points[0].Y - Points[0].X * Points[n - 1].Y) / 2.0;    
        }

        public double CalculatePerimeter()
        {
            double p = 0.0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                p += Math.Sqrt(Math.Pow(Points[i + 1].X - Points[i].X, 2) + Math.Pow(Points[i + 1].Y - Points[i].Y, 2));
            }
            p += Math.Sqrt(Math.Pow(Points[0].X - Points[Points.Count - 1].X, 2) + Math.Pow(Points[0].Y - Points[Points.Count - 1].Y, 2));
            return p;
        }

    }
}
