using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    [Flags]
    public enum RoleType
    {
        /// <summary>
        /// 啥都没有
        /// </summary>
        NONE = 0,
        /// <summary>
        /// 建筑
        /// </summary>
        A = 2,
        /// <summary>
        /// 给排水
        /// </summary>
        W = 4,
        /// <summary>
        /// 电气
        /// </summary>
        E = 8,
        /// <summary>
        /// 暖通
        /// </summary>
        H = 16
    }
}
