using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for Graph2.xaml
    /// </summary>
    public partial class Graph2 : UserControl
    {
        private string xAxisLabel;
        public Graph2()
        {
            InitializeComponent(); InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Broj novih pacijenata",
                    Values = new ChartValues<int> { 10, 6, 3, 0, 1, 5, 3, 10, 13, 5, 2, 7, 10, 14, 15, 16, 17, 0, 3, 5, 7, 12, 0, 0, 5, 0, 2, 5, 0, 0 }
                }
            };
            Labels = new string[30];
            for (int i = 0; i < 30; i++)
            {
                Labels[29 - i] = DateTime.Today.AddDays(-i).ToString("dd.MM.");
            }
            YFormatter = value => value.ToString();
            XAxisLabel = "\n" + DateTime.Today.AddDays(-29).ToString("dd.MM.yyyy.") + " – " + DateTime.Today.ToString("dd.MM.yyyy.");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string XAxisLabel { get => xAxisLabel; set => xAxisLabel = value; }
    }
}

