using Authorization.Desktop.Entities.Users;
using Authorization.Desktop.ViewModels.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Authorization.Desktop.Interfaces.Users
{
    public interface IUserRepository:IRepository<User,UserViewModel>
    {
        public Task<IList<User>> GetByIdUserName(string UserName); 
    }
}
