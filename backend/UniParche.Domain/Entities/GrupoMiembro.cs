using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities
{
	public class Grupo
	{
		public int IdGrupo { get; set; }
		public int GrupoId { get; set; }
		public RolGrupo Rol { get; set; } = RolGrupo.Miembro;
		public DateTime FechaUnion { get; set; } = DateTime.UtcNow;

		// Navigation properties

		public Grupo Grupo { get; set; }
		public Usuario Usuario { get; set; }
	}
}