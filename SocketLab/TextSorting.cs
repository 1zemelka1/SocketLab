using System;
using System.Collections.Generic;
using System.Linq;

namespace SocketLab
{
    internal class TextSorting
    {
        public List<string> ProcessTextBlock(string text)
        {
            text = text.ToLower();

            char[] punctuation = { ',', '.', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>' };

            foreach (char punctuationChar in punctuation)
            {
                text = text.Replace(punctuationChar.ToString(), "");
            }

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            HashSet<string> uniqueWords = new HashSet<string>(words);

            List<string> sortedWords = uniqueWords.OrderBy(word => word).ToList();

            return sortedWords;
        }
    }
}
