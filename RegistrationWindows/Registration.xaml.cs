﻿using System;
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
using FINALBANK.Service;
using FINALBANK.Models;

namespace bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Registation : Window
    {
        int code;
        bool issend = false;

        public Registation()
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
                if (list.Any(x => x.Nickname == txtUser.Text))
                {
                    MessageBox.Show("This user allready exists", "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearAll();
                    return;
                }
                if(list.Any(x => x.Email == txtEmail.Text))
                {
                    MessageBox.Show("This user allready exists", "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearAll();
                    return;
                }


                var newUser = new User()
                {
                    card = new Card()
                    {
                        CardNumber = "0000000000000000",
                        Cvv = 000,
                        Carddate = DateTime.Now,
                        Balance = 0,
                    },
                    Nickname = txtUser.Text,
                    Password = txtPass.Password,
                    Email = txtEmail.Text,
                    Phone = "00000000000",                  
                    Id = new Random().Next(10000000, 99999999),                                    
                    Firstname = "Unknow",
                    Lastname = "Unknow",                   
                };

                if (issend == false)
                {
                    MessageBox.Show("Code was sended to your email", "REG INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    code = Func.SendCodeEmail(newUser);
                    issend = true;
                    txtUser.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    txtPass.IsEnabled = false;
                    return;
                }

                list.Add(newUser);
                Func.LoadUserInFile(list);
                MessageBox.Show("User was added", "REG TRUE", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "REG ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        
    }
}
