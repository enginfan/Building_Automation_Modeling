using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    /// <summary>
    /// 暖通元素
    /// </summary>
    public abstract class HAVC : MEPModel
    {
        public string FamilyName { get; set; }

        public string TypeName { get; set; }

        public Vector3D Location { get; set; }

        public double Rotation { get; set; }

        public Vector3D RoomDirection { get; set; }

        public double Height { get; set; }
    }
}
