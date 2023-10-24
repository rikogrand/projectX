using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectX;
public class Repair
{
    public int Repair_id { get; set; }
    public int Application { get; set; }
    public string Worker { get; set; }
    public string Order { get; set; }
    public string Service { get; set; }
}