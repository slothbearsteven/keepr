using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Keepr.Services;

namespace Keepr.Controllers
{

  [ApiController]
  [Route("/api/[controller]")]
  public class VaultsController : ControllerBase
  {

    private readonly VaultsService _vs;
    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      try
      {
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vs.Get(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vs.Get(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public ActionResult<Vault> Create([FromBody] Vault newVault)
    {
      try
      {
        newVault.UserId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vs.Create(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{id}")]
    public ActionResult<Vault> Edit([FromBody] Vault newVault, int id)
    {
      try
      {
        newVault.Id = id;
        return Ok(_vs.Edit(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        var userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vs.Delete(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    //vaultkeeps
    [HttpPut("{id}/addKeep/{keepId}")]
    public ActionResult<string> AddKeepToVault([FromBody] Keep keep, int id, int keepId)
    {
      try
      {
        keep.Id = keepId;
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vs.AddKeep(id, keep.Id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<Vault> GetKeeps(int id)
    {
      try
      {
        return Ok(_vs.GetKeep(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}/removeKeep/{keepId}")]
    public ActionResult<string> RemoveKeepFromVault([FromBody] VaultKeeps vk, int id, int keepId)
    {
      try
      {
        vk.VaultId = id;
        vk.KeepId = keepId;
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vs.RemoveKeep(vk, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}