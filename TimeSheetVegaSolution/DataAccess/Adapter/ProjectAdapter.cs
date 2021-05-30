using DataAccess.Adaptee;
using DataAccess.Target;
using System.Data;

namespace DataAccess.Adapter
{
    public class ProjectAdapter : ITarget
    {
        private readonly ProjectAdaptee projectAdaptee;
        public ProjectAdapter(ProjectAdaptee projectAdaptee)
        {
            this.projectAdaptee = projectAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return projectAdaptee.ConvertSqlDataReaderToProject(dataRow);
        }
    }
}
