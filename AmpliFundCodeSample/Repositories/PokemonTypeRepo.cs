using AmpliFundCodeSample.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace AmpliFundCodeSample.Repositories
{
    public class PokemonTypeRepo
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;

        public IEnumerable<PokemonType> GetTypes(int? id = null, string name = "")
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"SELECT PokemonTypeId, Name
                            FROM PokemonType
                            /**where**/";

                var sqlBuilder = new SqlBuilder();

                var sqlTemplate = sqlBuilder.AddTemplate(sql);

                if (id.HasValue && id.Value > 0)
                {
                    sqlBuilder.Where("PokemonTypeId = @id", new { id });
                }

                if (!string.IsNullOrEmpty(name))
                {
                    sqlBuilder.Where("Name = @name", new { name });
                }

                return db.Query<PokemonType>(sqlTemplate.RawSql, sqlTemplate.Parameters);
            }
        }

        public void CreateType(PokemonType type)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"INSERT INTO PokemonType(Name) 
                            VALUES (@Name)";

                db.Execute(sql, new
                {
                    Name = type.Name
                });
            }
        }

        public void UpdateType(PokemonType type)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"UPDATE PokemonType 
                            SET Name = @Name
                            WHERE PokemonTypeId = @PokemonTypeId";


                db.Execute(sql, new
                {
                    PokemonTypeId = type.PokemonTypeId,
                    Name = type.Name
                });
            }
        }

        public void DeleteType(int pokemontypeId)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"DELETE FROM PokemonType WHERE PokemonTypeId = @PokemonTypeId";

                db.Execute(sql, new
                {
                    PokemonTypeId = pokemontypeId
                });
            }
        }
    }
}