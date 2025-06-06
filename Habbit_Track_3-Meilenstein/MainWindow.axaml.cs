using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;


namespace Habbit_Track_3_Meilenstein;

public delegate void SaveActivityDelegate(List<ActivityItem> list, string filename = "SavedActivities");

public partial class MainWindow : Window
{
    List<ActivityItem> ActivityList = new List<ActivityItem>(); // reduced ActivityList to List for testing
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
    #endregion

    // Function for creating new Item
    #region NewItemCreation
    public void Button_Click(object sender, RoutedEventArgs e)
    {

        InputActivity = nameInput.Text!;

        // no empty input allowed
        if (string.IsNullOrWhiteSpace(nameInput.Text))
        {
            System.Console.WriteLine("Activity name cant be empty");
            return;
        }

        // same with drop-down menu
        if (string.IsNullOrWhiteSpace(Input_SelectedTime))
        {
            System.Console.WriteLine("Time-interval must be selected");
            return;
        }

        // check for activities with the same name
        foreach (var item in ActivityList)
        {
            if (item.ActivityName == nameInput.Text)
            {
                System.Console.WriteLine("Activities need to have a different name");
                return;
            }
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

        var activitiesPanelLeft = this.FindControl<StackPanel>("ActivitiesPanelLeft");
        var activitiesPanelRight = this.FindControl<StackPanel>("ActivitiesPanelRight");

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

        var typeBlock = new TextBlock
        {
            Text = $"Time-interval: {Input_SelectedTime}",
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
            var activity = ActivityList.Find(a => a.ActivityName == activityName); // For every a, compare a.ActivityName with activityName
            if (activity != null)
            {
                activity.TaskDone = 1;
                System.Console.WriteLine($"{activityName} done!");
                activitiesPanelLeft.Children.Remove(border);

                try
                {
                    activitiesPanelRight.Children.Add(border);

                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
            else
            {
                System.Console.WriteLine($"{activityName} not found!");
            }
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
        panel.Children.Add(typeBlock);
        panel.Children.Add(doneButton);
        panel.Children.Add(partiallyButton);
        panel.Children.Add(removeButton);
        border.Child = panel;
        activitiesPanelLeft.Children.Add(border);

        // Check for due activities, idk if this is working
        foreach (var item in ActivityList)
        {
            if (item.CheckedInOnTime == item.WhenNeedToCheck)
            {
                activitiesPanelRight.Children.Remove(border);

                try
                {
                    activitiesPanelLeft.Children.Add(border);

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
    #endregion
    public void ComboBox_TimeSpanSelect(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = this.FindControl<ComboBox>("TimeSpanComboBox");
        var selectedItem = comboBox.SelectedItem as ComboBoxItem;
        Input_SelectedTime = selectedItem.Content.ToString();
    }
}