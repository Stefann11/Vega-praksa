using DataAccess.Adaptee;
using DataAccess.Target;
using System.Data;

namespace DataAccess.Adapter
{
    public class CategoryAdapter : ITarget
    {
        private readonly CategoryAdaptee categoryAdaptee;

        public CategoryAdapter(CategoryAdaptee categoryAdaptee)
        {
            this.categoryAdaptee = categoryAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return categoryAdaptee.ConvertSqlDataReaderToCategory(dataRow);
        }
    }
}
