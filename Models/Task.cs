using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SW_Parcial_v3.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre de la tarea debe tener menos de 100 caracteres")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int IsActive { get; set; } = 1;
        [Required]
        [ValidEstado(ErrorMessage = "El estado debe ser 'Pendiente', 'En Progreso', o 'Completado'.")]
        public string Status { get; set; } // Pending, In Progress, Completed
        [Required]
        [PrioAttribute(ErrorMessage = "La prioridad debe ser 'Baja', 'Media' o 'Alta'.")]
        public string Priority { get; set; }
        public int ProjectId { get; set; }

        [JsonIgnore]  // Esta anotación evita que la propiedad Project sea serializada
        public Project Project { get; set; }
    }

    public class PrioAttribute : ValidationAttribute
    {
        private readonly string[] _validEstados = { "Baja", "Media", "Alta" };

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
