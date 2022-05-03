using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MetricsAgentClient.Models;
using Microsoft.VisualBasic;
using MetricsAgentClient.UserControls;

namespace MetricsAgentClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        MyButton.Content = "Start getting Metrics";
    }

    private void MyButton_Click(object sender, RoutedEventArgs e)
    {
        Bars.StartDispatcher();
        Ram.StartDispatcher();
        DotNet.StartDispatcher();
        Network.StartDispatcher();
        Hdd.StartDispatcher();
    }

    private void UserControl2_MouseMove(object sender, MouseEventArgs e)
    {
        Bars.Mask_MouseMove(sender, e);
        
    }
}

