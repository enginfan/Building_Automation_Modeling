using FirstBlood.Arrangement.ModelFactory;
using FirstBlood.Core;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FRoom = FirstBlood.Core.Room;
using FWall = FirstBlood.Core.Wall;

namespace FirstBlood.Arrangement
{
    public class RoomArranger
    {
        private RoleType _roleType;
        private RoomType _roomType;
        public RoomArranger(RoleType roleType, RoomType roomType)
        {
            _roleType = roleType;
            _roomType = roomType;
        }

        public void Arrange(FRoom room)
        {
            switch (_roomType)
            {
                case RoomType.A:
                    ArrangeRoomA(room);
                    break;
                case RoomType.B:
                case RoomType.C:
                case RoomType.D:
                    break;
            }
        }

        private void ArrangeRoomA(FRoom room)
        {
            ArrangeRoom_A(room);
            ArrangeRoom_E(room);
            ArrangeRoom_H(room);
            ArrangeRoom_W(room);
        }

        private bool ArrangeRoom_A(FRoom room)
        {
            if (!_roleType.HasFlag(RoleType.A))
                return false;

            var wall1 = new FWall();
            wall1.Location = Line3D.Create(new Vector3D(0, 3600, 0), new Vector3D(0, -1500, 0));
            wall1.FamilyName = "基本墙";
            wall1.TypeName = "填充墙-200mm";
            wall1.BaseLevelId = room.LevelId;
            wall1.Thickness = 200;
            room.Walls.Add(wall1);
            var wall2 = new FWall();
            wall2.Location = Line3D.Create(new Vector3D(-3600, -1500, 0), new Vector3D(3600, -1500, 0));
            wall2.FamilyName = "基本墙";
            wall2.TypeName = "填充墙-200mm";
            wall2.BaseLevelId = room.LevelId;
            wall2.Thickness = 200;
            room.Walls.Add(wall2);
            var wall3 = new FWall();
            wall3.Location = Line3D.Create(new Vector3D(-2450, -1500, 0), new Vector3D(-2450, -3600, 0));
            wall3.FamilyName = "基本墙";
            wall3.TypeName = "填充墙-100mm";
            wall3.BaseLevelId = room.LevelId;
            wall3.Thickness = 100;
            room.Walls.Add(wall3);
            var wall4 = new FWall();
            wall4.Location = Line3D.Create(new Vector3D(2450, -1500, 0), new Vector3D(2450, -3600, 0));
            wall4.FamilyName = "基本墙";
            wall4.TypeName = "填充墙-100mm";
            wall4.BaseLevelId = room.LevelId;
            wall4.Thickness = 100;
            room.Walls.Add(wall4);
            var door1Location = new Vector3D(-1950, -1500, 0);
            var door2Location = new Vector3D(1950, -1500, 0);
            var door3Location = new Vector3D(-2450, -2550, 0);
            var door4Location = new Vector3D(2450, -2550, 0);
            room.Furniture.Add(FurnitureFactory.ArrangeDoor("平开木门-单扇1", "M0821", door1Location, wall2, true, true));
            room.Furniture.Add(FurnitureFactory.ArrangeDoor("平开木门-单扇1", "M0821", door2Location, wall2, true, false));
            room.Furniture.Add(FurnitureFactory.ArrangeDoor("平开木门-单扇1", "M0821", door3Location, wall3, true, false));
            room.Furniture.Add(FurnitureFactory.ArrangeDoor("平开木门-单扇1", "M0821", door4Location, wall4, false, false));

            var host = room.ReferenceWalls.Find(x => x.Location.Intersect(Line3D.Create(room.Central, room.Central + room.Direction * 10000)) != null);
            var host1 = room.ReferenceWalls.Find(x => x.Location.Intersect(Line3D.Create(room.Central, room.Central - room.Direction * 10000)) != null);

            var doorLocation1 = new Vector3D(-1800, 3600, 0);
            var doorLocation2 = new Vector3D(1800, 3600, 0);
            room.Furniture.Add(FurnitureFactory.ArrangeDoor("平开木门-单扇1", "M1021", doorLocation1, host, true, false));
            room.Furniture.Add(FurnitureFactory.ArrangeDoor("平开木门-单扇1", "M1021", doorLocation2, host, true, false));

            var windowLocation1 = new Vector3D(-2950, -3600, 0);
            var windowLocation2 = new Vector3D(2950, 3600, 0);
            room.Furniture.Add(FurnitureFactory.ArrangeWindow("平开窗10", "C0906", windowLocation1, host1, 2100));
            room.Furniture.Add(FurnitureFactory.ArrangeWindow("平开窗10", "C0906", windowLocation2, host1, 2100));

            var hostXY = room.Walls[1];
            var windowLocationX = new Vector3D(-1000, -1500, 0);
            var windowLocationY = new Vector3D(1000, -1500, 0);
            room.Furniture.Add(FurnitureFactory.ArrangeWindow("平开窗10", "C0918", windowLocationX, hostXY, 900));
            room.Furniture.Add(FurnitureFactory.ArrangeWindow("平开窗10", "C0918", windowLocationY, hostXY, 900));
            return true;
        }

        private bool ArrangeRoom_W(FRoom room)
        {
            if (!_roleType.HasFlag(RoleType.W))
                return false;
            var wallThickness = room.ReferenceWalls.FirstOrDefault().Thickness;
            var wallThickness1 = room.Walls[2].Thickness;
            var rDir1 = room.Direction;
            var rDir = Vector3D.BasisY;
            var leftDir1 = Vector3D.BasisZ.Cross(rDir);
            var leftDir = -Vector3D.BasisX;

            //顶部进水管水平
            var start = new Vector3D(-3600 + wallThickness / 2 + 100, -1500 - 365, 2800);
            var mid_0 = new Vector3D(314, -1500 - 365, 2800);
            var mid_1 = new Vector3D(314, -1500 - 365, 2800);
            var end = new Vector3D(3600 - 130, -1500 - 365, 2800);

            var mid0 = new Vector3D(314, -1500 - wallThickness / 2 - 365, 2800);
            var mid1 = new Vector3D(314, -1500 - wallThickness / 2 - 38.1, 2800);
            var valveTop = new Vector3D(314, -1500 - wallThickness / 2 - 38.1, 933.2);
            //进水管中间竖直
            var end1 = new Vector3D(314, -1500 - wallThickness / 2 - 38.1, 886.2);

            var valveBottom = new Vector3D(314, -1500 - wallThickness / 2 - 38.1, 839.2);

            var mid1_1 = new Vector3D(314, -1500 - wallThickness / 2 - 38.1, 886.2);
            var end1_1 = new Vector3D(314, -1500 - wallThickness / 2 - 38.1, 500);

            //底部进水管水平,从左边开始
            var p1 = new Vector3D(-1064.2, -1500 - wallThickness / 2 - 38.1, 500);
            var p2 = new Vector3D(-529.2, -1500 - wallThickness / 2 - 38.1, 500);
            var p3 = new Vector3D(107.95, -1500 - wallThickness / 2 - 38.1, 500);
            var p4 = new Vector3D(314.458, -1500 - wallThickness / 2 - 38.1, 500);
            var p5 = new Vector3D(670.8, -1500 - wallThickness / 2 - 38.1, 500);
            var p6 = new Vector3D(1205.8, -1500 - wallThickness / 2 - 38.1, 500);

            var pSink1B = p3;
            var pSink2B = p3 + rDir * 63.1;
            var pSinkT = pSink2B + Vector3D.BasisZ * 225;
            var pSinkIn = pSinkT - rDir * 25;
            //顶部进水管水平
            var pipe1 = PipeFactory.ArrangePipe(start, mid_0, 25, PipeType.J, "管道类型", "生活给水管");
            var pipe2 = PipeFactory.ArrangePipe(mid_0, end, 25, PipeType.J, "管道类型", "生活给水管");
            var pipe3 = PipeFactory.ArrangePipe(mid0, mid1, 25, PipeType.J, "管道类型", "生活给水管");
            var pipe4 = PipeFactory.ArrangePipe(mid1, valveTop, 25, PipeType.J, "管道类型", "生活给水管");
            var pipe5 = PipeFactory.ArrangePipe(valveBottom, end1_1, 25, PipeType.J, "管道类型", "生活给水管");

            //沐浴进水管竖直水平点位
            #region 淋浴+蹲便器
            //沐浴1
            var showerPipeVStart = start - Vector3D.BasisZ * 2800;
            var showerPipeVMid = start + Vector3D.BasisZ * 200;
            var showerPipeVEnd = showerPipeVStart + Vector3D.BasisZ * 3300;
            var showerPipeHP1 = showerPipeVMid - rDir * 213.3;
            var showerPipeHP2 = showerPipeHP1 + leftDir * 69.8;
            var showerPipeBV = showerPipeHP2 - Vector3D.BasisZ * 2100;//
            var showerPipeVP1 = showerPipeBV + Vector3D.BasisZ * 400;
            var showerPipeVP2_1 = showerPipeBV + Vector3D.BasisZ * 529.3;//淋浴水阀底
            var showerPipeVP2_2 = showerPipeBV + Vector3D.BasisZ * 569.8;//淋浴水阀顶
            var showerPipeVP2 = (showerPipeVP2_1 + showerPipeVP2_2) / 2;//淋浴水阀
            var showerPipeVP3 = showerPipeHP2;
            var showerPipeMidH = showerPipeVP1 - rDir * 372;
            var showerPipeInP = showerPipeMidH - Vector3D.BasisZ * 130.5;
            var showerLoc = new Vector3D(-3600 + wallThickness / 2, -1500 - 1010, 1100);
            var squartPipeInP = showerPipeBV;
            var squartPipeHP1 = squartPipeInP + rDir * 459.6;
            var squartPipeHP2 = squartPipeHP1 - leftDir * 420;
            var squartPipeHP3 = squartPipeHP2 - rDir * 191.3;
            var squartPipeVB = squartPipeHP3 - Vector3D.BasisZ * 175;



            //淋浴进水管（粗），从下往上
            var showerPipeV1 = PipeFactory.ArrangePipe(showerPipeVStart, start, 32, PipeType.J, "管道类型", "生活给水管");
            var showerPipeV2 = PipeFactory.ArrangePipe(start, showerPipeVMid, 32, PipeType.J, "管道类型", "生活给水管");
            var showerPipeV3 = PipeFactory.ArrangePipe(showerPipeVMid, showerPipeVEnd, 32, PipeType.J, "管道类型", "生活给水管");
            var showerPipeH1 = PipeFactory.ArrangePipe(showerPipeVMid, showerPipeHP1, 20, PipeType.J, "管道类型", "生活给水管");
            var showerPipeH2 = PipeFactory.ArrangePipe(showerPipeHP1, showerPipeHP2, 20, PipeType.J, "管道类型", "生活给水管");
            //淋浴进水管（细），从下往上
            var showerPipeV4 = PipeFactory.ArrangePipe(showerPipeBV, showerPipeVP1, 20, PipeType.J, "管道类型", "生活给水管");
            var showerPipeV5 = PipeFactory.ArrangePipe(showerPipeVP1, showerPipeVP2_1, 20, PipeType.J, "管道类型", "生活给水管");
            var showerPipeV6 = PipeFactory.ArrangePipe(showerPipeVP2_2, showerPipeVP3, 20, PipeType.J, "管道类型", "生活给水管");
            var showerPipeH3 = PipeFactory.ArrangePipe(showerPipeVP1, showerPipeMidH, 15, PipeType.J, "管道类型", "生活给水管");
            var showerPipeV7 = PipeFactory.ArrangePipe(showerPipeMidH, showerPipeInP, 15, PipeType.J, "管道类型", "生活给水管");
            var shower = PipeFactory.ArrangePlumbingFixtureByPoint(showerLoc, -Math.PI / 2, "淋浴", "淋浴");
            var sFitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeV1, start), new FittingCore(showerPipeV2, start), new FittingCore(pipe1, start) });
            var sFitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeV2, showerPipeVMid), new FittingCore(showerPipeV3, showerPipeVMid), new FittingCore(showerPipeH1, showerPipeVMid) });
            var sFitting3 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeH1, showerPipeHP1), new FittingCore(showerPipeH2, showerPipeHP1) });
            var sFitting4 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeH2, showerPipeHP2), new FittingCore(showerPipeV6, showerPipeVP3) });
            var showerValve = PipeFactory.ArrangeAccessoryByPoint(new List<FittingCore> { new FittingCore(showerPipeV6, showerPipeVP2_2), new FittingCore(showerPipeV5, showerPipeVP2_1) }, showerPipeVP2, new List<Tuple<Vector3D, double>> { Tuple.Create(-rDir, -Math.PI / 2) }, "截止阀 - 10-50 mm - 螺纹", "20 mm");
            var sFitting5 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeV4, showerPipeVP1), new FittingCore(showerPipeV5, showerPipeVP1), new FittingCore(showerPipeH3, showerPipeVP1) });
            var sFitting6 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeH3, showerPipeMidH), new FittingCore(showerPipeV7, showerPipeMidH) });
            //蹲便器给水
            var squartPipeH1 = PipeFactory.ArrangePipe(squartPipeInP, squartPipeHP1, 20, PipeType.J, "管道类型", "生活给水管");
            var squartPipeH2 = PipeFactory.ArrangePipe(squartPipeHP1, squartPipeHP2, 20, PipeType.J, "管道类型", "生活给水管");
            var squartPipeH3 = PipeFactory.ArrangePipe(squartPipeHP2, squartPipeHP3, 20, PipeType.J, "管道类型", "生活给水管");
            var squartPipeV1 = PipeFactory.ArrangePipe(squartPipeHP3, squartPipeVB, 20, PipeType.J, "管道类型", "生活给水管");

            var squartFitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(showerPipeV4, showerPipeBV), new FittingCore(squartPipeH1, squartPipeInP) });
            var squartFitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(squartPipeH1, squartPipeHP1), new FittingCore(squartPipeH2, squartPipeHP1) });
            var squartFitting3 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(squartPipeH2, squartPipeHP2), new FittingCore(squartPipeH3, squartPipeHP2) });
            var squartFitting4 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(squartPipeH3, squartPipeHP3), new FittingCore(squartPipeV1, squartPipeHP3) });

            //淋浴2
            //var shower2PipeVStart = end - Vector3D.BasisZ * 1000;
            //var shower2PipeVMid = end + Vector3D.BasisZ * 200;
            //var shower2PipeVEnd = shower2PipeVStart + Vector3D.BasisZ * 3300;
            //var shower2PipeHP1 = shower2PipeVMid - rDir * 213.3;
            //var shower2PipeHP2 = shower2PipeHP1 + leftDir * 69.8;
            var shower2PipeBV = new Vector3D(end.X, end.Y, 1000);
            var shower2PipeVP1 = shower2PipeBV + Vector3D.BasisZ * 300;
            var shower2PipeVP2_1 = shower2PipeVP1 + Vector3D.BasisZ * 95.9;//淋浴水阀底

            var shower2PipeVP2_2 = shower2PipeVP1 + Vector3D.BasisZ * 176.9;//淋浴水阀顶
            var shower2PipeVP2 = (shower2PipeVP2_1 + shower2PipeVP2_2) / 2;//淋浴水阀
            //var shower2PipeVP3 = shower2PipeHP2;
            var shower2PipeMidH = shower2PipeVP1 - rDir * 694.7;
            var shower2PipeInP = shower2PipeMidH - Vector3D.BasisZ * 130.5;
            var shower2Loc = new Vector3D(3600 - wallThickness / 2, -1500 - 1000, 1100);
            var squart2PipeInP = shower2PipeBV;
            var squart2PipeHP1 = squart2PipeInP + rDir * 245;
            var squart2PipeHP2 = squart2PipeHP1 + leftDir * 420;
            var squart2PipeHP3 = squart2PipeHP2 - rDir * 189.7;
            var squart2PipeVB = squart2PipeHP3 - Vector3D.BasisZ * 275;

            //淋浴进水管（细），从下往上
            var shower2PipeV4 = PipeFactory.ArrangePipe(shower2PipeBV, shower2PipeVP1, 20, PipeType.J, "管道类型", "生活给水管");
            var shower2PipeV5 = PipeFactory.ArrangePipe(shower2PipeVP1, shower2PipeVP2_1, 20, PipeType.J, "管道类型", "生活给水管");
            var shower2PipeV6 = PipeFactory.ArrangePipe(shower2PipeVP2_2, end, 20, PipeType.J, "管道类型", "生活给水管");
            var shower2PipeH3 = PipeFactory.ArrangePipe(shower2PipeVP1, shower2PipeMidH, 15, PipeType.J, "管道类型", "生活给水管");
            var shower2PipeV7 = PipeFactory.ArrangePipe(shower2PipeMidH, shower2PipeInP, 15, PipeType.J, "管道类型", "生活给水管");
            var shower2 = PipeFactory.ArrangePlumbingFixtureByPoint(shower2Loc, Math.PI / 2, "淋浴", "淋浴");
            var shower2Valve = PipeFactory.ArrangeAccessoryByPoint(new List<FittingCore> { new FittingCore(shower2PipeV6, shower2PipeVP2_2), new FittingCore(shower2PipeV5, shower2PipeVP2_1) }, shower2PipeVP2, new List<Tuple<Vector3D, double>> { Tuple.Create(-rDir, Math.PI / 2) }, "截止阀 - 10-50 mm - 螺纹", "20 mm");

            var s2Fitting4 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(shower2PipeV6, end), new FittingCore(pipe2, end) });
            var s2Fitting5 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(shower2PipeV4, shower2PipeVP1), new FittingCore(shower2PipeV5, shower2PipeVP1), new FittingCore(shower2PipeH3, shower2PipeVP1) });
            var s2Fitting6 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(shower2PipeH3, shower2PipeMidH), new FittingCore(shower2PipeV7, shower2PipeMidH) });
            //蹲便器给水
            var squart2PipeH1 = PipeFactory.ArrangePipe(squart2PipeInP, squart2PipeHP1, 20, PipeType.J, "管道类型", "生活给水管");
            var squart2PipeH2 = PipeFactory.ArrangePipe(squart2PipeHP1, squart2PipeHP2, 20, PipeType.J, "管道类型", "生活给水管");
            var squart2PipeH3 = PipeFactory.ArrangePipe(squart2PipeHP2, squart2PipeHP3, 20, PipeType.J, "管道类型", "生活给水管");
            var squart2PipeV1 = PipeFactory.ArrangePipe(squart2PipeHP3, squart2PipeVB, 20, PipeType.J, "管道类型", "生活给水管");

            var squart2Fitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(shower2PipeV4, shower2PipeBV), new FittingCore(squart2PipeH1, squart2PipeInP) });
            var squart2Fitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(squart2PipeH1, squart2PipeHP1), new FittingCore(squart2PipeH2, squart2PipeHP1) });
            var squart2Fitting3 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(squart2PipeH2, squart2PipeHP2), new FittingCore(squart2PipeH3, squart2PipeHP2) });
            var squart2Fitting4 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(squart2PipeH3, squart2PipeHP3), new FittingCore(squart2PipeV1, squart2PipeHP3) });
            #endregion
            //底部进水管
            var pipeB1 = PipeFactory.ArrangePipe(p2, p1, 15, PipeType.J, "管道类型", "生活给水管");
            var pipeB2 = PipeFactory.ArrangePipe(p3, p2, 20, PipeType.J, "管道类型", "生活给水管");
            var pipeB3 = PipeFactory.ArrangePipe(p4, p3, 20, PipeType.J, "管道类型", "生活给水管");
            var pipeB4 = PipeFactory.ArrangePipe(p4, p5, 20, PipeType.J, "管道类型", "生活给水管");
            var pipeB5 = PipeFactory.ArrangePipe(p5, p6, 15, PipeType.J, "管道类型", "生活给水管");
            var pipeSinkH = PipeFactory.ArrangePipe(pSink1B, pSink2B, 15, PipeType.J, "管道类型", "生活给水管");
            var pipeSinkV = PipeFactory.ArrangePipe(pSink2B, pSinkT, 15, PipeType.J, "管道类型", "生活给水管");

            var tFittingSink = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeB2, p3), new FittingCore(pipeB3, p3), new FittingCore(pipeSinkH, p3) });//水池三通
            tFittingSink.Diameter = 15;
            var eFittingSink = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeSinkH, pSink1B), new FittingCore(pipeSinkV, pSink1B) });
            room.Fittings.Add(tFittingSink);
            room.Fittings.Add(eFittingSink);


            var con1 = new FittingCore(pipe1, mid_0);
            var con2 = new FittingCore(pipe2, mid_0);
            var con3 = new FittingCore(pipe3, mid0);
            var tFittingTop = PipeFactory.ArrangeFitting(new List<FittingCore> { con1, con2, con3 });
            var eFitting = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipe3, mid1), new FittingCore(pipe4, mid1) });

            var tFittingBottom = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeB3, p4), new FittingCore(pipeB4, p4), new FittingCore(pipe5, end1_1) });

            var valveCon = new FittingCore(pipe4, valveTop);
            var valveCon1 = new FittingCore(pipe5, valveBottom);


            //var rotatePairs = new List<Tuple<Vector3D, double>> { Tuple.Create(Vector3D.BasisZ, -Math.PI / 2), Tuple.Create(leftDir, -Math.PI / 2) };
            var rotatePairs = new List<Tuple<Vector3D, double>> { Tuple.Create(-leftDir, Math.PI / 2), Tuple.Create(-rDir, Math.PI / 2) };
            var valve = PipeFactory.ArrangeAccessoryByPoint(new List<FittingCore> { valveCon, valveCon1 }, end1, rotatePairs, "截止阀 - 10-50 mm - 螺纹", "25 mm");
            //脸盆位置
            var locW1 = new Vector3D(-1115, -1500 - wallThickness / 2, 700);
            var locW2 = new Vector3D(-580, -1500 - wallThickness / 2, 700);
            var locW = new Vector3D(0, -1500 - wallThickness / 2, 500);
            var locW3 = new Vector3D(620, -1500 - wallThickness / 2, 700);
            var locW4 = new Vector3D(1155, -1500 - wallThickness / 2, 700);

            var hostWall = room.Walls[1];
            var faceDir = -rDir;

            //洗脸盆进水管
            var washPipe1 = PipeFactory.ArrangePipe(p1, p1 + Vector3D.BasisZ * 181, 15, PipeType.J, "管道类型", "生活给水管");
            var washPipe2 = PipeFactory.ArrangePipe(p2, p2 + Vector3D.BasisZ * 181, 15, PipeType.J, "管道类型", "生活给水管");
            var washPipe3 = PipeFactory.ArrangePipe(p5, p5 + Vector3D.BasisZ * 181, 15, PipeType.J, "管道类型", "生活给水管");
            var washPipe4 = PipeFactory.ArrangePipe(p6, p6 + Vector3D.BasisZ * 181, 15, PipeType.J, "管道类型", "生活给水管");
            //水池进水管
            var sinkPipeIn = PipeFactory.ArrangePipe(p3 + rDir * 30, p3 + rDir * 30 + Vector3D.BasisZ * 169, 15, PipeType.J, "管道类型", "生活给水管");

            var washBasin1 = PipeFactory.ArrangePlumbingFixtureByFace(locW1, faceDir, hostWall, "面盆 - 椭圆形", "535 mmx485 mm");
            washBasin1.Connectors.Add(new FittingCore(washPipe1, p1 + Vector3D.BasisZ * 181));
            var washBasin2 = PipeFactory.ArrangePlumbingFixtureByFace(locW2, faceDir, hostWall, "面盆 - 椭圆形", "535 mmx485 mm");
            washBasin2.Connectors.Add(new FittingCore(washPipe2, p2 + Vector3D.BasisZ * 181));
            var sink = PipeFactory.ArrangePlumbingFixtureByFace(locW, faceDir, hostWall, "洗涤池", "560 mmx485 mm");
            sink.Connectors.Add(new FittingCore(pipeSinkV, pSinkIn));
            var sinkFitting3 = PipeFactory.ArrangeFitting(new FittingCore(pipeSinkV, pipeSinkV.End), new FittingCore(sink, pSinkIn));

            room.Fittings.Add(sinkFitting3);
            var washBasin3 = PipeFactory.ArrangePlumbingFixtureByFace(locW3, faceDir, hostWall, "面盆 - 椭圆形", "535 mmx485 mm");
            washBasin3.Connectors.Add(new FittingCore(washPipe3, p5 + Vector3D.BasisZ * 181));
            var washBasin4 = PipeFactory.ArrangePlumbingFixtureByFace(locW4, faceDir, hostWall, "面盆 - 椭圆形", "535 mmx485 mm");
            washBasin4.Connectors.Add(new FittingCore(washPipe4, p6 + Vector3D.BasisZ * 181));
            var eFitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(washPipe1, p1), new FittingCore(pipeB1, p1) });

            var tFittingBottom1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeB1, p2), new FittingCore(pipeB2, p2), new FittingCore(washPipe2, p2) });

            var tFittingBottom2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeB4, p5), new FittingCore(pipeB5, p5), new FittingCore(washPipe3, p5) });

            var eFitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(washPipe4, p6), new FittingCore(pipeB5, p6) });

            //排水管点位            
            var wStart = new Vector3D(-3600 + wallThickness / 2 + 100, -1500 - wallThickness / 2 - 100, -350);
            var wP0T = wStart + Vector3D.BasisZ * (room.Height + 350);
            var wP0D = wStart - Vector3D.BasisZ * 480;
            var wP1 = wStart - leftDir * (550 - 200);
            var wP2 = wStart - leftDir * (2485 - 200);
            var wP3 = wStart - leftDir * (3020 - 200);
            var wP4 = wStart - leftDir * (3299 - 200);
            var wP5 = wStart - leftDir * (3600 - 200);
            var wP6 = wStart - leftDir * (4200 - 200);
            var wP7 = wStart - leftDir * (4755 - 200);
            var wP8 = wStart - leftDir * (6650 - 200);
            var wEnd = wStart - leftDir * (6900 - 200);
            var wEndV = wEnd + Vector3D.BasisZ * 300;

            //底部排水管
            var pipeWV1 = PipeFactory.ArrangePipe(wP0D, wStart, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWV2 = PipeFactory.ArrangePipe(wStart, wP0T, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH1 = PipeFactory.ArrangePipe(wStart, wP1, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH2 = PipeFactory.ArrangePipe(wP1, wP2, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH3 = PipeFactory.ArrangePipe(wP2, wP3, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH4 = PipeFactory.ArrangePipe(wP3, wP4, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH5 = PipeFactory.ArrangePipe(wP4, wP5, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH6 = PipeFactory.ArrangePipe(wP5, wP6, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH7 = PipeFactory.ArrangePipe(wP6, wP7, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH8 = PipeFactory.ArrangePipe(wP7, wP8, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWH9 = PipeFactory.ArrangePipe(wP8, wEnd, 100, PipeType.W, "管道类型", "生活排水管");
            var pipeWV3 = PipeFactory.ArrangePipe(wEnd, wEndV, 100, PipeType.W, "管道类型", "生活排水管");
            var wFitting = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWH9, wEnd), new FittingCore(pipeWV3, wEnd) });
            var endPipe = PipeFactory.ArrangeAccessoryByPoint(new List<FittingCore> { new FittingCore(pipeWV3, wEndV) }, new Vector3D(wEndV.X, wEndV.Y, -50), new List<Tuple<Vector3D, double>> { Tuple.Create(-rDir, Math.PI / 2) }, "清扫口 - PVC-U ", "100 mm");
            endPipe.Type = FittingType.Other;
            //蹲便器排水管1
            var squrt1PH1 = wP1 - rDir * 345.2;
            var squrt1PV0 = wP1 - rDir * 570;
            var squrt1PV1 = squrt1PV0 + Vector3D.BasisZ * 195;
            var pipeWS1H1 = PipeFactory.ArrangePipe(wP1, squrt1PH1, 80, PipeType.W, "管道类型", "生活排水管");
            var pipeWS1V1 = PipeFactory.ArrangePipe(squrt1PV0, squrt1PV1, 80, PipeType.W, "管道类型", "生活排水管");

            var tFitting1W1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWV1, wStart), new FittingCore(pipeWV2, wStart), new FittingCore(pipeWH1, wStart) });
            var tFitting1W2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWH1, wP1), new FittingCore(pipeWH2, wP1), new FittingCore(pipeWS1H1, wP1) });

            //蹲便器1
            var squatLoc1 = new Vector3D(-3600 + 550, -1500 - 770, 10);
            var squat1 = PipeFactory.ArrangePlumbingFixtureByPoint(squatLoc1, 0, "蹲便器- 蹲厕-蹲式便器-按钮（上下水）", "DBQ-1");
            //存水弯
            var fitting1PLoc = new Vector3D(-3600 + 550, -1500 - 770, -350);
            var rotatePairs1 = new List<Tuple<Vector3D, double>> { Tuple.Create(rDir, Math.PI / 2), Tuple.Create(Vector3D.BasisZ, 0d) };
            var fittingP1 = PipeFactory.ArrangePTraps(new List<FittingCore> { new FittingCore(pipeWS1H1, squrt1PH1), new FittingCore(pipeWS1V1, squrt1PV0) }, fitting1PLoc, rotatePairs1, 80, "P 型存水弯 - PVC-U - 排水", "标准");

            //蹲便器排水管2
            var squrt2PH1 = wP8 - rDir * 345.2;
            var squrt2PV0 = wP8 - rDir * 570;
            var squrt2PV1 = squrt2PV0 + Vector3D.BasisZ * 195;
            var pipeWS2H1 = PipeFactory.ArrangePipe(wP8, squrt2PH1, 80, PipeType.W, "管道类型", "生活排水管");
            var pipeWS2V1 = PipeFactory.ArrangePipe(squrt2PV0, squrt2PV1, 80, PipeType.W, "管道类型", "生活排水管");

            var tFitting2W1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWV1, wStart), new FittingCore(pipeWV2, wStart), new FittingCore(pipeWH1, wStart) });
            var tFitting2W2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWH8, wP8), new FittingCore(pipeWH9, wP8), new FittingCore(pipeWS2H1, wP8) });

            //蹲便器
            var squatLoc2 = new Vector3D(3600 - 550, -1500 - 770, 10);
            var squat2 = PipeFactory.ArrangePlumbingFixtureByPoint(squatLoc2, 0, "蹲便器- 蹲厕-蹲式便器-按钮（上下水）", "DBQ-1");
            //存水弯
            var fitting2PLoc = new Vector3D(3600 - 550, -1500 - 770, -350);
            var fittingP2 = PipeFactory.ArrangePTraps(new List<FittingCore> { new FittingCore(pipeWS2H1, squrt2PH1), new FittingCore(pipeWS2V1, squrt2PV0) }, fitting2PLoc, rotatePairs1, 80, "P 型存水弯 - PVC-U - 排水", "标准");

            //脸盆排水管，从左到右                      
            var pairs = new List<Tuple<Vector3D, Pipe, Pipe>> { Tuple.Create(wP2, pipeWH2, pipeWH2), Tuple.Create(wP3, pipeWH3, pipeWH4), Tuple.Create(wP6, pipeWH6, pipeWH7), Tuple.Create(wP7, pipeWH7, pipeWH8) };
            foreach (var item in pairs)
            {
                var wb1H1 = item.Item1 - rDir * 193.3;
                var wb1V1 = wb1H1 + Vector3D.BasisZ * 600;
                var wb1V2 = item.Item1 - rDir * 77.8 + Vector3D.BasisZ * 646.2;
                var wb1V3 = wb1V2 + Vector3D.BasisZ * 251.8;
                var pipeWB_w1 = PipeFactory.ArrangePipe(item.Item1, wb1H1, 32, PipeType.W, "管道类型", "生活排水管");
                var pipeWB_w2 = PipeFactory.ArrangePipe(wb1H1, wb1V1, 32, PipeType.W, "管道类型", "生活排水管");
                var pipeWB_w3 = PipeFactory.ArrangePipe(wb1V2, wb1V3, 32, PipeType.W, "管道类型", "生活排水管");

                var rotatePairs_wb1 = new List<Tuple<Vector3D, double>> { Tuple.Create(-rDir, Math.PI / 2), Tuple.Create(Vector3D.BasisZ, 0d) };
                var fittingP_wb1 = PipeFactory.ArrangePTraps(new List<FittingCore> { new FittingCore(pipeWB_w2, wb1V1), new FittingCore(pipeWB_w3, wb1V2) }, wb1V1, rotatePairs_wb1, 32, "S 型存水弯 - PVC-U - 排水", "标准");
                var wb1Fitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(item.Item2, item.Item1), new FittingCore(item.Item3, item.Item1), new FittingCore(pipeWB_w1, item.Item1) });
                var wb1Fitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWB_w1, wb1H1), new FittingCore(pipeWB_w2, wb1H1) });
                //洗脸盆排水管
                room.Pipes.Add(pipeWB_w1);
                room.Pipes.Add(pipeWB_w2);
                room.Pipes.Add(pipeWB_w3);
                room.Accessories.Add(fittingP_wb1);
                room.Fittings.Add(wb1Fitting1);
                room.Fittings.Add(wb1Fitting2);
            }

            //地漏floor drain
            var fdHP1 = wP4 - rDir * 248.1;
            var fdVP1 = fdHP1 + Vector3D.BasisZ * 90.5;
            var fdVP2 = wP4 - rDir * 403 + Vector3D.BasisZ * 150;
            var fdVP3 = fdVP2 + Vector3D.BasisZ * 97.7;
            var fdLoc = new Vector3D(fdVP3.X, fdVP3.Y, 0);
            var fdPipe1 = PipeFactory.ArrangePipe(wP4, fdHP1, 50, PipeType.W, "管道类型", "生活排水管");
            var fdPipe2 = PipeFactory.ArrangePipe(fdHP1, fdVP1, 50, PipeType.W, "管道类型", "生活排水管");
            var fdPipe3 = PipeFactory.ArrangePipe(fdVP2, fdVP3, 50, PipeType.W, "管道类型", "生活排水管");
            var fdRotatePair = new List<Tuple<Vector3D, double>> { Tuple.Create(-rDir, -Math.PI / 2), Tuple.Create(Vector3D.BasisZ, 0d) };
            var fdS = PipeFactory.ArrangePTraps(new List<FittingCore> { new FittingCore(fdPipe3, fdVP2), new FittingCore(fdPipe2, fdVP1) }, fdVP2, fdRotatePair, 50, "S 型存水弯 - PVC-U - 排水", "标准");
            var fdFitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWH4, wP4), new FittingCore(pipeWH5, wP4), new FittingCore(fdPipe1, wP4) });
            var fdFitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(fdPipe1, fdHP1), new FittingCore(fdPipe2, fdHP1) });


            //污水池排水
            var sinkHP1 = wP5 - rDir * 142.5;
            var sinkVP1 = sinkHP1 + Vector3D.BasisZ * 116.2;
            var sinkVP2 = sinkVP1 + Vector3D.BasisZ * 46.2 + rDir * 115.5;
            var sinkVP3 = sinkVP2 + Vector3D.BasisZ * 383.5;
            var sinkPipe1 = PipeFactory.ArrangePipe(wP5, sinkHP1, 32, PipeType.W, "管道类型", "生活排水管");
            var sinkPipe2 = PipeFactory.ArrangePipe(sinkHP1, sinkVP1, 32, PipeType.W, "管道类型", "生活排水管");
            var sinkPipe3 = PipeFactory.ArrangePipe(sinkVP3, sinkVP2, 50, PipeType.W, "管道类型", "生活排水管");

            var rotatePairs_sink1 = new List<Tuple<Vector3D, double>> { Tuple.Create(-rDir, Math.PI / 2), Tuple.Create(Vector3D.BasisZ, 0d) };
            var sinkS = PipeFactory.ArrangePTraps(new List<FittingCore> { new FittingCore(sinkPipe2, sinkVP1), new FittingCore(sinkPipe3, sinkVP2) }, sinkVP1, rotatePairs_sink1, 32, "S 型存水弯 - PVC-U - 排水", "标准");
            //var bian = PipeFactory.ArrangeFitting(new FittingCore(sinkS, sinkVP1), new FittingCore(sinkPipe2, sinkVP1));
            //room.Fittings.Add(bian);
            var sinkFitting1 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(pipeWH5, wP5), new FittingCore(pipeWH6, wP5), new FittingCore(sinkPipe1, wP5) });
            var sinkFitting2 = PipeFactory.ArrangeFitting(new List<FittingCore> { new FittingCore(sinkPipe1, sinkHP1), new FittingCore(sinkPipe2, sinkHP1) });


            room.Pipes.Add(pipe1);//-------------
            room.Pipes.Add(pipe2);//-------------
            room.Pipes.Add(pipe3);
            room.Pipes.Add(pipe4);
            room.Pipes.Add(pipe5);
            room.Pipes.Add(pipeB1);
            room.Pipes.Add(pipeB2);
            room.Pipes.Add(pipeB3);
            room.Pipes.Add(pipeB4);
            room.Pipes.Add(pipeB5);
            room.Pipes.Add(washPipe1);
            room.Pipes.Add(washPipe2);
            room.Pipes.Add(washPipe3);
            room.Pipes.Add(washPipe4);
            room.Pipes.Add(pipeSinkH);
            room.Pipes.Add(pipeSinkV);

            room.Pipes.Add(showerPipeV1);
            room.Pipes.Add(showerPipeV2);
            room.Pipes.Add(showerPipeV3);
            room.Pipes.Add(showerPipeH1);
            room.Pipes.Add(showerPipeH2);
            room.Pipes.Add(showerPipeV4);
            room.Pipes.Add(showerPipeV5);
            room.Pipes.Add(showerPipeV6);
            room.Pipes.Add(showerPipeH3);
            room.Pipes.Add(showerPipeV7);

            room.Fittings.Add(sFitting1);
            room.Fittings.Add(sFitting2);
            room.Fittings.Add(sFitting3);
            room.Fittings.Add(sFitting4);
            room.Fittings.Add(sFitting5);
            room.Fittings.Add(sFitting6);
            room.Fittings.Add(sinkFitting1);
            room.Fittings.Add(sinkFitting2);
            room.PlumbingFixtures.Add(shower);
            room.Accessories.Add(showerValve);

            room.Pipes.Add(squartPipeH1);
            room.Pipes.Add(squartPipeH2);
            room.Pipes.Add(squartPipeH3);
            room.Pipes.Add(squartPipeV1);
            room.Fittings.Add(squartFitting1);
            room.Fittings.Add(squartFitting2);
            room.Fittings.Add(squartFitting3);
            room.Fittings.Add(squartFitting4);

            room.Pipes.Add(shower2PipeV4);
            room.Pipes.Add(shower2PipeV5);
            room.Pipes.Add(shower2PipeV6);
            room.Pipes.Add(shower2PipeV7);
            room.Pipes.Add(shower2PipeH3);
            room.PlumbingFixtures.Add(shower2);
            room.Accessories.Add(shower2Valve);
            room.Fittings.Add(s2Fitting4);
            room.Fittings.Add(s2Fitting5);
            room.Fittings.Add(s2Fitting6);

            room.Pipes.Add(squart2PipeH1);
            room.Pipes.Add(squart2PipeH2);
            room.Pipes.Add(squart2PipeH3);
            room.Pipes.Add(squart2PipeV1);
            room.Fittings.Add(squart2Fitting1);
            room.Fittings.Add(squart2Fitting2);
            room.Fittings.Add(squart2Fitting3);
            room.Fittings.Add(squart2Fitting4);



            //污水管
            room.Pipes.Add(pipeWV1); room.Pipes.Add(pipeWV2);
            room.Pipes.Add(pipeWH1); room.Pipes.Add(pipeWH2);
            room.Pipes.Add(pipeWH3); room.Pipes.Add(pipeWH4);
            room.Pipes.Add(pipeWH5); room.Pipes.Add(pipeWH6);
            room.Pipes.Add(pipeWH7); room.Pipes.Add(pipeWH8);
            room.Pipes.Add(pipeWH9); room.Pipes.Add(pipeWV3);
            room.Pipes.Add(pipeWS1H1); room.Pipes.Add(pipeWS1V1);
            room.Fittings.Add(wFitting); room.Accessories.Add(endPipe);

            room.Pipes.Add(pipeWS2H1); room.Pipes.Add(pipeWS2V1);


            //地漏
            room.Pipes.Add(fdPipe1); room.Pipes.Add(fdPipe2);
            room.Pipes.Add(fdPipe3);
            room.Accessories.Add(fdS);
            room.Fittings.Add(fdFitting1);
            room.Fittings.Add(fdFitting2);
            //污水池排水
            room.Pipes.Add(sinkPipe1);
            room.Pipes.Add(sinkPipe2);
            room.Pipes.Add(sinkPipe3);
            room.Accessories.Add(sinkS);
            room.Fittings.Add(sinkFitting1);
            room.Fittings.Add(sinkFitting2);

            room.Fittings.Add(tFittingTop);
            room.Fittings.Add(eFitting);
            room.Fittings.Add(tFittingBottom);
            room.Fittings.Add(eFitting1);
            room.Fittings.Add(eFitting2);
            room.Fittings.Add(tFittingBottom1);
            room.Fittings.Add(tFittingBottom2);
            room.Fittings.Add(tFitting1W1);
            room.Fittings.Add(tFitting1W2);
            room.Fittings.Add(tFitting2W1);
            room.Fittings.Add(tFitting2W2);
            room.Accessories.Add(valve);
            room.Accessories.Add(fittingP1);
            room.Accessories.Add(fittingP2);
            room.PlumbingFixtures.Add(washBasin1);
            room.PlumbingFixtures.Add(washBasin2);
            room.PlumbingFixtures.Add(sink);
            room.PlumbingFixtures.Add(washBasin3);
            room.PlumbingFixtures.Add(washBasin4);
            room.PlumbingFixtures.Add(squat1);
            room.PlumbingFixtures.Add(squat2);

            return true;
        }

        private bool ArrangeRoom_H(FRoom room)
        {
            if (!_roleType.HasFlag(RoleType.H))
                return false;
            var loc1 = new Vector3D(-3200, -3500, 2900);
            var host1 = room.ReferenceWalls.Find(x => x.Location.Intersect(Line3D.Create(room.Central, room.Central - room.Direction * 10000)) != null);
            room.HAVCs.Add(HAVCFactory.ArrangeFan("墙式换气扇3", "标准", loc1, host1, host1.Location.Direction, room.Direction, Math.PI));
            var loc2 = new Vector3D(3200, -3500, 2900);
            room.HAVCs.Add(HAVCFactory.ArrangeFan("墙式换气扇3", "标准", loc2, host1, host1.Location.Direction, room.Direction, 0));
            return true;
            
        }

        private bool ArrangeRoom_E(FRoom room)
        {
            if (!_roleType.HasFlag(RoleType.E))
                return false;
            var wt = room.ReferenceWalls.FirstOrDefault().Thickness;
            var wt1 = room.Walls[2].Thickness;
            var loc1 = new Vector3D(-3600 + wt / 2, 3600 - 550, 300);
            var loc2 = new Vector3D(-3600 + wt / 2, 3600 - 2150, 300);
            var loc3 = new Vector3D(-3600 + wt / 2, 3600 - 4000, 300);
            var loc1_1 = new Vector3D(0 - wt / 2, 3600 - 550, 300);
            var loc2_1 = new Vector3D(0 - wt / 2, 3600 - 2150, 300);
            var loc3_1 = new Vector3D(0 - wt / 2, 3600 - 4000, 300);
            var loc1_2 = new Vector3D(0 + wt / 2, 3600 - 550, 300);
            var loc2_2 = new Vector3D(0 + wt / 2, 3600 - 2150, 300);
            var loc3_2 = new Vector3D(0 + wt / 2, 3600 - 4000, 300);
            var loc1_3 = new Vector3D(3600 - wt / 2, 3600 - 550, 300);
            var loc2_3 = new Vector3D(3600 - wt / 2, 3600 - 2150, 300);
            var loc3_3 = new Vector3D(3600 - wt / 2, 3600 - 4000, 300);

            var locA = new Vector3D(-2450, -1500 + wt / 2, 2200);
            var locA_1 = new Vector3D(2450, -1500 + wt / 2, 2200);
            var locB = new Vector3D(-2450 - wt1 / 2, -1500 - 250, 2000);
            var locB_1 = new Vector3D(2450 + wt1 / 2, -1500 - 250, 2000);
            var locC = new Vector3D(-1200, 3600 - wt / 2, 1300);
            var locC_1 = new Vector3D(1200, 3600 - wt / 2, 1300);

            var loc0 = new Vector3D(-1056, 3600 - wt / 2, 1300);
            var loc0_1 = new Vector3D(1056, 3600 - wt / 2, 1300);
            //厕所阳台开关
            var locX = new Vector3D(-2450 + wt1 / 2, -1500 - 425, 1300);
            var locX_1 = new Vector3D(-2450 + wt1 / 2, -1500 - 539, 1300);
            var locY = new Vector3D(2450 - wt1 / 2, -1500 - 425, 1300);
            var locY_1 = new Vector3D(2450 - wt1 / 2, -1500 - 539, 1300);

            var locBox1 = new Vector3D(-2940, 3600 - wt / 2, 1400);
            var locBox2 = new Vector3D(2940, 3600 - wt / 2, 1400);
            room.Fixtures.Add(FixtureFactory.ArrangeEquipment("家居配线箱-壁装", "标准", locBox1, Math.PI));
            room.Fixtures.Add(FixtureFactory.ArrangeEquipment("家居配线箱-壁装", "标准", locBox2, Math.PI));

            var pLocDoor1 = new Vector3D(-1056, 3600 - wt / 2, 1300);
            var pLocDoor2 = new Vector3D(1056, 3600 - wt / 2, 1300);
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("双联单控照明开关", "标准", pLocDoor1, Math.PI));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("双联单控照明开关", "标准", pLocDoor2, Math.PI));

            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc1, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc2, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc3, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc1_1, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc2_1, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc3_1, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc1_2, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc2_2, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc3_2, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc1_3, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc2_3, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单相插座", "标准", loc3_3, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("壁挂式分体空调插座(带开关)", "标准", locA, 0));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("壁挂式分体空调插座(带开关)", "标准", locA_1, 0));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("热水器专用插座(带开关)(防水防潮)", "标准", locB, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("热水器专用插座(带开关)(防水防潮)", "标准", locB_1, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("双联单控照明开关", "标准", locB, 180 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("双联单控照明开关", "标准", locB_1, 180 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单联单控照明开关", "标准", locX, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("双联单控照明开关", "标准", locX_1, 270 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("单联单控照明开关", "标准", locY, 90 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangePlugin("双联单控照明开关", "标准", locY_1, 90 * Math.PI / 180));

            var loc_light1 = new Vector3D(-1800, 3600 - 1325, room.Height);
            var loc_light2 = new Vector3D(1800, 3600 - 1325, room.Height);
            var loc_light3 = new Vector3D(-1800, 3600 - 3775, room.Height);
            var loc_light4 = new Vector3D(1800, 3600 - 3775, room.Height);
            var loc_light5 = new Vector3D(-1225, -1500 - 1050, 2700);
            var loc_light6 = new Vector3D(1225, -1500 - 1050, 2700);
            var loc_light7 = new Vector3D(-3000, -1500 - 1050, 2700);
            var loc_light8 = new Vector3D(3000, -1500 - 1050, 2700);

            room.Fixtures.Add(FixtureFactory.ArrangeLighting("单管荧光灯-吸顶", "标准", loc_light1, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("单管荧光灯-吸顶", "标准", loc_light2, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("单管荧光灯-吸顶", "标准", loc_light3, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("单管荧光灯-吸顶", "标准", loc_light4, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("吸顶圆灯", "标准", loc_light5, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("吸顶圆灯", "标准", loc_light6, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("吸顶密闭灯", "标准", loc_light7, 0));
            room.Fixtures.Add(FixtureFactory.ArrangeLighting("吸顶密闭灯", "标准", loc_light8, 0));

            var locEquipment1 = new Vector3D(-3318, -1500 - wt / 2, 300);
            var locEquipment2 = new Vector3D(3318, -1500 - wt / 2, 300);
            room.Fixtures.Add(FixtureFactory.ArrangeEquipment("局部等电位端子箱", "标准", locEquipment1, 180 * Math.PI / 180));
            room.Fixtures.Add(FixtureFactory.ArrangeEquipment("局部等电位端子箱", "标准", locEquipment2, 180 * Math.PI / 180));
            return true;
        }

        
    }
}
