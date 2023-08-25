using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    /// <summary>
    /// 换气扇
    /// </summary>
    public class VentilatingFan : HAVC
    {
        public Wall Host { get; set; }

        /// <summary>
        /// 墙面的朝向
        /// </summary>
        public Vector3D Direction { get; set; }
    }
}
