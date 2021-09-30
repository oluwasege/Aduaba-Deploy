using Aduaba.DTO.Card;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardControl _cardControl;

        public CardController(ICardControl cardControl)
        {
            _cardControl = cardControl;
        }

        [HttpGet("view-cards")]
        public async Task<IActionResult>GetCustomerCard()
        {
            var result = await _cardControl.GetAllCustomerCreditCardsAsync();
            if(result==null)
            {
                return NotFound("No cards have been added");
            }

            return Ok(result);
        }

        [HttpPost("add-card")]
        public async Task<IActionResult>AddCard([FromBody]AddCardRequest model)
        {
            var result = await _cardControl.SaveCreditCardAsync(model);
            
            return Ok(result);
        }

        [HttpDelete("delete-card")]
        public async Task<IActionResult>DeleteCard([FromBody]List<string>cardIds)
        {
            var result = await _cardControl.DeleteCreditCardAsync(cardIds);
            return Ok(result);
        }
    }
}
