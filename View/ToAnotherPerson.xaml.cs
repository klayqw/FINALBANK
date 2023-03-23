using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;
using FINALBANK.Service;
using FINALBANK.Models;

namespace bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ToAnotherPerson : Window
    {
        User user;
        public ToAnotherPerson(User user)
        {
            InitializeComponent();
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
            var storage = Func.GetUsers();
            try
            {
                if(storage.Any(x => x.Nickname == txtUser.Text) == false)
                {
                    MessageBox.Show("name not find");
                    ClearAll();
                    return;
                }             
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

                var user_toadd = storage.Find(x => x.Nickname == txtUser.Text);
               
                if(user_toadd.card.CardNumber == "0000000000000000" || user.card.CardNumber == "0000000000000000")
                {
                    MessageBox.Show("Card is null");
                    ClearAll();
                    this.Close();
                    return;
                }
              
                
                if(user.card.Balance - double.Parse(txtMoney.Text) < 0)
                {
                    MessageBox.Show("Not enought money");
                    ClearAll();
                    this.Close();
                    return;
                }
                if(user.card.Carddate < DateTime.Now || user_toadd.card.Carddate < DateTime.Now)
                {
                    MessageBox.Show("Card is broken");
                    ClearAll();
                    this.Close();
                    return;
                }

                user_toadd.card.Balance += double.Parse(txtMoney.Text);
                user.card.Balance -= double.Parse(txtMoney.Text);
                storage.RemoveAt(storage.FindIndex(x => x.Nickname == txtUser.Text));
                storage.RemoveAt(storage.FindIndex(x => x.Nickname == user.Nickname));
                storage.Add(user_toadd);
                storage.Add(user);
                Func.LoadUserInFile(storage);
                MessageBox.Show("All done!", "Replish", MessageBoxButton.OK, MessageBoxImage.Information);
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
