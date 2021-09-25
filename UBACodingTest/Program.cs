using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace UBACodingTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string Password = "Olusegun487#/";
            Console.WriteLine($"Is this Password: {Password} Valid? {IsPasswordValid(Password)}");

            string AccountNumber = "1234567890";
            Console.WriteLine($"Is this Account Number: {AccountNumber} Valid? {IsAccountNumberValid(AccountNumber)}");

            Console.WriteLine("\n\nGet The Most Occuring Word from a String\n");
            string Sentence = "the cat ate the cat's food the cat ate the cat's food the cat ate the cat's food the cat ate the cat's food";
            GetMostOccuringWord(Sentence);

            string uniqueCharsWord = "Heluwovcnxsawas";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"There are {CheckHowManyUniqueCharacters(uniqueCharsWord)} unique characters in the word {uniqueCharsWord}");
            stopwatch.Stop();
            Console.WriteLine($"\nTOtal time taken for this method to run is {stopwatch.ElapsedMilliseconds} milliseconds");


            stopwatch.Restart();
            string RemoveDuplicateCharactersTestString = "Google"; //"Csharpstar"; //"Google"
            Console.WriteLine($"\nRemoving duplicate chars from {RemoveDuplicateCharactersTestString} translates to {RemoveDuplicateCharacters(RemoveDuplicateCharactersTestString)}");
            stopwatch.Stop();
            Console.WriteLine($"\nTOtal time taken for this method to run is {stopwatch.ElapsedMilliseconds} milliseconds\n");
            //RemoveDuplicateCharactersOOfN
            stopwatch.Restart();
            Console.WriteLine($"\nFor V2 Removing duplicate chars from {RemoveDuplicateCharactersTestString} translates to {RemoveDuplicateCharactersOOfN(RemoveDuplicateCharactersTestString)}");
            stopwatch.Stop();
            Console.WriteLine($"\nTOtal time taken for this method to run is {stopwatch.ElapsedMilliseconds} milliseconds\n");

            //Check two words are anagrams?
            string anagramWord1 = "WELD";//"Game";
            string anagramWord2 = "lEwd"; // "aegm";
            Console.WriteLine($"\n\nCheck to determine if two words \"{anagramWord1}\" and \"{anagramWord2}\" are Anagrams is {CheckTwoWordsAreAnagrams(anagramWord1, anagramWord2)}");

            //String Reversal Iterative and Recursive
            string wordToReverse = "Faith";
            Console.WriteLine($"Reversal of Word \"{wordToReverse}\" is => {ReverseStringIteratively(wordToReverse)}\n\n");

            //Binary Search
            int[] arr = new int[] { 0, 4, 7, 10, 14, 23, 45, 47, 53 };
            var index = BinarySearch(arr, 47);
            Console.WriteLine("Element found at {0}", index);

            //int[] array = new int[]{0};
            var indexLinear = LinearSearch(arr, 47);
            Console.WriteLine($"\n\nLinear Search found at: {indexLinear}");


            Console.WriteLine("\n\nSum of the Element in the Array is: {0}", ArraySum(arr));   
            Console.WriteLine("\n\nMaximum Value in the Array is: {0}", GetMaxValueInArray(arr));   
            Console.WriteLine("\n\nMinimum Value in the Array is: {0}", GetMinValueInArray(arr));   



            Console.WriteLine("\n\nPress any key to quit the program");
            Console.ReadKey();
        }

        private static bool IsPasswordValid(string Password)
        {
            bool passwordValid = true;
            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@+=-_!$%^&*()~\/,/]).{8,}$")) passwordValid = false;

            return passwordValid;
        }

        private static bool IsAccountNumberValid(string AccountNumber)
        {
            bool accountNumberValid = true;
            //if (!Regex.IsMatch(AccountNumber, @"^(?=.*\d).{10}$")) accountNumberValid = false;
            if (!Regex.IsMatch(AccountNumber, @"^(?=.*[0-9]).{10}$")) accountNumberValid = false;

            return accountNumberValid;
        }

        private static int GetMostOccuringWord(string Sentence)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Sentence = Sentence.Replace(",", "");
            Sentence = Sentence.Replace(".", "");
            //string Sentence = "the cat ate the cat's food the cat ate the cat's food the cat ate the cat's food the cat ate the cat's food";

            string[] wordArray = Sentence.Split(" ");
            foreach(string word in wordArray)
            {
                if(word.Length >= 3)
                {
                    if (dictionary.ContainsKey(word)) //if it's in the dict already increment
                        dictionary[word] = dictionary[word] + 1;
                    else
                        dictionary[word] = 1;
                }
            }

            foreach (KeyValuePair<string, int> pair in dictionary)
                Console.WriteLine("Key: {0}, Pair: {1}", pair.Key, pair.Value);

            var max = dictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            int count = dictionary[max];
            Console.Write($"\n\nMost Occuring Word: '{max}' occuring {count} times in the sentence.\n\n");
            //return dictionary.Values.Max();
            return count;
        }

        private static int CheckHowManyUniqueCharacters(string Word)
        {
            HashSet<char> wordSet = new HashSet<char>();
            //int charCount = 0;
            //foreach (char c in Word)
            //{
            //    wordSet.Add(c);
            //}
            for (int i = 0; i < Word.Length - 1; i++)
            {
                wordSet.Add(Word[i]);
            }
            //Parallel.ForEach(Word, new ParallelOptions { MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.25) * 2.0)) }, (i) =>
            //  {
            //      wordSet.Add(i);
            //  });

            return wordSet.Count;
        }

        private static string RemoveDuplicateCharacters(string subject){
            HashSet<char> charSet = new HashSet<char>();
            subject = subject.Replace(" ", "");
            string finalString = "";
            StringBuilder sb = new StringBuilder();
            foreach(char c in subject){
                charSet.Add(c);
            }
            foreach(char c in charSet){
                sb.Append(c);
            }
            finalString = sb.ToString();

            return finalString;


        }

        private static string RemoveDuplicateCharactersOOfN(string subject){

            string table = "";
            string result = "";

            foreach(char c in subject){
                while(table.IndexOf(c) == -1){

                    table+=c;
                    result+=c;

                }
            }

            return result;


        }

        private static bool CheckTwoWordsAreAnagrams(string word1, string word2){

            bool areTwoWordsAnagrams = true;

            char[] wordArray1 = word1.ToLower().ToCharArray();
            char[] wordArray2 = word2.ToLower().ToCharArray();

            //step 2
            Array.Sort(wordArray1);
            Array.Sort(wordArray2);

            //Step 3
            string newWord1 = new string(wordArray1);
            string newWord2 = new string(wordArray2);

            //compare them
            if(!newWord1.Equals(newWord2)){
                areTwoWordsAnagrams = false;
            }


            return areTwoWordsAnagrams;

        }

        private static string ReverseStringIteratively(string subject){

            string result = "";
            for(int i = subject.Length-1; i>=0; i--){
                result +=subject[i];
            }

            return result;
        }

        private static int BinarySearch(int[] arr, int key){
            int left = 0;
            int right = arr.Length;
            while(left < right){
                int mid = (left + right) / 2;
                if(arr[mid] == key) return mid;
                else if(arr[mid] > key){
                    right = mid;
                }else{
                    left = mid + 1;
                }
            }
            return -1;
        }

        private static int BinarySearchNickWhite(int[] nums, int target){
            if(nums.Length == 0) return -1;

            int left = 0;
            int right = nums.Length - 1;

            while(left <= right){
            int midpoint = left + (right - left) / 2;

            if(nums[midpoint] == target){
                return midpoint;
            }else if(nums[midpoint] > target){
                right = midpoint -1;
            }
            else{
                left = midpoint + 1;
            }

            }

            return -1;
        }

        private static int LinearSearch(int[] arr, int target){
            if(arr.Length == 0) return -1;

            for(int i=0; i<arr.Length-1; i++){
                if(arr[i] == target) return i;

            }

            return -1;
        }

        private static int ArraySum(int[] arr){
            return arr.Aggregate((l,r) => l+r);
        }

        private static int GetMaxValueInArray(int[] arr){
            return arr.Aggregate((l,r) => l>r ? l : r);
        }

        private static int GetMinValueInArray(int[] arr){
            return arr.Aggregate((l,r) => l<r ? l : r);
        }

    }
}
