using Aduaba.DTO.Card;
using Aduaba.DTOPresentation.Card;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface ICardControl
    {
        public  Task<string> SaveCreditCardAsync(AddCardRequest card);
        public Task<List<CardView>> GetAllCustomerCreditCardsAsync();
        public  Task<string> DeleteCreditCardAsync(List<string> cardIds);
    }
}
