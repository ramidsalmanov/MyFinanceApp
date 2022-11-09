using System.ComponentModel.DataAnnotations;

namespace MyFinance.Domain.Models;

public abstract class EntityBase : IEntity<long>
{
    [Key] 
    public long Id { get; set; }
}