using Autodesk.Revit.DB;
using FirstBlood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWall = Autodesk.Revit.DB.Wall;
using FWall = FirstBlood.Core.Wall;
using FirstBlood.Geometry;
using FirstBlood.APIHelper;

namespace FirstBlood.Transform
{
    public class WallExtractor
    {
        public FWall Create(AWall aWall)
        {
            var fWall = new FWall();
            fWall.TypeName = aWall.WallType.Name;
            fWall.Location = CoordConverter.Curve2Line3D((aWall.Location as LocationCurve).Curve);
            fWall.ReferenceId = aWall.Id.IntegerValue;
            fWall.Thickness = aWall.WallType.Width * 304.8;
            fWall.BaseLevelId = aWall.LevelId.IntegerValue;
            var para = aWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
            if (para != null)
                fWall.TopLevelId = para.AsElementId().IntegerValue;
            else fWall.TopLevelId = 0;
            return fWall;
        }
    }
}
