using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Builder.InstanceManager
{
    public class AccessoryManager : InstanceManager
    {

        public AccessoryManager(Document document, string familyName, string typeName, BuiltInCategory category) : base(document)
        {
            GetElementType(familyName, typeName, category, typeof(FamilySymbol));
        }

        public FamilyInstance Create(Level baseLevel, XYZ location, List<Tuple<XYZ, double>> rotatePairs)
        {
            //location = location - XYZ.BasisZ * baseLevel.Elevation;
            FamilyInstance familyInstance = Document.Create.NewFamilyInstance(location, ElementType as FamilySymbol, baseLevel, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            //FamilyInstance familyInstance = Document.Create.NewFamilyInstance(location, FamilySymbol,  Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            familyInstance.get_Parameter(BuiltInParameter.INSTANCE_ELEVATION_PARAM).Set(location.Z - baseLevel.Elevation);
            for (int i = 0; i < rotatePairs.Count; i++)
            {
                ElementTransformUtils.RotateElement(Document, familyInstance.Id, Line.CreateBound(location, location + rotatePairs[i].Item1), rotatePairs[i].Item2);
            }

            return familyInstance;
        }

    }
}
