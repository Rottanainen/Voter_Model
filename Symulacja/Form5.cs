using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Symulacja
{
    public partial class Form5 : Form
    {
        public Form5(List<int> stepCounts)
        {
            InitializeComponent();
            DisplayStatistics(stepCounts);
        }

        private void DisplayStatistics(List<int> stepCounts)
        {
            if (stepCounts == null || stepCounts.Count == 0)
            {
                MessageBox.Show("Brak danych do wyświetlenia.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double mean = stepCounts.Average();
            double stdDev = Math.Sqrt(stepCounts.Select(s => Math.Pow(s - mean, 2)).Sum() / (stepCounts.Count - 1));
            double uncertainty = stdDev / Math.Sqrt(stepCounts.Count);

            lblMean.Text = $"Średnia liczba kroków: {mean:F2}";
            lblStdDev.Text = $"Odchylenie standardowe: {stdDev:F2}";
            lblUncertainty.Text = $"Niepewność: {uncertainty:F2}";
        }
    }
}
