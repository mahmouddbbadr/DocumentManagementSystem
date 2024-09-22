﻿using Application.IServices;
using DocumentManagementSystem.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetDirectoryies()
        {
            var result = await directoryService.GetDirectoryies();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("UserId")]
        public async Task<IActionResult> AdminGetDirectoryies(string id)
        {
            var result = await directoryService.AdminGetDirectoryies(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }


        [HttpPost("Name")]
        public async Task<IActionResult> CreateDirectory(string name)
        {
            var result = await directoryService.CreateDirectory(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Name")]
        public async Task<IActionResult> EditDirectory(string name, DirectoryOutputDto directory)
        {
            var result = await directoryService.EditDirectory(name, directory);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("Name")]
        public async Task<IActionResult> DeleteDirectory(string name)
        {

            var result = await directoryService.DeleteDirectory(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
