using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Numbers2Words
{
    public static class Translator
    {
        public static Dictionary<int, string> words;

		//Takes the digit directly from the dictionary or if the digit is "0" returns it. 
        public static string TranslateAdigit(string number)
        {
            string numWord = "";
            words = new Dictionary<int, string>();

            words.Add(1, "едно");
            words.Add(2, "две");
            words.Add(3, "три");
            words.Add(4, "четири");
            words.Add(5, "пет");
            words.Add(6, "шест");
            words.Add(7, "седем");
            words.Add(8, "осем");
            words.Add(9, "девет");

            if (number == "0")
                return "нула";

            numWord = words[Convert.ToInt32(number)];
            return numWord;
        }

        /// <summary>
        /// Takes a 2 digit number and translates it.
        /// </summary>
        /// <param name="number">2 digit number</param>
        /// <returns>string representation in bulgarian</returns>
        public static string Translate2digit(string number)
        {
            string numWord = ""; 
            words = new Dictionary<int, string>();

            words.Add(1, "едно");
            words.Add(2, "две");
            words.Add(3, "три");
            words.Add(4, "четири");
            words.Add(5, "пет");
            words.Add(6, "шест");
            words.Add(7, "седем");
            words.Add(8, "осем");
            words.Add(9, "девет");

			//Special cases
            if (number[0] == '1')
            {
                if (number[1] == '1' || number[1] == '2' || number[1] == '0')
                {
					//Special cases for numbers 10, 11, 12
                    switch (number[1])
                    {
                        case '0':
                            numWord += " десет";
                            break;
                        case '1':
                            numWord += " единадесет";
                            break;
                        case '2':
                            numWord += " дванадесет";
                            break;
                    }
                }
                else
                {
                    numWord += " " + words[Convert.ToInt32(number.Substring(1, 1))] + "надесет";
                }
            }
			//Special case for 20.
            else if (number[0] == '2')
            {
                numWord += " " + "двадесет";
                if (number[1] == '0')
                    numWord += "";
                else
                    numWord += " " + words[Convert.ToInt32(number.Substring(1, 1))];
            }
            else if (number[0] == '0')
            {
				//Special case for number 02, 03, 08. Just missing the 0.
                if (number[1] == '0')
                    numWord += " ";
                else
                    numWord += " " + words[Convert.ToInt32(number.Substring(1, 1))];
            }
            else
            {
				//Ordinary cases for 30,40,50..99 
                numWord += " " + words[Convert.ToInt32(number.Substring(0, 1))] + "десет";
                if (number[1] == '0')
                    numWord += "";
                else
                    numWord += " " + words[Convert.ToInt32(number.Substring(1, 1))];
            }
            
            return(numWord);
        }


        public static string Translate3digit(string number)
        {
            words = new Dictionary<int, string>();



            words.Add(1, "едно");
            words.Add(2, "две");
            words.Add(3, "три");
            words.Add(4, "четири");
            words.Add(5, "пет");
            words.Add(6, "шест");
            words.Add(7, "седем");
            words.Add(8, "осем");
            words.Add(9, "девет");

            string numWord = "";
            //Check first digit for cases 100, 200, 300 or 0**.
            if (number[0] == '1' || number[0] == '2' || number[0] == '3' || number[0] == '0')
            {
                //Specific words
                switch (number[0])
                {
                    case '0':
                        numWord = "";
                        break;
                    case '1':
                        numWord = "сто";
                        break;
                    case '2':
                        numWord = "двеста";
                        break;
                    case '3':
                        numWord = "триста";
                        break;
                }
            }
            else
            {
                //Ordinary words
                numWord = words[Convert.ToInt32(number.Substring(0, 1))];
                numWord += "стотин";
            }
			//Pass the remaining 2 digits of the 3 to the Translator of 2 digits for not repeating code.
            string twoDigits = number.Substring(1,1) + number.Substring(2,1);
            numWord += Translate2digit(twoDigits);

            //Check second digit
            return (numWord);
        }

        public static string CheckNum(string number)
        {
			//Check if the entered data is not a number
			//First check if the data is a number, then check the length ;)
            if (!Regex.IsMatch(number, @"^[0-9]+$"))
            {
				//If not keep repeating to enter correct data
                while (!Regex.IsMatch(number, @"^[0-9]+$"))
                {
                    Console.WriteLine("It is not a number! Try again: ");
                    number = Console.ReadLine();
                }
				//Check the length of number. 
                if (number.Length > 9)
                {
					//Keep repeating for shorter number.
                    while (number.Length > 9)
                    {
                        Console.WriteLine("Length over 9! Please try again:");
                        number = Console.ReadLine();
                    }
                    return number;
                }
                return number;
            }
            else
            {
				//If it is a number, but length bigger than 9 keep repeating
                if (number.Length > 9)
                {
                    while (number.Length > 9)
                    {
                        Console.WriteLine("Length over 9! Please try again:");
                        number = Console.ReadLine();
                    }
                    return number;
                }
            }
            return number;
        }
    }
}
