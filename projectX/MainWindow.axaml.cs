using Avalonia.Controls;
using Avalonia.Interactivity;
using projectX.Entities;

namespace projectX;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Service_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        ServiceWindow serviceWindow = new ServiceWindow();
        serviceWindow.Show();
    }

    private void Worker_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        WorkerWindow workerWindow = new WorkerWindow();
        workerWindow.Show();
    }

    private void Report_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        ReportWindow reportWindow = new ReportWindow();
        reportWindow.Show();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
   
    }

    private void Add_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        AddApplication addApplication = new AddApplication();
        addApplication.Show();
    }

    private void Application_Count_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private void Average_Complete_OnClick(object? sender, RoutedEventArgs e)
    {
    }
    
    private void Stats_OnClick(object? sender, RoutedEventArgs e)
    {
      
    }
}