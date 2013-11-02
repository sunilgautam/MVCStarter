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
        IEnumerable<User> GetAll(int? skip, int? take);
        int Count();
        User GetById(int post_id);
        bool Save(User user);
        bool Update(UserEdit user);
        bool ChangePassword(UserChangePassword user);
        bool Delete(User user);
    }
}
