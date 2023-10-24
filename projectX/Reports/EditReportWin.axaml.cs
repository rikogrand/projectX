using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace projectX.Reports;

public partial class EditReportWin : Window
{
    private readonly Report _report;
    private List<Repair> _repairs;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=remont;user=user_1;password=1234";
    public EditReportWin(Report report)
    {
        InitializeComponent();
        DataContext = report;
        _report = report;
        FillRepair();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        List<Report> _reports;
        MySqlConnection _connection;
        string sql = "update Reports set Report_id = @Report_id, Repair = @Repair, Complete_time = @Complete_time, Defect_cause = @Defect_cause, Work_description = @Work_description " +
                     "where Report_id = @Report_id";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Report_id", MySqlDbType.Int32);
        command.Parameters["@Report_id"].Value = TBxId.Text;
        
        command.Parameters.Add("@Repair", MySqlDbType.Int32);
        command.Parameters["@Repair"].Value = (CBRepair.SelectedItem as projectX.Repair).Repair_id;
        
        command.Parameters.Add("@Complete_time", MySqlDbType.Time);
        command.Parameters["@Complete_time"].Value = TPTime.SelectedTime;
        
        command.Parameters.Add("@Defect_cause", MySqlDbType.String);
        command.Parameters["@Defect_cause"].Value = TBCause.Text;
        
        command.Parameters.Add("@Work_description", MySqlDbType.String);
        command.Parameters["@Work_description"].Value = TBDescription.Text;

        command.ExecuteReader();
        _connection.Close();
        Close(true);
    }

    public void FillRepair()
    {
        string sql = "select Repair_id from Repairs";
        _repairs = new List<Repair>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurRepair = new Repair()
            {
                Repair_id = reader.GetInt32("Repair_id"),
                
            };
            _repairs.Add(CurRepair);
        }
        _connection.Close();
        var repairComboBox = this.Find<ComboBox>("CBRepair");
        repairComboBox.ItemsSource = _repairs;
        repairComboBox.SelectedItem = repairComboBox.ItemsSource.Cast<Repair>().First(it => it.Repair_id == _report.Repair);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }
}