using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<Vault> Get()
    {
      string sql = "SELECT * FROM vaults";
      return _db.Query<Vault>(sql);
    }

    public Vault Get(int id)
    {
      //FIXME get the relational data
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
      return _db.ExecuteScalar<int>(sql, newVault); //get the new id back from line 36 and return to service

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

    public void AddKeep(int vaultId, string keepId)
    {
      string sql = @"
            INSERT INTO vaultkeeps
            (vaultId, keepId)
            VALUES
            (@vaultId, @keepId)";
      _db.Execute(sql, new { vaultId, keepId });
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