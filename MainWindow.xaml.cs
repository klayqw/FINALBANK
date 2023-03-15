using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FINALBANK.Classes;

namespace bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var temp2 = File.ReadAllText("User Base/USERBASE.json");
            var list = JsonSerializer.Deserialize<List<User>>(temp2);
            var user = list.Find(x => x.Nickname == "klay");
            new Main(user).Show();
            if (Directory.Exists("User Base") == false)
            {
                Directory.CreateDirectory("User Base");
                var temp = new List<User>();
                var json = JsonSerializer.Serialize(temp);
                File.WriteAllText("User Base/USERBASE.json", json);
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("User Base/USERBASE.json");
            var list = JsonSerializer.Deserialize<List<User>>(json);

            if (list.Any(x => x.Nickname == txtUser.Text))
            {
                var user = list.Find(x => x.Nickname == txtUser.Text);
                if (user.Nickname == txtUser.Text || user.Password == txtPass.Password)
                {
                    MessageBox.Show("Welcome " + user.Nickname + "!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                    //var mainwin = new Main(user);
                    //mainwin.Show();
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("User not found", "LOG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

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

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            new Registation().ShowDialog();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            new Forgot().ShowDialog();
        }

    }
}
