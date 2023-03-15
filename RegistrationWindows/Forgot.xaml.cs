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
    public partial class Forgot : Window
    {
        bool issend = false;
        int code;
        public Forgot()
        {
            InitializeComponent();
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
            txtPass.Clear();
            txtCode.Clear();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("User Base/USERBASE.json");
            var list = JsonSerializer.Deserialize<List<User>>(json);

            try
            {
                if (list.Any(x => x.Nickname == txtUser.Text))
                {
                    var user = list.Find(x => x.Nickname == txtUser.Text);
                    if (issend)
                    {
                        if(txtCode.Text != code.ToString())
                        {
                            MessageBox.Show("Invalid code!", "Forgot", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        } 
                    }
                    if(issend == false)
                    {
                        code = Func.SendCodeEmail(user);
                        MessageBox.Show("Code sendet!", "Forgot", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtUser.IsEnabled = false;
                        txtPass.IsEnabled = false;
                        issend = true;
                        return;
                    }
                    if (user.Nickname == txtUser.Text)
                    {
                        if (user.Password == txtPass.Password)
                        {
                            MessageBox.Show("You cant set password that set allready", "Forgot", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearAll();
                            return;
                        }
                        user.Password = txtPass.Password;
                        list.RemoveAt(list.FindIndex(x => x.Nickname == txtUser.Text));
                        list.Add(user);
                        var jsonfile = JsonSerializer.Serialize(list);
                        File.WriteAllText("User Base/USERBASE.json", jsonfile);
                        MessageBox.Show("All done!", "Forgot", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                        return;
                    }
                }
                MessageBox.Show("User not found", "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearAll();
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

          
        }

        
    }
}
