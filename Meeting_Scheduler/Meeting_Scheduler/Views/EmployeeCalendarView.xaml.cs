using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for EmployeeCalendarView.xaml
    /// </summary>
    public partial class EmployeeCalendarView : UserControl
    {

        public Grid g { get; set; }
        public DateTime CurrentCalendarPage { get; set; }

        public string Session { get; set; }


        List<DateTime> sastanci = new List<DateTime>() { DateTime.Now, DateTime.Now.AddHours(2) };
        Dictionary<string, int> scheduleRows;
        Dictionary<string, int> scheduleColumns;


        public EmployeeCalendarView()
        {
            InitializeComponent();

            scheduleRows = new Dictionary<string, int>();
            scheduleColumns = new Dictionary<string, int>();


            g = new Grid();
            CurrentCalendarPage = DateTime.Now;
            GetHeaderMonth(g, CurrentCalendarPage);
            FillTheCalendarMonth(g, CurrentCalendarPage);
            GetButtonsMonth(g);
            Loaded += MainWindow_Loaded;

            this.Content = g;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var viewModel = DataContext as EmployeeCalendarViewModel;

            if (viewModel != null)
            {

                Session = viewModel.session;
                MenuButtonsMonth(g);
            }

        }



        //Week view

        public void GetButtons(Grid g)
        {

            Button next = new Button();
            next.Content = "  Next  ";
            next.HorizontalAlignment = HorizontalAlignment.Center;
            next.VerticalAlignment = VerticalAlignment.Center;
            next.Click += NextButton;
            next.AddHandler(Button.ClickEvent, new RoutedEventHandler(NextButton));

            Grid.SetColumn(next, 0);
            g.Children.Add(next);


            Button previous = new Button();
            previous.Content = "Previous";
            previous.HorizontalAlignment = HorizontalAlignment.Center;
            previous.VerticalAlignment = VerticalAlignment.Center;
            previous.Click += PreviousButton;
            previous.AddHandler(Button.ClickEvent, new RoutedEventHandler(PreviousButton));

            Grid.SetColumn(previous, 1);


            g.Children.Add(previous);



        }



        private void NextButton(object sender, RoutedEventArgs e)
        {
            ScrollViewer scroll = new ScrollViewer();
            CurrentCalendarPage = CurrentCalendarPage.AddDays(7);
            scheduleRows = new Dictionary<string, int>();
            scheduleColumns = new Dictionary<string, int>();

            g = new Grid();
            GetButtons(g);
            GetHeader(g, CurrentCalendarPage);
            FillHours(g, CurrentCalendarPage);
            GetDates(g, CurrentCalendarPage);
            MenuButtonsWeek(g);

            scroll.Content = g;

            this.Content = scroll;

            e.Handled = true;

        }


        private void PreviousButton(object sender, RoutedEventArgs e)
        {
            ScrollViewer scroll = new ScrollViewer();
            CurrentCalendarPage = CurrentCalendarPage.AddDays(-7);
            scheduleRows = new Dictionary<string, int>();
            scheduleColumns = new Dictionary<string, int>();

            g = new Grid();
            GetButtons(g);
            GetHeader(g, CurrentCalendarPage);
            FillHours(g, CurrentCalendarPage);
            GetDates(g, CurrentCalendarPage);
            MenuButtonsWeek(g);

            scroll.Content = g;

            this.Content = scroll;

            e.Handled = true;

        }







        public void GetHeader(Grid g, DateTime now)
        {

            RowDefinition cal = new RowDefinition();


            RowDefinition r = new RowDefinition();

            RowDefinition dates = new RowDefinition();

            RowDefinition weekly = new RowDefinition();

            ColumnDefinition col = new ColumnDefinition();

            g.RowDefinitions.Add(cal);
            g.RowDefinitions.Add(r);
            g.RowDefinitions.Add(dates);
            g.RowDefinitions.Add(weekly);
            g.ColumnDefinitions.Add(col);

            var days = Enum.GetValues(typeof(DayOfWeek));

            CultureInfo ciCurr = new CultureInfo("en-US");

            Label l = new Label();
            l.Content = $"{now.ToString("MMMM", ciCurr)}, {now.Year}";
            l.HorizontalAlignment = HorizontalAlignment.Center;
            l.FontSize = 22;
            Grid.SetRow(l, 0);
            l.SetValue(Grid.ColumnSpanProperty, 7);

            g.Children.Add(l);

            int cnt = 1;

            for (int i = 1; i <= days.Length; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                g.ColumnDefinitions.Add(columnDefinition);
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                switch ((DayOfWeek)(days.GetValue(i % days.Length)))
                {

                    case DayOfWeek.Monday:


                        tb.Text = DayOfWeek.Monday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Tuesday:

                        tb.Text = DayOfWeek.Tuesday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Wednesday:

                        tb.Text = DayOfWeek.Wednesday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Thursday:

                        tb.Text = DayOfWeek.Thursday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Friday:

                        tb.Text = DayOfWeek.Friday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Saturday:

                        tb.Text = DayOfWeek.Saturday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Sunday:

                        tb.Text = DayOfWeek.Sunday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    default:
                        MessageBox.Show("Error occured"); break;

                }

                Border b = new Border();
                b.BorderBrush = Brushes.Black;
                b.BorderThickness = new Thickness(1);
                Grid.SetRow(b, 1);
                Grid.SetColumn(b, cnt);
                g.Children.Add(b);

                g.Children.Add(tb);

                cnt++;

            }





        }


        public void GetDates(Grid g, DateTime now)
        {

            Dictionary<DayOfWeek, int> days = new Dictionary<DayOfWeek, int>()
            {
                { DayOfWeek.Monday,0 },
                { DayOfWeek.Tuesday,1 },
                { DayOfWeek.Wednesday,2 },
                { DayOfWeek.Thursday,3 },
                { DayOfWeek.Friday,4 },
                { DayOfWeek.Saturday,5 },
                { DayOfWeek.Sunday,6 }

            };
            var start = days[CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek];
            var end = days[now.DayOfWeek];

            DateTime startOfWeek = now.AddDays(
            start -
            end);

            CultureInfo ciCurr = new CultureInfo("en-US");

            string[] result = Enumerable.Range(0, 7)
                             .Select(i => startOfWeek.AddDays(i).ToString("d-MMMM-yyyy", ciCurr))
                             .ToArray();



            for (int i = 1; i <= days.Count; i++)
            {

                TextBlock tb = new TextBlock();
                tb.Text = result[i - 1];
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(tb, 2);
                Grid.SetColumn(tb, i);

                scheduleColumns.Add(result[i - 1], i);


                g.Children.Add(tb);


            }




        }




        public void FillHours(Grid g, DateTime now)
        {

            var hours = Enumerable.Range(0, 25)
                       .Select(i => (DateTime.MinValue.AddHours(i)).ToString("HH:mm"))
                       .ToArray();



            int cnt = 0;

            for (int i = 4; i < hours.Length + 4; i++)
            {

                RowDefinition row = new RowDefinition();
                g.RowDefinitions.Add(row);

                TextBlock tb = new TextBlock();
                tb.Height = 100;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.Text = hours[cnt];

                Grid.SetRow(tb, i);
                Grid.SetColumn(tb, 0);

                Border b = new Border();
                b.BorderBrush = Brushes.Black;
                b.BorderThickness = new Thickness(1);
                Grid.SetRow(b, i);
                Grid.SetColumn(b, 0);

                if (!scheduleRows.ContainsKey(hours[cnt]))
                    scheduleRows.Add(hours[cnt], i);

                g.Children.Add(b);



                g.Children.Add(tb);
                cnt++;

            }






        }





        public void WeekScheduleFill(Grid g)
        {

            TextBlock tb = new TextBlock();

            DateTime start = sastanci[0];
            DateTime end = sastanci[1];

            var startMett = start.ToString("HH") + ":00";
            var endMett = end.ToString("HH") + ":00";


            tb.Background = Brushes.Green;
            tb.Text = "Sastanak";

            var strow = scheduleRows[startMett];
            var enrow = scheduleRows[endMett];
            CultureInfo ciCurr = new CultureInfo("en-US");
            var stcol = scheduleColumns[start.ToString("d-MMMM-yyyy", ciCurr)];

            // tb.Height = tb.ActualHeight * 0.5;

            //Grid.SetColumn(tb, stcol);
            //Grid.SetRow(tb, strow);
            //Grid.SetRowSpan(tb, enrow - strow);


            //g.Children.Add(tb);








        }



        //Monthly view

        //Menu
        public void MenuButtonsMonth(Grid g)
        {
            Button next = new Button();
            next.Content = "  Week  ";
            next.HorizontalAlignment = HorizontalAlignment.Center;
            next.VerticalAlignment = VerticalAlignment.Center;
            next.Click += GetWeek;
            next.AddHandler(Button.ClickEvent, new RoutedEventHandler(GetWeek));


            Grid.SetColumn(next, 5);
            g.Children.Add(next);


            Button previous = new Button();
            previous.Content = "Back";
            previous.HorizontalAlignment = HorizontalAlignment.Center;
            previous.VerticalAlignment = VerticalAlignment.Center;

            previous.DataContext = DataContext as EmployeeCalendarViewModel;
            Binding binding = new Binding("Back");
            BindingOperations.SetBinding(previous, Button.CommandProperty, binding);




            Grid.SetColumn(previous, 6);


            g.Children.Add(previous);


        }




        public void MenuButtonsWeek(Grid g)
        {
            Button next = new Button();
            next.Content = "  Month  ";
            next.HorizontalAlignment = HorizontalAlignment.Center;
            next.VerticalAlignment = VerticalAlignment.Center;
            next.Click += GetMonth;
            next.AddHandler(Button.ClickEvent, new RoutedEventHandler(GetMonth));


            Grid.SetColumn(next, 5);
            g.Children.Add(next);


            Button previous = new Button();
            previous.Content = "Back";
            previous.HorizontalAlignment = HorizontalAlignment.Center;
            previous.VerticalAlignment = VerticalAlignment.Center;

            previous.DataContext = DataContext as EmployeeCalendarViewModel;
            Binding binding = new Binding("Back");
            BindingOperations.SetBinding(previous, Button.CommandProperty, binding);




            Grid.SetColumn(previous, 6);


            g.Children.Add(previous);


        }


        public void GetMonth(object sender, RoutedEventArgs e)
        {
            g = new Grid();
            CurrentCalendarPage = CurrentCalendarPage;
            GetHeaderMonth(g, CurrentCalendarPage);
            FillTheCalendarMonth(g, CurrentCalendarPage);
            GetButtonsMonth(g);
            MenuButtonsMonth(g);

            this.Content = g;

            e.Handled = true;

        }


        public void GetWeek(object sender, RoutedEventArgs e)
        {
            ScrollViewer scroll = new ScrollViewer();
            CurrentCalendarPage = DateTime.Now;
            scheduleRows = new Dictionary<string, int>();
            scheduleColumns = new Dictionary<string, int>();


            g = new Grid();
            GetButtons(g);
            GetHeader(g, CurrentCalendarPage);
            FillHours(g, CurrentCalendarPage);
            GetDates(g, CurrentCalendarPage);
            WeekScheduleFill(g);
            MenuButtonsWeek(g);

            scroll.Content = g;

            this.Content = scroll;

            e.Handled = true;

        }


        public void GetButtonsMonth(Grid g)
        {

            Button next = new Button();
            next.Content = "  Next  ";
            next.HorizontalAlignment = HorizontalAlignment.Center;
            next.VerticalAlignment = VerticalAlignment.Center;
            next.Click += NextButtonMonth;
            next.AddHandler(Button.ClickEvent, new RoutedEventHandler(NextButtonMonth));

            Grid.SetColumn(next, 0);
            g.Children.Add(next);


            Button previous = new Button();
            previous.Content = "Previous";
            previous.HorizontalAlignment = HorizontalAlignment.Center;
            previous.VerticalAlignment = VerticalAlignment.Center;
            previous.Click += PreviousButtonMonth;
            previous.AddHandler(Button.ClickEvent, new RoutedEventHandler(PreviousButtonMonth));

            Grid.SetColumn(previous, 1);


            g.Children.Add(previous);



        }


        private void NextButtonMonth(object sender, RoutedEventArgs e)
        {
            g = new Grid();
            CurrentCalendarPage = CurrentCalendarPage.AddMonths(1);
            GetHeaderMonth(g, CurrentCalendarPage);
            FillTheCalendarMonth(g, CurrentCalendarPage);
            GetButtonsMonth(g);
            MenuButtonsMonth(g);

            this.Content = g;

            e.Handled = true;

        }


        private void PreviousButtonMonth(object sender, RoutedEventArgs e)
        {
            g = new Grid();
            CurrentCalendarPage = CurrentCalendarPage.AddMonths(-1);
            GetHeaderMonth(g, CurrentCalendarPage);
            FillTheCalendarMonth(g, CurrentCalendarPage);
            GetButtonsMonth(g);
            MenuButtonsMonth(g);

            this.Content = g;

            e.Handled = true;

        }



        public void GetHeaderMonth(Grid g, DateTime now)
        {

            RowDefinition cal = new RowDefinition();


            RowDefinition r = new RowDefinition();


            g.RowDefinitions.Add(cal);
            g.RowDefinitions.Add(r);

            var days = Enum.GetValues(typeof(DayOfWeek));

            CultureInfo ciCurr = new CultureInfo("en-US");

            Label l = new Label();
            l.Content = $"{now.ToString("MMMM", ciCurr)}, {now.Year}";
            l.HorizontalAlignment = HorizontalAlignment.Center;
            l.FontSize = 22;
            Grid.SetRow(l, 0);
            l.SetValue(Grid.ColumnSpanProperty, 7);

            g.Children.Add(l);

            int cnt = 0;

            for (int i = 1; i <= days.Length; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                g.ColumnDefinitions.Add(columnDefinition);
                TextBlock tb = new TextBlock();
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                switch ((DayOfWeek)(days.GetValue(i % days.Length)))
                {

                    case DayOfWeek.Monday:


                        tb.Text = DayOfWeek.Monday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Tuesday:

                        tb.Text = DayOfWeek.Tuesday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Wednesday:

                        tb.Text = DayOfWeek.Wednesday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Thursday:

                        tb.Text = DayOfWeek.Thursday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Friday:

                        tb.Text = DayOfWeek.Friday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Saturday:

                        tb.Text = DayOfWeek.Saturday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    case DayOfWeek.Sunday:

                        tb.Text = DayOfWeek.Sunday.ToString();
                        Grid.SetRow(tb, 1);
                        Grid.SetColumn(tb, cnt);

                        break;

                    default:
                        MessageBox.Show("Error occured"); break;

                }

                g.Children.Add(tb);

                cnt++;

            }





        }

        public void FillTheCalendarMonth(Grid g, DateTime now)
        {
            DateTime first = new DateTime(now.Year, now.Month, 1);
            DateTime end = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            int dayNo = (((int)(first.DayOfWeek)) - 1) == -1 ? 6 : (((int)(first.DayOfWeek)) - 1);
            int dayNum = 0;

            for (int i = 2; i <= 7; i++)
            {
                RowDefinition r = new RowDefinition();
                g.RowDefinitions.Add(r);


                for (int j = dayNo; j < 7; j++)
                {


                    TextBlock tb = new TextBlock();
                    tb.Text = first.AddDays(dayNum).ToString("d").Split('.')[0];
                    tb.VerticalAlignment = VerticalAlignment.Top;
                    tb.HorizontalAlignment = HorizontalAlignment.Right;
                    tb.Margin = new Thickness(0, 0, 5, 0);
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    g.Children.Add(tb);

                    //ListBox ls = new ListBox();
                    //ls.ItemsSource = new List<string>() { "Sastanak1", "Sastanak2" };
                    //ls.Margin = new Thickness(0, 20, 0, 0);
                    //Grid.SetRow(ls, i);
                    //Grid.SetColumn(ls, j);
                    //g.Children.Add(ls);

                    Border b = new Border();
                    b.BorderBrush = Brushes.Black;
                    b.BorderThickness = new Thickness(1);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    g.Children.Add(b);

                    dayNum++;

                    if (dayNum == end.Day)
                    {
                        break;
                    }

                }

                dayNo = 0;


                if (dayNum == end.Day)
                {
                    break;
                }

            }


        }



    }
}
