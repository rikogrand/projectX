using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using projectX.Entities;

namespace projectX;

public partial class MainWindow : Window
{
    private string _con = "server=localhost;database=remont;user=user_1;password=1234" ; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro"
    private List<Application> _applications;
    private MySqlConnection _connection;
    public MainWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql = "select App_id, Pr_name, App_Date, Equipment, Serial_number, Type_name, Description, Client, Status_name, Comment from remont.Application" +
                     " join remont.Priority P on Application.Priority = P.Pr_id" +
                     " join remont.Types T on Application.Type = T.Type_id" +
                     " join remont.Statuses S on Application.Status = S.Status_id";
        _applications = new List<Application>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurApp = new Application()
            {
               App_id = reader.GetInt32("App_id"),
               Priority = reader.GetString("Pr_name"),
               App_Date = reader.GetDateTime("App_Date"),
               Equipment = reader.GetString("Equipment"),
               Serial_number = reader.GetString("Serial_number"),
               Type = reader.GetString("Type_name"),
               Description = reader.GetString("Description"),
               Client = reader.GetString("Client"),
               Status = reader.GetString("Status_name"),
               Comment = reader.GetString("Comment")
                   
            };
            _applications.Add(CurApp);
        }
        _connection.Close();
        AppDataGrid.ItemsSource = _applications;
    }

    private void Service_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private void Worker_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        WorkerWindow workerWindow = new WorkerWindow();
        workerWindow.Show();
    }

    private void Report_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        ReportsWin reportsWin= new ReportsWin();
        reportsWin.ShowDialog(this);
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void Add_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        AddApplication addApplication = new AddApplication();
        addApplication.ShowDialog(this);
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

    private void Search_app_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Application> AppSearch = _applications.Where(x => x.App_id.ToString() == search_app.Text).ToList();
        AppDataGrid.ItemsSource = AppSearch;
        if (search_app.Text == "")
        {
            ShowTable();
        }
    }

    private void Edit_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = AppDataGrid.SelectedItem as Application;
        if (MyValue is null) return;
        
        EditApplication edit = new EditApplication(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowTable();
            }
        };
    }


    private void Repair_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        
        RepairsWin rep = new RepairsWin();
        rep.Show();
        
    }
}