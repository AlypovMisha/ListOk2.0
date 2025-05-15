using ListOk.Application.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ListOk.Presentation.Controllers
{
    [ApiController]
    [Route("api/Boards")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly ReportService _reportService;

        public BoardController(IBoardService boardService, ReportService reportService)
        {
            _boardService = boardService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardDto>>> GetBoards()
        {
            var userId = new Guid("01968d1d-751f-725b-acab-3ad037415c65");
            var boards = await _boardService.GetBoardsAsync(userId);
            return Ok(boards);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BoardDto>> GetBoard(Guid id)
        {
            var board = await _boardService.GetBoardByIdAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard([FromBody] CreateBoardRequest request)
        {
            var board = await _boardService.CreateBoardAsync(new Guid("01968d1d-751f-725b-acab-3ad037415c65"), request.Title, request.Description); 
            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBoard(Guid id, [FromBody] UpdateBoardRequest request)
        {
            var board = await _boardService.UpdateBoardAsync(id, request.Title, request.Description);
            if (board == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            try
            {
                await _boardService.DeleteBoardAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}/export")]
        public async Task<IActionResult> ExportBoardReport(Guid id)
        {
            try
            {
                var reportBytes = await _reportService.GenerateBoardReportAsync(id);
                return File(reportBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Board_{id}_Report.csv");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Board not found");
            }
        }
    }

}

