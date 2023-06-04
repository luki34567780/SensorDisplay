﻿using System.Windows;

namespace SensorDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SizeChanged += (_, args) => ((MainViewModel)DataContext).WindowSizeChanged(args.NewSize);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.test = new CircularGauge.CircularGaugeControl();
        }
    }
}
