using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RandomizationKit
{
    /// <summary>
    /// GENERIC RANDOM FUNCTIONS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    
    public class RandomFuncs : MonoBehaviour
    {   
        public static T RandomElement<T>(T[] array)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }



        public static T RandomElement<T>(List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }



        public static T[] FYShuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i >= 1; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                T tempShuffleSpace = array[randomIndex];
                array[randomIndex] = array[i];
                array[i] = tempShuffleSpace;
            }
            return array;
        }



        public static List<T> FYShuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i >= 1; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                T tempShuffleSpace = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = tempShuffleSpace;
            }
            return list;
        }



        public static void GeometricShuffle<T>(List<T> list, System.Comparison<T> compareFunc, float coinWeight)
        {
            FYShuffle(list);
            list.Sort(compareFunc);

            List<T> shuffledList = new List<T>();

            while (list.Count > 0)
            {
                int currentIndex = 0;

                while (currentIndex < list.Count - 1)
                {
                    if (Random.value <= coinWeight)
                    {
                        break;
                    }
                    currentIndex++;
                }
                shuffledList.Add(list[currentIndex]);
                list.RemoveAt(currentIndex);
            }

            foreach (T item in shuffledList)
            {
                list.Add(item);
            }
        }
    }
}
