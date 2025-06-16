using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Diagnostics.Screenshots;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;

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

}