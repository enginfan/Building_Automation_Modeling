using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.APIHelper
{
    public static class GeometryObjectHelper
    {
        public static List<Autodesk.Revit.DB.GeometryObject> GetGeometryObjects(this Autodesk.Revit.DB.Element elem, Autodesk.Revit.DB.Options options = default(Autodesk.Revit.DB.Options))
        {
            var result = new List<Autodesk.Revit.DB.GeometryObject>();
            options = options ?? new Autodesk.Revit.DB.Options();
            RecursionObject(elem.get_Geometry(options), ref result);
            return result;
        }

        public static void RecursionObject(this Autodesk.Revit.DB.GeometryElement geometryElement, ref List<Autodesk.Revit.DB.GeometryObject> geometryObjects)
        {
            if (geometryElement == null) return;
            var eum = geometryElement.GetEnumerator();
            while (eum.MoveNext())
            {
                switch (eum.Current)
                {
                    case Autodesk.Revit.DB.GeometryInstance instance:
                        instance.SymbolGeometry.RecursionObject(ref geometryObjects);
                        break;
                    case Autodesk.Revit.DB.GeometryElement element:
                        element.RecursionObject(ref geometryObjects);
                        break;
                    case Autodesk.Revit.DB.Solid solid:
                        if (solid.Edges.Size == 0 || solid.Faces.Size == 0) continue;
                        geometryObjects.Add(eum.Current);
                        break;
                    default:
                        geometryObjects.Add(eum.Current);
                        break;
                }
            }
        }
    }
}
