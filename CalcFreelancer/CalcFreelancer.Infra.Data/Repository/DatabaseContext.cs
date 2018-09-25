using CalcFreelancer.Domain.Interfaces;
using CalcFreelancer.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalcFreelancer.Infra.Data.Repository
{
    public class DatabaseContext
    {
        static DatabaseContext DatabaseSingleton;
        static SQLiteAsyncConnection Conn;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (DatabaseSingleton == null)
                    DatabaseSingleton = new DatabaseContext();

                return Conn;
            }
        }

        public DatabaseContext()
        {
            var caminhoBancoDeDados = DependencyService.Get<IDatabaseFile>().GetFilePath("calcfreelancer.db3");
            Conn = new SQLiteAsyncConnection(caminhoBancoDeDados);

            Conn.CreateTableAsync<Projeto>().Wait();
            Conn.CreateTableAsync<Imc>().Wait();
            Conn.CreateTableAsync<Profissional>().Wait();
        }
    }
}
