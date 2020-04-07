using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGameLogic
{
    class Hand
    {
        private List<Card> cards;

        public List<Card> GetCards { get { return this.cards; } }

        public Hand(int startingHand, Deck deck)
        {
            if (deck == null)
                Console.WriteLine("There are no decks to draw from");
            else if (deck.Cards.Count == 0)
                Console.WriteLine("there are no more cards to draw");
            else
            {
                cards = new List<Card>();
                for (int i = 0; i < startingHand; i++)
                {
                    deck.DealCards(this);
                }
            }
        }

        public void DrawCard(Deck deck)
        {
            deck.DealCards(this);
        }

        public void ShowHand()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine(cards[i].Value + " of " + cards[i].Suit);
            }
        }

        public void AddValue(Card card, ref int currentSum)
        {
            if (card.Value == CardValue.ace)
            {
                if (currentSum <= 10)
                    currentSum += 10;
                else
                    currentSum += 1;
            }
            else if (card.Value == CardValue.jack || card.Value == CardValue.queen || card.Value == CardValue.king)
                currentSum += 10;
            else
                currentSum += (int)card.Value;
        }
    }
}
