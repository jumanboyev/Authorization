using Authorization.Desktop.Entities.Shops;
using Authorization.Desktop.Entities.Users;
using Authorization.Desktop.Security;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Authorization.Desktop.Repositories
{
    public class BaseRepository
    {
        protected readonly MySqlConnection _connection;
        public BaseRepository()
        {
            IdentitySingleton identity = IdentitySingleton.GetInstance();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this._connection = new MySqlConnection(
                identity.connectionString
                );
        }        
    }
}
