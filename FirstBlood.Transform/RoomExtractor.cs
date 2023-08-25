using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARoom = Autodesk.Revit.DB.Architecture.Room;
using FRoom = FirstBlood.Core.Room;
using FirstBlood.Geometry;
using FirstBlood.APIHelper;

namespace FirstBlood.Transform
{
    public class RoomExtractor
    {
        public FRoom Create(ARoom aRoom, XYZ dir)
        {
            var newRoom = new FRoom();
            newRoom.NameTag = aRoom.Name;
            var referenceWalls = GetRoomReferenceWalls(aRoom);
            var wallExtractor = new WallExtractor();
            newRoom.ReferenceWalls = referenceWalls.Select(x => wallExtractor.Create(x)).ToList();
            newRoom.Direction = CoordConverter.XYZ2Vector3D(dir).Normalize();
            newRoom.Height = aRoom.get_Parameter(BuiltInParameter.ROOM_UPPER_OFFSET).AsDouble() * 304.8;
            var outlines = GetRoomOutlines(referenceWalls);
            newRoom.Central = GetRoomCentralPoint(outlines);
            newRoom.LevelId = aRoom.LevelId.IntegerValue;
            return newRoom;
        }

        private List<Wall> GetRoomReferenceWalls(ARoom room)
        {
            var doc = room.Document;
            var dic = new Dictionary<ElementId, int>();
            var wallList = new List<Wall>();
            var options = new SpatialElementBoundaryOptions();
            options.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
            options.StoreFreeBoundaryFaces = true;
            var boundaries = room.GetBoundarySegments(options);
            foreach (var item in boundaries)
            {
                foreach (var segment in item)
                {
                    var wall = doc.GetElement(segment.ElementId) as Autodesk.Revit.DB.Wall;
                    var curve = segment.GetCurve();
                    if (wall == null) continue;
                    if (!dic.Keys.Contains(wall.Id))
                    {
                        dic.Add(wall.Id, 0);
                        wallList.Add(wall);
                    }
                    else { dic[wall.Id]++; }
                }
            }
            return wallList;
        }


        private List<Line3D> GetRoomOutlines(List<Wall> referenceWalls)
        {
            if (referenceWalls.Count != 4)
                throw new Exception("暂时只支持4片墙的房间");
            var lines = referenceWalls.Select(x => (x.Location as LocationCurve).Curve).Select(x => CoordConverter.Curve2Line3D(x)).ToList();
            var line0 = lines[0];
            var outlines = new List<Line3D>();
            for (int j = 0; j < lines.Count; j++)
            {
                var points = new List<Vector3D>();
                for (int i = 0; i < lines.Count; i++)
                {
                    var point = lines[i].Intersect(lines[j]);
                    if (point != null)
                    {
                        points.Add(point);
                    }
                }
                if (points.Count == 2)
                    outlines.Add(Line3D.Create(points[0], points[1]));
            }
            return outlines;

        }

        private Vector3D GetRoomCentralPoint(List<Line3D> outlines)
        {
            return (outlines[0].Evaluate(0.5) + outlines[2].Evaluate(0.5)) / 2;
        }
    }
}
