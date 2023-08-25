using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class Fitting : MEPModel
    {
        public List<FittingCore> Connectors { get; set; } = new List<FittingCore>();

        public FittingType Type { get; set; }

        public string FittingTypeName { get; set; }

        public double Diameter { get; set; }
    }

    public enum FittingType
    {
        None,
        /// <summary>
        /// 弯管
        /// </summary>
        Elbow,
        /// <summary>
        /// 三通
        /// </summary>
        Tee,
        /// <summary>
        /// 四通
        /// </summary>
        Cross,
        /// <summary>
        /// 存水弯
        /// </summary>
        P,
        /// <summary>
        /// 阀门
        /// </summary>
        V,
        /// <summary>
        /// 清扫口之类 
        /// </summary>
        Other

    }
}
