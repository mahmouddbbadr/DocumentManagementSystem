using Application.Dtos;
using Application.IServices;
using Infrasturcture.Services;
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



        [HttpGet("SortByNameAscending")]
        public async Task<IActionResult> GetDocumentsSortedByNameAccending(string directoryName, int page, int pageSize)
        {
            var result = await documentService.SortByNameAscending(directoryName, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortBySizeAscending")]
        public async Task<IActionResult> GetDocumentsSortedBySizeAccending(string directoryName, int page, int pageSize)
        {
            var result = await documentService.SortBySizeAscending (directoryName, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortByDateAscending")]
        public async Task<IActionResult> GetDocumentsSortedByDateAccending(string directoryName, int page, int pageSize)
        {
            var result = await documentService.SortByDateAscending(directoryName, page,  pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }


        [HttpGet("SortByNameDescending")]

        public async Task<IActionResult> GetDocumentsSortedByNameDescending(string directoryName, int page, int pageSize)
        {
            var result = await documentService.SortByNameDescending(directoryName, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortBySizeDescending")]
        public async Task<IActionResult> GetDocumentsSortedBySizeDescending(string directoryName, int page, int pageSize)
        {
            var result = await documentService.SortBySizeDescending(directoryName, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SortByDateDescending")]
        public async Task<IActionResult> GetDocumentsSortedByDateDescending(string directoryName, int page, int pageSize)
        {
            var result = await documentService.SortByDateDescending(directoryName, page, pageSize);
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


        [HttpGet("SharedSortedByNameAscending")]
        public async Task<IActionResult> SharedSortedByNameAscending(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocumentSortedByNameAscending(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("SharedSortedBySizeAscending")]
        public async Task<IActionResult> SharedSortedBySizeAscending(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocumentSortedBySizeAscending(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("SharedSortedByDateAscending")]
        public async Task<IActionResult> SharedSortedByDateAscending(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocumentSortedByDateAscending(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("SharedSortedByNameDescending")]
        public async Task<IActionResult> SharedSortedByNameDescending(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocumentSortedByNameDescending(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("SharedSortedBySizeDescending")]
        public async Task<IActionResult> SharedSortedBySizeDescending(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocumentSortedBySizeDescending(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpGet("SharedSortedByDateDescending")]
        public async Task<IActionResult> SharedSortedByDateeDescending(int page, int pageSize)
        {
            var result = await documentService.GetSharedDocumentSortedByDateDescending(page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchDocuments(string filter, int page, int pageSize)
        {
            var result = await documentService.SearchDocuments(filter, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SearchShared")]
        public async Task<IActionResult> SearchSharedDocuments(string filter, int page, int pageSize)
        {
            var result = await documentService.SearchSharedDocuments(filter, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminSearchDocuments")]
        public async Task<IActionResult> AdminSearchDocumentsByDirectoryName(string filter, string name, string email, int page, int pageSize)

        {
            var result = await documentService.AdminSearchDocumentsByDirectoryName(filter, name, email, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



    }
}
