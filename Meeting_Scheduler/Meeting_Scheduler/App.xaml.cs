using Meeting_Scheduler.Services;
using Meeting_Scheduler.Stores;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Meeting_Scheduler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationStore _navigationStore;
        //private UserRepository userRepository = new UserRepository();
        public App()
        {

            _navigationStore = new NavigationStore();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurretViewModel = new LoginViewModel(new NavigationUtility(_navigationStore, () => { return new ViewModelBase(); }));

            MainWindow = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };
            MainWindow.Show();

            //string p1 = HashPassword("admin");
            //string p2 = HashPassword("1234");


            //UserDTO u1 = new UserDTO(Guid.NewGuid(), "admin", p1, "Marko", "Markovic", Role.ADMINISTRATOR, "admin@admin.com", "+38162554432");
            //UserDTO u2 = new UserDTO(Guid.NewGuid(), "ivan123", p2, "Ivan", "Ivanovic", Role.EMPLOYEE, "ivan@mail.com", "+38162554432");
            //UserDTO u3 = new UserDTO(Guid.NewGuid(), "ana123", p2, "Ana", "Maric", Role.EMPLOYEE, "ana@mail.com", "+38162554432");

            //userRepository.AddUser(u1);
            //userRepository.AddUser(u2);
            //userRepository.AddUser(u3);



            base.OnStartup(e);
        }



    }
}
