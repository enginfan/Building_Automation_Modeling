using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Geometry
{
    public static class Extension
    {
        public static bool AreEqual(this double source1, double source2)
        {
            return Math.Abs(source1 - source2) < 1E-05;
        }
    }
}
