using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ListOk.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColumnsController : ControllerBase
    {
        private readonly IColumnService _columnService;

        public ColumnsController(IColumnService columnService)
        {
            _columnService = columnService;
        }

        [HttpGet("board/{boardId:guid}")]
        public async Task<ActionResult<IEnumerable<ColumnDto>>> GetColumns(Guid boardId)
        {
            var columns = await _columnService.GetColumnsByBoardIdAsync(boardId);
            return Ok(columns);
        }

        [HttpPost]
        public async Task<ActionResult<ColumnDto>> CreateColumn([FromBody] CreateColumnRequest request)
        {
            if (string.IsNullOrEmpty(request.Title) || request.BoardId == Guid.Empty)
            {
                return BadRequest("Title и BoardId обязательны");
            }
            var column = await _columnService.CreateColumnAsync(request.Title, request.BoardId);
            return Ok(column); // Или CreatedAtAction, если нужен 201
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateColumn(Guid id, [FromBody] UpdateColumnRequest request)
        {
            try
            {
                await _columnService.UpdateColumnAsync(id, request.Title);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteColumn(Guid id)
        {
            try
            {
                await _columnService.DeleteColumnAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
