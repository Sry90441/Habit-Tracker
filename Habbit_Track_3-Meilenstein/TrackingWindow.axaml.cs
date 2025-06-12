using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Diagnostics.Screenshots;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Habbit_Track_3_Meilenstein;

public partial class TrackingWindow : Window
{
    public TrackingWindow(ActivityItem item)
    {
        InitializeComponent();
        this.FindControl<TextBlock>("ActivityNameText").Text = $"{item.ActivityName}";
        this.FindControl<TextBlock>("ActivityTypeText").Text = $"Type: {item.TimeIntervalType}";
        this.FindControl<TextBlock>("StatusText").Text = $"Status: {(item.TaskDone == 10 ? "Done" : item.TaskDone == 5 ? "Partially Done" : "Not Done")}";
        //DrawDiagram();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void HandleTimeSpanClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            switch (button.Name)
            {
                case "SevenDays":
                    HandleSevenDays();
                    break;
                case "TwoWeeks":
                    HandleTwoWeeks();
                    break;
                case "OneMonth":
                    HandleOneMonth();
                    break;
                case "SixMonths":
                    HandleSixMonths();
                    break;
                case "AllTime":
                    HandleAllTime();
                    break;
            }
        }
    }
    private void HandleSevenDays()
    {

    }
    private void HandleTwoWeeks()
    {

    }
    private void HandleOneMonth()
    {

    }
    public void HandleSixMonths()
    {

    }
    public void HandleAllTime()
    {
        
    }


    
    public void DrawDiagram()
    {
        var diagramCanvas = this.FindControl<Canvas>("DiagramCanvas");

        var line = new Line
        {
            StartPoint = new Point(10, 10),
            EndPoint = new Point(100, 100),
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        var ellipse = new Ellipse
        {
            Width = 30,
            Height = 30,
            Fill = Brushes.Blue
        };

        Canvas.SetLeft(ellipse, 100);
        Canvas.SetTop(ellipse, 50);

        diagramCanvas.Children.Add(line);
        diagramCanvas.Children.Add(ellipse);
    }

}