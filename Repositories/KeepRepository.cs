using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
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
      string sql = "SELECT * FROM keeps WHERE private=0";
      return _db.Query<Keep>(sql);
    }

    public Keep Get(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    public IEnumerable<Keep> GetByUser(string id)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @id";
      return _db.Query<Keep>(sql, new { id });
    }

    public int Create(Keep newKeep)
    {
      string sql = @"
            INSERT INTO keeps
            (name, description, imgUrl,private,views,kept,userId)
            VALUES
            (@Name, @Description,@ImgUrl,@Private,@Views,@Kept,@UserId);
             SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    public void Edit(Keep keep)
    {
      string sql = @"
            UPDATE keeps
            SET
                name = @Name,
                description = @Description,
                imgUrl = @ImgUrl,
                views=@Views,
                kept=@Kept
            WHERE id = @Id";
      _db.Execute(sql, keep);

    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }

  }
}