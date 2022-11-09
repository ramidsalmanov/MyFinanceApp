namespace MyFinance.Domain.Models;

public class Currency : EntityBase
{
    public int Code { get; set; }
    public string CodeString { get; set; }
    public string Name { get; set; }
}