using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_dental.Models;

[Table("Motivo")]
public partial class Motivo
{
    [Key]
    [Column("MotivoID")]
    public int MotivoId { get; set; }

    [StringLength(200)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("Motivo")]
    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
}
