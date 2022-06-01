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
    public partial class Graph1 : UserControl
    {
        public Graph1()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Broj pacijenata",
                    Values = new ChartValues<int> { 10, 15, 25, 13, 10 }
                }
            };

            Labels = new[] { "Pon", "Uto", "Sre", "Čet", "Pet" };
            Formatter = value => value.ToString();

            DataContext = this;
            InitializeComponent();
        }

        public int MaxValue { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
