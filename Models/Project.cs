using System.ComponentModel.DataAnnotations;

namespace SW_Parcial_v3.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre de la tarea debe tener menos de 100 caracteres")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IsActive { get; set; } = 1;
        [Required]
        [ValidEstado(ErrorMessage = "El estado debe ser 'Pendiente', 'En Progreso', o 'Completado'.")]
        public string Status { get; set; } // Pending, In Progress, Completed

        public ICollection<Task> Tasks { get; set; }
    }

    public class ValidEstadoAttribute : ValidationAttribute
    {
        private readonly string[] _validEstados = { "Pendiente", "En Progreso", "Completado" };

        public override bool IsValid(object value)
        {
            if (value is string estado)
            {
                return _validEstados.Contains(estado);
            }
            return false;
        }
    }

}
