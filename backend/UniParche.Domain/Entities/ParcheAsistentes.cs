using UniParche.Domaing.Enums;


namespace UniParche.Domain.Entities
{
    public class ParcheAsistentes
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int EstadoAsistencia Estado { get; set; } = EstadoAsistencia.Pendiente;
        // Navigation properties
        public Parche Parche { get; set; }
        public Usuario Usuario { get; set; }
    }
}