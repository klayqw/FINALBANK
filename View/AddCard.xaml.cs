using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using FINALBANK.Classes;
using FINALBANK.Models;
using FINALBANK.Service;

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
           
            var list = Func.GetUsers();

            try
            {
                if(txtCvv.Text.Any(x => char.IsLetter(x)))
                {
                    MessageBox.Show("Error value is letter");
                    this.Close();
                    return;
                }
                if(list.Any(x => x.card?.CardNumber == txtCard.Text))
                {
                    MessageBox.Show("Such card allready used");
                    this.Close();
                    return;
                }

                list.RemoveAt(list.FindIndex(x => x.Nickname == user.Nickname));
                var card = new Card()
                {
                    CardNumber = txtCard.Text,
                    Cvv = int.Parse(txtCvv.Text),
                    Carddate = DateTime.Parse(txtDate.Text),
                    Balance = 0,
                };
                user.card = card;                             
                Console.WriteLine(user.card.CardNumber);
                list.Add(user);
                Func.LoadUserInFile(list);
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
