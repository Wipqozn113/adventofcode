using System;
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

        /// <summary>
        /// Calcualtes the total area, including the edges.
        /// </summary>
        /// <returns></returns>
        public double CalculateInclusiveArea()
        {
            var area = CalculateArea();
            var perm = CalculatePerimeter();

            return area + (perm / 2) + 1;
        }

        public double CalculateArea()
        {
            // Shoelace formula
            var area = 0.0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                area += Points[i].X * Points[i + 1].Y - Points[i + 1].X * Points[i].Y;
            }
            return Math.Abs(area + Points[Points.Count - 1].X * Points[0].Y - Points[0].X * Points[Points.Count - 1].Y) / 2.0;    
        }

        public double CalculatePerimeter()
        {
            var perm = 0.0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
               perm += Math.Sqrt(Math.Pow(Points[i + 1].X - Points[i].X, 2) + Math.Pow(Points[i + 1].Y - Points[i].Y, 2));
            }
            perm += Math.Sqrt(Math.Pow(Points[0].X - Points[Points.Count - 1].X, 2) + Math.Pow(Points[0].Y - Points[Points.Count - 1].Y, 2));
            return perm;
        }

    }
}
