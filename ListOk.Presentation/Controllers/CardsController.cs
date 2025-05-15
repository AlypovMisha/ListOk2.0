using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ListOk.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("column/{columnId:guid}")]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards(Guid columnId)
        {
            var cards = await _cardService.GetCardsByColumnIdAsync(columnId);
            return Ok(cards);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CardDto>> GetCard(Guid id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null)
                return NotFound();
            return Ok(card);
        }

        [HttpPost]
        public async Task<ActionResult<CardDto>> CreateCard([FromBody] CreateCardRequest request)
        {
            var card = await _cardService.CreateCardAsync(request.Title, request.Description, request.ColumnId);
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCard(Guid id, [FromBody] UpdateCardRequest request)
        {
            try
            {
                await _cardService.UpdateCardAsync(id, request.Title, request.Description, request.Status, request.DueDate);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatusCard(Guid id, [FromBody] UpdateStatusCard request)
        {
            try
            {
                await _cardService.UpdateCardStatusAsync(id, request.Status);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id:guid}/move")]
        public async Task<IActionResult> MoveCard(Guid id, [FromBody] MoveCardRequest request)
        {
            try
            {
                await _cardService.MoveCardAsync(id, request.DestinationColumnId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            try
            {
                await _cardService.DeleteCardAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}