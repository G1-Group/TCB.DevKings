using System.ComponentModel.DataAnnotations.Schema;

namespace TCBApp.Models;

public abstract class ModelBase
{
    [Column("id")]
    public long Id { get; set; }
}