using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_dental.Models;

public partial class Dentistum
{
    [Key]
    [Column("DentistaID")]
    public int DentistaId { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Especialidad { get; set; } = null!;

    [InverseProperty("Dentista")]
    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
}
