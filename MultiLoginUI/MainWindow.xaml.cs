﻿using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Threading;
using System.Diagnostics;

namespace MultiLoginUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly SqlConnection con = new();
        readonly SqlCommand com = new();
        SqlDataReader dr;
        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        private bool VerfiyUser(string username, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select Status from Users where username='"+username+"' and password='"+password+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if(Convert.ToBoolean(dr["Status"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (VerfiyUser(txtUsername.Text, txtPassword.Password))
            {
                MessageBox.Show("Login Successful");
            }
            else
            {
                MessageBox.Show("User or password is incorrect");
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
