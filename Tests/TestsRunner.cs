using AI_labs.AI_lab3.Heuristics;
using AI_labs.Core;
using AI_labs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests
{
    public static class TestsRunner
    {
        private static readonly int[] depths = { 3, 5, 7, 9, 11 };

        /// <summary>
        /// General test results collection.
        /// </summary>
        private static Dictionary<string, List<(int d, int iterCount, int genCount, int mem)>> testResults = new();
        /// <summary>
        /// Average number of nodes generated for each depth.
        /// </summary>
        private static Dictionary<string, List<(int d, double avgN)>> avgN = new();
        /// <summary>
        /// Effective branching factor b* for each depth.
        /// </summary>
        private static Dictionary<string, List<(int d, double avgN, double bStar)>> bStar = new();
        /// <summary>
        /// Average memory usage for each depth.
        /// </summary>
        private static Dictionary<string, List<(int d, double avgMem)>> avgMem = new();

        public static void RunTestsAndCollectStats()
        {
            var tests = TestsGenerator();

            testResults = new Dictionary<string, List<(int d, int iter, int gen, int mem)>>()
            {
                { "BFS", new() },
                { "Bi-BFS", new() },
                { "A*(Manhattan)", new() },
                { "A*(Chebyshev)", new() }
            };

            foreach (var (start, target, field) in tests)
            {
                var solver = new Solver((CellStates[,])field.Clone());
                RunTests("BFS", (s, t) => solver.FindPathBFS(s, t), start, target, solver, testResults);
                RunTests("Bi-BFS", (s, t) => solver.FindPathBiBFS(s, t), start, target, solver, testResults);
                RunTests("A*(Manhattan)", (s, t) => solver.FindPathAStar(s, t, Heuristics.Manhattan), start, target, solver, testResults);
                RunTests("A*(Chebyshev)", (s, t) => solver.FindPathAStar(s, t, Heuristics.Chebyshev), start, target, solver, testResults);
            }

            avgN.Clear();
            foreach (var result in testResults)
            {
                var algorithmResult = result.Value;
                var avgNForDepth = new List<(int d, double avgN)>();

                foreach (var d in depths)
                {
                    var filteredByDResults = algorithmResult.Where(r => r.d == d).ToList();
                    if (filteredByDResults.Count > 0)
                    {
                        double averageN = filteredByDResults.Average(r => r.genCount);
                        avgNForDepth.Add((d, averageN));
                    }
                }
                avgN[result.Key] = avgNForDepth;
            }

#if DEBUG
            foreach (var algorithm in avgN)
            {
                Console.WriteLine($"\n--- Average nodes generated for {algorithm.Key} ---");
                foreach (var (d, averageN) in algorithm.Value)
                {
                    Console.WriteLine($"Depth: {d}, N: {averageN:F1}");
                }
            }
#endif


            bStar.Clear();
            foreach(var result in testResults)
            {
                var bStarForDepth = new List<(int d, double avgN, double bStar)>();
                foreach(var (d, averageN) in avgN[result.Key])
                {
                    double bStarValue = CalculateBStar(averageN, d);
                    bStarForDepth.Add((d, averageN, bStarValue));
                }
                bStar[result.Key] = bStarForDepth;
            }

#if DEBUG
            foreach(var algorithm in bStar)
            {
                Console.WriteLine($"\n--- b* for {algorithm.Key} ---");
                foreach (var (d, averageN, bStar) in algorithm.Value)
                {
                    Console.WriteLine($"Depth: {d}, N: {averageN:F1}, b*: {bStar:F2}");
                }
            }
#endif


            avgMem.Clear();
            foreach(var result in testResults)
            {
                var avgMemForDepth = new List<(int d, double avgMem)>();
                foreach(var d in depths)
                {
                    var filteredByDResults = result.Value.Where(r => r.d == d).ToList();
                    if (filteredByDResults.Count > 0)
                    {
                        double averageMem = filteredByDResults.Average(r => r.mem);
                        avgMemForDepth.Add((d, averageMem));
                    }
                }
                avgMem[result.Key] = avgMemForDepth;
            }

#if DEBUG
            foreach(var algorithm in avgMem)
            {
                Console.WriteLine($"\n--- Average memory usage for {algorithm.Key} ---");
                foreach(var (d, averageMem) in algorithm.Value)
                {
                    Console.WriteLine($"Depth: {d}, Memory: {averageMem:F1}");
                }
            }
#endif
        }

        /// <summary>
        /// Runs a specific search method and collects statistics.
        /// </summary>
        /// <param name="name">Method name.</param>
        /// <param name="searchMethod">Method delegate.</param>
        /// <param name="start">Initial coordinates.</param>
        /// <param name="target">Target coordinates.</param>
        /// <param name="solver">Current <see cref="Solver"/> instance containing current <see cref="CellStates"/>.</param>
        /// <param name="testResults">Results collection.</param>
        private static void RunTests(string name,
            Func<(int x, int y), (int x, int y), List<Node>?> searchMethod,
            (int x, int y) start,
            (int x, int y) target,
            Solver solver,
            Dictionary<string, List<(int d, int iterCount, int genCount, int mem)>> testResults)
        {
            var path = searchMethod(start, target);
            if (path == null) return;

            int depth = path.Count - 1;

            testResults[name].Add((depth, solver.pCount, solver.genNodesCount, solver.oLengthMax + solver.cLengthMax));
        }

        /// <summary>
        /// Generates test data for the solver with specified path depths.
        /// </summary>
        /// <returns>
        /// List of test data where each element contains start position, 
        /// target position, and grid state.
        /// Type: <see cref="List{T}"/> where T is 
        /// ((int, int) start, (int, int) target, CellStates[,] grid)
        /// </returns>
        private static List<((int, int) start, (int, int) target, CellStates[,])> TestsGenerator()
        {
            const int fieldSize = 8;
            var target = (x: 3, y: 0);
            var rnd = new Random();
            var tests = new List<((int, int), (int, int), CellStates[,])>();

            Console.WriteLine("--- Test data generated ---");

            foreach (int d in depths)
            {
                Console.WriteLine($"\n Tests generated for d = {d}");
                int testsCount = 0;

                while (testsCount < 10)
                {
                    var field = new CellStates[fieldSize, fieldSize];
                    for (int i = 0; i < fieldSize; i++)
                    {
                        for (int j = 0; j < fieldSize; j++)
                        {
                            field[i, j] = CellStates.Clean;
                        }
                    }
                    var start = (x: rnd.Next(fieldSize), y: rnd.Next(fieldSize));

                    int abyssesCount = rnd.Next(8, 14);
                    List<(int, int)> abysses = new();
                    for (int i = 0; i < abyssesCount; i++)
                    {
                        int x = rnd.Next(fieldSize);
                        int y = rnd.Next(fieldSize);
                        if ((x, y) != start && (x, y) != target && field[x, y] == CellStates.Clean)
                        {
                            field[x, y] = CellStates.Abyss;
                            abysses.Add((x, y));
                        }
                    }

                    var solver = new Solver(field);
                    var path = solver.FindPathBFS(start, target);

                    if (path == null) continue;

                    if (path.Count == d + 1)
                    {
                        tests.Add((start, target, field));
                        testsCount++;
                        string abyssesStr = string.Join(", ", abysses.Select(a => $"({a.Item1}, {a.Item2})"));
                        Console.WriteLine($"[{testsCount,-2}]: start: ({start.x}, {start.y}), abysses({abysses.Count}): {abyssesStr}");

                    }
                }
            }
            return tests;
        }

        /// <summary>
        /// Calculates the value of B* (B-star) for a given average generated nodes count and depth.
        /// </summary>
        /// <remarks>This method uses a binary search algorithm to compute the B* value that satisfies the
        /// equation: (B^(d+1) - 1) / (B - 1) = avgN. The calculation is performed with a precision of 1e-6.</remarks>
        /// <param name="avgN">The average value used in the calculation. Represents the target value for the summation formula.</param>
        /// <param name="d">The depth. Must be greater than or equal to 0.</param>
        /// <returns>The calculated B* value as a <see langword="double"/>. Returns 0 if <paramref name="d"/> is 0.</returns>
        private static double CalculateBStar(double avgN, int d)
        {
            if (d == 0) return 0;
            double low = 1.0001;
            double high = 10;
            double mid;
            while(high - low > 1e-6)
            {
                mid = (high + low) / 2;
                double f = (Math.Pow(mid, d + 1) - 1) / (mid - 1) - avgN;

                if (f > 0) high = mid;
                else low = mid;
            }
            return (low + high) / 2;
        }

        /// <summary>
        /// Combines statistical data from multiple algorithms into a unified structure.
        /// </summary>
        /// <remarks>This method aggregates data from various sources, including average node counts,
        /// branching factors,  and memory usage, for each algorithm. The resulting dictionary maps algorithm names to a
        /// list of  tuples, where each tuple contains the depth, average node count, branching factor, and average
        /// memory usage.</remarks>
        /// <returns>A dictionary where the keys are algorithm names and the values are lists of tuples containing  statistical
        /// data for each depth. Each tuple includes: <list type="bullet"> <item><description><c>d</c>: The depth of the
        /// algorithm's execution.</description></item> <item><description><c>avgN</c>: The average number of nodes
        /// processed at the given depth.</description></item> <item><description><c>bStar</c>: The branching factor at
        /// the given depth.</description></item> <item><description><c>avgMem</c>: The average memory usage at the
        /// given depth.</description></item> </list> Returns <see langword="null"/> if any of the required data sources
        /// are missing.</returns>
        public static Dictionary<string, List<(int d, double avgN, double bStar, double avgMem)>>? CombineStats()
        {
            Dictionary<string, List<(int d, double avgN, double bStar, double avgMem)>> finalResults = new();

            foreach (var algo in testResults.Keys)
            {
                if (avgN == null || bStar == null || avgMem == null)
                    return null;

                finalResults[algo] = new List<(int d, double avgN, double bStar, double avgMem)>();

                var avgNList = avgN[algo];
                var bStarList = bStar[algo];
                var avgMemList = avgMem[algo];

                for (int i = 0; i < avgNList.Count; i++)
                {
                    int depth = avgNList[i].d;
                    double n = avgNList[i].avgN;
                    double b = bStarList[i].bStar;
                    double mem = avgMemList[i].avgMem;

                    finalResults[algo].Add((depth, n, b, mem));
                }
            }
            return finalResults;
        }
    }
}
