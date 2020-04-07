﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        hearts = 1,
        spades = 2,
        clubs = 3,
        diamonds = 4
    }

    /// <summary>
    /// card class for creating each card
    /// </summary>
    class Card
    {

        private CardValue cardValue;
        private CardSuit cardSuit;

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

        /// <summary>
        /// initiator for creating a card object
        /// </summary>
        public Card()
        {
            cardValue = 0;
            cardSuit = 0;
        }
       
    }
}
