using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class PlumbingFixture : MEPModel
    {
        public Wall Host { get; set; }

        public Vector3D FaceDir { get; set; }

        public Vector3D Location { get; set; }

        public string FamilyName { get; set; }

        public string TypeName { get; set; }

        public double Rotation { get; set; }


        public List<FittingCore> Connectors { get; set; } = new List<FittingCore>();

    }
}
