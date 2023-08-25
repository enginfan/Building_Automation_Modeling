using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class Door : Furniture
    {
        public Wall Host { get; set; }

        public bool FlipHand { get; set; }

        public bool FlipFace { get; set; }
    }
}
