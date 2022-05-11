using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Net.Http;
using System.Windows.Threading;

namespace MetricsAgentClient.UserControls;

/// <summary>
/// Логика взаимодействия для UserControl1.xaml
/// </summary>
public partial class RAMMetricsControl : UserControl
{
    private DispatcherTimer _dispatcherTimer;
    private HttpClient _client = new HttpClient();
    private MetricsAgentHttpClient _metrics;
    public RAMMetricsControl()
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
        string req = $@"http://localhost:5056/api/metrics/ram/available/from/{DateTime.Now.AddMinutes(-1):s}/to/{DateTime.Now:s}";
        var rammetrics = _metrics.GetMetricsList(new HttpRequestMessage(HttpMethod.Get, req));
        for (int i = 0; i < rammetrics.Count; i++)
        {
            nums.Add(rammetrics[i].Value);
        }
        RamLabel.Content = $"{rammetrics[^1].Value} MB" ;
    }
}
