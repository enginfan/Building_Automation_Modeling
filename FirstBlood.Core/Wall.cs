using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class Wall
    {
        public string FamilyName { get; set; }

        public string TypeName { get; set; }

        public Line3D Location { get; set; }


        public int BaseLevelId { get; set; }

        public int TopLevelId { get; set; }

        public double BaseOffset { get; set; }

        public bool IsFlip { get; set; }


        public bool IsStructural { get; set; }

        public int ReferenceId { get; set; }

        public double Thickness { get; set; }
    }
}
