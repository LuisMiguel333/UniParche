using Microsoft.EntityFrameworkCore;
using UniParche.DataAccess.DbContext;
using UniParche.Domain.Entities;

namespace UniParche.DataAccess.Seeders;

public static class DataSeeder
{
	public static async Task SeedAsync(UniParcheDbContext context)
	{
		await context.Database.MigrateAsync();

		// ═══════════════════════════════════════
		//  1. UNIVERSITIES
		// ═══════════════════════════════════════
		if (!await context.Universities.AnyAsync())
		{
			var universities = new List<University>
			{
				new() { Name = "Universidad de Antioquia",          DomainEmail = "udea.edu.co" },
				new() { Name = "Universidad Nacional de Colombia",   DomainEmail = "unal.edu.co" },
				new() { Name = "EAFIT",                             DomainEmail = "eafit.edu.co" },
				new() { Name = "Universidad Pontificia Bolivariana", DomainEmail = "upb.edu.co" }
			};
			await context.Universities.AddRangeAsync(universities);
			await context.SaveChangesAsync();
		}

		// ═══════════════════════════════════════
		//  2. USERS
		// ═══════════════════════════════════════
		if (!await context.Users.AnyAsync())
		{
			var udea = await context.Universities.FirstAsync(u => u.DomainEmail == "udea.edu.co");
			var unal = await context.Universities.FirstAsync(u => u.DomainEmail == "unal.edu.co");
			var eafit = await context.Universities.FirstAsync(u => u.DomainEmail == "eafit.edu.co");
			var upb = await context.Universities.FirstAsync(u => u.DomainEmail == "upb.edu.co");

			var users = new List<User>
			{
				new() {
					UserName = "carlos_dev",
					Email = "carlos@udea.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Ingeniería de Sistemas",
					Semester = 6,
					UniversityId = udea.Id,
					RegisterTime = DateTime.UtcNow.AddMonths(-5)
				},
				new() {
					UserName = "valentina_m",
					Email = "valentina@udea.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Psicología",
					Semester = 4,
					UniversityId = udea.Id,
					RegisterTime = DateTime.UtcNow.AddMonths(-4)
				},
				new() {
					UserName = "santiago_unal",
					Email = "santiago@unal.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Ingeniería Civil",
					Semester = 8,
					UniversityId = unal.Id,
					RegisterTime = DateTime.UtcNow.AddMonths(-6)
				},
				new() {
					UserName = "laura_eafit",
					Email = "laura@eafit.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Administración de Negocios",
					Semester = 5,
					UniversityId = eafit.Id,
					RegisterTime = DateTime.UtcNow.AddMonths(-3)
				},
				new() {
					UserName = "miguel_dev",
					Email = "miguel@udea.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Ingeniería de Sistemas",
					Semester = 7,
					UniversityId = udea.Id,
					RegisterTime = DateTime.UtcNow.AddMonths(-7)
				},
				new() {
					UserName = "andrea_upb",
					Email = "andrea@upb.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Diseño Gráfico",
					Semester = 3,
					UniversityId = upb.Id,
					RegisterTime = DateTime.UtcNow.AddMonths(-2)
				},
				new() {
					UserName = "juanpa_unal",
					Email = "juanpa@unal.edu.co",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
					CareerName = "Física",
					Semester = 9,
					UniversityId = unal.Id,

					