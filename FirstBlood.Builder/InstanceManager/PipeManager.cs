using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Builder.InstanceManager
{
    public class PipeManager : InstanceManager
    {

        public PipeManager(Document document, string familyName, string typeName, BuiltInCategory category) : base(document)
        {
            GetElementType(familyName, typeName, BuiltInCategory.OST_PipeCurves, typeof(PipeType));
        }

        public Pipe CreatePipe(Level level, XYZ start, XYZ end, PipingSystemType systemType, double diameter)
        {
            var pipe = Pipe.Create(Document, systemType.Id, ElementType.Id, level.Id, start, end);
            pipe.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).Set(diameter / 304.8);
            return pipe;
        }
    }
}
