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
    /// Interaction logic for EventView.xaml
    /// </summary>
    public partial class EventView : UserControl
    {
        public EventView()
        {
            InitializeComponent();
            Type.ItemsSource = new List<SpecialEvents>() { SpecialEvents.STATE,SpecialEvents.NATIONAL,SpecialEvents.RELIGIOUS,SpecialEvents.SPECIAL_EVENT };
        }


        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                ((EventViewModel)DataContext).Type = ((Absence)Type.SelectedItem).ToString();

            }


        }



    }
}
