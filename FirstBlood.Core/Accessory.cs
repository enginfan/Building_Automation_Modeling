using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    /// <summary>
    /// 管道附件（阀门等）
    /// </summary>
    public class Accessory : Fitting
    {
        public Vector3D Location { get; set; }

        public string FamilyName { get; set; }

        public string TypeName { get; set; }
        /// <summary>
        /// 旋转轴指向的方向和对应的角度
        /// </summary>
        public List<Tuple<Vector3D, double>> RotatePairs { get; set; } = new List<Tuple<Vector3D, double>>();

        public AType TubeType { get; set; }
    }
    public enum AType
    {
        Pipe,
        Duct
    }
}
