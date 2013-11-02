using NetBlogger.Domain.Abstract;
using NetBlogger.Infrastructure;
using NetBlogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace NetBlogger.Domain.Concrete
{
    public class PostRepository : DataContext, IPostRepository
    {
        public IEnumerable<Post> GetAll()
        {
            try
            {
                db.Open();
                IEnumerable<Post> posts = db.Query<Post>("Select * from posts ORDER BY PostId DESC");
                return posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public IEnumerable<Post> GetAll(int? skip, int? take, string sort, string searchfield, string searchvalue)
        {
            try
            {
                db.Open();
                IEnumerable<Post> posts = db.Query<Post>(@"
                                                        SELECT * FROM 
                                                        (
	                                                        SELECT	*,
			                                                        ROW_NUMBER() OVER (ORDER BY @sort) AS ROW_NUM
	                                                        FROM	Posts
                                                            WHERE   @searchfield = @searchvalue
                                                        ) 
                                                        AS QUERY
                                                        WHERE QUERY.ROW_NUM BETWEEN @lower AND @higher
                                                        ", new { @lower = skip.Value + 1, @higher = skip.Value + take.Value, @sort = sort, @searchfield = string.IsNullOrEmpty(searchfield) ? "1" : searchfield, @searchvalue = string.IsNullOrEmpty(searchvalue) ? "1" : searchvalue, @sunil = "123" });
                return posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public int Count()
        {
            try
            {
                db.Open();
                int c = db.Query<int>("Select count(1) from posts").FirstOrDefault();
                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public IEnumerable<Post> GetAllWithAuthors()
        {
            try
            {
                db.Open();
                IEnumerable<Post> posts = db.Query<Post, User, Post>("Select * FROM Posts INNER JOIN Users ON Users.UserId = Posts.AuthorId ORDER BY PostId DESC", (post, user) => { post.Author = user; return post; }, splitOn: "UserId,PostId");
                return posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public Post GetById(int post_id)
        {
            try
            {
                db.Open();
                Post post = db.Query<Post>("Select * from posts where PostId = @id", new { @id = post_id }).SingleOrDefault<Post>();
                return post;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public Post GetByIdWithAuthors(int post_id)
        {
            try
            {
                db.Open();
                Post post = db.Query<Post, User, Post>("Select * FROM Posts INNER JOIN Users ON Users.UserId = Posts.AuthorId Where PostId = @id", (_post, user) => { _post.Author = user; return _post; }, new { @id = post_id }, splitOn: "UserId,PostId").SingleOrDefault<Post>();
                return post;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }


        public bool Save(Post post)
        {
            try
            {
                db.Open();
                int n = db.Execute(@"Insert into posts (Title, Slug, MetaKeywords, MetaDescription, Content, CreateDate, UpdateDate, AuthorId)
                            values (@Title, @Slug, @MetaKeywords, @MetaDescription, @Content, @CreateDate, @UpdateDate, @AuthorId)",
                            new
                            {
                                @Title = post.Title,
                                @Slug = post.Slug,
                                @MetaKeywords = post.MetaKeywords,
                                @MetaDescription = post.MetaDescription,
                                @Content = post.Content,
                                @CreateDate = post.CreateDate,
                                @UpdateDate = post.UpdateDate,
                                @AuthorId = post.AuthorId
                            }
                          );
                return (n > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public bool Update(Post post)
        {
            try
            {
                db.Open();
                int n = db.Execute(@"Update posts set 
                            Title = @Title,
                            Slug = @Slug,
                            MetaKeywords = @MetaKeywords,
                            MetaDescription = @MetaDescription,
                            Content = @Content,
                            UpdateDate = @UpdateDate,
                            AuthorId = @AuthorId
                            where PostId = @id", 
                            new
                            {
                                @id = post.PostId,
                                @Title = post.Title,
                                @Slug = post.Slug,
                                @MetaKeywords = post.MetaKeywords,
                                @MetaDescription = post.MetaDescription,
                                @Content = post.Content,
                                @UpdateDate = post.UpdateDate,
                                @AuthorId = post.AuthorId
                            }
                          );
                return (n > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public bool Delete(Post post)
        {
            try
            {
                db.Open();
                int n = db.Execute("Delete from posts where PostId = @id", new { @id = post.PostId });
                return (n > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }
}