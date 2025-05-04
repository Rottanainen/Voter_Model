using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Symulacja
{
    public partial class Form3 : Form
    {
        public Form3(List<List<KeyValuePair<int, int>>> allOpinionCounts)
        {
            InitializeComponent();

            var plotModel = new PlotModel { Title = "Liczba opinii 'tak' w zależności od liczby kroków" };

            var colors = new[] { OxyColors.Red, OxyColors.Green, OxyColors.Blue, OxyColors.Purple, OxyColors.Orange,
                                 OxyColors.Cyan, OxyColors.Magenta, OxyColors.Yellow, OxyColors.Brown, OxyColors.Gray };

            for (int i = 0; i < allOpinionCounts.Count; i++)
            {
                var lineSeries = new LineSeries
                {
                    Title = $"Powtórzenie {i + 1}",
                    MarkerType = MarkerType.Circle,
                    Color = colors[i % colors.Length]
                };

                var opinionCounts = allOpinionCounts[i];

                foreach (var data in opinionCounts)
                {
                    lineSeries.Points.Add(new DataPoint(data.Key, data.Value));
                }

                plotModel.Series.Add(lineSeries);
            }

            plotView1.Model = plotModel;
        }
    }
}