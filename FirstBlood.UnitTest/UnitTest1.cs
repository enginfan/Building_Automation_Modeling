using FirstBlood.Geometry;
using FirstBlood.View.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FirstBlood.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var p1 = new Vector3D(0, 0, 0);
            var p5 = new Vector3D(0, 0, 0);
            var b = p1.Equals(p5);
            var p2 = new Vector3D(10, 10, 0);
            var p3 = new Vector3D(5, 0, 0);
            var p4 = new Vector3D(0, 15, 0);
            var line1 = Line3D.Create(p1, p2);
            var line2 = Line3D.Create(p3, p4);
            var intersect = line1.Intersect(line2);
            var result = new Vector3D(3.75, 3.75, 0);
            Assert.AreEqual(intersect, result);

        }

        [TestMethod]
        public void TestMethod2()
        {
            
            var vec = new Vector3D(1, 2, 3);
            var point = new Point3D(1, 2, 3);
            Add(ref vec);
            Add(ref point);
        }

        private void Add(ref Vector3D vec)
        {
            vec = new Vector3D(4, 5, 6);
        }

        private void Add(ref Point3D point)
        {
            point.X++;
            point.Y++;
            point.Z++;
        }
    }

    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
