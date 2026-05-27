using Microsoft.EntityFrameworkCore;
using UniParche.DataAccess.DbContext;
using UniParche.Domain.Entities;
using UniParche.Domain.Enums;

namespace UniParche.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(UniParcheDbContext context)
    {
        await context.Database.MigrateAsync();

        // ???????????????????????????????????????
        //  1. UNIVERSITIES
        // ???????????????????????????????????????
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

        // ???????????????????????????????????????
        //  2. USERS
        // ???????????????????????????????????????
        if (!await context.Users.AnyAsync())
        {
            var udea  = await context.Universities.FirstAsync(u => u.DomainEmail == "udea.edu.co");
            var unal  = await context.Universities.FirstAsync(u => u.DomainEmail == "unal.edu.co");
            var eafit = await context.Universities.FirstAsync(u => u.DomainEmail == "eafit.edu.co");
            var upb   = await context.Universities.FirstAsync(u => u.DomainEmail == "upb.edu.co");

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
                    RegisterTime = DateTime.UtcNow.AddMonths(-8)
                }
            };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  3. POSTS
        // ???????????????????????????????????????
        if (!await context.Posts.AnyAsync())
        {
            var carlos    = await context.Users.FirstAsync(u => u.UserName == "carlos_dev");
            var valentina = await context.Users.FirstAsync(u => u.UserName == "valentina_m");
            var santiago  = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");
            var laura     = await context.Users.FirstAsync(u => u.UserName == "laura_eafit");
            var miguel    = await context.Users.FirstAsync(u => u.UserName == "miguel_dev");
            var andrea    = await context.Users.FirstAsync(u => u.UserName == "andrea_upb");
            var juanpa    = await context.Users.FirstAsync(u => u.UserName == "juanpa_unal");

            var posts = new List<Post>
            {
                new() {
                    Title = "¿Alguien para estudiar cálculo este finde?",
                    Content = "Voy a estudiar cálculo diferencial el sábado en el bloque 14 desde las 2pm. Si alguien quiere unirse avise por acá ??",
                    UserId = carlos.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new() {
                    Title = "La mejor cafetería del campus UdeA",
                    Content = "Seré honesta: la cafetería del bloque 9 tiene el mejor tinto de toda la universidad ?. ¿Cuál es su favorita?",
                    UserId = valentina.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                },
                new() {
                    Title = "Grupo de programación competitiva UNAL",
                    Content = "Estamos armando equipo para participar en el ICPC este año. Si te gusta la programación competitiva y estás en UNAL, escríbeme. Nivel mínimo: resolver problemas en Codeforces rating 1200+",
                    UserId = santiago.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new() {
                    Title = "Tips para primer semestre en EAFIT",
                    Content = "Para los que entran este semestre a EAFIT: 1) Vayan a las tutorías desde el primer día 2) No subestimen cálculo 3) El comedor de la cuarta planta es el mejor 4) Hagan amigos en los primeros meses, después todos están ocupados ??",
                    UserId = laura.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new() {
                    Title = "Perdí mis apuntes de Redes I — ¿alguien los tiene?",
                    Content = "Se me dañó el computador y perdí todos los apuntes del parcial pasado de Redes I. Si alguien los tiene y me los puede compartir, sería un salvavidas total ??",
                    UserId = miguel.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new() {
                    Title = "Busco colaboración para proyecto de diseño",
                    Content = "Estoy trabajando en un proyecto de identidad visual para una empresa local y necesito un desarrollador web que me ayude con la parte técnica. Es pagado. Si te interesa manda DM.",
                    UserId = andrea.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-18)
                },
                new() {
                    Title = "¿Alguien más tiene problemas con la física cuántica?",
                    Content = "Literalmente me siento en otro planeta cada vez que entro a cuántica. El profesor explica bien pero los ejercicios son otro nivel. ¿Alguien arma grupo de estudio?",
                    UserId = juanpa.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-6)
                },
                new() {
                    Title = "Repositorio útil para aprender .NET",
                    Content = "Les dejo este repositorio que encontré para aprender ASP.NET Core con Clean Architecture. Muy bueno para los de sistemas: github.com/jasontaylordev/CleanArchitecture",
                    UserId = carlos.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-2)
                }
            };
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  4. COMMENTS
        // ???????????????????????????????????????
        if (!await context.Comments.AnyAsync())
        {
            var carlos    = await context.Users.FirstAsync(u => u.UserName == "carlos_dev");
            var valentina = await context.Users.FirstAsync(u => u.UserName == "valentina_m");
            var santiago  = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");
            var miguel    = await context.Users.FirstAsync(u => u.UserName == "miguel_dev");
            var andrea    = await context.Users.FirstAsync(u => u.UserName == "andrea_upb");
            var juanpa    = await context.Users.FirstAsync(u => u.UserName == "juanpa_unal");

            var postCalculo   = await context.Posts.FirstAsync(p => p.Title.Contains("cálculo"));
            var postCafeteria = await context.Posts.FirstAsync(p => p.Title.Contains("cafetería"));
            var postICPC      = await context.Posts.FirstAsync(p => p.Title.Contains("competitiva"));
            var postRedes     = await context.Posts.FirstAsync(p => p.Title.Contains("Redes"));
            var postDotnet    = await context.Posts.FirstAsync(p => p.Title.Contains(".NET"));
            var postCuantica  = await context.Posts.FirstAsync(p => p.Title.Contains("cuántica"));

            var comments = new List<Comment>
            {
                // Post: cálculo
                new() {
                    Content = "¡Yo me uno! ¿Cuál bloque exactamente?",
                    UserId = valentina.Id, PostId = postCalculo.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-4).AddHours(1)
                },
                new() {
                    Content = "Yo también, avisa por este hilo cuando confirmes el salón ??",
                    UserId = miguel.Id, PostId = postCalculo.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-4).AddHours(2)
                },
                new() {
                    Content = "Perfecto, bloque 14 salón 201, nos vemos a las 2pm ??",
                    UserId = carlos.Id, PostId = postCalculo.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-4).AddHours(3)
                },

                // Post: cafetería
                new() {
                    Content = "El tinto del bloque 9 sí está bueno pero la comida del 12 es lo máximo ??",
                    UserId = miguel.Id, PostId = postCafeteria.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-3).AddHours(1)
                },
                new() {
                    Content = "Para mí la del bloque 18, tiene las mejores empanadas de toda la U",
                    UserId = carlos.Id, PostId = postCafeteria.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-3).AddHours(2)
                },

                // Post: ICPC
                new() {
                    Content = "Yo estoy en rating 1400 en Codeforces, me apunto. ¿Cuándo es la primera reunión?",
                    UserId = juanpa.Id, PostId = postICPC.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-2).AddHours(1)
                },
                new() {
                    Content = "Genial! La primera reunión puede ser este viernes virtual, así entran de otras sedes también",
                    UserId = santiago.Id, PostId = postICPC.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-2).AddHours(4)
                },

                // Post: Redes
                new() {
                    Content = "Yo tengo los apuntes! Te los mando al correo, ¿cuál es?",
                    UserId = carlos.Id, PostId = postRedes.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-20)
                },
                new() {
                    Content = "Yo también los tengo, los subí a Drive: drive.google.com/... (ficticio)",
                    UserId = valentina.Id, PostId = postRedes.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-19)
                },
                new() {
                    Content = "Uy parcero gracias, salvaron mi semestre ????",
                    UserId = miguel.Id, PostId = postRedes.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-18)
                },

                // Post: .NET
                new() {
                    Content = "Ese repo es buenísimo, yo lo usé para aprender CQRS con MediatR",
                    UserId = miguel.Id, PostId = postDotnet.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-1)
                },
                new() {
                    Content = "¿Tienen algún recurso para aprender Entity Framework desde cero?",
                    UserId = andrea.Id, PostId = postDotnet.Id,
                    CreatedAt = DateTime.UtcNow.AddMinutes(-45)
                },

                // Post: cuántica
                new() {
                    Content = "Bienvenido al club ?? yo llevo 3 semestres peleando con eso",
                    UserId = santiago.Id, PostId = postCuantica.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-5)
                },
                new() {
                    Content = "El libro de Griffiths es el mejor para cuántica, busca la versión PDF",
                    UserId = carlos.Id, PostId = postCuantica.Id,
                    CreatedAt = DateTime.UtcNow.AddHours(-4)
                }
            };
            await context.Comments.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  5. LIKES
        // ???????????????????????????????????????
        if (!await context.Likes.AnyAsync())
        {
            var carlos    = await context.Users.FirstAsync(u => u.UserName == "carlos_dev");
            var valentina = await context.Users.FirstAsync(u => u.UserName == "valentina_m");
            var santiago  = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");
            var miguel    = await context.Users.FirstAsync(u => u.UserName == "miguel_dev");
            var andrea    = await context.Users.FirstAsync(u => u.UserName == "andrea_upb");
            var juanpa    = await context.Users.FirstAsync(u => u.UserName == "juanpa_unal");

            var postCalculo   = await context.Posts.FirstAsync(p => p.Title.Contains("cálculo"));
            var postCafeteria = await context.Posts.FirstAsync(p => p.Title.Contains("cafetería"));
            var postICPC      = await context.Posts.FirstAsync(p => p.Title.Contains("competitiva"));
            var postRedes     = await context.Posts.FirstAsync(p => p.Title.Contains("Redes"));
            var postDotnet    = await context.Posts.FirstAsync(p => p.Title.Contains(".NET"));
            var postTips      = await context.Posts.FirstAsync(p => p.Title.Contains("Tips"));

            var likes = new List<Like>
            {
                new() { UserId = valentina.Id, PostId = postCalculo.Id,   ReactionType = ReactionType.Like },
                new() { UserId = miguel.Id,    PostId = postCalculo.Id,   ReactionType = ReactionType.Like },
                new() { UserId = andrea.Id,    PostId = postCalculo.Id,   ReactionType = ReactionType.Like },
                new() { UserId = carlos.Id,    PostId = postCafeteria.Id, ReactionType = ReactionType.Like },
                new() { UserId = santiago.Id,  PostId = postCafeteria.Id, ReactionType = ReactionType.Like },
                new() { UserId = juanpa.Id,    PostId = postCafeteria.Id, ReactionType = ReactionType.Like },
                new() { UserId = andrea.Id,    PostId = postCafeteria.Id, ReactionType = ReactionType.Like },
                new() { UserId = carlos.Id,    PostId = postICPC.Id,      ReactionType = ReactionType.Like },
                new() { UserId = juanpa.Id,    PostId = postICPC.Id,      ReactionType = ReactionType.Like },
                new() { UserId = valentina.Id, PostId = postRedes.Id,     ReactionType = ReactionType.Like },
                new() { UserId = andrea.Id,    PostId = postRedes.Id,     ReactionType = ReactionType.Like },
                new() { UserId = miguel.Id,    PostId = postDotnet.Id,    ReactionType = ReactionType.Like },
                new() { UserId = santiago.Id,  PostId = postDotnet.Id,    ReactionType = ReactionType.Like },
                new() { UserId = juanpa.Id,    PostId = postDotnet.Id,    ReactionType = ReactionType.Like },
                new() { UserId = carlos.Id,    PostId = postTips.Id,      ReactionType = ReactionType.Like },
                new() { UserId = miguel.Id,    PostId = postTips.Id,      ReactionType = ReactionType.Like },
                new() { UserId = valentina.Id, PostId = postTips.Id,      ReactionType = ReactionType.Like }
            };
            await context.Likes.AddRangeAsync(likes);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  6. EVENTS
        // ???????????????????????????????????????
        if (!await context.Events.AnyAsync())
        {
            var carlos   = await context.Users.FirstAsync(u => u.UserName == "carlos_dev");
            var santiago = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");
            var laura    = await context.Users.FirstAsync(u => u.UserName == "laura_eafit");
            var udea     = await context.Universities.FirstAsync(u => u.DomainEmail == "udea.edu.co");
            var unal     = await context.Universities.FirstAsync(u => u.DomainEmail == "unal.edu.co");
            var eafit    = await context.Universities.FirstAsync(u => u.DomainEmail == "eafit.edu.co");

            var events = new List<Event>
            {
                new() {
                    Title = "Hackathon UdeA 2026",
                    Description = "Competencia de programación de 24 horas. Forma tu equipo y a codear. Habrá premios para los 3 primeros puestos.",
                    Location = "Bloque 21 - Sala de Cómputo",
                    EventDate = DateTime.UtcNow.AddDays(10),
                    Capacity = 50, 
                    CreatorId = carlos.Id, UniversityId = udea.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new() {
                    Title = "Parche de fútbol 5",
                    Description = "Partido amistoso entre estudiantes de ingeniería. Traer ropa deportiva y ganas de sudar.",
                    Location = "Cancha de fútbol UdeA",
                    EventDate = DateTime.UtcNow.AddDays(3),
                    Capacity = 20, 
                    CreatorId = carlos.Id, UniversityId = udea.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new() {
                    Title = "Feria de proyectos UNAL",
                    Description = "Exposición de proyectos de grado del semestre. Entrada libre, habrá jurados y posibilidad de financiamiento.",
                    Location = "Edificio de Ingeniería UNAL",
                    EventDate = DateTime.UtcNow.AddDays(15),
                    Capacity = 100, 
                    CreatorId = santiago.Id, UniversityId = unal.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new() {
                    Title = "Charla: Emprendimiento universitario",
                    Description = "Emprendedores egresados de EAFIT comparten sus experiencias. Networking al final del evento.",
                    Location = "Auditorio EAFIT - Bloque 38",
                    EventDate = DateTime.UtcNow.AddDays(7),
                    Capacity = 80, 
                    CreatorId = laura.Id, UniversityId = eafit.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };
            await context.Events.AddRangeAsync(events);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  7. GROUPS
        // ???????????????????????????????????????
        if (!await context.Groups.AnyAsync())
        {
            var carlos   = await context.Users.FirstAsync(u => u.UserName == "carlos_dev");
            var laura    = await context.Users.FirstAsync(u => u.UserName == "laura_eafit");
            var santiago = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");
            var udea     = await context.Universities.FirstAsync(u => u.DomainEmail == "udea.edu.co");
            var eafit    = await context.Universities.FirstAsync(u => u.DomainEmail == "eafit.edu.co");
            var unal     = await context.Universities.FirstAsync(u => u.DomainEmail == "unal.edu.co");

            var groups = new List<Group>
            {
                new() {
                    Name = "Devs UdeA",
                    Description = "Grupo de estudiantes apasionados por el desarrollo de software. Compartimos recursos, hacemos code reviews y organizamos proyectos colaborativos.",
                    Subject = "Programación", 
                    CreatorId = carlos.Id, UniversityId = udea.Id,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2)
                },
                new() {
                    Name = "Emprendedores EAFIT",
                    Description = "Espacio para compartir ideas de negocio, conectar con mentores y co-fundar startups.",
                    Subject = "Emprendimiento", 
                    CreatorId = laura.Id, UniversityId = eafit.Id,
                    CreatedAt = DateTime.UtcNow.AddMonths(-1)
                },
                new() {
                    Name = "Física & Matemáticas UNAL",
                    Description = "Grupo de estudio para las carreras más duras de la UNAL. Resolución de talleres, preparación de parciales y olimpiadas.",
                    Subject = "Ciencias Exactas", 
                    CreatorId = santiago.Id, UniversityId = unal.Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                }
            };
            await context.Groups.AddRangeAsync(groups);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  8. EVENT ATTENDEES
        // ???????????????????????????????????????
        if (!await context.EventAttendees.AnyAsync())
        {
            var valentina = await context.Users.FirstAsync(u => u.UserName == "valentina_m");
            var miguel    = await context.Users.FirstAsync(u => u.UserName == "miguel_dev");
            var andrea    = await context.Users.FirstAsync(u => u.UserName == "andrea_upb");
            var juanpa    = await context.Users.FirstAsync(u => u.UserName == "juanpa_unal");

            var hackathon = await context.Events.FirstAsync(e => e.Title.Contains("Hackathon"));
            var futbol    = await context.Events.FirstAsync(e => e.Title.Contains("fútbol"));
            var feria     = await context.Events.FirstAsync(e => e.Title.Contains("Feria"));

            var attendees = new List<EventAttendee>
            {
                new() { EventId = hackathon.Id, UserId = valentina.Id, Status = "Confirmed" },
                new() { EventId = hackathon.Id, UserId = miguel.Id,    Status = "Confirmed" },
                new() { EventId = hackathon.Id, UserId = juanpa.Id,    Status = "Pending"   },
                new() { EventId = hackathon.Id, UserId = andrea.Id,    Status = "Pending"   },
                new() { EventId = futbol.Id,    UserId = miguel.Id,    Status = "Confirmed" },
                new() { EventId = futbol.Id,    UserId = juanpa.Id,    Status = "Confirmed" },
                new() { EventId = feria.Id,     UserId = valentina.Id, Status = "Confirmed" },
                new() { EventId = feria.Id,     UserId = andrea.Id,    Status = "Pending"   }
            };
            await context.EventAttendees.AddRangeAsync(attendees);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  9. GROUP MEMBERS
        // ???????????????????????????????????????
        if (!await context.GroupMembers.AnyAsync())
        {
            var valentina = await context.Users.FirstAsync(u => u.UserName == "valentina_m");
            var miguel    = await context.Users.FirstAsync(u => u.UserName == "miguel_dev");
            var andrea    = await context.Users.FirstAsync(u => u.UserName == "andrea_upb");
            var juanpa    = await context.Users.FirstAsync(u => u.UserName == "juanpa_unal");
            var santiago  = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");

            var devsGroup    = await context.Groups.FirstAsync(g => g.Name == "Devs UdeA");
            var emprendGroup = await context.Groups.FirstAsync(g => g.Name.Contains("Emprendedores"));
            var fisicaGroup  = await context.Groups.FirstAsync(g => g.Name.Contains("Física"));

            var members = new List<GroupMember>
            {
                new() { GroupId = devsGroup.Id,    UserId = miguel.Id,    Role = "Moderator", JoinDate = DateTime.UtcNow.AddMonths(-2) },
                new() { GroupId = devsGroup.Id,    UserId = valentina.Id, Role = "Member",    JoinDate = DateTime.UtcNow.AddMonths(-1) },
                new() { GroupId = devsGroup.Id,    UserId = andrea.Id,    Role = "Member",    JoinDate = DateTime.UtcNow.AddDays(-10)  },
                new() { GroupId = emprendGroup.Id, UserId = andrea.Id,    Role = "Moderator", JoinDate = DateTime.UtcNow.AddMonths(-1) },
                new() { GroupId = emprendGroup.Id, UserId = valentina.Id, Role = "Member",    JoinDate = DateTime.UtcNow.AddDays(-5)   },
                new() { GroupId = fisicaGroup.Id,  UserId = juanpa.Id,    Role = "Moderator", JoinDate = DateTime.UtcNow.AddDays(-14)  },
                new() { GroupId = fisicaGroup.Id,  UserId = miguel.Id,    Role = "Member",    JoinDate = DateTime.UtcNow.AddDays(-7)   }
            };
            await context.GroupMembers.AddRangeAsync(members);
            await context.SaveChangesAsync();
        }

        // ???????????????????????????????????????
        //  10. FRIENDSHIPS
        // ???????????????????????????????????????
        if (!await context.Friendships.AnyAsync())
        {
            var carlos    = await context.Users.FirstAsync(u => u.UserName == "carlos_dev");
            var valentina = await context.Users.FirstAsync(u => u.UserName == "valentina_m");
            var miguel    = await context.Users.FirstAsync(u => u.UserName == "miguel_dev");
            var andrea    = await context.Users.FirstAsync(u => u.UserName == "andrea_upb");
            var juanpa    = await context.Users.FirstAsync(u => u.UserName == "juanpa_unal");
            var santiago  = await context.Users.FirstAsync(u => u.UserName == "santiago_unal");

            var friendships = new List<Friendship>
            {
                new() { User1Id = carlos.Id,    User2Id = valentina.Id, Status = FriendshipStatus.Accepted, Date = DateTime.UtcNow.AddMonths(-3) },
                new() { User1Id = carlos.Id,    User2Id = miguel.Id,    Status = FriendshipStatus.Accepted, Date = DateTime.UtcNow.AddMonths(-4) },
                new() { User1Id = carlos.Id,    User2Id = santiago.Id,  Status = FriendshipStatus.Accepted, Date = DateTime.UtcNow.AddMonths(-2) },
                new() { User1Id = valentina.Id, User2Id = andrea.Id,    Status = FriendshipStatus.Accepted, Date = DateTime.UtcNow.AddMonths(-1) },
                new() { User1Id = miguel.Id,    User2Id = juanpa.Id,    Status = FriendshipStatus.Accepted, Date = DateTime.UtcNow.AddDays(-20)  },
                new() { User1Id = santiago.Id,  User2Id = juanpa.Id,    Status = FriendshipStatus.Accepted, Date = DateTime.UtcNow.AddDays(-15)  },
                new() { User1Id = andrea.Id,    User2Id = miguel.Id,    Status = FriendshipStatus.Pending,  Date = DateTime.UtcNow.AddDays(-2)   },
                new() { User1Id = juanpa.Id,    User2Id = valentina.Id, Status = FriendshipStatus.Pending,  Date = DateTime.UtcNow.AddDays(-1)   }
            };
            await context.Friendships.AddRangeAsync(friendships);
            await context.SaveChangesAsync();
        }
    }
}
