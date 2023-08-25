using Autodesk.Revit.DB;
using FirstBlood.APIHelper;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FRoom = FirstBlood.Core.Room;
using AWall = Autodesk.Revit.DB.Wall;
using FirstBlood.Builder.InstanceManager;
using AFace = Autodesk.Revit.DB.Face;
using FirstBlood.Core;
using Autodesk.Revit.DB.Plumbing;

namespace FirstBlood.Builder
{
    public class RoomBuilder
    {
        private Document doc;
        public RoomBuilder(Document doc)
        {
            this.doc = doc;
        }
        public void Build(FRoom room)
        {
            var angle = room.Direction.AngleFrom(Vector3D.BasisY);
            var central = CoordConverter.Vector3D2XYZ(room.Central);
            var level = doc.GetElement(new ElementId(room.LevelId)) as Level;
            var transformR = Transform.CreateRotationAtPoint(XYZ.BasisZ, angle, central);
            var transformT = Transform.CreateTranslation(central);
            var transformTWithoutLevel = Transform.CreateTranslation(central - XYZ.BasisZ * level.Elevation);
            var transform = transformR * transformT;
            var test = transform.OfPoint(new XYZ(0, -10, 3));
            var transformWithoutLevel = transformR * transformTWithoutLevel;
            var roomRotation = room.Direction.AngleFrom(Vector3D.BasisY);
            //房间内墙体
            room.Walls.ForEach(x => BuildWall(x, central, transform, room.Height));
            //房间内家具（门、窗、床等）
            room.Furniture.ForEach(x => BuildFurniture(x, level, central, transform));
            //暖通相关
            room.HAVCs.ForEach(x => BuildHAVC(x, level, central, transform));
            //电器元器件（固定装置：插座、灯）
            room.Fixtures.ForEach(x => BuildFixture(x, level, angle, transformWithoutLevel));
            //管道类型
            room.Pipes.ForEach(x => BuildPipe(x, level, transform));

            //洗涤用具
            room.PlumbingFixtures.ForEach(x => BuildPlumbingFixture(x, level, transform, roomRotation));

            //阀门
            room.Accessories.ForEach(x => BuildAccessories(x, level, transform, roomRotation));

            //连通器
            var i = 0;
            room.Fittings.ForEach(x =>
            {
                BuildFitting(x, transform);
                i++;
            });
        }

        private FamilyInstance BuildFitting(Core.Fitting fitting, Transform transform)
        {
            try
            {
                FamilyInstance instance = null;
                if (fitting.Connectors.Count == 2)
                {
                    var con1 = GetConnector(fitting.Connectors[0].Model, fitting.Connectors[0].EndPoint, transform);
                    var con2 = GetConnector(fitting.Connectors[1].Model, fitting.Connectors[1].EndPoint, transform);
                    instance = doc.Create.NewElbowFitting(con1, con2);
                    fitting.RevitId = instance.Id.IntegerValue;
                }
                else if (fitting.Connectors.Count == 3)
                {
                    var con1 = GetConnector(fitting.Connectors[0].Model, fitting.Connectors[0].EndPoint, transform);
                    var con2 = GetConnector(fitting.Connectors[1].Model, fitting.Connectors[1].EndPoint, transform);
                    var con3 = GetConnector(fitting.Connectors[2].Model, fitting.Connectors[2].EndPoint, transform);
                    instance = doc.Create.NewTeeFitting(con1, con2, con3);
                    if (instance != null && fitting.FittingTypeName != null)
                    {
                        var familyManager = new FurnitureManager(doc, fitting.FittingTypeName, "标准", BuiltInCategory.OST_PipeFitting);
                        if (familyManager.ElementType != null)
                            instance.Symbol = familyManager.ElementType as FamilySymbol;
                    }
                    fitting.RevitId = instance.Id.IntegerValue;
                }
                else if (fitting.Connectors.Count == 4)
                {
                    var con1 = GetConnector(fitting.Connectors[0].Model, fitting.Connectors[0].EndPoint, transform);
                    var con2 = GetConnector(fitting.Connectors[1].Model, fitting.Connectors[1].EndPoint, transform);
                    var con3 = GetConnector(fitting.Connectors[2].Model, fitting.Connectors[2].EndPoint, transform);
                    var con4 = GetConnector(fitting.Connectors[3].Model, fitting.Connectors[3].EndPoint, transform);
                    instance = doc.Create.NewCrossFitting(con1, con2, con3, con4);
                    fitting.RevitId = instance.Id.IntegerValue;
                }
                if (instance != null && fitting.Diameter != 0)
                {
                    var para = instance.LookupParameter("公称半径");
                    if (para != null)
                        para.Set(fitting.Diameter / 2 / 304.8);
                }
                return instance;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }


        private Autodesk.Revit.DB.Plumbing.Pipe BuildPipe(FirstBlood.Core.Pipe pipe, Level level, Transform transform)
        {
            var start = transform.OfPoint(CoordConverter.Vector3D2XYZ(pipe.Start));
            var end = transform.OfPoint(CoordConverter.Vector3D2XYZ(pipe.End));
            var familyManager = new PipeManager(doc, pipe.FamilyName, pipe.TypeName, BuiltInCategory.OST_PipeCurves);
            PipingSystemType pipeSystem = null;
            var collector = new FilteredElementCollector(doc).OfClass(typeof(PipingSystemType)).Cast<PipingSystemType>().ToList();
            if (pipe.Type == Core.PipeType.J)
                pipeSystem = collector.Find(x => x.Name == "生活给水");
            else if (pipe.Type == Core.PipeType.W)
                pipeSystem = collector.Find(x => x.Name == "生活排水");
            var revitPipe = familyManager.CreatePipe(level, start, end, pipeSystem, pipe.Diameter);
            pipe.RevitId = revitPipe.Id.IntegerValue;
            return revitPipe;

        }

        private FamilyInstance BuildAccessories(Accessory pipeAcc, Level level, Transform transform, double roomRotation)
        {
            var category = BuiltInCategory.INVALID;
            if (pipeAcc.TubeType == AType.Pipe)
            {
                if (pipeAcc.Type == FittingType.P)
                {
                    category = BuiltInCategory.OST_PipeFitting;
                }
                else category = BuiltInCategory.OST_PipeAccessory;
            }
            else category = BuiltInCategory.OST_DuctAccessory;

            var familyManager = new AccessoryManager(doc, pipeAcc.FamilyName, pipeAcc.TypeName, category);
            var p = CoordConverter.Vector3D2XYZ(pipeAcc.Location);
            var realLocation = transform.OfPoint(CoordConverter.Vector3D2XYZ(pipeAcc.Location));
            var rotatePairs1 = pipeAcc.RotatePairs.Select(x => Tuple.Create(transform.OfVector(CoordConverter.Vector3D2XYZ(x.Item1)), pipeAcc.Type == FittingType.V ? x.Item2 : x.Item2 + roomRotation)).ToList();
            var rotatePairs = pipeAcc.RotatePairs.Select(x =>
            {
                var axis = transform.OfVector(CoordConverter.Vector3D2XYZ2(x.Item1));
                var rotation = x.Item2;
                if (pipeAcc.Type == FittingType.P || pipeAcc.Type == FittingType.Other || pipeAcc.Type == FittingType.V)
                    axis = CoordConverter.Vector3D2XYZ2(x.Item1);
                if (pipeAcc.Type == FittingType.P || pipeAcc.Type == FittingType.V)
                {
                    if (axis.IsAlmostEqualTo(XYZ.BasisZ))
                        rotation += roomRotation;
                }

                return Tuple.Create(axis, rotation);
            }).ToList();
            var accessory = familyManager.Create(level, realLocation, rotatePairs);
            if (pipeAcc.Diameter != 0)
            {
                var param = accessory.LookupParameter("公称半径");
                if (param == null)
                {
                    param = accessory.LookupParameter("风管半径");
                }
                if (param != null)
                    param.Set(pipeAcc.Diameter / 2 / 304.8);
            }
            var cons = accessory.MEPModel.ConnectorManager.Connectors;
            var pipeCons = pipeAcc.Connectors.Select(x => GetConnector(x.Model, x.EndPoint, transform)).ToList();
            var enumerator = cons.GetEnumerator();
            var valveCons = new List<Connector>();
            while (enumerator.MoveNext())
            {
                valveCons.Add(enumerator.Current as Connector);
            }
            var conPairs = new List<Tuple<Connector, Connector>>();
            foreach (var con1 in valveCons)
            {
                foreach (var con2 in pipeCons)
                {
                    if (con1.Origin.IsAlmostEqualTo(con2.Origin))
                    {
                        conPairs.Add(Tuple.Create(con1, con2));
                        break;
                    }
                }
            }
            
            try
            {
                foreach (var item in conPairs)
                {
                    item.Item1.ConnectTo(item.Item2);
                }
            }
            catch (Exception e)
            {

            }

            pipeAcc.RevitId = accessory.Id.IntegerValue;
            return accessory;

        }

        private Connector GetConnector(Core.MEPModel mep, Vector3D origin, Transform transform)
        {
            var revitPipe = doc.GetElement(new ElementId(mep.RevitId)) as Autodesk.Revit.DB.MEPCurve;
            var conManager = revitPipe.ConnectorManager.Connectors;
            var endPoint = transform.OfPoint(CoordConverter.Vector3D2XYZ(origin));
            foreach (Connector item in conManager)
            {
                if (item.Origin.DistanceTo(endPoint) < 1 / 304.8)
                    return item;
                //if (item.Origin.IsAlmostEqualTo(endPoint))
                //    return item;
            }
            return null;
        }

        private FamilyInstance BuildFixture(Fixture x, Level level, double roomRotation, Transform transform)
        {
            var realLocation = transform.OfPoint(CoordConverter.Vector3D2XYZ(x.Location));
            var category = BuiltInCategory.INVALID;
            switch (x.Type)
            {
                case FixtureType.Lighting:
                    category = BuiltInCategory.OST_LightingFixtures;
                    break;
                case FixtureType.Plugin:
                    category = BuiltInCategory.OST_ElectricalFixtures;
                    break;
                case FixtureType.Equipment:
                    category = BuiltInCategory.OST_ElectricalEquipment;
                    break;
            }
            var familyManager = new FurnitureManager(doc, x.FamilyName, x.TypeName, category);
            var family = familyManager.Create(level, realLocation, x.Rotation + roomRotation);
            //family.flipFacing();
            return family;


        }

        private FamilyInstance BuildHAVC(HAVC x, Level level, XYZ central, Transform transform)
        {
            var realLocation = CoordConverter.Vector3D2XYZ(x.Location);

            var familyManager = new HavcManager(doc, x.FamilyName, x.TypeName, BuiltInCategory.OST_MechanicalEquipment);
            if (x is VentilatingFan fan)
            {
                var wall = doc.GetElement(new ElementId(fan.Host.ReferenceId)) as AWall;
                var wallSolid = wall.GetGeometryObjects(new Options { ComputeReferences = true }).OfType<Solid>().FirstOrDefault();
                var realDirection = CoordConverter.Vector3D2XYZ(fan.Direction);
                AFace face = null;
                foreach (AFace wallFace in wallSolid.Faces)
                {
                    if (wallFace is PlanarFace pl && pl.FaceNormal.IsAlmostEqualTo(CoordConverter.Vector3D2XYZ(x.RoomDirection).Normalize()))
                    {
                        face = wallFace;
                        break;
                    }
                }
                BoundingBoxUV bboxUV = face.GetBoundingBox();
                var location = transform.OfPoint(CoordConverter.Vector3D2XYZ(fan.Location));
                UV center = (bboxUV.Max + bboxUV.Min) / 2.0;
                //XYZ location = face.Evaluate(center);
                XYZ normal = face.ComputeNormal(center);
                XYZ refDir = normal.CrossProduct(XYZ.BasisZ);

                if (face != null)
                {
                    var family = familyManager.CreateInstance(face, location, refDir, fan.Rotation);
                    //family.flipFacing();
                    x.RevitId = family.Id.IntegerValue;
                    return family;

                }
            }
            else
            {
                var location = transform.OfPoint(realLocation);
                location = location - XYZ.BasisZ * level.Elevation;
                var family = familyManager.CreateInstance(level, location, x.Rotation);
                //family.flipFacing();
                x.RevitId = family.Id.IntegerValue;
                return family;
            }

            return null;

        }

        public FamilyInstance BuildFurniture(Furniture furniture, Level level, XYZ referencePoint, Transform transform)
        {
            var realLocation = CoordConverter.Vector3D2XYZ(furniture.Location);
            FurnitureManager furnitureMaker = null;
            if (furniture is Door door)
            {
                furnitureMaker = new FurnitureManager(doc, furniture.FamilyName, furniture.TypeName, BuiltInCategory.OST_Doors);
                var familyInstance = furnitureMaker.Create(level, transform.OfPoint(realLocation), furniture.Rotation, doc.GetElement(new ElementId(door.Host.ReferenceId)));

                if (door.FlipFace) familyInstance.flipFacing();
                if (door.FlipHand) familyInstance.flipHand();
                return familyInstance;
            }
            else if (furniture is Window window)
            {
                furnitureMaker = new FurnitureManager(doc, furniture.FamilyName, furniture.TypeName, BuiltInCategory.OST_Windows);
                var familyinstance = furnitureMaker.Create(level, transform.OfPoint(realLocation), furniture.Rotation, doc.GetElement(new ElementId(window.Host.ReferenceId)));
                familyinstance.LookupParameter("底高度").Set(window.Height / 304.8);
                return familyinstance;
            }
            return null;

        }

        private FamilyInstance BuildPlumbingFixture(PlumbingFixture pFixture, Level level, Transform transform, double roomRotation)
        {
            var familyManager = new HavcManager(doc, pFixture.FamilyName, pFixture.TypeName, BuiltInCategory.OST_PlumbingFixtures);
            var realLocation = transform.OfPoint(CoordConverter.Vector3D2XYZ(pFixture.Location));
            if (pFixture.Host == null)
            {
                var location = realLocation - XYZ.BasisZ * level.Elevation;
                var fixtureP = familyManager.CreateInstance(level, location, pFixture.Rotation + roomRotation);
                pFixture.RevitId = fixtureP.Id.IntegerValue;
                return fixtureP;
            }
            var hostWall = doc.GetElement(new ElementId(pFixture.Host.ReferenceId)) as AWall;
            var wallSolid = hostWall.GetGeometryObjects(new Options { ComputeReferences = true }).OfType<Solid>().FirstOrDefault();
            AFace face = null;
            var fitDirection = transform.OfVector(CoordConverter.Vector3D2XYZ2(pFixture.FaceDir));
            foreach (AFace wallFace in wallSolid.Faces)
            {
                if (wallFace is PlanarFace pl && pl.FaceNormal.IsAlmostEqualTo(fitDirection))
                {
                    face = wallFace;
                    break;
                }
            }
            BoundingBoxUV bboxUV = face.GetBoundingBox();
            UV center = (bboxUV.Max + bboxUV.Min) / 2.0;
            XYZ normal = face.ComputeNormal(center);
            var refDir = XYZ.Zero;
            var fixture = familyManager.CreateInstance(face, realLocation, refDir, pFixture.Rotation);

            pFixture.RevitId = fixture.Id.IntegerValue;
            return fixture;
        }

        private AWall BuildWall(FirstBlood.Core.Wall bWall, XYZ referencePoint, Transform transform, double height)
        {

            var wallMaker = new WallManager(doc, bWall.FamilyName, bWall.TypeName);
            var wall = wallMaker.Create(Line.CreateBound(transform.OfPoint(CoordConverter.Vector3D2XYZ(bWall.Location.Start)), transform.OfPoint(CoordConverter.Vector3D2XYZ(bWall.Location.End))), new ElementId(bWall.BaseLevelId), height);
            bWall.ReferenceId = wall.Id.IntegerValue;
            return wall;

        }
    }
}
