using Aduaba.Data;
using Aduaba.DTO.Card;
using Aduaba.DTOPresentation.Card;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Aduaba.Services.CardService.CardTypeInfo;

namespace Aduaba.Services.CardService
{
    public class CardControl:ICardControl
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;
        public CardControl(IAuthenticatedUserService authenticatedUser, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
        }

        public async Task<string> SaveCreditCardAsync(AddCardRequest card)
        {
            //Check if card already exist using card number
            //Encrypt card number to use for search along with user id
            var encryptedCardNumber = Encrypt(card.CardNumber);
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var existingCard = await _context.Cards.FirstOrDefaultAsync(c => c.ApplicationUserId==currentUser.Id && c.CardNumber == encryptedCardNumber);

            if (existingCard != null) 
            {
                return "This card exists, Please enter a new card details";
            }

           var outcome= IsCardNumberValid(card.CardNumber);
            if(outcome==true)
            {
               var cardtype= CardTypeInfo.GetCardType(card.CardNumber);
                if(cardtype==CardType.Unknown)
                {
                    return "Invalid card details or we do not support this card";
                }
                else
                {
                    var newCard = new Card
                    {
                        CardHolderName = card.CardHolderName,
                        CardNumber = encryptedCardNumber,
                        CCV = Encrypt(card.CCV),
                        ExpiryDate = card.ExpiryDate,
                        ApplicationUserId = currentUser.Id,
                        Id = Guid.NewGuid().ToString(),
                        CardType = cardtype.ToString()
                        
                    };

                    await _context.Cards.AddAsync(newCard);
                    await _context.SaveChangesAsync();
                    return "Card Added";
                }

            }

            else
            {
                return "Invalid card details or we do not support this card";
            }
                

            
        }

        public async Task<List<CardView>> GetAllCustomerCreditCardsAsync()
        {
            List<Card> availableCards = new List<Card>();
            List<CardView> cardstoShow = new List<CardView>();
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            availableCards = await _context.Cards.Where(c => c.ApplicationUserId == currentUser.Id).ToListAsync();

            if (availableCards.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var card in availableCards)
                {
                    cardstoShow.Add(new CardView()
                    {
                        Id = card.Id,
                        CardNumber =Decrypt(card.CardNumber),
                        ExpiryDate = card.ExpiryDate,
                        CardType = card.CardType
                    });
                }

                return cardstoShow;
            }
        }

        public async Task<string> DeleteCreditCardAsync(List<string> cardIds)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            List<Card> cardsToDelete = new List<Card>();
            cardsToDelete = await _context.Cards.Where(c => c.ApplicationUserId == currentUser.Id && cardIds.Contains(c.Id)).ToListAsync();

            if (cardsToDelete.Count != 0)
            {
                _context.Cards.RemoveRange(cardsToDelete);
                await _context.SaveChangesAsync();
                return "Card(s) Deleted";
            }

            return "Card(s) doesnt exist";
        }

        private static string Decrypt(string value)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(value);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }
            return decrypted;
        }

        private static string Encrypt(string value)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        private static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;

            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            // Number is valid if sum of both checksums MOD 10 equals 0
            return ((checkSum % 10) == 0);
        }
    }
}
    

