using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBlood.Builder.InstanceManager
{
    public abstract class InstanceManager
    {
        protected Document Document
        {
            get;
            private set;
        }

        public ElementType ElementType { get; set; }


        /// <summary>
        /// 族类的名称
        /// </summary>
        protected string FamilyName
        {
            set;
            get;
        }
        /// <summary>
        /// 当前的类型信息
        /// </summary>
        protected string TypeName
        {
            set;
            get;
        }

        public InstanceManager(Document document)
        {
            Document = document;
        }

        protected void GetElementType(string familyName, string typeName, BuiltInCategory category, Type type)
        {
            this.FamilyName = familyName;
            this.TypeName = typeName;
            var collector = new FilteredElementCollector(Document).OfClass(type);
            collector.OfCategory(category);

            var targetElems = from element in collector
                              where element.Name.Equals(typeName) && element.get_Parameter
                              (BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString().Equals(familyName)
                              select element;
            IList<Element> elems = targetElems.ToList();
            if (elems.Count == 0)
                throw new Exception("没有载入指定的族，族名：" + familyName + "，类型名：" + typeName);

            ElementType = elems.FirstOrDefault() as ElementType; ;

            if (ElementType is FamilySymbol fi && !fi.IsActive)
                fi.Activate();
        }
    }
}
