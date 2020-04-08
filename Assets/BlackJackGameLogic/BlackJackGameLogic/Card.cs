using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlackJackGameLogic
{
    /// <summary>
    /// enum for the types of cards that will appear. 
    /// </summary>
    public enum CardValue
    {
        ace = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5, 
        six = 6,
        seven = 7,
        eight = 8, 
        nine = 9,
        ten = 10,
        jack = 11,
        queen = 12,
        king = 13
    }

    /// <summary>
    /// enum for the various suits that are associated to each card
    /// </summary>
    public enum CardSuit
    {
        clubs = 1,
        diamonds = 2,
        hearts = 3,
        spades = 4
    }

    /// <summary>
    /// card class for creating each card
    /// </summary>
    public class Card
    {

        private CardValue cardValue;
        private CardSuit cardSuit;
        private GameObject cardPrefab;

        /// <summary>
        /// property for retrieving and setting the value of card
        /// </summary>
        public CardValue Value
        {
            get { return this.cardValue; }
            set { this.cardValue = value; }
        }

        /// <summary>
        /// property for retrieving and setting the suit of card
        /// </summary>
        public CardSuit Suit
        {
            get { return this.cardSuit; }
            set { this.cardSuit = value; }
        }

        public GameObject CardPrefab
        {
            get
            {
                return cardPrefab;
            }
            set
            {
                cardPrefab = value;
            }
        }

        /// <summary>
        /// initiator for creating a card object
        /// </summary>
        public Card(int value, GameObject prefab)
        {
            cardValue = getCardValue(value);
            cardSuit = 0;
            cardPrefab = prefab;
        }

        public CardValue getCardValue(int value)
        {
            switch (value)
            {
                case (1):
                    return CardValue.ace;
                case (2):
                    return CardValue.two;
                case (3):
                    return CardValue.three;
                case (4):
                    return CardValue.four;
                case (5):
                    return CardValue.five;
                case (6):
                    return CardValue.six;
                case (7):
                    return CardValue.seven;
                case (8):
                    return CardValue.eight;
                case (9):
                    return CardValue.nine;
                case (10):
                    return CardValue.ten;
                case (11):
                    return CardValue.jack;
                case (12):
                    return CardValue.queen;
                    
            }
            return CardValue.king;
        }
    }
}
