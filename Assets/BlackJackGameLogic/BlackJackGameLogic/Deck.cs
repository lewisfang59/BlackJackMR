using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlackJackGameLogic
{
    class Deck
    {
        private List<Card> cards;
        
        public List<Card> Cards{ 
            get { return this.cards; } set { this.cards = value; } 
        }

        public void printDeck()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Debug.Log(cards[i].Value.ToString() + " of " + cards[i].Suit.ToString());
            }
        }


        public Card DealRandomCard()
        {
            int index = UnityEngine.Random.Range(0, cards.Count - 1);

            Card drawn = cards[index];
            cards.RemoveAt(index);
            return drawn;
        }

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
