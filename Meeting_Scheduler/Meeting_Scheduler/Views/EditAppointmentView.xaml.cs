using Meeting_Scheduler.Enums;
using Meeting_Scheduler.ViewModels;
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

namespace Meeting_Scheduler.Views
{
    /// <summary>
    /// Interaction logic for EditAppointmentView.xaml
    /// </summary>
    public partial class EditAppointmentView : UserControl
    {
        public EditAppointmentView()
        {
            InitializeComponent();
            Type.ItemsSource = new List<AppointmentType>() { AppointmentType.ONLINE, AppointmentType.OFFLINE };
            start.ItemsSource = Enumerable.Range(0, 24)
                       .Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm"))
                       .ToList();
            end.ItemsSource = Enumerable.Range(0, 24)
                       .Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm"))
                       .ToList();
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                ((EditAppointmentViewModel)DataContext).Type = ((AppointmentType)Type.SelectedItem).ToString();

            }


        }

        private void start_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataContext != null)
            {
                ((EditAppointmentViewModel)DataContext).StartTime = (string)start.SelectedItem;

            }

        }

        private void end_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataContext != null)
            {
                ((EditAppointmentViewModel)DataContext).EndTime = (string)end.SelectedItem;

            }


        }

        private void employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataContext != null)
            {
                ((EditAppointmentViewModel)DataContext).Users = new List<string>(employees.SelectedItems.Cast<string>());

            }

        }


    }
}
