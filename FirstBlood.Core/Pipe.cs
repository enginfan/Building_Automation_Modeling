using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class Pipe : MEPModel
    {
        public Vector3D Start { get; set; }

        public Vector3D End { get; set; }

        public double Diameter { get; set; }

        public string FamilyName { get; set; }

        public string TypeName { get; set; }

        public PipeType Type { get; set; }

    }

    public enum PipeType
    {
        /// <summary>
        /// 给水
        /// </summary>
        J,
        /// <summary>
        /// 污水
        /// </summary>
        W
    }
}
