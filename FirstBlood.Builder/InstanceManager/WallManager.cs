using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Builder.InstanceManager
{
    public class WallManager : InstanceManager
    {
        public WallManager(Document doc, string familyName, string typeName) : base(doc)
        {
            GetElementType(familyName, typeName, BuiltInCategory.OST_Walls, typeof(WallType));
        }

        public Wall Create(Curve locationCurve, ElementId baseLevelId, double height)
        {
            return Wall.Create(Document, locationCurve, ElementType.Id, baseLevelId, height / 304.8, 0, false, false);
        }
    }
}
