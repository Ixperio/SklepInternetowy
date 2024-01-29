using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Sklep.Models;
using Sklep.Db_Context;
using System.Timers;
using Sklep.Models.Utils;

namespace Sklep
{
    public class GlobalDataSaver : System.Web.HttpApplication
    {
        private Timer timer;

        public GlobalDataSaver()
        {
            int intervalMilliseconds = 10000;
            timer = new Timer(intervalMilliseconds);
            timer.Elapsed += TimerElapsed;
        }

        public void Start()
        {
            // Pobieranie danych z bazy danych i ustawianie wartości początkowej
            int initialValue = GetDataFromDatabase();
            AppGlobalDataContext.LiczbaWyswietlen = initialValue;

            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var dataToSave = GetInformationFromContext();
            if (dataToSave.HasValue)
            {
                SaveToDatabase(dataToSave.Value);
            }
        }

        private int GetDataFromDatabase()
        {
            using (var _db = new MyDbContext())
            {
                var entity = _db.Globals.FirstOrDefault(c => c.Name == "liczba_wyswietlen");
                return entity != null ? entity.Value : 0;
            }
        }

        private void SaveToDatabase(int data)
        {
            using (var _db = new MyDbContext())
            {
                var entity = _db.Globals.FirstOrDefault(c => c.Name == "liczba_wyswietlen");
                if (entity != null)
                {
                    entity.Value = data;
                    _db.SaveChanges();
                }
            }
        }

        private int? GetInformationFromContext()
        {
            return AppGlobalDataContext.LiczbaWyswietlen;
        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Inicjalizacja bazy danych
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>());
            MyDbContext context = MyDbContext.GetInstance();

            // Licznik wyświetleń strony
            GlobalDataSaver contextGlobalData = new GlobalDataSaver();
            contextGlobalData.Start();
        }
    }

}
