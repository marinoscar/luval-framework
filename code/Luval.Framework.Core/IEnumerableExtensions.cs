using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Shuffles the elements of a given list and returns a <see cref="IList{T}"/> with the elements in random order.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The list to be shuffled. This list remains unchanged.</param>
        /// <returns>A new <see cref="IList{T}"/> containing all the elements of the original list in random order.</returns>
        /// <remarks>
        /// This method uses the Fisher-Yates shuffle algorithm for randomness. 
        /// It creates a new list to avoid modifying the original list.
        /// The randomness provided by the System.Random class is sufficient for most non-security-related purposes but is not cryptographically secure.
        /// </remarks>
        public static IList<T> Shuffle<T>(this IList<T> collection)
        {
            var shuffledList = new List<T>(collection);
            var rng = new Random();

            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int randomIndex = rng.Next(n + 1);

                //Random value
                var value = shuffledList[randomIndex]; 

                shuffledList[randomIndex] = shuffledList[n];

                shuffledList[n] = value;
            }

            return shuffledList;
        }

        /// <summary>
        /// Shuffles the elements of a given list and returns a <see cref="IEnumerable{T}"/> with the elements in random order.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The list to be shuffled. This list remains unchanged.</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing all the elements of the original list in random order.</returns>
        /// <remarks>
        /// This method uses the Fisher-Yates shuffle algorithm for randomness. 
        /// It creates a new list to avoid modifying the original list.
        /// The randomness provided by the System.Random class is sufficient for most non-security-related purposes but is not cryptographically secure.
        /// </remarks>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            return Shuffle<T>(collection.ToList());
        }
    }
}
