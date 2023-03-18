using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using FINALBANK.Classes;

namespace bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Game : Window
    {
        User user;
        public Game(string buttonname,User user)
        {
            InitializeComponent();
            DataContext = this;
            LabelSet.Text = buttonname;
            this.user = user;
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
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearAll()
        {
            txtUser.Clear();
            txtMoney.Clear();
        }

        private void btnApp_Click(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("User Base/USERBASE.json");
            var list = JsonSerializer.Deserialize<List<User>>(json);
            try
            {                         
                if(txtMoney.Text.Length == 0)
                {
                    MessageBox.Show("Money not find");
                    ClearAll();
                    return;
                }

                if (txtMoney.Text.Any(x => char.IsLetter(x)))
                {
                    MessageBox.Show("Money is string!");
                    ClearAll();
                    return;
                }
                               

                if(user.card.CardNumber == "0000000000000000")
                {
                    MessageBox.Show("Card is null");
                    ClearAll();
                    this.Close();
                    return;
                }
                
                if(user.card.Balance - double.Parse(txtMoney.Text)  < 0)
                {
                    MessageBox.Show("Not enought money on balance!");
                    ClearAll();
                    this.Close();
                    return;
                }
                list.RemoveAt(list.FindIndex(x => x.Nickname == user.Nickname));
                user.card.Balance -= double.Parse(txtMoney.Text);
                list.Add(user);
                var jsonnew = JsonSerializer.Serialize(list);
                File.WriteAllText("User Base/USERBASE.json", jsonnew);
                MessageBox.Show("All done!", "Games", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
