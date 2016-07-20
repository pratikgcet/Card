using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGame;

namespace TestApp
{
    class TestCardGame
    {
        public static void Main(string[] args)
        {
            var myDeck = new Deck();
            var shuffledCards = myDeck.Shuffle();

            Card drawCard = shuffledCards[2];
            Console.WriteLine(Deck.GetCard(drawCard));

            var shuffleAgain = myDeck.Shuffle();
        }
    }
}
