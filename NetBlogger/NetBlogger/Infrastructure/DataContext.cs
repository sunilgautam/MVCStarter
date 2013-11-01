using System.Configuration;
using System.Data.SqlClient;

namespace NetBlogger.Infrastructure
{
    public class DataContext
    {
        private SqlConnection _db;
        public SqlConnection db
        {
            get
            {
                if (_db == null)
                {
                    _db = new SqlConnection(ConfigurationManager.ConnectionStrings["appCon"].ConnectionString);
                }
                return _db;
            }
            private set
            {
                _db = value;
            }
        }
    }
}