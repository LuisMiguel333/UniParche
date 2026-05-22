using Uniparches.Domain.Enums;

namespace UniParche.Domain.Entities

    public class Parche
    {
        public int Id { get; set; }
        public int IdCreador { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public DateTime FechaEvento { get; set; }
        public int Cupos { get; set; }
        public string ImagenUrl { get; set; }
        public EstadoParche Estado { get; set; } = EstadoParche.Proximo;
        public RolGrupo TipoGrupo { get; set; }
        public int CreadorId { get; set; }
        public Usuario IdUniversidad { get; set; }


        // navigation properties

        public Usuario Creador { get; set; }
        public Universidad Universidad { get; set; }
    public ICollection<ParcheUsuario> ParcheUsuarios { get; set; } = new List<ParcheUsuario>();

}


       