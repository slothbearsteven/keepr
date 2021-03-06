using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }


    public IEnumerable<Vault> GetByUser(string id)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @id";
      return _db.Query<Vault>(sql, new { id });
    }

    public Vault Get(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    public int Create(Vault newVault)
    {
      string sql = @"
            INSERT INTO vaults
            (name,description,userId)
            VALUES
            (@Name,@Description,@UserId);
            SELECT LAST_INSERT_ID();
            ";
      return _db.ExecuteScalar<int>(sql, newVault);

    }

    public void Edit(Vault vault)
    {
      string sql = @"
            UPDATE vaults
            SET
              name=@Name,
              description=@Description  
            WHERE id = @Id";
      _db.Execute(sql, vault);

    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id";
      _db.Execute(sql, new { id });
    }

    public void AddKeep(int vaultId, int keepId, string userId)
    {
      string sql = @"
            INSERT INTO vaultkeeps
            (vaultId, keepId, userId)
            VALUES
            (@vaultId, @keepId, @userId)";
      _db.Execute(sql, new { vaultId, keepId, userId });
    }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
                SELECT * FROM vaultkeeps so
                INNER JOIN keeps s ON s.id = so.keepId
                WHERE so.vaultId = @vaultId
            ";
      return _db.Query<Keep>(sql, new { vaultId });
    }

    internal VaultKeeps GetVaultKeeps(VaultKeeps so)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @VaultId AND keepId = @KeepId";
      return _db.QueryFirstOrDefault<VaultKeeps>(sql, so);
    }

    public void RemoveKeepFromVault(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}