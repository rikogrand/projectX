using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectX;

public class Application
{
    private int App_id { get; set; }
    private int Priority { get; set; }
    private DateTime App_Date { get; set; }
    private string Equipment { get; set; }
    private int Serial_number { get; set; }
    private int Type { get; set; }
    private string Description { get; set; }
    private string Client { get; set; }
    private int Status { get; set; }
    private string Comment { get; set; }
}