using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Symulacja
{
    public partial class Form4 : Form
    {
        private List<int[,]> simulationSteps;
        private List<KeyValuePair<int, int>> selectedStepIndices;
        private List<KeyValuePair<int, int>> selectedStepsKVP;
        private int currentStepIndex = 0;
        private int N;
        private Panel gridPanel;
        private Button nextButton;
        private Button prevButton;
        private Label stepInfoLabel;
        private int cellSpacing;
        private bool useOldMethod = false;


        public Form4(List<int[,]> allSteps, List<KeyValuePair<int, int>> selectedIndices, int matrixSize)
        {
            InitializeComponent();
            Console.WriteLine("Initializing Form4");
            this.simulationSteps = allSteps;
            
            this.N = matrixSize;
            this.cellSpacing = 2;
            this.useOldMethod = false;

            if (selectedIndices.First().Key != 0)
            {
                selectedIndices.Insert(0, new KeyValuePair<int, int>(0, CountYesOpinions(allSteps[0])));
            }

            int lastStepIndex = allSteps.Count - 1;
            if (selectedIndices.Last().Key != lastStepIndex)
            {
                selectedIndices.Add(new KeyValuePair<int, int>(lastStepIndex, CountYesOpinions(allSteps[lastStepIndex])));
            }

            this.selectedStepIndices = selectedIndices;


            Console.WriteLine("Selected step indices:");
            foreach (var kvp in selectedStepIndices)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }


            SetupCustomControls();
            DisplayCurrentStep();
        }

        private void SetupCustomControls()
        {
            if (this.Size.Width < 600)
            {
                this.Size = new Size(800, 600);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Text = "Simulation Steps Viewer";
            }

            InitializeGridPanel();
            InitializeNavControls();
        }

        private void InitializeGridPanel()
        {
            gridPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10)
            };
            this.Controls.Add(gridPanel);
        }

        private void InitializeNavControls()
        {
            Panel controlPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom
            };

            prevButton = new Button
            {
                Text = "Previous Step",
                Width = 120,
                Height = 30,
                Location = new Point(20, 15),
                Enabled = false
            };
            prevButton.Click += PrevButton_Click;

            nextButton = new Button
            {
                Text = "Next Step",
                Width = 120,
                Height = 30,
                Location = new Point(160, 15),
                Enabled = useOldMethod ?
                    selectedStepsKVP.Count > 1 :
                    selectedStepIndices.Count > 1
            };
            nextButton.Click += NextButton_Click;

            stepInfoLabel = new Label
            {
                AutoSize = true,
                Location = new Point(300, 22),
                Font = new Font("Arial", 10)
            };

            controlPanel.Controls.Add(prevButton);
            controlPanel.Controls.Add(nextButton);
            controlPanel.Controls.Add(stepInfoLabel);
            this.Controls.Add(controlPanel);

            gridPanel.Dock = DockStyle.Fill;
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            if (currentStepIndex > 0)
            {
                currentStepIndex--;
                DisplayCurrentStep();
                UpdateButtonStates();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            int maxIndex = useOldMethod ?
                selectedStepsKVP.Count - 1 :
                selectedStepIndices.Count - 1;

            if (currentStepIndex < maxIndex)
            {
                currentStepIndex++;
                DisplayCurrentStep();
                UpdateButtonStates();
            }
        }

        private void UpdateButtonStates()
        {
            prevButton.Enabled = currentStepIndex > 0;

            int maxIndex = useOldMethod ?
                selectedStepsKVP.Count - 1 :
                selectedStepIndices.Count - 1;

            nextButton.Enabled = currentStepIndex < maxIndex;
        }

        private void DisplayCurrentStep()
        {
            
            var stepNumber = selectedStepIndices[currentStepIndex].Key;
            if (stepNumber < simulationSteps.Count)
            {
                int[,] stepMatrix = simulationSteps[stepNumber];

                int yesCount = CountYesOpinions(stepMatrix);
                int totalCells = N * N;

                stepInfoLabel.Text = $"Step {stepNumber} ({currentStepIndex + 1} of {selectedStepIndices.Count}) - Yes: {yesCount}, No: {totalCells - yesCount}";

                DrawGrid(stepMatrix);
            }
                
            
        }

        private int CountYesOpinions(int[,] matrix)
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void DrawGrid(int[,] matrix)
        {
            if (gridPanel.Width <= 0 || gridPanel.Height <= 0)
                return;
            Console.WriteLine("Printing matrix");
            Bitmap bitmap = new Bitmap(gridPanel.Width, gridPanel.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                int cellSize = Math.Min(
                    (gridPanel.Width - (N + 1) * cellSpacing) / N,
                    (gridPanel.Height - (N + 1) * cellSpacing) / N
                );

                int xOffset = (gridPanel.Width - (N * cellSize + (N - 1) * cellSpacing)) / 2;
                int yOffset = (gridPanel.Height - (N * cellSize + (N - 1) * cellSpacing)) / 2;

                PrintMatrix(matrix);

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        Brush cellBrush = matrix[i, j] == 1 ? Brushes.Green : Brushes.Red;
                        int x = xOffset + j * (cellSize + cellSpacing);
                        int y = yOffset + i * (cellSize + cellSpacing);

                        g.FillRectangle(cellBrush, x, y, cellSize, cellSize);
                    }
                }
            }

            if (gridPanel.BackgroundImage != null)
            {
                gridPanel.BackgroundImage.Dispose();
            }
            gridPanel.BackgroundImage = bitmap;
            gridPanel.Invalidate();
        }

    }
}