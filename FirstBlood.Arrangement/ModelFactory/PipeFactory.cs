using FirstBlood.Core;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Arrangement.ModelFactory
{
    public class PipeFactory
    {
        /// <summary>
        /// 创建管道
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="diameter"></param>
        /// <param name="type"></param>
        /// <param name="familyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Pipe ArrangePipe(Vector3D start, Vector3D end, double diameter, PipeType type, string familyName, string typeName)
        {
            var pipe = new Pipe();
            pipe.Start = start;
            pipe.End = end;
            pipe.Diameter = diameter;
            pipe.FamilyName = familyName;
            pipe.TypeName = typeName;
            pipe.Type = type;
            return pipe;
        }

        /// <summary>
        /// 连通器
        /// </summary>
        /// <param name="connectors"></param>
        /// <returns></returns>
        public static Fitting ArrangeFitting(List<FittingCore> connectors)
        {
            var fitting = new Fitting();
            fitting.Connectors = connectors;
            if (connectors.Count == 2)
            {
                fitting.Type = FittingType.Elbow;
            }
            else if (connectors.Count == 3)
                fitting.Type = FittingType.Tee;
            else if (connectors.Count == 4)
                fitting.Type = FittingType.Cross;
            else fitting.Type = FittingType.None;
            return fitting;
        }

        public static Fitting ArrangeFitting(params FittingCore[] fittingCores)
        {
            var fitting = new Fitting();
            fitting.Connectors = fittingCores.ToList();
            if (fittingCores.Count() == 2)
            {
                fitting.Type = FittingType.Elbow;
            }
            else if (fittingCores.Count() == 3)
                fitting.Type = FittingType.Tee;
            else if (fittingCores.Count() == 4)
                fitting.Type = FittingType.Cross;
            else fitting.Type = FittingType.None;
            return fitting;
        }

        /// <summary>
        /// 管道附件
        /// </summary>
        /// <param name="connectors"></param>
        /// <param name="location"></param>
        /// <param name="rotatePairs"></param>
        /// <param name="familyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Accessory ArrangeAccessoryByPoint(List<FittingCore> connectors, Vector3D location, List<Tuple<Vector3D, double>> rotatePairs, string familyName, string typeName, bool ispipe = true, double R = 0)
        {
            var valve = new Accessory();
            valve.Location = location;
            valve.Connectors = connectors;
            valve.Type = FittingType.V;
            valve.FamilyName = familyName;
            valve.TypeName = typeName;
            valve.RotatePairs = rotatePairs;
            if (!ispipe)
            {
                valve.TubeType = AType.Duct;
            }
            if (R != 0)
                valve.Diameter = R;

            return valve;
        }

        /// <summary>
        /// 存水弯
        /// </summary>
        /// <param name="connectors"></param>
        /// <param name="location"></param>
        /// <param name="rotatePairs"></param>
        /// <param name="familyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Accessory ArrangePTraps(List<FittingCore> connectors, Vector3D location, List<Tuple<Vector3D, double>> rotatePairs, double diameter, string familyName, string typeName)
        {
            var pTraps = new Accessory();
            pTraps.Location = location;
            pTraps.Connectors = connectors;
            pTraps.Type = FittingType.P;
            pTraps.FamilyName = familyName;
            pTraps.TypeName = typeName;
            pTraps.Diameter = diameter;
            pTraps.RotatePairs = rotatePairs;
            return pTraps;
        }


        public static PlumbingFixture ArrangePlumbingFixtureByFace(Vector3D location, Vector3D faceDir, Wall host, string familyName, string typeName)
        {
            var fixture = new PlumbingFixture();
            fixture.Location = location;
            fixture.FaceDir = faceDir;
            fixture.Host = host;
            fixture.FamilyName = familyName;
            fixture.TypeName = typeName;
            return fixture;
        }

        public static PlumbingFixture ArrangePlumbingFixtureByPoint(Vector3D location, double rotation, string familyName, string typeName)
        {
            var fixture = new PlumbingFixture();
            fixture.Location = location;
            fixture.Rotation = rotation;
            fixture.FamilyName = familyName;
            fixture.TypeName = typeName;
            return fixture;
        }
    }
}
