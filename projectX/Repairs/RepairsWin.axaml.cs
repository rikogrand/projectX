using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using projectX.Repairs;

namespace projectX;

public partial class RepairsWin : Window
{
    private string _con = "server=localhost;database=remont;user=user_1;password=1234";
    private List<Repair> _repairs;
    private MySqlConnection _connection;

    public RepairsWin()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql = "select Repair_id, Application, Worker_name, Detail, Service_name from Repairs " +
                     "join remont.Workers W on Repairs.Worker = W.Worker_id " +
                     "join remont.`Order` O on O.Order_id = Repairs.`Order` " +
                     "join remont.Services S on S.Service_id = Repairs.Service";
        _repairs = new List<Repair>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurRep = new Repair()
            {
                Repair_id = reader.GetInt32("Repair_id"),
                Application = reader.GetInt32("Application"),
                Worker = reader.GetString("Worker_name"),
                Order = reader.GetString("Detail"),
                Service = reader.GetString("Service_name")
            };
            _repairs.Add(CurRep);

        }
        _connection.Close();
        RepairDataGrid.ItemsSource = _repairs;
    }

    private void Search_repair_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Repair> RepairSearch = _repairs.Where(x => x.Repair_id.ToString() == search_repair.Text).ToList();
        RepairDataGrid.ItemsSource = RepairSearch;
        if (search_repair.Text == "")
        {
            ShowTable();
        }
    }

    private void AddRepair_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        AddRepairWin addRepair = new AddRepairWin();
        addRepair.ShowDialog(this);
    }

    private void EditRepair_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = RepairDataGrid.SelectedItem as Repair;
        if (MyValue is null) return;
        
        EditRepairWin edit = new EditRepairWin(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowTable();
            }
        };
    }

    private void DelRepair_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = RepairDataGrid.SelectedItem as Repair;
        if (myValue is null)
        {
            return;
        }
        DeleteRepairWin del = new DeleteRepairWin(myValue);
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
