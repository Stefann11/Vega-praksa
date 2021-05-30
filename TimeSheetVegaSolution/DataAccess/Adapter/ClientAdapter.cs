using DataAccess.Adaptee;
using DataAccess.Target;
using System.Data;

namespace DataAccess.Adapter
{
    public class ClientAdapter : ITarget
    {
        private readonly ClientAdaptee clientAdaptee;

        public ClientAdapter(ClientAdaptee clientAdaptee)
        {
            this.clientAdaptee = clientAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return clientAdaptee.ConvertSqlDataReaderToClient(dataRow);
        }
    }
}
