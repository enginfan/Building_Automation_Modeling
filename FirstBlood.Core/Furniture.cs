using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public abstract class Furniture
    {
        public string FamilyName { get; set; }

        public string TypeName { get; set; }

        public Vector3D Location { get; set; }

        public double Rotation { get; set; }
    }
}
