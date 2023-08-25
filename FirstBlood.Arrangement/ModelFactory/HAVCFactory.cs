using FirstBlood.Core;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Arrangement.ModelFactory
{
    public class HAVCFactory
    {
        public static VentilatingFan ArrangeFan(string familyName, string typeName, Vector3D location, Wall host, Vector3D dir, Vector3D roomDir, double rotation)
        {
            var fan = new VentilatingFan();
            fan.Location = location;
            fan.FamilyName = familyName;
            fan.TypeName = typeName;
            fan.Host = host;
            fan.Direction = dir;
            fan.RoomDirection = roomDir;
            fan.Rotation = rotation;
            return fan;
        }
    }
}
