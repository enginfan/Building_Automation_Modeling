using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.UnitTest
{
    /// <summary>
    /// 手机公共接口
    /// </summary>
    public interface IMobilePhone
    {
        void Make();
    }

    /// <summary>
    /// 小米手机
    /// </summary>
    public class MiPhone : IMobilePhone
    {
        public MiPhone()
        {
            Make();
        }
        public void Make()
        {
            Console.WriteLine("Make XIAOMI phone!");
        }
    }

    /// <summary>
    /// 平果手机
    /// </summary>
    public class iPhone : IMobilePhone
    {
        public iPhone()
        {
            Make();
        }
        public void Make()
        {
            Console.WriteLine("Make iPhone!");
        }
    }

    /// <summary>
    /// 手机工厂
    /// </summary>
    public class PhoneFactory
    {
        public IMobilePhone MakePhone(string phoneType)
        {
            if (phoneType == "XIAOMI")
                return new MiPhone();
            else if (phoneType == "iPhone")
                return new iPhone();
            return null;
        }
    }

    [TestClass]
    public class Demo
    {
        [TestMethod]
        public void Main()
        {
            var factory = new PhoneFactory();
            var miPhone = factory.MakePhone("XIAOMI");
            var iPhone = factory.MakePhone("iPhone");
        }
    }
}
