using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Geometry
{
    public class Line3D
    {
        public Vector3D Start { get; set; }

        public Vector3D End { get; set; }

        private Line3D(Vector3D start, Vector3D end)
        {
            Start = start;
            End = end;
        }

        public static Line3D Create(Vector3D start, Vector3D end)
        {
            return new Line3D(start, end);
        }


        public Vector3D Direction
        {
            get
            {
                return (End - Start).Normalize();
            }
        }

        public double Length => Start.Distance(End);

        public Vector3D Evaluate(double parameter)
        {
            return (End - Start) * parameter + Start;
        }


        public Vector3D Intersect(Line3D line, bool isSegment = true, double epsilon = 1e-6)
        {
            var flag = LineLine(this, line, out double a, out double b, epsilon);
            if (!flag) return null;
            if (isSegment && (a < -epsilon || a > 1 + epsilon || b < -epsilon || b > 1 + epsilon)) return null;
            var point = Evaluate(a);
            if (point.Distance(line.Evaluate(b)) < epsilon)
                return point;
            return null;
        }


        private bool LineLine(Line3D line1, Line3D line2, out double a, out double b, double epsilon = 1e-6)
        {
            a = b = 0;
            if (line1.Length < epsilon || line2.Length < epsilon) return false;
            var vec13 = line1.Start - line2.Start;
            var vec43 = line2.End - line2.Start;
            var vec21 = line1.End - line1.Start;
            var d1343 = vec13.Dot(vec43);
            var d4321 = vec43.Dot(vec21);
            var d1321 = vec13.Dot(vec21);
            var d4343 = vec43.Dot(vec43);
            var d2121 = vec21.Dot(vec21);
            var d = d2121 * d4343 - d4321 * d4321;
            if (Math.Abs(d) < epsilon) return false;
            var n = d1343 * d4321 - d1321 * d4343;
            a = n / d;
            b = (d1343 + d4321 * a) / d4343;
            return true;
        }

    }
}
