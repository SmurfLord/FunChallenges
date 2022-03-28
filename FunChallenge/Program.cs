using System;
using System.Collections.Generic;
using System.Linq;

namespace FunChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gl;hf");
            char[][] grid1 = new[] {
            new [] {'c', 'c', 'x', 't', 'i', 'b'},//0
            new [] {'c', 'c', 'a', 't', 'n', 'i'},//1
            new [] {'a', 'c', 'n', 'n', 't', 't'},//2
            new [] {'t', 'c', 's', 'i', 'p', 't'},//3
            new [] {'a', 'o', 'o', 'o', 'a', 'a'},//4
            new [] {'o', 'a', 'a', 'a', 'o', 'o'},//5
            new [] {'k', 'a', 'i', 'c', 'k', 'i'}//6
            };
            string word1 = "catnip"; //base case
            string word2 = "cccc"; //Catches out problems with letter reuse and solutions that mark off letters instead of character indexes in visited sets
            string word3 = "s"; //Single letter word for solutions that assume more than one letter
            string word4 = "bit"; //Word where a backwards version of the word appears earlier in the input
            string word5 = "aoi"; //Word that appears down the bottom-right side of the grid
            string word6 = "ki"; //Word that appears down the bottom side of the grid
            string word7 = "aaa"; //Catches out some problems with skipping letters instead of breaking out of a search that has failed.
            string word8 = "ooo"; //Catches out some problems with skipping letters instead of breaking out of a search that has failed.

            char[][] grid2 = new[] { new[] { 'a' } };
            string word9 = "a"; //Single letter word on a 1x1 grid

            var coor1 = find_word_location(grid1, word1);
            Console.WriteLine(GetCoordinatesBeautified(coor1, word1));

            var coor2 = find_word_location(grid1, word2);
            Console.WriteLine(GetCoordinatesBeautified(coor2, word2)); // note in this case it should return all possible combinations

            var coor3 = find_word_location(grid1, word3);
            Console.WriteLine(GetCoordinatesBeautified(coor3, word3));

            var coor4 = find_word_location(grid1, word4);
            Console.WriteLine(GetCoordinatesBeautified(coor4, word4));
            
            var coor5 = find_word_location(grid1, word5);
            Console.WriteLine(GetCoordinatesBeautified(coor5, word5));

            var coor6 = find_word_location(grid1, word6);
            Console.WriteLine(GetCoordinatesBeautified(coor6, word6));

            var coor7 = find_word_location(grid1, word7);
            Console.WriteLine(GetCoordinatesBeautified(coor7, word7));

            var coor8 = find_word_location(grid1, word8);
            Console.WriteLine(GetCoordinatesBeautified(coor8, word8));

            var coor9 = find_word_location(grid2, word9);
            Console.WriteLine(GetCoordinatesBeautified(coor9, word9));

        }

        private static IEnumerable<(int, int)> find_word_location(char[][] grid, string word)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    var coordinates = WalkTree(new List<(int, int)>(), grid, word, x, y);
                    if (coordinates != null && coordinates.Count() == word.Length)
                        return coordinates;
                }
            }

            return null;

        }

        private static IEnumerable<(int,int)>  WalkTree(List<(int,int)> coordinates, char[][] grid, string word, int x, int y)
        {
            var indexInRange = y < grid.Length && x < grid[0].Length;
            if (indexInRange && !string.IsNullOrEmpty(word) && grid[y][x] == word[0])
            {
                coordinates.Add((y, x));
                //Console.WriteLine($"Coordinate x:{x}, y:{y}, at letter added: {grid[y][x]}, word left: {word}, coordinate count {coordinates.Count()}");
               
                var walkRight = WalkTree(coordinates, grid, word.Remove(0, 1), x + 1, y);
                var walkDown = WalkTree(coordinates, grid, word.Remove(0, 1), x, y + 1);

                if (word.Length == 1)
                    return coordinates;

                if (walkRight != null)
                    return walkRight;

                if (walkDown != null)
                    return walkDown;

                coordinates.Remove((y, x));

                //Console.WriteLine($"letter removed: {grid[y][x]}");

            }

            return null;
        }

        private static string GetCoordinatesBeautified(IEnumerable<(int, int)> grid, string word)
        {
            var coordinates = $"Found the following coordinates for {word}:";

            foreach(var coor in grid)
            {
                coordinates += $" ({coor.Item1}, {coor.Item2})";
            }

            return coordinates;
        }

    }
}
