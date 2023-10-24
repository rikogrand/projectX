using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using projectX.Repairs;
using projectX.Reports;

namespace projectX;

public partial class ReportsWin : Window
{
    private string _con = "server=localhost;database=remont;user=user_1;password=1234";
    private List<Report> _reports;
    private MySqlConnection _connection;
    public ReportsWin()
    {
        InitializeComponent();
        ShowTable();
    }
    
    public void ShowTable()
    {
        string sql = "select Report_id, Repair_id, Complete_time, Defect_cause, Work_description from reports " +
                     "join remont.repairs r on r.Repair_id = reports.Repair";
        _reports = new List<Report>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurReport = new Report()
            {
                Report_id = reader.GetInt32("Report_id"),
                Repair = reader.GetInt32("Repair_id"),
                Complete_time = reader.GetTimeSpan("Complete_time"),
                Defect_cause = reader.GetString("Defect_cause"),
                Work_description = reader.GetString("Work_description")
            };
            _reports.Add(CurReport);

        }
        _connection.Close();
        ReportDataGrid.ItemsSource = _reports;
    }

    private void Search_report_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Report> ReportSearch = _reports.Where(x => x.Report_id.ToString() == search_report.Text).ToList();
        ReportDataGrid.ItemsSource = ReportSearch;
        if (search_report.Text == "")
        {
            ShowTable();
        }
    }

    private void AddReportBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        AddReportWin addReportWin = new AddReportWin();
        addReportWin.ShowDialog(this);
    }

    private void EditReportBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = ReportDataGrid.SelectedItem as Report;
        if (MyValue is null) return;
        
        EditReportWin edit = new EditReportWin(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowTable();
            }
        };
    }

    private void DelReportBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = ReportDataGrid.SelectedItem as Report;
        if (myValue is null)
        {
            return;
        }
        DeleteReportWin del = new DeleteReportWin(myValue);
        del.ShowDialog(this);
        del.Closed += (o, args) =>
        {
            if (del.Result)
            {
                ShowTable();
            }
        };
    }

    private void CloseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}