using FirstBlood.Core;
using FirstBlood.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Arrangement.ModelFactory
{
    public class FixtureFactory
    {
        public static Fixture ArrangePlugin(string familyName, string typeName, Vector3D location, double rotation)
        {
            var fixture = new Fixture();
            fixture.Type = FixtureType.Plugin;
            fixture.FamilyName = familyName;
            fixture.TypeName = typeName;
            fixture.Location = location;
            fixture.Rotation = rotation;
            return fixture;
        }

        public static Fixture ArrangeLighting(string familyName, string typeName, Vector3D location, double rotation)
        {
            var fixture = new Fixture();
            fixture.Type = FixtureType.Lighting;
            fixture.FamilyName = familyName;
            fixture.TypeName = typeName;
            fixture.Location = location;
            fixture.Rotation = rotation;
            return fixture;
        }

        public static Fixture ArrangeEquipment(string familyName, string typeName, Vector3D location, double rotation)
        {
            var fixture = new Fixture();
            fixture.Type = FixtureType.Equipment;
            fixture.FamilyName = familyName;
            fixture.TypeName = typeName;
            fixture.Location = location;
            fixture.Rotation = rotation;
            return fixture;
        }

        public static Fixture ArrangeFixture(FixtureType type, string familyName, string typeName, Vector3D location, double rotation)
        {
            var fixture = new Fixture();
            fixture.Type = type;
            fixture.FamilyName = familyName;
            fixture.TypeName = typeName;
            fixture.Location = location;
            fixture.Rotation = rotation;
            return fixture;
        }
    }
}
