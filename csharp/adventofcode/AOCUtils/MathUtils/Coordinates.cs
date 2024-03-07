using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCUtils.MathUtils
{
    public class Coordinate
    {
        public Coordinate(long x, long y)
        {
            X = x;
            Y = y;
        }

        public long X { get; set; }

        public long Y { get; set; }  
    
        public long ManhattenDistance(Coordinate other) 
        {
            if (other is null)
                return -1;

            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }
    }

    public class Coordinate3D
    {
        public Coordinate3D(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public long X { get; set; } 

        public long Y { get; set; }

        public long Z { get; set; }
    }
}
