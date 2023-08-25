using FirstBlood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FirstBlood.View.Model
{
    public class RoomModel
    {
        public ImageSource Thumbnail { get; set; }

        public RoomType RoomType { get; set; }

        public int Id { get; set; }


        public string Name { get; set; }
    }
}
