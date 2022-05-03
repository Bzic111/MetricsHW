using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MetricsAgentClient.UserControls;

/// <summary>
/// Логика взаимодействия для UserControl2.xaml
/// </summary>
public partial class CPUMetricsControl : UserControl
{
    private List<Rectangle> Bars = new List<Rectangle>();
    private DispatcherTimer _dispatcherTimer;
    private HttpClient _client = new HttpClient();
    private MetricsAgentHttpClient _metrics;
    public CPUMetricsControl()
    {
        InitializeComponent();
        Bars.Add(Bar0);
        Bars.Add(Bar1);
        Bars.Add(Bar2);
        Bars.Add(Bar3);
        Bars.Add(Bar4);
        Bars.Add(Bar5);
        Bars.Add(Bar6);
        Bars.Add(Bar7);
        Bars.Add(Bar8);
        Bars.Add(Bar9);
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
        _metrics = new(_client);
        //_dispatcherTimer.Start();
    }
    public void StartDispatcher()
    {
        _dispatcherTimer.Start();
    }
    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        List<int> nums = new List<int>(10);
        var cpuMetrics = _metrics.GetMetricsList(new HttpRequestMessage(
                                        HttpMethod.Get,
                                        $@"http://localhost:5056/api/metrics/cpu/from/{DateTime.Now.AddMinutes(-3).ToString("s")}/to/{DateTime.Now.ToString("s")}"));
        for (int i = 0; i < cpuMetrics.Count; i++)
        {
            nums.Add(cpuMetrics[i].Value);
        }
        SetBars(nums);
    }
    public void Mask_MouseMove(object sender, MouseEventArgs e)
    {
        Point p = e.GetPosition(Mask);
        double PositionY = p.Y > 180 ? 180 : p.Y;
        var thk = new Thickness(0, PositionY, 0, 0);
        CursorLabel.Margin = thk;
        Frame.Margin = thk;
        CursorLabel.Content = $"{-((int)(p.Y / 2) - 100)} %";
        CursorLine.Y1 = CursorLine.Y2 = p.Y + 1;
        //Point p = e.GetPosition(Mask);
        //Bar0.Height = p.Y;
        //CursorLabel.Content = p.Y;
        //CursorLine.Y1 = CursorLine.Y2 = p.Y;
        //Random rnd = new();
        //List<int> nums = new List<int>() { rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100) };
        //SetBars(nums);

    }

    public void SetBars(List<int> nums)
    {
        for (int i = 0; i <nums.Count; i++)
        {
            SolidColorBrush brush = new SolidColorBrush();
            Bars[i].Height = nums[i] * 2;
            
            switch (nums[i])
            {
                case > 70: brush.Color = Colors.Red; goto default;
                case > 50: brush.Color = Colors.Yellow; goto default;
                case > 30: brush.Color = Colors.Green; goto default;
                case <= 30:
                    brush.Color = Colors.Blue; goto default;
                default:  break;
            }
            Bars[i].Fill = brush;
        }
    }

    private void Mask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Point p = e.GetPosition(Mask);
        Bar0.Height = p.Y;
        CursorLabel.Content = p.Y;
        CursorLine.Y1 = CursorLine.Y2 = p.Y;
    }
}
