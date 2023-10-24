using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace projectX;

public partial class AddApplication : Window
{
    private List<Priority> _priority;
    private List<Type> _types;
    private List<Status> _status;
    private List<Application> _applications;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=remont;user=user_1;password=1234" ; //"server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro" "server=localhost;database=remont;user=user_1;password=1234"
    public AddApplication()
    {
        InitializeComponent();
        FillPr();
        FillType();
        FillStatus();
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _applications = new List<Application>();
        MySqlConnection _connection;
        string sql = "insert into Application (App_id, Priority, App_Date, Equipment, Serial_number, Type, Description, Client, Status, Comment)" +
                     "  values (@App_id, @Priority, @App_Date, @Equipment, @Serial_number, @Type, @Description, @Client, @Status, @Comment)";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@App_id", MySqlDbType.Int32);
        command.Parameters["@App_id"].Value = TBxId.Text;
        command.Parameters.Add("@Priority", MySqlDbType.Int32);
        command.Parameters["@Priority"].Value = (CBoxPr.SelectedItem as projectX.Priority).Pr_id;
        command.Parameters.Add("@App_Date", MySqlDbType.Date);
        command.Parameters["@App_Date"].Value = DPApp_Date.SelectedDate;
        command.Parameters.Add("@Equipment", MySqlDbType.VarChar);
        command.Parameters["@Equipment"].Value = TBEquipment.Text;
        command.Parameters.Add("@Serial_number", MySqlDbType.VarChar);
        command.Parameters["@Serial_number"].Value = TBSerial_number.Text;
        command.Parameters.Add("@Type", MySqlDbType.Int32);
        command.Parameters["@Type"].Value = (CBType.SelectedItem as projectX.Type).Type_id;
        command.Parameters.Add("@Description", MySqlDbType.VarChar);
        command.Parameters["@Description"].Value = TBDescription.Text;
        command.Parameters.Add("@Client", MySqlDbType.VarChar);
        command.Parameters["@Client"].Value = TBClient.Text;
        command.Parameters.Add("@Status", MySqlDbType.Int32);
        command.Parameters["@Status"].Value = (CBStatus.SelectedItem as projectX.Status).Status_id;
        command.Parameters.Add("@Comment", MySqlDbType.VarChar);
        command.Parameters["@Comment"].Value = TBComment.Text;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }
    
    public void Close(bool result)
    {
        Result = result;
        base.Close(result);
    }
    
    public bool Result { get; private set; }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void FillPr()
    {
        string sql = "select Pr_name, Pr_id from Priority";
        _priority = new List<Priority>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurPr = new Priority()
            {
                Pr_id = reader.GetInt32("Pr_id"),
                Pr_name = reader.GetString("Pr_name")
            };
            _priority.Add(CurPr);
        }
        _connection.Close();
        var PrComboBox = this.Find<ComboBox>("CBoxPr");
        PrComboBox.ItemsSource = _priority;

    }
    
    public void FillType()
    {
        string sql = "select Type_name, Type_id from Types";
        _types = new List<Type>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurType = new Type()
            {
                Type_id = reader.GetInt32("Type_id"),
                Type_name = reader.GetString("Type_name")
            };
            _types.Add(CurType);
        }
        _connection.Close();
        var typesComboBox = this.Find<ComboBox>("CBType");
        typesComboBox.ItemsSource = _types;

    }
    
    public void FillStatus()
    {
        string sql = "select Status_name, Status_id from Statuses";
        _status = new List<Status>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurStatus = new Status()
            {
                Status_id = reader.GetInt32("Status_id"),
                Status_name = reader.GetString("Status_name")
            };
            _status.Add(CurStatus);
        }
        _connection.Close();
        var statusComboBox = this.Find<ComboBox>("CBStatus");
        statusComboBox.ItemsSource = _status;

    }
}