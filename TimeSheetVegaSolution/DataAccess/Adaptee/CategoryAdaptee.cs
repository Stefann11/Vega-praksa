using Model.Entities;
using System.Data;

namespace DataAccess.Adaptee
{
    public class CategoryAdaptee
    {
        public Category ConvertSqlDataReaderToCategory(DataRow dataRow)
        {
            return new Category
            {
                Id = int.Parse(dataRow[0].ToString()),
                Name = dataRow[1].ToString().Trim()
            };
        }
    }
}
