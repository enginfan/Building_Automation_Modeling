using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Core
{
    public class Room
    {
        public Vector3D Central { get; set; }

        public string NameTag { get; set; }

        public double Height { get; set; }

        public int LevelId { get; set; }

        public List<Wall> Walls { get; set; } = new List<Wall>();

        public List<Wall> ReferenceWalls { get; set; }
        /// <summary>
        /// 固定装置
        /// </summary>
        public List<Fixture> Fixtures { get; set; } = new List<Fixture>();
        /// <summary>
        /// 家具
        /// </summary>
        public List<Furniture> Furniture { get; set; } = new List<Furniture>();
        /// <summary>
        /// 暖通
        /// </summary>
        public List<HAVC> HAVCs { get; set; } = new List<HAVC>();
        /// <summary>
        /// 管线 
        /// </summary>
        public List<Pipe> Pipes { get; set; } = new List<Pipe>();
        /// <summary>
        /// 弯头
        /// </summary>
        public List<Fitting> Fittings { get; set; } = new List<Fitting>();
        /// <summary>
        /// 管道附件（阀门等）
        /// </summary>
        public List<Accessory> Accessories { get; set; } = new List<Accessory>();
        /// <summary>
        /// 卫生洁具等
        /// </summary>
        public List<PlumbingFixture> PlumbingFixtures { get; set; } = new List<PlumbingFixture>();

        public Vector3D Direction { get; set; }


        public RoomType RoomType { get; set; }


    }

    public enum RoomType
    {
        A,
        B,
        C,
        D
    }
}
