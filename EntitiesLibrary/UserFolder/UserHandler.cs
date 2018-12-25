using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLibrary.UserFolder
{
    public class UserHandler
    {
        public List<Role> GetRoles()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from r in context.roles select r).ToList();
            }
        }
        public List<User> GetUsers()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from user in context.Users
                        .Include(u=> u.Role)
                        .Include(u=> u.Address)
                        select user).ToList();
            }
        }
        public User GetUser(string emailID)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from usr in context.Users
                        .Include(u => u.Role)
                        .Include(u => u.Address)
                        where usr.Email ==emailID select usr).FirstOrDefault();
            }
        }
        public Role getRole(string name)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from r in context.roles where r.Name == name select r).FirstOrDefault();
            }
        }

        public void AddUser(User usr)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(usr.Address.City).State = System.Data.Entity.EntityState.Unchanged;
                context.Entry(usr.Role).State = System.Data.Entity.EntityState.Unchanged;
                context.Users.Add(usr);
                context.SaveChanges();
            }
        }
        public void AddRoll(Role role)
        {
            using (ContextClass context = new ContextClass())
            {
                context.roles.Add(role);
                context.SaveChanges();
            }
        }
       
        public void GiveAdminAthority(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                User user = context.Users.Find(id);
                user.Role = new Role { Id = 2 };

                context.Entry(user.Role).State = EntityState.Unchanged;
                context.SaveChanges();
            }
        }
        public void RemoveAdminship(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                User user = context.Users.Find(id);
                user.Role = new Role { Id = 3 };
                context.Entry(user.Role).State = EntityState.Unchanged;
                context.SaveChanges();
            }
        }
        public User GetUserForLogin(string LoginId, string LoginPass)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from u in context.Users
                        .Include(u => u.Address.City.Province.Country)
                        .Include(u => u.Role)
                        where u.Email == LoginId && u.Password == LoginPass
                        select u).FirstOrDefault();
            }

        }
        public void UpdateUser(User user, int id)
        {
            using (ContextClass context = new ContextClass())
            {
                User find = (from u in context.Users
                            .Include(u => u.Address.City.Province.Country)
                             where u.Id == id
                             select u).FirstOrDefault();

                find.FirstName = user.FirstName;
                find.LastName = user.LastName;
                find.ContactNumber = user.ContactNumber;
                find.Address.StreetAddress = user.Address.StreetAddress;
                if (find.Address.City.Id!=user.Address.City.Id)
                {
                    find.Address.City = user.Address.City;
                }
                context.Entry(find.Address.City).State = EntityState.Unchanged;
                context.SaveChanges();
            }
        }
        public User GetUserForRenewSession(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from u in context.Users
                        .Include(u => u.Address.City.Province.Country)
                        .Include(u => u.Role)
                        where u.Id == id
                        select u).FirstOrDefault();
            }
        }
        public void ChangePassword(string password, int id)
        {
            using(ContextClass context = new ContextClass())
            {
                User find = context.Users.Find(id);
                find.Password = password;
                context.SaveChanges();
            }
        }
        public User ForgotPass_getUser(string emailId)
        {
            using (ContextClass context = new ContextClass())
            {
                User Find = (from u in context.Users where u.Email == emailId select u).FirstOrDefault();
                return Find;
            }
        }
    }
}
