using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps";
      return _db.Query<Keep>(sql);
    }

    public Keep Get(string id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    public void Create(Keep newKeep)
    {
      string sql = @"
            INSERT INTO keeps
            (id, name, description, img)
            VALUES
            (@Id, @Name, @Description,@ImgUrl)";
      _db.Execute(sql, newKeep);
    }

    public void Edit(Keep keep)
    {
      string sql = @"
            UPDATE keeps
            SET
                name = @Name,
                description = @Description,
                img = @ImgUrl
            WHERE id = @Id";
      _db.Execute(sql, keep);

    }

    public void Delete(string id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }

  }
}