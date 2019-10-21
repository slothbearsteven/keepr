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
      if (exists == null) { throw new Exception("Invalid Id Homie"); }
      return exists;
    }

    public Keep Create(Keep newKeep)
    {
      int id = _repo.Create(newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    public Keep Edit(Keep newData)
    {
      Keep Keep = _repo.Get(newData.Id);
      if (Keep == null) { throw new Exception("Invalid Id Homie"); }
      Keep.Name = newData.Name;
      Keep.Description = newData.Description;
      Keep.Views = newData.Views;
      Keep.Kept = newData.Kept;
      Keep.ImgUrl = newData.ImgUrl;
      _repo.Edit(Keep);
      return Keep;
    }

    public string Delete(int id)
    {
      Keep Keep = _repo.Get(id);
      if (Keep == null) { throw new Exception("Invalid Id Homie"); }
      _repo.Delete(id);
      return "Successfully Booted";
    }
  }
}