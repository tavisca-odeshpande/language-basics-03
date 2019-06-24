using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int len = protein.Length;
            int[] calorie = new int[len];
            int[] meals = new int[dietPlans.Length];
            for( int i = 0; i < len; i++)
            {
                calorie[i] = CalCalorie(protein[i], carbs[i], fat[i]);
            }
            for ( int i = 0; i < dietPlans.Length; i++)
            {
                var list = Enumerable.Range(0,len).ToList();
                foreach ( char x in dietPlans[i])
                {
                    switch (x)
                    {
                        case 'C':
                            list = FindLargest(carbs, list);
                            break;
                        case 'c':
                            list = FindSmallest(carbs, list);
                            break;
                        case 'P':
                            list = FindLargest(protein, list);
                            break;
                        case 'p':
                            list = FindSmallest(protein, list);
                            break;
                        case 'F':
                            list = FindLargest(fat, list);
                            break;
                        case 'f':
                            list = FindSmallest(fat, list);
                            break;
                        case 'T':
                            list = FindLargest(calorie, list);
                            break;
                        case 't':
                            list = FindSmallest(calorie, list);
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                meals[i] = list[0];
            }
            
            return meals;
        }

        private static List<int> FindSmallest(int[] arr,List<int> list)
        {
            int min=arr[list[0]];
            List<int> newlist = new List<int>();
            foreach (int x in list)
            {
                if(min > arr[x])
                {
                    newlist.Clear();
                    min = arr[x];
                    newlist.Add(x);
                }else if(min == arr[x])
                {
                    newlist.Add(x);
                }
                else
                {
                    continue;
                }
            }
            return newlist;
        }

        private static List<int> FindLargest(int[] arr, List<int> list)
        {
            int max = arr[list[0]];
            List<int> newlist = new List<int>();
            foreach (int x in list)
            {
                if (max < arr[x])
                {
                    newlist.Clear();
                    max = arr[x];
                    newlist.Add(x);
                }
                else if (max == arr[x])
                {
                    newlist.Add(x);
                }
                else
                {
                    continue;
                }
            }
            return newlist;
        }

        private static int CalCalorie(int carb,int pro,int fat)
        {
            return carb * 5 + pro * 5 + fat * 9;
        }
    }
}
