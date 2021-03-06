using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MetricsAgentClient.UserControls;

/// <summary>
/// Логика взаимодействия для NetworkMetricsControl.xaml
/// </summary>
public partial class NetworkMetricsControl : UserControl
{
    //байт/с
    private DispatcherTimer _dispatcherTimer;
    private HttpClient _client = new HttpClient();
    private MetricsAgentHttpClient _metrics;
    public NetworkMetricsControl()
    {
        InitializeComponent(); 
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        _dispatcherTimer.Interval = new TimeSpan(0, 0, 9);
        _metrics = new(_client);
    }
    public void StartDispatcher()
    {
        _dispatcherTimer.Start();
    }
    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        List<int> nums = new List<int>(10);
        string req = $@"http://localhost:5056/api/metrics/network/from/{DateTime.Now.AddMinutes(-1):s}/to/{DateTime.Now:s}";
        var networkMetrics = _metrics.GetMetricsList(new HttpRequestMessage(HttpMethod.Get, req));
        for (int i = 0; i < networkMetrics.Count; i++)
        {
            nums.Add(networkMetrics[i].Value);
        }
        NetworkLabel.Content = $"{networkMetrics[^1].Value} байт/с";
    }
}
