using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_dental.Models;

[Table("Paciente")]
public partial class Paciente
{
    [Key]
    [Column("PacienteID")]
    public int PacienteId { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string? Correo { get; set; }

    [InverseProperty("Paciente")]
    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
}
