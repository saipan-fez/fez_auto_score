using FEZAutoScore.Model.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace FEZAutoScore.Model.Repository
{
    public class ScoreRepository : DbContext
    {
        private const string ScoreDbFileName = "score.db";

        public DbSet<ScoreEntity> ScoreDbSet { get; protected set; }

        public static void CreateDbFileIfNotExists()
        {
            var filePath = GetDbFilePath();

            if (!File.Exists(filePath))
            {
                using (var src = Application.GetResourceStream(new Uri(ScoreDbFileName, UriKind.Relative)).Stream)
                using (var dest = new FileStream(filePath, FileMode.CreateNew))
                {
                    src.CopyTo(dest);
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var filePath = GetDbFilePath();

            var connectionString = new SqliteConnectionStringBuilder { DataSource = filePath }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }

        private static string GetDbFilePath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ScoreDbFileName);
        }
    }
}
