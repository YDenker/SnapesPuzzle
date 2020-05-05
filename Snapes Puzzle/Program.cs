using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snapes_Puzzle
{
    class Program
    {
        static readonly string puzzleText = "Danger lies before you, while safety lies behind,\nTwo of us will help you, whichever you would find,\nOne among us seven will let you move ahead,\nAnother will transport the drinker back instead,\nTwo among our number hold only nettle-wine,\nThree of us are killers, waiting hidden in line\nChoose, unless you wish to stay here forevermore\nTo help you in your choice, we give you these clues four:\nFirst, however slyly the poison tries to hide\nYou will always find some on nettle wine's left side\nSecond, different are those who stand at either end\nBut if you would move onward, neither is your friend;\nThird as you see clearly, all are different size\nNeither dwarf nor giant hold death in their insides;\nFourth, the second left and the second on the right\nAre twins once you taste them, though different at first sight.";

        enum Type { poison, wine, forward, backward}

        static void Main(string[] args)
        {
            Console.WriteLine("Snapes Puzzle:\n" + puzzleText);

            int[] potions = new int[7];

            int i = 0;

            for (; i < ((int)Math.Pow(4,7)); i++) // Do this for the number of possibilities where each potions has one specific value! (0-16383 base 10)
            {
                potions = ConvertToBase4(i);
                if(ApplyRules(potions))
                    break;
            }

            Console.WriteLine("Iterations: " + (i +1));
            PrintSolution(potions);
        }

        static private int[] ConvertToBase4(int i)
        {
            int[] potions = { 0, 0, 0, 0, 0, 0, 0 }; //Init with all poison

            for (int n = 0; n < 7; n++)
            {
                potions[6 - n] = i % 4;
                i = (int)(i / 4);
                if (i == 0)
                    break;
            }

            return potions;
        }

        static private bool ApplyRules(int[] potions)
        {
            // only 3 poison, only 2 wine, exactly 1 forward and 1 backward
            if (Count(0, potions) != 3 || Count(1, potions) != 2 || Count(2,potions) != 1 || Count(3,potions) != 1)
                return false;
            // always poison left of wine
            for (int iterator = 0; iterator < 7; iterator++)
            {
                if (potions[iterator] == 1 && !CheckLeftIs(0, potions, iterator))
                    return false;
            }
            //leftmost and rightmost do not contain forward
            if (potions[0] == 2 || potions[6] == 2)
                return false;
            //left and right do not contain the same
            if (potions[0] == potions[6])
                return false;
            //smallest (3rd) and largest (2nd) do not contain poison
            if (potions[1] == 0 || potions[2] == 0)
                return false;
            //secondleft and secondright do contain the same
            if (potions[1] != potions[5])
                return false;

            return true;
        }

        static private int Count(int number , int[] potions)
        {
            int i = 0;
            foreach (var item in potions)
            {
                if (item == number)
                    i++;
            }
            return i;
        }

        static private bool CheckLeftIs(int number, int[] potions, int iterator)
        {
            if (iterator == 0)
                return false;

            return potions[iterator-1] == number;
        }

        static private void PrintSolution(int[] solution)
        {
            Console.Write("\nSolution: ");
            foreach (var item in solution)
            {
                Console.Write("["+(Type)item+"]");
            }

            Console.ReadLine();
        }
    }
}
