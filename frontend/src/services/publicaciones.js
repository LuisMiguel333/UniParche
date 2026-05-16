const BASE_URL = 'http://localhost:5292/api'

const publicacionesMock = [
  {
    id: 1,
    autor: 'Valentina Ríos',
    universidad: 'ITM',
    carrera: 'Ingeniería de Sistemas',
    contenido: 'Alguien más tiene el parcial de cálculo mañana? 😭',
    fecha: 'Hace 10 minutos',
    likes: 24,
  },
  {
    id: 2,
    autor: 'Sebastián Mora',
    universidad: 'UdeA',
    carrera: 'Medicina',
    contenido: 'Terminé el semestre con todas las materias. No lo puedo creer.',
    fecha: 'Hace 1 hora',
    likes: 87,
  },
  {
    id: 3,
    autor: 'Daniela Castro',
    universidad: 'EAFIT',
    carrera: 'Administración',
    contenido: 'Buscando grupo para el proyecto de finanzas. Somos 2, necesitamos 1 más.',
    fecha: 'Hace 3 horas',
    likes: 5,
  },
]

const usarMock = true

export const obtenerPublicaciones = async () => {
  if (usarMock) return publicacionesMock

  const response = await fetch(`${BASE_URL}/publicaciones`)
  return response.json()
}