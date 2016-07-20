using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    //Create a Deck of Card class with properties of a Deck of Cards
    public class Deck
    {
        private Card[] Cards;
        
        private static String[] cardRanks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        private static String[] cardSuits = { "hearts", "spades", "diamonds", "clubs" };



        public Deck()
        {
            //Initialize the dec
            Cards=new Card[52];
            int k = 0;
            //populate the dec
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Cards[k]=new Card(i,j);
                    k++;
                }
            }
        }
        
        //Shuffle the Deck
        public Card[] Shuffle()
        {
            Random rnRandom=new Random();
            for (int i = Cards.GetLength(0)-1; i >=0 ; i--)
            {
                //Generate a random number and swap it with last card in the deck
                var randomRank = rnRandom.Next(0, 51);
                var temp = Cards[i];
                Cards[i] = Cards[randomRank];
                Cards[randomRank] = temp;
            }

            return Cards;
        }

        public static string GetCard(Card card)
        {
            try
            {
                return  String.Format("Rank is:{0} and suit is {1}", cardRanks[card.value], cardSuits[card.suit]);
            }
            catch (Exception exception)
            {

                return String.Format("Invalid card:{0}", exception.Message);
            }
        }
    }
}
