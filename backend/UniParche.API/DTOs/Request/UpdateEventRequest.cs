using System.ComponentModel.DataAnnotations;
using UniParche.Domain.Enums;

//sumary
// DTO para actualizar un evento, se necesita de manera obligatoria titulo , descripcion, lugar, fecha, capacidad, estado del evento
namespace UniParche.API.DTOs.Request;

public class UpdateEventRequest
{
	[Required]
	public string Title { get; set; } = string.Empty;

	[Required]
	public string Description { get; set; } = string.Empty;

	[Required]
	public string Location { get; set; } = string.Empty;

	[Required]
	public DateTime EventDate { get; set; }

	[Range(1, 500)]
	public int Capacity { get; set; }

	public string ImageUrl { get; set; } = string.Empty;

	public EventStatus Status { get; set; }
}