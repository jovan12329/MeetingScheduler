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
    /// Interaction logic for UpdateUserView.xaml
    /// </summary>
    public partial class UpdateUserView : UserControl
    {
        public UpdateUserView()
        {
            InitializeComponent();
        }

        private void MyPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((UpdateUserViewModel)DataContext).Password = PswdBox.Password;
            }
        }

    }
}
