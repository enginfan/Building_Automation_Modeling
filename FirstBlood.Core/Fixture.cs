using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{

    public class Fixture
    {
        public Vector3D Location { get; set; }

        public string FamilyName { get; set; }

        public string TypeName { get; set; }

        public double Rotation { get; set; }

        public FixtureType Type { get; set; }
    }

    public enum FixtureType
    {
        Lighting,
        Plugin,
        Equipment
    }
}
