using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Dialogs.Internal;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Habbit_Track_3_Meilenstein;

namespace Habbit_Track_3_Meilenstein;

public delegate void SaveActivityDelegate(List<ActivityItem> list, string filename = "LordHaveMercy");
public delegate void SaveTrackerDelegate(List<Tracker> list, string filename2 = "Tracking");    //Delegate for Save function
public delegate void ActivityButtonClickAction(ActivityItem activity);  // Delegate for Button functions

public partial class MainWindow : Window
{
    List<ActivityItem> ActivityList = new List<ActivityItem>(); // reduced ActivityList to List for testing
    List<Tracker> TrackerList = new List<Tracker>();
    Dictionary<ActivityItem, Border> ActivityBorders = new Dictionary<ActivityItem, Border>();  // needed for storing the created item connected to the ui element
    public string InputActivity { get; set; }
    public string Input_SelectedTime { get; set; }
    public ActivityButtonClickAction ActivityDoneMethod;
    public ActivityButtonClickAction ActivityPartiallyDoneMethod;
    public ActivityButtonClickAction DeleteActivityMethod;

    public MainWindow()
    {
        InitializeComponent();
        LoadListFromJson(ActivityList, "LordHaveMercy");
        LoadListFromJson(TrackerList, "Tracking");

        // Assign delegates to methods
        ActivityDoneMethod = SetActivityDone;
        ActivityPartiallyDoneMethod = SetActivityPartiallyDone;
        DeleteActivityMethod = DeleteActivity;

        foreach (var item in ActivityList)
        {
            CreateUI(item);
        }
    }

    // Button functions for delegates
    public void SetActivityDone(ActivityItem activity)
    {
        activity.TaskDone = 10;
        activity.CheckedInOnTime();
        System.Console.WriteLine($"{activity.ActivityName} done!");
    }

    public void SetActivityPartiallyDone(ActivityItem activity)
    {
        activity.TaskDone = 5;
        System.Console.WriteLine($"{activity.ActivityName} partially done!");
    }

    public void DeleteActivity(ActivityItem activity)
    {
        ActivityList.Remove(activity);
        System.Console.WriteLine($"{activity.ActivityName} removed!");
    }

    // Load function
    #region LoadFunction
    public static void LoadListFromJson<T>(List<T> list, string filename)
    {
        try
        {
            if (File.Exists(filename) == false)
            {
                throw new ArgumentException("File does not exist");
            }
            else
            {
                string json = File.ReadAllText(filename);
                var items = JsonSerializer.Deserialize<List<T>>(json);

                list.Clear();
                foreach (var item in items)
                {
                    list.Add(item);
                }

                System.Console.WriteLine("List loaded");

            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

    }
    #endregion

    // Save function
    #region SaveFunction
    public static void SaveListToJson<T>(List<T> list, string filename)
    {
        var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        System.Console.WriteLine("File saved successfully");
    }

    public SaveActivityDelegate saveMethod = SaveListToJson;
    public SaveTrackerDelegate saveMethod2 = SaveListToJson;

    public void SaveAndExit_Click(object? sender, RoutedEventArgs e)
    {
        saveMethod(ActivityList);
        saveMethod2(TrackerList);
        Environment.Exit(0);
    }
    #endregion
    // Refresh button
    public void Refresh_Click(object sender, RoutedEventArgs e)
    {
        var activitiesPanelLeft = this.FindControl<StackPanel>("ActivitiesPanelLeft");
        var activitiesPanelRight = this.FindControl<StackPanel>("ActivitiesPanelRight");
        Border border;

        // Check for due activities
        foreach (var item in ActivityList)
        {
            if (DateTime.Now > item.WhenNeedToCheck())
            {
                foreach (var element in TrackerList)
                {
                    if (item.ActivityName == element.TrackerName)
                    {
                        element.AddToList(item);
                    }
                }
                item.TaskDone = 0;

                try
                {
                    ActivityBorders.TryGetValue(item, out border);
                    if (border.Parent is Panel parentPanel)
                    {
                        parentPanel.Children.Remove(border);
                        border.Background = Brushes.White;
                    }
                    activitiesPanelLeft.Children.Add(border);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
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
        ActivityItem newActivity = null;

        if (Input_SelectedTime == "Daily")
        {
            newActivity = new ActivityItem(InputActivity!, new DailyActivity());
        }
        else if (Input_SelectedTime == "Weekly")
        {
            newActivity = new ActivityItem(InputActivity!, new WeeklyActivity());
        }
        else if (Input_SelectedTime == "Monthly")
        {
            newActivity = new ActivityItem(InputActivity!, new MonthlyActivity());
        }

        Tracker newTracker = new Tracker(newActivity.ActivityName, newActivity);
        TrackerList.Add(newTracker);
        ActivityList.Add(newActivity!);
        /*
        else if (Input_SelectedTime == "Other")
        {
            ActivityList.Add(new SpecialTimeActivity(InputActivity));
        }
        */
        //activityOutput.Text += $"{InputActivity}\n";

        var activitiesPanelLeft = this.FindControl<StackPanel>("ActivitiesPanelLeft");
        var activitiesPanelRight = this.FindControl<StackPanel>("ActivitiesPanelRight");

        CreateUI(newActivity);
    }

    public void CreateUI(ActivityItem newActivity)
    {
        var activitiesPanelLeft = this.FindControl<StackPanel>("ActivitiesPanelLeft");
        var activitiesPanelRight = this.FindControl<StackPanel>("ActivitiesPanelRight");

        var border = new Border
        {
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4),
            Margin = new Thickness(0, 5),
            Padding = new Thickness(5)
        };

        ActivityBorders[newActivity!] = border;

        var panel = new StackPanel { Orientation = Orientation.Horizontal };

        var textBlock = new TextBlock
        {
            Text = newActivity.ActivityName,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Center,
            Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand),
            Margin = new Thickness(5),
        };
        textBlock.PointerPressed += (sender, args) =>
        {
            var trackingWindow = new TrackingWindow(newActivity);
            trackingWindow.Show(); 
        };
        var typeBlock = new TextBlock
        {
            Text = newActivity.TimeIntervalType,
            Width = 200,
            VerticalAlignment = VerticalAlignment.Center
        };

        var doneButton = new Button
        {
            Content = "Done",
            Margin = new Thickness(5, 0, 0, 0)
        };

        var partiallyButton = new Button
        {
            Content = "Partially",
            Margin = new Thickness(5, 0, 0, 0)
        };
 
        var removeButton = new Button
        {
            Content = "Delete Activity",
            Margin = new Thickness(5, 0, 0, 0)
        };

        doneButton.Click += (s, args) =>
        {
            var activityName = textBlock.Text;
            var activity = ActivityList.Find(a => a.ActivityName == activityName); // For every a, compare a.ActivityName with activityName
            if (activity != null)
            {
                ActivityDoneMethod(activity);   // Button function method delegate
                activitiesPanelLeft.Children.Remove(border);

                try
                {
                    activitiesPanelRight.Children.Add(border);
                    border.Background = Brushes.Green;
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

        partiallyButton.Click += (s, args) =>
        {
            var activityName = textBlock.Text;
            var activity = ActivityList.Find(a => a.ActivityName == activityName); // For every a, compare a.ActivityName with activityName
            if (activity != null)
            {
                if (activity.TaskDone != 10)
                {
                    ActivityPartiallyDoneMethod(activity);
                    border.Background = Brushes.Yellow;
                }
                else
                {
                    System.Console.WriteLine("Can't set done activity to partially done!");
                }
                
            }
        };

        removeButton.Click += (s, args) =>
        {
            var activityName = textBlock.Text;
            var activity = ActivityList.Find(a => a.ActivityName == activityName);
            if (activity != null)
            {
                DeleteActivityMethod(activity);
                activitiesPanelLeft.Children.Remove(border);
                activitiesPanelRight.Children.Remove(border);
            }
        };

        panel.Children.Add(textBlock);
        panel.Children.Add(typeBlock);
        panel.Children.Add(doneButton);
        panel.Children.Add(partiallyButton);
        panel.Children.Add(removeButton);
        border.Child = panel;

        if (newActivity.TaskDone == 10)
        {
            activitiesPanelRight.Children.Add(border);
            border.Background = Brushes.Green;
        }
        else if(newActivity.TaskDone == 5)
        {
            activitiesPanelLeft.Children.Add(border);
            border.Background = Brushes.Yellow;
        }
        else
        {
            activitiesPanelLeft.Children.Add(border);
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