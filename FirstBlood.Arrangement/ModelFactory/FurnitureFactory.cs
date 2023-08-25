using FirstBlood.Core;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Arrangement.ModelFactory
{
    public class FurnitureFactory
    {
        public static Door ArrangeDoor(string familyName, string typeName, Vector3D location, Wall host, bool flipFace, bool flipHand)
        {
            var door = new Door();
            door.FamilyName = familyName;
            door.TypeName = typeName;
            door.Location = location;
            door.Host = host;
            door.FlipFace = flipFace;
            door.FlipHand = flipHand;
            return door;
        }

        public static Window ArrangeWindow(string familyName, string typeName, Vector3D location, Wall host, double height)
        {
            var window = new Window();
            window.FamilyName = familyName;
            window.TypeName = typeName;
            window.Location = location;
            window.Host = host;
            window.Height = height;
            return window;
        }       
    }
}
