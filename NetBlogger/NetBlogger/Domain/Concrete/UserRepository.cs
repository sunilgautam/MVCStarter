using NetBlogger.Domain.Abstract;
using NetBlogger.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using NetBlogger.Models;

namespace NetBlogger.Domain.Concrete
{
    public class UserRepository : DataContext, IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            try
            {
                db.Open();
                IEnumerable<User> users = db.Query<User>("Select * from users ORDER BY UserId DESC");
                return users;
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

        public IEnumerable<User> GetAll(int? skip, int? take)
        {
            try
            {
                db.Open();
                IEnumerable<User> users = db.Query<User>(@"
                                                        SELECT TOP (@take) * FROM 
                                                        (
	                                                        SELECT	*,
			                                                        ROW_NUMBER() OVER (ORDER BY UserId) AS ROW_NUM
	                                                        FROM	Users
                                                        ) 
                                                        AS QUERY
                                                        WHERE QUERY.ROW_NUM > @skip
                                                        ORDER BY QUERY.UserId DESC", new { @take = take.Value, @skip = skip.Value });
                return users;
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
                int c = db.Query<int>("Select count(1) from users").FirstOrDefault();
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

        public User GetById(int user_id)
        {
            try
            {
                db.Open();
                User user = db.Query<User>("Select * from users where UserId = @id", new { @id = user_id }).SingleOrDefault<User>();
                return user;
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


        public bool Save(User user)
        {
            try
            {
                db.Open();
                int n = db.Execute(@"Insert into Users (Login, Password, FirstName, LastName, Email, Website, Info)
                            values (@Login, @Password, @FirstName, @LastName, @Email, @Website, @Info)",
                            new
                            {
                                @Login = user.Login,
                                @Password = user.Password,
                                @FirstName = user.FirstName,
                                @LastName = user.LastName,
                                @Email = user.Email,
                                @Website = user.Website,
                                @Info = user.Info
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

        public bool Update(UserEdit user)
        {
            try
            {
                db.Open();
                int n = db.Execute(@"Update Users set 
                            FirstName = @FirstName,
                            LastName = @LastName,
                            Email = @Email,
                            Website = @Website,
                            Info = @Info
                            where UserId = @id",
                            new
                            {
                                @id = user.UserId,
                                @FirstName = user.FirstName,
                                @LastName = user.LastName,
                                @Email = user.Email,
                                @Website = user.Website,
                                @Info = user.Info
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

        public bool ChangePassword(UserChangePassword user)
        {
            try
            {
                db.Open();
                int n = db.Execute(@"Update Users set 
                            Password = @Password
                            where UserId = @id",
                            new
                            {
                                @id = user.UserId,
                                @Password = user.Password
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

        public bool Delete(User user)
        {
            try
            {
                db.Open();
                int n = db.Execute("Delete from Users where UserId = @id", new { @id = user.UserId });
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