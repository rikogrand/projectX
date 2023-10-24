using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace projectX.Reports;

public partial class DeleteReportWin : Window
{
    private readonly Report _report;
    public DeleteReportWin(Report report)
    {
        InitializeComponent();
        _report = report;
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string _con = "server=localhost;database=remont;user=user_1;password=1234";
        MySqlConnection _connection;
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Reports where Report_id = @Report_id LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Report_id", MySqlDbType.Int32);
        command.Parameters["@Report_id"].Value = _report.Report_id;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}