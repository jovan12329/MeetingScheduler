using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Stores;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
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
        private UserRepository userRepository= new UserRepository();
        public App() 
        {
            
            _navigationStore = new NavigationStore();
        
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurretViewModel= new LoginViewModel(new Services.NavigationService(_navigationStore, () => { return new ViewModelBase(); } ));

            MainWindow = new MainWindow() { DataContext=new MainViewModel(_navigationStore)};
            MainWindow.Show();

            //UserDTO u1 = new UserDTO(Guid.NewGuid(),"admin","admin","Marko","Markovic",Role.ADMINISTRATOR,"admin@admin.com","+38162554432");
            //UserDTO u2 = new UserDTO(Guid.NewGuid(),"ivan123","1234","Ivan","Ivanovic",Role.EMPLOYEE,"ivan@mail.com","+38162554432");
            //UserDTO u3 = new UserDTO(Guid.NewGuid(),"ana123","1234","Ana","Maric",Role.EMPLOYEE,"ana@mail.com","+38162554432");

            //userRepository.AddUser(u1);
            //userRepository.AddUser(u2);
            //userRepository.AddUser(u3);



            base.OnStartup(e);
        }

        



    }
}
