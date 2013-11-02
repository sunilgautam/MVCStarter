using NetBlogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBlogger.Domain.Abstract
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAll(int? skip, int? take, string sort, string searchfield, string searchvalue);
        int Count();
        Post GetByIdWithAuthors(int post_id);
        IEnumerable<Post> GetAllWithAuthors();
        Post GetById(int post_id);
        bool Save(Post post);
        bool Update(Post post);
        bool Delete(Post post);
    }
}
