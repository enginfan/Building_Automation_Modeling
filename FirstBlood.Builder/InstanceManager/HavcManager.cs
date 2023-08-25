using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Builder.InstanceManager
{
    public class HavcManager : InstanceManager
    {

        public HavcManager(Document document, string familyName, string typeName, BuiltInCategory category) : base(document)
        {
            GetElementType(familyName, typeName, category, typeof(FamilySymbol));
        }


        public FamilyInstance CreateInstance(Face face, XYZ location, XYZ direction, double angle)
        {
            FamilyInstance familyInstance = Document.Create.NewFamilyInstance(face, location, direction, ElementType as FamilySymbol);
            //ElementTransformUtils.RotateElement(Document, familyInstance.Id, Line.CreateBound(location, location + direction), angle);
            return familyInstance;
        }

        public FamilyInstance CreateInstance(Level level, XYZ location, double rotation)
        {
            var familyInstance = Document.Create.NewFamilyInstance(location, ElementType as FamilySymbol, level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            ElementTransformUtils.RotateElement(Document, familyInstance.Id, Line.CreateBound(location, location + XYZ.BasisZ), rotation);
            return familyInstance;
        }
        public FamilyInstance CreateInstance(Level level, XYZ location, double rotation, Wall host)
        {
            var familyInstance = Document.Create.NewFamilyInstance(location, ElementType as FamilySymbol, host, level, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            ElementTransformUtils.RotateElement(Document, familyInstance.Id, Line.CreateBound(location, location + XYZ.BasisZ), rotation);
            return familyInstance;
        }
    }
}
