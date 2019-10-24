using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepService
  {
    private readonly KeepRepository _repo;
    public KeepService(KeepRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    public Keep Get(int id)
    {
      Keep exists = _repo.Get(id);
      if (exists == null) { throw new Exception("Invalid Id "); }
      return exists;
    }

    public IEnumerable<Keep> GetByUser(string id)
    {
      if (id == null) { throw new Exception("Invalid User Id "); }
      return _repo.GetByUser(id);
    }

    public Keep Create(Keep newKeep)
    {
      int id = _repo.Create(newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    public Keep Edit(Keep newData)
    {
      Keep keep = _repo.Get(newData.Id);
      if (keep == null) { throw new Exception("Invalid Id "); }
      keep.Name = newData.Name;
      keep.Description = newData.Description;
      keep.Views = newData.Views;
      keep.Kept = newData.Kept;
      keep.ImgUrl = newData.ImgUrl;
      _repo.Edit(keep);
      return keep;
    }

    public string Delete(int id)
    {
      Keep Keep = _repo.Get(id);
      if (Keep == null) { throw new Exception("Invalid Id "); }
      _repo.Delete(id);
      return "Successfully Deleted";
    }
  }
}