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
    public partial class AddCard : Window
    {
        User user;
        public AddCard(User user)
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
            txtCard.Clear();
            txtCvv.Clear();
        }

        private void btnApp_Click(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("User Base/USERBASE.json");
            var list = JsonSerializer.Deserialize<List<User>>(json);

            try
            {
                if(txtCvv.Text.Any(x => char.IsLetter(x)))
                {
                    MessageBox.Show("Error value is letter");
                    this.Close();
                    return;
                }
                if(list.Any(x => x.card.CardNumber == txtCard.Text))
                {
                    MessageBox.Show("Such card allready used");
                    this.Close();
                    return;
                }

                list.RemoveAt(list.FindIndex(x => x.Nickname == user.Nickname));
                user.card.CardNumber = txtCard.Text;               
                user.card.Cvv = int.Parse(txtCvv.Text);
                user.card.Carddate = DateTime.Parse(txtDate.SelectedDate.Value.ToString());
                user.card.Balance = 0;
                Console.WriteLine(user.card.CardNumber);
                list.Add(user);
                var newjson = JsonSerializer.Serialize(list);
                File.WriteAllText("User Base/USERBASE.json", newjson);
                this.Close();
                return;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return;
            }
        }

        
    }
}
