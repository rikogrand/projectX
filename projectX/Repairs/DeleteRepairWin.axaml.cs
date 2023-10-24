using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace projectX.Repairs;

public partial class DeleteRepairWin : Window
{
    private readonly Repair _repair;
    public DeleteRepairWin(Repair repair)
    {
        InitializeComponent();
        _repair = repair;
    }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string _con = "server=localhost;database=remont;user=user_1;password=1234";
        MySqlConnection _connection;
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Repairs where Repair_id = @Repair_id LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Repair_id", MySqlDbType.Int32);
        command.Parameters["@Repair_id"].Value = _repair.Repair_id;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}