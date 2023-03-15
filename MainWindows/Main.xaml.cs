using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using FINALBANK.Classes;

namespace bank;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Main : Window, INotifyPropertyChanged
{
    User user;
    Func func = new Func(); 

    public Main(User user)
    {
        InitializeComponent();
        HideAll();
        this.home_Click(this, new RoutedEventArgs(LoadedEvent));
        this.DataContext = this;
        DispatcherTimer LiveTime = new DispatcherTimer();
        LiveTime.Interval = TimeSpan.FromSeconds(1);
        LiveTime.Tick += timer_Tick;
        LiveTime.Start();
        this.user = user;
    }

    void timer_Tick(object sender, EventArgs e)
    {
        this.time.Content = DateTime.Now.ToLongTimeString();
       
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
    

    private void Window_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void home_Click(object sender, RoutedEventArgs e)
    {         
        HideAll();
        this.homestack.Visibility = Visibility.Visible;
        var storyboard = FindResource("HomeStack") as Storyboard;
        storyboard.Begin();
       
    }

    private void HideAll()
    {
        this.homestack.Visibility = Visibility.Hidden;
        Translition.Visibility = Visibility.Hidden;
        ProfileGrid.Visibility = Visibility.Hidden; 
        Payment.Visibility = Visibility.Hidden;
        GameGrid.Visibility = Visibility.Hidden;
    }

    private void translations_Click(object sender, RoutedEventArgs e)
    {
        HideAll();
        Translition.Visibility = Visibility.Visible;
        var storyboard = FindResource("TranslitDock") as Storyboard;
        storyboard.Begin();
    }


    private void readmore_Click(object sender, RoutedEventArgs e)
    {
        var prog = new ProcessStartInfo()
        {
            FileName = "https://www.youtube.com/watch?v=d43lJsK7Kvo6",
            UseShellExecute = true,
        };
        Process.Start(prog);
    }

    private void replenish_Click(object sender, RoutedEventArgs e)
    {
        new Replish().ShowDialog();
        return;
    }

    private void payment_Click(object sender, RoutedEventArgs e)
    {
        HideAll();
        Payment.Visibility = Visibility.Visible;
    }
  

    private void profile_Click(object sender, RoutedEventArgs e)
    {      
        HideAll();
        user = func.UserUpdate(user);
        ProfileGrid.Visibility = Visibility.Visible;
        nickname.Text = user.Nickname;
        password.Text = user.Password;       
        creditcard.Text = user.Creditcard;    
        cvv.Text = user.Cvv.ToString();
        creditdate.Text = user.Creditcarddate.ToString();   
        phone.Text = user.Phone;
        balance.Text = user.Balance.ToString();
        id.Text = user.Id.ToString();
        lastname.Text = user.Lastname;
        firstname.Text = user.Firstname;
        email.Text = user.Email;
        return;
    }

    private void addcart_Click(object sender, RoutedEventArgs e)
    {
        new AddCard(user).ShowDialog();
    }

    private void ToAnotherPerson_Click(object sender, RoutedEventArgs e)
    {
        new ToAnotherPerson(user).ShowDialog();
    }

    private void ToAnotherCard_Click(object sender, RoutedEventArgs e)
    {
        new ToAnotherBank(user).ShowDialog();
    }

    private void Comun_Click(object sender, RoutedEventArgs e)
    {
      
        new Comunal((sender as Button).Content.ToString()).ShowDialog();
    }

    private void changeprofile_Click(object sender, RoutedEventArgs e)
    {
        new EditProfile(user).ShowDialog();
    }

    private void game_Click(object sender, RoutedEventArgs e)
    {
        HideAll();
        GameGrid.Visibility = Visibility.Visible;

    }

    private void games_Click(object sender, RoutedEventArgs e)
    {
        new Game((sender as Button).Content.ToString(),user).ShowDialog();
    }
}
