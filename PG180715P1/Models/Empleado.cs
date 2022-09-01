using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PG180715P1.Models
{
    public class Empleado
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }
        public decimal SueldoBase { get; set; }
    }

    public class EmpleadoDBContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
    }
}