using System;
using Org.BouncyCastle.Asn1.Cms;

namespace projectX;

public class Report
{
    public int Report_id { get; set; }
    public int Repair { get; set; }
    public TimeSpan Complete_time { get; set; }
    public string Defect_cause { get; set; }
    public string Work_description { get; set; }

}