using UniParche.Domain.Enums;

namespace Uniparche.Domain.Entities
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IdUniversidad { get; set; }
        public string Materia { get; set; }
        public int IdCreador { get; set; }
        public TipoGrupo Tipo { get; set; } = TipoGrupo.Estudio;
        // Navigation properties
        public Universidad Universidad { get; set; }
        public Usuario Creador { get; set; }
        public ICollection<GrupoMiembro> Parches { get; set; } = new List<GrupoMiembro>();
    }
}