using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace projectX.Repairs;

public partial class EditRepairWin : Window
{
    private readonly Repair _repairs;
    private List<Worker> _workers;
    private List<Order> _orders;
    private List<Service> _services;
    private List<Application> _applications;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=remont;user=user_1;password=1234";
    
    public EditRepairWin(Repair repairs)
    {
        InitializeComponent();
        DataContext = repairs;
        _repairs = repairs;
        FillApp();
        FillOrder();
        FillService();
        FillWorker();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }


    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        List<Repair> _repairs;
        MySqlConnection _connection;
        string sql = "update Repairs set Repair_id = @Repair_id, Application = @Application, Worker = @Worker, repairs.Order = @Order, Service = @Service " +
                     "where Repair_id = @Repair_id";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Repair_id", MySqlDbType.Int32);
        command.Parameters["@Repair_id"].Value = TBxId.Text;
        
        command.Parameters.Add("@Application", MySqlDbType.Int32);
        command.Parameters["@Application"].Value = (CBApp.SelectedItem as projectX.Application).App_id;
        
        command.Parameters.Add("@Worker", MySqlDbType.Int32);
        command.Parameters["@Worker"].Value = (CBWorker.SelectedItem as projectX.Worker).Worker_id;
        
        command.Parameters.Add("@Order", MySqlDbType.Int32);
        command.Parameters["@Order"].Value = (CBOrder.SelectedItem as projectX.Order).Order_id;
        
        command.Parameters.Add("@Service", MySqlDbType.Int32);
        command.Parameters["@Service"].Value = (CBService.SelectedItem as projectX.Service).Service_id;

        command.ExecuteReader();
        _connection.Close();
        Close(true);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }
    
    public void FillWorker()
    {
        string sql = "select Worker_id, Worker_name from Workers";
        _workers = new List<Worker>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurWorker = new Worker()
            {
                Worker_id = reader.GetInt32("Worker_id"),
                Worker_name = reader.GetString("Worker_name")
            };
            _workers.Add(CurWorker);
        }
        _connection.Close();
        var WorkerComboBox = this.Find<ComboBox>("CBWorker");
        WorkerComboBox.ItemsSource = _workers;
        WorkerComboBox.SelectedItem = WorkerComboBox.ItemsSource.Cast<Worker>().First(it => it.Worker_name == _repairs.Worker);

    }
    
    public void FillOrder()
    {
        string sql = "select Order_id, Detail from remont.Order";
        _orders = new List<Order>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurOrder = new Order()
            {
                Order_id = reader.GetInt32("Order_id"),
                Detail = reader.GetString("Detail")
            };
            _orders.Add(CurOrder);
        }
        _connection.Close();
        var ordersComboBox = this.Find<ComboBox>("CBOrder");
        ordersComboBox.ItemsSource = _orders;
        ordersComboBox.SelectedItem = ordersComboBox.ItemsSource.Cast<Order>().First(it => it.Detail == _repairs.Order);

    }
    
    public void FillService()
    {
        string sql = "select Service_id, Service_name from Services";
        _services = new List<Service>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var CurService = new Service()
            {
                Service_id = reader.GetInt32("Service_id"),
                Service_name = reader.GetString("Service_name")
            };
            _services.Add(CurService);
        }
        _connection.Close();
        var serviceComboBox = this.Find<ComboBox>("CBService");
        serviceComboBox.ItemsSource = _services;
        serviceComboBox.SelectedItem = serviceComboBox.ItemsSource.Cast<Service>().First(it => it.Service_name == _repairs.Service);

    }
    
    public void FillApp()
    {
        string sql = "select App_id from Application";
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
                
            };
            _applications.Add(CurApp);
        }
        _connection.Close();
        var appComboBox = this.Find<ComboBox>("CBApp");
        appComboBox.ItemsSource = _applications;
        appComboBox.SelectedItem = appComboBox.ItemsSource.Cast<Application>().First(it => it.App_id == _repairs.Application);

    }
}