using FEZAutoScore.Model.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace FEZAutoScore.Model.Repository
{
    public class ScoreRepository : DbContext
    {
        private const string ScoreDbFileName = "score.db";

        public DbSet<ScoreEntity> ScoreDbSet { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ScoreDbFileName);

            var connectionString = new SqliteConnectionStringBuilder { DataSource = filePath }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }
    }
}
