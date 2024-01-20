
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sklep.Db_Context 
{
    public class DbContextConnectionSingleton
    {
        private static DbContextConnectionSingleton _instance;

        private readonly MyDbContext _dbContext;
        private DbContextConnectionSingleton()
        {
            _dbContext = new();
        }
        public static DbContextConnectionSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbContextConnectionSingleton();
            }
            return _instance;
        }

        public MyDbContext MyDbContext { get { return _dbContext; } }   

    }
}