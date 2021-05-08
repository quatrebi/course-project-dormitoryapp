﻿using DormitoryApp.ViewModels;
using System.Windows;

namespace DormitoryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += App.Window_DragMove;
            this.DataContext = new MainViewModel();
        }

        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //    => this.BarPanel.Width = e.NewSize.Width - e.NewSize.Width / App.GoldenRatio;
    }
}