const BASE_URL = 'http://localhost:5292/api'

const parchesMock = [
  {
    id: 1,
    titulo: 'Torneo de FIFA en el ITM',
    lugar: 'Bloque 1 - Sala de sistemas',
    fecha: 'Sábado 17 de Mayo · 2:00 PM',
    cupos: 16,
    inscritos: 9,
    creador: 'Luis M.',
    universidad: 'ITM',
  },
  {
    id: 2,
    titulo: 'Salida al Parque Arví',
    lugar: 'Metro Acevedo - Punto de encuentro',
    fecha: 'Domingo 18 de Mayo · 8:00 AM',
    cupos: 20,
    inscritos: 15,
    creador: 'Valentina R.',
    universidad: 'UdeA',
  },
  {
    id: 3,
    titulo: 'Noche de estudio parciales',
    lugar: 'Biblioteca EAFIT - Sala grupal',
    fecha: 'Viernes 16 de Mayo · 6:00 PM',
    cupos: 10,
    inscritos: 10,
    creador: 'Daniela C.',
    universidad: 'EAFIT',
  },
]

const usarMock = true

export const obtenerParches = async () => {
  if (usarMock) return parchesMock

  const response = await fetch(`${BASE_URL}/parches`)
  return response.json()
}

export const crearParche = async (nuevoParche) => {
  if (usarMock) return { ...nuevoParche, id: Date.now() }

  const response = await fetch(`${BASE_URL}/parches`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(nuevoParche),
  })
  return response.json()
}