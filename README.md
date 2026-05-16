# 🎓 UniParche

> Red social exclusiva para estudiantes de universidades colombianas.

**Proyecto Final Grupal — Programación Web — ITM 2026**

---

## 👥 Integrantes

| Nombre | Rol |
|---|---|
| Luis Miguel Cardona Jimenez | Backend |
| Pastor Felipe Garces Zapata | Frontend  |
| Moisés De Jesús González Navarro | Backend  |

---

## 📋 Descripción

UniParche es una red social donde estudiantes universitarios colombianos (ITM, EAFIT, UdeA, etc.) pueden:

- Crear su **perfil universitario** con foto, carrera y semestre
- Publicar en su **feed social** con fotos, likes y comentarios
- Organizar **parches** (eventos): fiestas, salidas, torneos, etc.
- Unirse a **grupos de estudio** por materia o universidad
- Conectar con otros estudiantes vía **amistades**

---

## 🛠️ Tecnologías

| Capa | Tecnología |
|---|---|
| Backend | .NET 8 Web API |
| ORM | pendiente |
| Base de datos | pendiente |
| Frontend | React |
| Documentación API | Swagger |
| Control de versiones | GitHub |

---

## 🏗️ Arquitectura del Backend

```
backend/
├── UniParche.Domain/        ← Entidades, Enums, Interfaces de servicios
├── UniParche.DataAccess/    ← DbContext, Repositorios, Migraciones, DataSeeder
└── UniParche.API/           ← Controllers, DTOs, AutoMapper, Program.cs
```

---

## 🚀 Cómo ejecutar el proyecto

### Backend

```bash
# 1. Clonar el repositorio
git clone https://github.com/LuisMiguel333/UniParche.git
cd UniParche/backend

# 2. Configurar la cadena de conexión en UniParche.API/appsettings.json
# Reemplazar "DefaultConnection" con tu cadena de PostgreSQL

# 3. Aplicar migraciones
dotnet ef database update --project UniParche.DataAccess --startup-project UniParche.API

# 4. Ejecutar el backend (el DataSeeder corre automáticamente)
cd UniParche.API
dotnet run

# 5. Abrir Swagger
# http://localhost:5000/swagger
```

### Frontend

```bash
cd UniParche/frontend
npm install
npm run dev
# Abrir http://localhost:5173
```

---

## 🗄️ Modelo de Base de Datos

Entidades principales: `Universidad`, `Usuario`, `Publicacion`, `Comentario`, `Like`, `Parche`, `ParcheAsistente`, `Grupo`, `GrupoMiembro`, `Amistad`

Relaciones:
- 1:N — Universidad → Usuarios
- 1:N — Usuario → Publicaciones
- 1:N — Usuario → Parches (como creador)
- N:M — Parches ↔ Usuarios (via `ParcheAsistente`)
- N:M — Grupos ↔ Usuarios (via `GrupoMiembro`)

---

## 📅 Fecha de entrega

Primera semana de Junio 2026 — ITM
