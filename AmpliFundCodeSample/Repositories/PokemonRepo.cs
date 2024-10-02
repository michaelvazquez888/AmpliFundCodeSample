using AmpliFundCodeSample.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace AmpliFundCodeSample.Repositories
{
    // Normally, I would create an Interface that had these methods.
    public class PokemonRepo
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;

        public IEnumerable<Pokemon> GetPokemon(int? pokedexNum = null, string name = null)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"SELECT p.PokemonId, p.Name, p.PokedexNum, p.Type1Id, p.Type2Id, 
                                t1.PokemonTypeId AS PrimaryType, t1.Name, t2.PokemonTypeId AS SecondaryType, t2.Name
                            FROM Pokemon p
                            INNER JOIN PokemonType t1 ON p.Type1Id = t1.PokemonTypeId
                            LEFT JOIN PokemonType t2 ON p.Type2Id = t2.PokemonTypeId
                            /**where**/";

                var sqlBuilder = new SqlBuilder();

                var sqlTemplate = sqlBuilder.AddTemplate(sql);

                if (pokedexNum.HasValue && pokedexNum.Value > 0)
                {
                    sqlBuilder.Where("p.PokedexNum = @pokedexNum", new { pokedexNum });
                }

                if (!string.IsNullOrEmpty(name))
                {
                    sqlBuilder.Where("p.Name = @name", new { name });
                }

                return db.Query<Pokemon, PokemonType, PokemonType, Pokemon>(sqlTemplate.RawSql, (pokemon, type1, type2) =>
                {
                    pokemon.Type1 = type1;
                    pokemon.Type1.PokemonTypeId = pokemon.Type1Id;
                    pokemon.Type2 = type2;
                    pokemon.Type2.PokemonTypeId = pokemon.Type2Id ?? 0;
                    return pokemon;
                },
                param: sqlTemplate.Parameters,
                splitOn: "PrimaryType, SecondaryType");
            }
        }

        public void CreatePokemon(Pokemon pokemon)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"INSERT INTO Pokemon(Name, PokedexNum, Type1Id, Type2Id) 
                            VALUES (@Name, @PokedexNum, @Type1Id, @Type2Id)";

                db.Execute(sql, new
                {
                    Name = pokemon.Name,
                    PokedexNum = pokemon.PokedexNum,
                    Type1Id = pokemon.Type1Id,
                    Type2Id = pokemon.Type2Id
                });
            }
        }

        public void UpdatePokemon(Pokemon pokemon)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"UPDATE Pokemon 
                            SET Name = @Name, 
                                PokedexNum = @PokedexNum, 
                                Type1Id = @Type1Id, 
                                Type2Id = @Type2Id
                            WHERE PokemonId = @PokemonId";


                db.Execute(sql, new
                {
                    PokemonId = pokemon.PokemonId,
                    Name = pokemon.Name,
                    PokedexNum = pokemon.PokedexNum,
                    Type1Id = pokemon.Type1Id,
                    Type2Id = pokemon.Type2Id
                });
            }
        }

        public void DeletePokemon(int pokemonId)
        {
            using (var db = new SQLiteConnection(_connectionString))
            {
                var sql = @"DELETE FROM Pokemon WHERE PokemonId = @PokemonId";

                db.Execute(sql, new
                {
                    PokemonId = pokemonId
                });
            }
        }
    }
}