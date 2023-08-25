using Autodesk.Revit.DB;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.APIHelper
{
    public class CoordConverter
    {
        public static Vector3D XYZ2Vector3D(XYZ xyz)
        {
            return new Vector3D(xyz.X * 304.8, xyz.Y * 304.8,
                xyz.Z * 304.8);
        }

        /// <summary>
        /// 将Curve转换为Line3D
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static Line3D Curve2Line3D(Curve curve)
        {
            return Line3D.Create(XYZ2Vector3D(curve.GetEndPoint(0)), XYZ2Vector3D(curve.GetEndPoint(1)));
        }

        /// <summary>
        /// Vector3D转换为XYZ
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static XYZ Vector3D2XYZ(Vector3D v)
        {
            return new XYZ(v.X / 304.8, v.Y / 304.8, v.Z
                / 304.8);
        }

        public static XYZ Vector3D2XYZ2(Vector3D v)
        {
            return new XYZ(v.X, v.Y, v.Z);
        }
    }
}
