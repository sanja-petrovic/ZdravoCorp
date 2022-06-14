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

namespace ZdravoKlinika.View.Manager.Views
{
    /// <summary>
    /// Interaction logic for ManagerRoomReportView.xaml
    /// </summary>
    public partial class ManagerRoomReportView : Page
    {
        private RoomController roomController;
        private AppointmentController appointmentController;
        private List<Room> rooms;
        private List<Appointment> appointments;
        private Room r;
        private DateTime? start;
        private DateTime? end;

        public ManagerRoomReportView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = new RoomController();
            this.rooms = this.roomController.GetAll();
            this.appointmentController = new AppointmentController();
            roomComboBox.ItemsSource = this.rooms;
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            r = (Room)roomComboBox.SelectedItem;
            start = startDatePicker.SelectedDate;
            end = endDatePicker.SelectedDate;
            this.appointments = this.appointmentController.GetAppointmentsByRoomIdInSpecificTimeFrame(r.RoomId, (DateTime)start, (DateTime)end);
            dataGridAppointments.ItemsSource = this.appointments;
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            ZdravoKlinika.Util.PdfCreator pdfCreator = new Util.PdfCreator("Izveštaj o zauzetosti prostorije " + r.Name);
            pdfCreator.CreatePdfForRoomReport(new ViewModel.RoomReportViewModel(this.appointments, r, (DateTime)start, (DateTime)end));
        }
    }
}
