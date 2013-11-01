using NetBlogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBlogger.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int post_id);
        bool Save(User user);
        bool Update(UserEdit user);
        bool ChangePassword(UserChangePassword user);
        bool Delete(User user);
    }
}
