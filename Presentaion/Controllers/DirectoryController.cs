using Application.Dtos;
using Application.IRepository;
using Application.IServices;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentaion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryService directoryService;


        public DirectoryController(IDirectoryService directoryService)
        {
            this.directoryService = directoryService;
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetDirectory(string name)
        {
            var result = await directoryService.GetDirectory(name);
            if(result.Success) 
            {
                return Ok(result.directory); 
            }
            return NotFound(result.Message);
        }


        [HttpGet]
        public async Task<IActionResult> GetDirectoryies()
        {
            var result = await directoryService.GetDirectoryies();
            if(result.Success)
            {
                return Ok(result.directories);
            }
            return NotFound(result.Message);

        }


        [HttpPost("Name")]
        public async Task<IActionResult> CreateDirectory(string name)
        {
            var result = await directoryService.CreateDirectory(name);
            if(result.Success )
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpDelete("Name")]
        public async Task<IActionResult> DeleteDirectory(string name)
        {

            var result = await directoryService.DeleteDirectory(name);
            if(result.Success )
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        }

        [HttpPut("MakeDirPublic")]
        public async Task<IActionResult> MakeDirectoryPublic(string name)
        {
            var result = await directoryService.MakeDirectoryPublic(name);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("MakeDirPrivate")]
        public async Task<IActionResult> MakeDirectoryPrivate(string name)
        {
            var result = await directoryService.MakeDirectoryPrivate(name);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


    }
}
