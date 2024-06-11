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
    /// Interaction logic for AbsenceView.xaml
    /// </summary>
    public partial class AbsenceView : UserControl
    {
        public AbsenceView()
        {
            InitializeComponent();
            Type.ItemsSource = new List<Absence>() { Absence.DAYS_OFF, Absence.SICK_LEAVE,Absence.VACATION };


        }


        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                ((AbsenceViewModel)DataContext).Type = ((Absence)Type.SelectedItem).ToString();

            }


        }


    }
}
