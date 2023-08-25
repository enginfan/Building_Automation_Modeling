using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Builder.InstanceManager
{
    public class FurnitureManager : InstanceManager
    {
        public FurnitureManager(Document document, string familyName, string typeName, BuiltInCategory category) : base(document)
        {
            GetElementType(familyName, typeName, category, typeof(FamilySymbol));
        }

        public FamilyInstance Create(Level baseLevel, XYZ location, double angle, Element host = null)
        {
            FamilyInstance familyInstance = null;
            if (host == null)
                familyInstance = Document.Create.NewFamilyInstance(location, ElementType as FamilySymbol, baseLevel, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            else
                familyInstance = Document.Create.NewFamilyInstance(location, ElementType as FamilySymbol, host, baseLevel, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            ElementTransformUtils.RotateElement(Document, familyInstance.Id, Line.CreateBound(location, location + XYZ.BasisZ), angle);
            return familyInstance;
        }

        public FamilyInstance Create(XYZ location, double angle, Element host, XYZ facedir)
        {

            var familyInstance = Document.Create.NewFamilyInstance(location, ElementType as FamilySymbol, facedir, host, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            ElementTransformUtils.RotateElement(Document, familyInstance.Id, Line.CreateBound(location, location + XYZ.BasisZ), angle);
            return familyInstance;
        }
    }
}
