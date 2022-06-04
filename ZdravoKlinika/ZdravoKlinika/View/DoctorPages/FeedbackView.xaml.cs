using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for FeedbackView.xaml
    /// </summary>
    public partial class FeedbackView : Page
    {
        public MyICommand Confirm { get; set; }
        public MyICommand SliderPlus { get; set; }
        public MyICommand SliderMinus { get; set; }
        public int Value { get => value; set => this.value = value; }

        private int value;
        public FeedbackView()
        {
            DataContext = this;
            Confirm = new MyICommand(ExecuteConfirm);
            SliderPlus = new MyICommand(Add);
            SliderMinus = new MyICommand(Substract);
            InitializeComponent();
            ThanksGrid.Visibility = Visibility.Collapsed;
        }

        public void Add()
        {
            /*if(Slidey.Value + 1 <= 10)
            {
                Slidey.Value++;
            }*/
        }
        
        public void Substract()
        {
            /*if(Slidey.Value - 1 >= 1)
            {
                Slidey.Value--;
            }*/
        }

        public void ExecuteConfirm()
        {
            ThanksGrid.Visibility = Visibility.Visible;

        }
    }
}
