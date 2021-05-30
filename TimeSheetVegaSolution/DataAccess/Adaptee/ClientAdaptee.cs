using Model.Entities;
using System.Data;

namespace DataAccess.Adaptee
{
    public class ClientAdaptee
    {
        public Client ConvertSqlDataReaderToClient(DataRow dataRow)
        {
            return new Client
            {
                Id = int.Parse(dataRow[0].ToString()),
                Name = dataRow[1].ToString().Trim(),
                Address = dataRow[2].ToString().Trim(),
                City = dataRow[3].ToString().Trim(),
                ZipCode = dataRow[4].ToString().Trim(),
                Country = new Country
                {
                    Id = int.Parse(dataRow[5].ToString()),
                    Name = dataRow[7].ToString().Trim()
                }
            };
        }
    }
}
