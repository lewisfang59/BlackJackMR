using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class that represents a deck with 52 cards.
    /// </summary>
    class Deck
    {
        private List<Card> cards;
        
        public List<Card> Cards{ 
            get { return this.cards; } set { this.cards = value; } 
        }

        /// <summary>
        /// Method to print what cards are in the deck
        /// </summary>
        public void printDeck()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Debug.Log(cards[i].Value.ToString() + " of " + cards[i].Suit.ToString());
            }
        }

        /// <summary>
        /// Pick a random card from deck and remove it from deck.
        /// </summary>
        /// <returns></returns>
        public Card DealRandomCard()
        {
            int index = UnityEngine.Random.Range(0, cards.Count - 1);

            Card drawn = cards[index];
            cards.RemoveAt(index);
            return drawn;
        }

        /// <summary>
        /// Constructs a Deck object
        /// </summary>
        public Deck(GameObject[] newDeck)
        {
            cards = new List<Card>();

            foreach(GameObject obj in newDeck)
            {
                String[] values = obj.name.Split('-');

                cards.Add(new Card(System.Int16.Parse(values[0]), obj));
            }
        }

    }
}
