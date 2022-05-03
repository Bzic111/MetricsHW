using System.Windows;
using System.Windows.Input;

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

