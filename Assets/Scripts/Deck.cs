using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGameLogic
{
    class Deck
    {
        private List<Card> cards;
        
        public List<Card> Cards{ 
            get { return this.cards; } set { this.cards = value; } 
        }

        public void shuffleDeck()
        {
            cards.Clear();
            for(int i = 1; i < 5; i++)
            {
                for(int j = 1; j < 14; j++)
                {
                    Card card = new Card();
                    card.Value = (CardValue)j;
                    card.Suit = (CardSuit)i;
                    cards.Add(card);
                }
            }
            Random r = new Random(); 
            cards = cards.OrderBy(x => r.Next()).ToList();
        }

        public void printDeck()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine(cards[i].Value.ToString() + " of " + cards[i].Suit.ToString());
            }
        }


        public Card DealCards(Hand hand)
        {
            Card drawn = cards[cards.Count - 1];
            cards.Remove(drawn);
            hand.GetCards.Add(drawn);
            return drawn;
        }

        public Deck()
        {
            Cards = new List<Card>();
            shuffleDeck();
        }

    }
}
