using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Geometry
{
    /// <summary>
    /// 一个三维点(向量)
    /// </summary>
    public class Vector3D
    {

        public static Vector3D BasisX = new Vector3D(1, 0, 0);
        public static Vector3D BasisY = new Vector3D(0, 1, 0);
        public static Vector3D BasisZ = new Vector3D(0, 0, 1);
        public static Vector3D Zero = new Vector3D(0, 0, 0);

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D()
        {
            X = Y = Z = 0;
        }

        public double AngleFrom(Vector3D source)
        {
            return AngleFrom(source, Vector3D.BasisZ);
        }

        public double AngleFrom(Vector3D source, Vector3D refNormal)
        {
            double num = AngleTo(source);
            if (num.AreEqual(0.0))
            {
                return 0.0;
            }
            if (Cross(source).AngleTo(refNormal) < Math.PI / 2)
            {
                return Math.PI * 2 - num;
            }
            return num;
        }

        public double AngleTo(Vector3D v2)
        {
            if ((Length * v2.Length).AreEqual(0.0))
            {
                return 0.0;
            }
            double num = this.Dot(v2) / (Length * v2.Length);
            if (num > 1.0)
            {
                num = 1.0;
            }
            if (num < -1.0)
            {
                num = -1.0;
            }
            return Math.Acos(num);
        }


        public double Distance(Vector3D vec)
        {
            return Math.Sqrt((X - vec.X) * (X - vec.X) + (Y - vec.Y) * (Y - vec.Y) + (Z - vec.Z) * (Z - vec.Z));
        }

        public double Dot(Vector3D vec)
        {
            return X * vec.X + Y * vec.Y + Z * vec.Z;
        }

        public Vector3D Cross(Vector3D vec)
        {
            return new Vector3D(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y + Z * Z);
            }

        }

        public Vector3D Normalize()
        {
            if (Length > 1e-6)
            {
                return new Vector3D(X / Length, Y / Length, Z / Length);
            }
            return Vector3D.Zero;
        }

        public static Vector3D operator +(Vector3D vec1, Vector3D vec2)
        {
            return new Vector3D(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public static Vector3D operator -(Vector3D vec1, Vector3D vec2)
        {
            return new Vector3D(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        public static Vector3D operator -(Vector3D vec)
        {
            return new Vector3D(-vec.X, -vec.Y, -vec.Z);
        }

        public static Vector3D operator *(Vector3D vec, double k)
        {
            return new Vector3D(vec.X * k, vec.Y * k, vec.Z * k);
        }

        public static Vector3D operator *(double k, Vector3D vec)
        {
            return new Vector3D(vec.X * k, vec.Y * k, vec.Z * k);
        }

        public static Vector3D operator /(Vector3D vec, double t)
        {
            return new Vector3D(vec.X / t, vec.Y / t, vec.Z / t);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3D vec)
            {
                if (X == vec.X && Y == vec.Y && Z == vec.Z)
                    return true;
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ X.GetHashCode();
                hash = (hash * 16777619) ^ Y.GetHashCode();
                hash = (hash * 16777619) ^ Z.GetHashCode();
                return hash;
            }
        }
    }
}
