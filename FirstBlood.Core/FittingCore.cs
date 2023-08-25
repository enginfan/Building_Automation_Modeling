using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class FittingCore
    {
        public MEPModel Model { get; private set; }

        public Vector3D EndPoint { get; private set; }

        public FittingCore(MEPModel mep, Vector3D point)
        {
            Model = mep;
            EndPoint = point;
        }

    }
}
