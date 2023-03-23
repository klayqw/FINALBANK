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
using FINALBANK.Classes;

namespace bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditProfile : Window
    {
        int code;
        bool issend = false;
        User user;
        public EditProfile(User user)
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
            txtEmail.Clear();
            txtPass.Clear();
        }

        

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            var list = Func.GetUsers();

            try
            {
                if (issend)
                {
                    if(txtCode.Text != code.ToString())
                    {
                        MessageBox.Show("Invalid Code!", "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);                        
                        return;
                    }
                }               
               

                var newUser = new User()
                {
                    Nickname = txtUser.Text,
                    Password = txtPass.Password,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    card = new Card()
                    {
                        CardNumber = "0000000000000000",
                        Cvv = 000,
                        Balance = 0,
                        Carddate = DateTime.Now,
                    },
                    Id = user.Id,                                      
                    Firstname = txtfirstname.Text,
                    Lastname = txtlastname.Text,                    
                };

                if (issend == false)
                {
                    MessageBox.Show("Code was sended to your email", "REG INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    code = Func.SendCodeEmail(newUser);
                    issend = true;
                    txtUser.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    txtPass.IsEnabled = false;
                    txtfirstname.IsEnabled = false;
                    txtlastname.IsEnabled = false;
                    txtPhone.IsEnabled = false;
                    return;
                }

                list.RemoveAt(list.FindIndex(x => x.Nickname == user.Nickname));
                list.Add(newUser);
                list.ForEach(x => Console.WriteLine(x.Firstname));
                Func.LoadUserInFile(list);
                MessageBox.Show("User was added", "REG TRUE", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void nickcheck_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)nickcheck.IsChecked)
            {
                this.txtUser.IsEnabled = false;
                this.txtUser.Text = user.Nickname;
                return;
            }
            this.txtUser.IsEnabled = true;           
        }

        private void passwordcheck_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)passwordcheck.IsChecked)
            {
                this.txtPass.IsEnabled = false;
                this.txtPass.Password = user.Password;
                return;
            }
            this.txtPass.IsEnabled = true;
        }

        private void emailcheck_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)emailcheck.IsChecked)
            {
                this.txtEmail.IsEnabled = false;
                this.txtEmail.Text = user.Email;
                return;
            }
            this.txtEmail.IsEnabled = true;
        }

        private void phone_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)phonecheck.IsChecked)
            {
                this.txtPhone.IsEnabled = false;
                this.txtPhone.Text = user.Phone;
                return;
            }
            this.txtPhone.IsEnabled = true;
        }

        private void firstcheck_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)firstcheck.IsChecked)
            {
                this.txtfirstname.IsEnabled = false;
                this.txtfirstname.Text = user.Firstname;
                return;
            }
            this.txtfirstname.IsEnabled = true;
        }

        private void lastcheck_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)lastcheck.IsChecked)
            {
                this.txtlastname.IsEnabled = false;
                this.txtlastname.Text = user.Lastname;
                return;
            }
            this.txtlastname.IsEnabled = true;
        }
    }
}
