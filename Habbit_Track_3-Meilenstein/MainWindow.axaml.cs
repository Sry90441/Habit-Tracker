using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Tmds.DBus.Protocol;

namespace Habbit_Track_3_Meilenstein;

public partial class MainWindow : Window
{
    ActivityList<Activity> ActivityList = new ActivityList<Activity>();
    public string InputActivity { get; set; }
    public string Input_SelectedTime { get; set; }
    public MainWindow()
    {
        InitializeComponent();
    }
    public void Button_Click(object sender, RoutedEventArgs e)
    {
        InputActivity = nameInput.Text;
        if (Input_SelectedTime == "Daily")
        {
            ActivityList.Add(new DailyActivity(InputActivity));
        }
        else if (Input_SelectedTime == "Weekly")
        {
            ActivityList.Add(new WeeklyActivity(InputActivity));
        }
        else if (Input_SelectedTime == "Monthly")
        {
            //Console.WriteLine($"{InputActivity} + {Input_SelectedTime}");
            ActivityList.Add(new MonthlyActivity(InputActivity));
        }
        else if (Input_SelectedTime == "Other")
        {
            ActivityList.Add(new SpecialTimeActivity(InputActivity));
        }
        //activityOutput.Text += $"{InputActivity}\n";
        var activitiesPanel = this.FindControl<StackPanel>("ActivitiesPanel");
        var border = new Border
        {
            BorderBrush = Brushes.White,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4),
            Margin = new Thickness(0, 5),
            Padding = new Thickness(5)
        };
        var panel = new StackPanel { Orientation = Orientation.Horizontal };

        var textBlock = new TextBlock
        {
            Text = $"{InputActivity}",
            Width = 200,
            VerticalAlignment = VerticalAlignment.Center
        };
        var doneButton = new Button
        {
            Content = "Done",
            Margin = new Thickness(5, 0, 0, 0)
        };
        doneButton.Click += (_, __) =>
        {

        };
        var partiallyButton = new Button
        {
            Content = "Partially",
            Margin = new Thickness(5, 0, 0, 0)
        };
        partiallyButton.Click += (_, __) =>
        {

        };
        var removeButton = new Button
        {
            Content = "Delete Activity",
            Margin = new Thickness(5, 0, 0, 0)
        };
        removeButton.Click += (_, __) =>
        {
            ActivitiesPanel.Children.Remove(border);
        };
        panel.Children.Add(textBlock);
        panel.Children.Add(doneButton);
        panel.Children.Add(partiallyButton);
        panel.Children.Add(removeButton);
        border.Child = panel;
        activitiesPanel.Children.Add(border);
        
    }
    public void ComboBox_TimeSpanSelect(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = this.FindControl<ComboBox>("TimeSpanComboBox");
        var selectedItem = comboBox.SelectedItem as ComboBoxItem;
        Input_SelectedTime = selectedItem.Content.ToString();
    }
}