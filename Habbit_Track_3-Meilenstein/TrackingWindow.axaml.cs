using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Diagnostics.Screenshots;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Habbit_Track_3_Meilenstein;

public partial class TrackingWindow : Window
{
    public TrackingWindow(ActivityItem item)
    {
        InitializeComponent();

        this.FindControl<TextBlock>("ActivityNameText").Text = $"{item.ActivityName}";
        this.FindControl<TextBlock>("ActivityTypeText").Text = $"Type: {item.TimeIntervalType}";
        this.FindControl<TextBlock>("StatusText").Text = $"Status: {(item.TaskDone == 10 ? "Done" : item.TaskDone == 5 ? "Partially Done" : "Not Done")}";

    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private void SevenDaysButton(object sender, RoutedEventArgs e)
    {

    }
    private void TwoWeeksButton(object sender, RoutedEventArgs e)
    {

    }
    private void OneMonthButton(object sender, RoutedEventArgs e)
    {

    }
    private void SixMonthsButton(object sender, RoutedEventArgs e)
    {

    }
    private void AllTimeButton(object sender, RoutedEventArgs e)
    {
        
    }
    
}