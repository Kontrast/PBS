using System;
using System.Windows;
using PBS.Models;

namespace PBS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            MainWindowModel mainWindowModel = DataContext as MainWindowModel;
            if (mainWindowModel != null)
            {
                mainWindowModel.SaveChanges();
            }
        }
    }
}
