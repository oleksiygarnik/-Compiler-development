﻿using CompilerDevelopment.Entities;
using CompilerDevelopment.Graphics;
using CompilerDevelopment.GUI;
using System;
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

namespace CompilerDevelopment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TableOfTokens.ReadFromFile();
            Switcher.pageSwitcher = this;
            ScaleConverter sc = new ScaleConverter();
            ContentConverter cc = new ContentConverter();
            ScaleConverter sco = new ScaleConverter();
            Switcher.Switch(new MainMenu());
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

    }
}
