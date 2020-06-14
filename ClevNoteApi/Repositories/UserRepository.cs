using Fiszki.Controllers;
using Fiszki.Services;
using Fiszki.VievModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiszki.Repositories
{
    public class UserRepository
    {

        public static string Login(CredentialsModel value)
        {
            using (var dbContext = new FiszkiContext())
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Login == value.Login);
                if (user == null)
                {
                    return null;
                }

                if (user.Password != value.Password)
                {
                    dbContext.UsersLogs.Add(new Repositories.UserLogs   //Dodanie informacji o logowaniu + złe hasło
                    {
                        UserId = user.Id,
                        LoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  // Data rejestracji
                        LoginStatus = "Nieprawidłowe hasło"
                        //var x = UserLogsRepository.AddUserLogsList(user.Id, )
                    });
                    dbContext.SaveChanges();

                    return null;
                }

                dbContext.UsersLogs.Add(new Repositories.UserLogs   //Dodanie informacji o logowaniu + złe hasło
                {
                    UserId = user.Id,
                    LoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  // Data rejestracji
                    LoginStatus = "Zalogowano"
                });
                dbContext.SaveChanges();

                return JWTService.GenerateToken(user.Id);
            }
        }


        public static (string status, string coment, string token) Register(CredentialsModel value)
        {
            using (var dbContext = new FiszkiContext())
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Login == value.Login);
                if (user != null)
                    return (null, "Login zajęty", null);

                if (value.Login.Length < 4)
                    return (null, "Login musi mieć minimum 5 znaków", null);

                if (value.Password.Length < 4)
                    return (null, "Hasło musi mieć minimum 5 znaków", null);

                if (value.Login == value.Password)
                    return (null, "Login musi się różnic od hasła", null);

                var newAccount = new User
                {
                    Login = value.Login,
                    Password = value.Password,
                    DateAdd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  // Data rejestracji
                };

                dbContext.Users.Add(newAccount);
                dbContext.SaveChanges();

                int newAccountId = newAccount.Id;
                //return ("ok", $"{value.Login} [{newAccountId}], konto zostało dodane, możesz się zalogować");
                return ("ok", null, JWTService.GenerateToken(newAccountId));
            }
        }


        public static (string login, string dateAdd) Account(string id)
        {
            var dbContext = new FiszkiContext(); ;
            var login = dbContext.Users.FirstOrDefault(item => item.Id.ToString() == id).Login;
            var dateAdd = dbContext.Users.FirstOrDefault(item => item.Id.ToString() == id).DateAdd;

            return (login, dateAdd);
        }











    }
}
