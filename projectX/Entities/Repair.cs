using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectX;
public class Repair
{
    private int Repair_id { get; set; }
    private int Application { get; set; }
    private int Worker { get; set; }
    private int Order { get; set; }
    private int Service { get; set; }
}