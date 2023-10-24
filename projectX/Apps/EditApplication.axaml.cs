using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace projectX;

public partial class EditApplication : Window
{
    private List<Priority> _priority;
    private List<Type> _types;
    private List<Status> _status;
    private readonly Application _applications;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=remont;user=user_1;password=1234" ;
    public EditApplication(Application applications)
    {
        InitializeComponent();
        DataContext = applications;
        _applications = applications;
        FillPr();
        FillType();
        FillStatus();
    }

    private void BackButton2_OnClick(object? sender, RoutedEventArgs e)
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
        PrComboBox.SelectedItem = PrComboBox.ItemsSource.Cast<Priority>().First(it => it.Pr_name == _applications.Priority);
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
        typesComboBox.SelectedItem = CBType.ItemsSource.Cast<Type>().First(it => it.Type_name == _applications.Type);

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
        statusComboBox.SelectedItem = CBStatus.ItemsSource.Cast<Status>().First(it => it.Status_name == _applications.Status);

    }

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        List<Application> _applications;
        MySqlConnection _connection;
        string sql = "update Application set App_id = @App_id, Priority = @Prioty, App_Date = @App_Date, Equipment = @Equipment, Serial_number = @Serial_number, " +
                     "Type = @Type, Description = @Description, Client = @Client, Status = @Status, Comment = @Comment where App_id = @App_id)";
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
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }
}