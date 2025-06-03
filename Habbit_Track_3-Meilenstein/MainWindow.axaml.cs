using Avalonia.Controls;
using Avalonia.Interactivity;
using Tmds.DBus.Protocol;

namespace Habbit_Track_3_Meilenstein;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void Button_Click(object sender, RoutedEventArgs e)
    {
        string userInput = nameInput.Text;
        activityOutput.Text = $"You entered: {userInput}";
    }
}