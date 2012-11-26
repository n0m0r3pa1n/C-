using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Numbers2Words
{
    class Program
    {
       
        static void Main(string[] args)
        {
			// Set console encoding
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Enter a number width 9 digits at most:");
            string number = Console.ReadLine();

            string newNumber;
            
			int numLenght;
			int counter = 0; 

            //If the entered data is not integer or is longer than 9 it keeps repeating until you enter correct data.
            number = Translator.CheckNum(number);
            numLenght = number.Length;

			//If lenght of the number is smaller than 3 choose the correct translator and print it. 
            if (numLenght <= 3)
            {
                newNumber = ChooseTranslator(numLenght, number);
                Console.WriteLine(newNumber);
            }
            else
            {
                while (numLenght > 3)
                {
					//Get rid of 3's and determing the triples.
                    numLenght -= 3;
                    counter++;
                }
				//When the numLenght get lower than 3 take the first digit with numLenght after it. 
				//That means if the left lenght is 2, we take the first one and the second one
                newNumber = ChooseTranslator(numLenght, number.Substring(0, numLenght));
                Console.WriteLine(newNumber + ChooseSuffix(counter));

				//Then we remove them from the number. There are only left triples.
                number = number.Remove(0, numLenght);
                while(number.Length > 0)
                {
                    counter--;
                    if (string.IsNullOrEmpty(Translator.Translate3digit(number.Substring(0, 3))) || Translator.Translate3digit(number.Substring(0, 3)) == " ")
                        Console.Write("");
                    else
                        Console.WriteLine(Translator.Translate3digit(number.Substring(0, 3)) + ChooseSuffix(counter));
                    number = number.Remove(0, 3);
                }
                
            }
			//Pause to see the result.
            Console.ReadLine();


        }

		//Just choosing the right translator.
        public static string ChooseTranslator(int numLength, string number)
        {
            switch (numLength)
            {
                case 1:
                    return Translator.TranslateAdigit(number);
                case 2:
                     return Translator.Translate2digit(number);
                case 3:
                    return Translator.Translate3digit(number);
                default: return "";
            }
        }

		//Choosing the right suffix based on the counter. Entered data only up to "millions"
        public static string ChooseSuffix(int counter)
        {
            switch (counter)
            { 
                case 1:
                    return " хиляди";
                case 2:
                    return " милиона";
                default: return "";
            }
        }

    }
}
