using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Tmds.DBus.Protocol;

namespace Habbit_Track_3_Meilenstein;

public delegate void SaveActivityDelegate(List<ActivityItem> list, string filename = "SavedActivities");

public partial class MainWindow : Window
{
    List<ActivityItem> ActivityList = new List<ActivityItem>(); // reduces ActivityList to List for testing
    public string InputActivity { get; set; }
    public string Input_SelectedTime { get; set; }
    public MainWindow()
    {
        InitializeComponent();
    }

    // Save function
    #region SaveFunction
    public static void SaveListToJson(List<ActivityItem> list, string filename)
    {
        var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        System.Console.WriteLine("File saved successfully");
    }

    public SaveActivityDelegate saveMethod = SaveListToJson;

    public void SaveAndExit_Click(object? sender, RoutedEventArgs e)
    {
        saveMethod(ActivityList);
        Environment.Exit(0);
    }

    // Function for creating new Item
    #region NewItem
    public void Button_Click(object sender, RoutedEventArgs e)
    {

        InputActivity = nameInput.Text!;
        if (string.IsNullOrWhiteSpace(nameInput.Text))
        {
            System.Console.WriteLine("Activity name cant be empty");
            return;
        }

        if (string.IsNullOrWhiteSpace(Input_SelectedTime))
        {
            System.Console.WriteLine("Time-interval must be selected");
            return;
        }

        if (Input_SelectedTime == "Daily")
        {
            ActivityList.Add(new ActivityItem(InputActivity!, new DailyActivity()));
        }
        else if (Input_SelectedTime == "Weekly")
        {
            ActivityList.Add(new ActivityItem(InputActivity!, new WeeklyActivity()));
        }
        else if (Input_SelectedTime == "Monthly")
        {
            //Console.WriteLine($"{InputActivity} + {Input_SelectedTime}");
            ActivityList.Add(new ActivityItem(InputActivity!, new MonthlyActivity()));
        }
        /*
        else if (Input_SelectedTime == "Other")
        {
            ActivityList.Add(new SpecialTimeActivity(InputActivity));
        }
        */
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
        doneButton.Click += (s, args) =>
        {
            var activityName = textBlock.Text;
            System.Console.WriteLine($"{activityName}");
        };
        var partiallyButton = new Button
        {
            Content = "Partially",
            Margin = new Thickness(5, 0, 0, 0)
        };
        partiallyButton.Click += (s, args) =>
        {
            var activityName = textBlock.Text;
            System.Console.WriteLine($"{activityName}");
        };
        var removeButton = new Button
        {
            Content = "Delete Activity",
            Margin = new Thickness(5, 0, 0, 0)
        };
        removeButton.Click += (s, args) =>
        {
            var activityName = textBlock.Text;
            System.Console.WriteLine($"{activityName}");

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