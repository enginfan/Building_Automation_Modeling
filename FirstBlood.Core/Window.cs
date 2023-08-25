using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class Window : Furniture
    {
        public Wall Host { get; set; }
        public double Height { get; set; }
    }
}
