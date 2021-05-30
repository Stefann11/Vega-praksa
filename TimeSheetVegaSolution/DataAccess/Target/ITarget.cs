using System.Data;

namespace DataAccess.Target
{
    public interface ITarget
    {
        public object ConvertSql(DataRow dataRow);
    }
}
