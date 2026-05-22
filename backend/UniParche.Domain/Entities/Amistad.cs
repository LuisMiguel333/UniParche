using UniParche.Domain.Enums;

namespace UniParche.Domain.Entities;

public class Amistad
{
	public int Id { get; set; }
	public int IdUsuario1 { get; set; }
	public int IdUsuario2 { get; set; }
	public EstadoAmistad Estado { get; set; } = EstadoAmistad.Pendiente;
	public DateTime Fecha { get; set; } = DateTime.UtcNow;

	// Navigation properties
	public Usuario Usuario1 { get; set; } = null!;
	public Usuario Usuario2 { get; set; } = null!;
}