using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultRepository _repo;
    private readonly KeepRepository _keepRepo;
    public VaultsService(VaultRepository repo, KeepRepository keepRepo)
    {
      _keepRepo = keepRepo;
      _repo = repo;
    }

    //VAULT SERVICES
    public IEnumerable<Vault> Get()
    {
      return _repo.Get();
    }

    public Vault Get(int id, string userId)
    {

      Vault exists = _repo.Get(id);
      if (exists == null || exists.UserId != userId) { throw new Exception("Invalid Id "); }
      return exists;
    }

    public Vault Create(Vault newVault)
    {
      int id = _repo.Create(newVault);
      newVault.Id = id;
      return newVault;
    }

    public Vault Edit(Vault newData)
    {
      Vault vault = _repo.Get(newData.Id);
      if (vault == null) { throw new Exception("Invalid Id "); }
      vault.UserId = newData.UserId;
      _repo.Edit(vault);
      return vault;
    }

    public IEnumerable<Keep> GetKeep(int vaultId)
    {
      Vault vault = _repo.Get(vaultId);
      if (vault == null) { throw new Exception("Invalid Id "); }
      return _repo.GetKeepsByVaultId(vaultId);
    }

    public string Delete(int id, string userId)
    {
      Vault vault = _repo.Get(id);
      if (vault == null || vault.UserId != userId) { throw new Exception("Invalid Request"); }
      _repo.Delete(id);
      return "Successfully Deleted";
    }


    // VAULTKEEP SERVICES VVV
    public string AddKeep(int id, int keepId, string userId)
    {
      Vault vault = _repo.Get(id);
      if (vault == null || vault.UserId != userId) { throw new Exception("Invalid Vault Id "); }
      Keep keepToAdd = _keepRepo.Get(keepId);
      if (keepToAdd == null) { throw new Exception("Invalid Keep Id "); }
      _repo.AddKeep(id, keepId);
      return "Successfully added Keep to Vault";
    }

    public string RemoveKeep(VaultKeeps vk, string userId)
    {
      VaultKeeps vault = _repo.GetVaultKeeps(vk);
      if (vault == null || vault.UserId != userId) { throw new Exception("Invalid Id"); }
      _repo.RemoveKeepFromVault(vault.Id);
      return "Successfully Booted";
    }
  }
}