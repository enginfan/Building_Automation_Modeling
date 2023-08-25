using FirstBlood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.View.Model
{
    public class RoomEventArgs
    {
        public RoomType Type { get; set; }

        public double Angle { get; set; }

        public RoleType Role { get; set; }
    }
}
