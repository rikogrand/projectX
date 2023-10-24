using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectX;

public class Application
{
    public int App_id { get; set; }
    public string Priority { get; set; }
    public DateTime App_Date { get; set; }
    public string Equipment { get; set; }
    public string Serial_number { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Client { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
}