using Application.Dtos;
using Application.IRepository;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
                return Ok(result.document);
            }
            return NotFound(result.Message);
        }


        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var result = await documentService.GetDocuments();
            if (result.Success)
            {
                return Ok(result.documents);
            }
            return NotFound(result.Message);
        }

        [HttpGet("SortByName")]
        public async Task<IActionResult> GetDocumentsSortedByName()
        {
            var result = await documentService.SortByName();
            if (result.Success)
            {
                return Ok(result.documents);
            }
            return NotFound(result.Message);
        }

        [HttpGet("SortBySize")]
        public async Task<IActionResult> GetDocumentsSortedBySize()
        {
            var result = await documentService.SortBySize();
            if (result.Success)
            {
                return Ok(result.documents);
            }
            return NotFound(result.Message);
        }

        [HttpGet("SortByDate")]
        public async Task<IActionResult> GetDocumentsSortedByDate()
        {
            var result = await documentService.SortByDate();
            if (result.Success)
            {
                return Ok(result.documents);
            }
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> UploadeDocument([FromForm] DocumentInputDto documentDto)
        {
            var result = await documentService.UploadeDocument(documentDto);
            if(result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("Download/name")]
        public async Task<IActionResult> DownloadDocument(string name)
        {
            var result = await documentService.DownloadDocument(name);
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
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

    }
}
