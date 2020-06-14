using Fiszki.Controllers;
using Fiszki.VievModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiszki.Repositories
{
    public class UserLogsRepository
    {
        public static List<UserLogs> GetUserLogsList(int idUser) // Lista z repozytorium, ponieważ będziemy zwracać (...VievModel)
        {
            //return Tascs.GetAllTasc();
            using (var dbContext = new FiszkiContext())       // Tutaj dodanemy new context stworzony w osobnym pliku
            {
                return dbContext.UsersLogs.Where(p => p.UserId == idUser).ToList();
            }
        }



        public static void AddUserLogsList(int id, string LoginEvent)
        {
            using (var dbContext = new FiszkiContext())   //standardowa linijka
            {

                dbContext.UsersLogs.Add(new Repositories.UserLogs   //Dodanie informacji o logowaniu + złe hasło
                {
                    UserId = id,
                    LoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  // Data rejestracji
                    LoginStatus = "Nieprawidłowe hasło"
                });
                dbContext.SaveChanges();

            }
        }


    }
}
