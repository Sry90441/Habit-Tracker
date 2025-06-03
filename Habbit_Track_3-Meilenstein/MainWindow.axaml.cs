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
    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        message.Text = "Button pressed";
    }
}