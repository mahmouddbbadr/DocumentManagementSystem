using Application.Dtos;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentaion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetDocument(string name)
        {
            var result = await documentService.GetDocument(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetDocuments(int page, int pageSize)
        {
            var result = await documentService.GetDocuments( page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("Shared")]
        public async Task<IActionResult> GetSharedDocuments(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocuments( page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("DirectoryName")]
        public async Task<IActionResult> GetDocumentsByDirectoryName(string name, int page, int pageSize)
        {
            var result = await documentService.GetDocumentsByDirectoryName(name, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("DirectoryName/Email")]
        public async Task<IActionResult> AdminGetDocumentsByDirectoryName(string name, string email, int page, int pageSize)
        {
            var result = await documentService.AdminGetDocumentsByDirectoryName(name, email, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortByName")]
        public async Task<IActionResult> GetDocumentsSortedByName()
        {
            var result = await documentService.SortByName();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortBySize")]
        public async Task<IActionResult> GetDocumentsSortedBySize()
        {
            var result = await documentService.SortBySize();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortByDate")]
        public async Task<IActionResult> GetDocumentsSortedByDate()
        {
            var result = await documentService.SortByDate();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadeDocument([FromForm] DocumentInputDto documentDto)
        {
            var result = await documentService.UploadeDocument(documentDto);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut()]
        public async Task<IActionResult> EditDocument(string OldName, string NewName )
        {
            var result = await documentService.EditDocument(OldName, NewName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("Download")]
        public async Task<IActionResult> DownloadDocument(string name, string wwwRootName)
        {
            var result = await documentService.DownloadDocument(name, wwwRootName);
            if (result.Success)
            {
                return File(result.bytes, result.contentType, result.name);

            }
            return NotFound(result.Message);
        }

        [HttpDelete("name")]
        public async Task<IActionResult> DeleteDocument(string name)
        {
            var result = await documentService.DeleteDocument(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
