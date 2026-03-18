using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_dental.Models;

public partial class Citum
{
    [Key]
    [Column("CitaID")]
    public int CitaId { get; set; }

    [Column("PacienteID")]
    public int PacienteId { get; set; }

    [Column("DentistaID")]
    public int DentistaId { get; set; }

    [Column("MotivoID")]
    public int MotivoId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public int Duracion { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    public int? DiasRestantes { get; set; }

    public int? HorasRestantes { get; set; }

    [ForeignKey("DentistaId")]
    [InverseProperty("Cita")]
    public virtual Dentistum Dentista { get; set; } = null!;

    [ForeignKey("MotivoId")]
    [InverseProperty("Cita")]
    public virtual Motivo Motivo { get; set; } = null!;

    [ForeignKey("PacienteId")]
    [InverseProperty("Cita")]
    public virtual Paciente Paciente { get; set; } = null!;
}
