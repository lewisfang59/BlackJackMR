using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGameLogic
{
    class GameLauncher
    {
        private readonly static int startingHand = 2;
        private readonly static int MAX_CARDS_ON_TABLE = 11;
        private static Deck deck;
        private static Hand playerHand;
        private static Hand computerHand;
        private static int computerSum;
        private static int playerSum;


        public static void gameLaunch()
        {
            Player user = new Player("John", 100);
            Player cmp1 = new Player("Jane", 100);
            Console.WriteLine("Game Start");
            deck = new Deck();
            deck.printDeck();
            Console.WriteLine(deck.Cards.Count);
            user.Hand = new Hand(startingHand, deck);
            Console.WriteLine(deck.Cards.Count);
            cmp1.Hand = new Hand(startingHand, deck);
            Console.WriteLine(deck.Cards.Count);

            Console.WriteLine("\nPRINTING PLAYER'S HAND");
            user.Hand.DrawCard(deck);
            user.Hand.ShowHand();
            Console.WriteLine(deck.Cards.Count);

            Console.WriteLine("PRINTING COMPUTER'S HAND");
            cmp1.Hand.DrawCard(deck);
            cmp1.Hand.ShowHand();
            Console.WriteLine(deck.Cards.Count);

        }
    }
}
