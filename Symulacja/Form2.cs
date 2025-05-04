using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Symulacja
{
    public partial class Form2 : Form
    {
        private int N;
        private List<List<KeyValuePair<int, int>>> allOpinionCounts = new List<List<KeyValuePair<int, int>>>();  // Zmienna przechowująca wszystkie dane
        private List<List<int[,]>> boardStateHolder = new List<List<int[,]>>();  // Zmienna przechowująca stan planszy w każdym kroku

        public Form2(int gridSize)
        {
            InitializeComponent();
            N = gridSize;
            StartSimulation();
        }

        private async void StartSimulation()
        {
            await Task.Run(() => RunSimulation());

            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
        }

        private void RunSimulation()
        {
            int[,] grid = new int[N, N];
            Random rand = new Random();
            List<int> stepCounts = new List<int>();

            // Symulacja 10 powtórzeń
            for (int i = 0; i < 10; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                bool shouldSaveState;

                if (i == 0)
                {
                    shouldSaveState = true;
                } else
                {
                    shouldSaveState = false;
                }
                    InitializeGrid(grid, rand);
                (int steps, var opinionCounts) = Simulate(grid, rand, i, shouldSaveState);

                allOpinionCounts.Add(opinionCounts);  // Dodanie danych powtórzenia do głównej listy

                stopwatch.Stop();
                stepCounts.Add(steps);
            }

            double mean = stepCounts.Average();
            double stdDev = Math.Sqrt(stepCounts.Select(s => Math.Pow(s - mean, 2)).Sum() / (stepCounts.Count - 1));
            double uncertainty = stdDev / Math.Sqrt(stepCounts.Count);

            MessageBox.Show($"Średnia liczba kroków: {mean:F2}\nNiepewność: {uncertainty:F2}", "Wyniki", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeGrid(int[,] grid, Random rand)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    grid[i, j] = rand.Next(2);
        }

        private (int, List<KeyValuePair<int, int>>) Simulate(int[,] grid, Random rand, int simulationId, bool shouldSaveState)
        {
            int steps = 0;
            bool isUniform = false;
            List<KeyValuePair<int, int>> opinionCounts = new List<KeyValuePair<int, int>>();
            Console.WriteLine("Simulation " + simulationId);
            boardStateHolder.Add(new List<int[,]>());
            while (!isUniform)
            {
                steps++;

                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        UpdateOpinion(grid, i, j, rand);
                int countYes = grid.Cast<int>().Count(value => value == 1);
                opinionCounts.Add(new KeyValuePair<int, int>(steps, countYes));
                if (shouldSaveState)
                {
                    boardStateHolder[simulationId].Add((int[,])grid.Clone());
                }
                
                isUniform = IsUniform(grid);
            }

            return (steps, opinionCounts);
        }

        private void UpdateOpinion(int[,] grid, int x, int y, Random rand)
        {
            int[] neighbors = GetNeighbors(grid, x, y);
            grid[x, y] = neighbors[rand.Next(neighbors.Length)];
        }

        private int[] GetNeighbors(int[,] grid, int x, int y)
        {
            List<int> neighbors = new List<int>();
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int nx = (x + dx[i] + N) % N;
                int ny = (y + dy[i] + N) % N;
                neighbors.Add(grid[nx, ny]);
            }

            return neighbors.ToArray();
        }

        private bool IsUniform(int[,] grid)
        {
            int firstOpinion = grid[0, 0];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (grid[i, j] != firstOpinion)
                        return false;

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["Form1"]?.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(allOpinionCounts);
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            int randomRunIndex = rand.Next(0, allOpinionCounts.Count);
            List<KeyValuePair<int, int>> randomOpinionCounts = allOpinionCounts[randomRunIndex];

            HashSet<int> selectedStepsIndices = new HashSet<int>();
            while (selectedStepsIndices.Count < 6)
            {
                int randomStep = rand.Next(0, randomOpinionCounts.Count);
                selectedStepsIndices.Add(randomStep);
            }

            List<KeyValuePair<int, int>> selectedSteps = selectedStepsIndices
                .OrderBy(index => index)
                .Select(index => randomOpinionCounts[index])
                .ToList();

            List<int[,]> selectedRunSteps = boardStateHolder[0];
            Form4 noweOkno2 = new Form4( selectedRunSteps, selectedSteps, N);
            noweOkno2.Show();
        }

        private bool AreStepsAdjacent(HashSet<int> selectedStepsIndices, int newStep)
        {
            foreach (var step in selectedStepsIndices)
            {
                // Sprawdzamy, czy nowy krok jest bezpośrednim sąsiadem któregoś z wybranych
                if (Math.Abs(step - newStep) == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<int> stepCounts = allOpinionCounts.Select(steps => steps.Count).ToList();

            if (stepCounts.Count == 0)
            {
                MessageBox.Show("Nie przeprowadzono żadnych symulacji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form5 noweOkno3 = new Form5(stepCounts);
            noweOkno3.Show();
        }
    }
}