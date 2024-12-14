using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestiones.models;

[Table("gestiones")]
public class Gestiones
{
    [Key]
    [Column("solicitudes")]
    public long solicitud { get; set; }
    [Column("tipo_solicitud")] 
    public string tipo_solicitud { get; set; }
    
}