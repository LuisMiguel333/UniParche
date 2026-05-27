const BASE_URL = 'http://localhost:5292/api'

const parchesMock = [
  {
    id: 1,
    title: 'Torneo de FIFA en el ITM',
    location: 'Bloque 1 - Sala de sistemas',
    eventDate: 'Sábado 17 de Mayo · 2:00 PM',
    capacity: 16,
    attendeeCount: 9,
    creatorName: 'Luis M.',
    universityId: 1,
    status: 0,
    description: '',
    imageUrl: null,
  },
  {
    id: 2,
    title: 'Salida al Parque Arví',
    location: 'Metro Acevedo - Punto de encuentro',
    eventDate: 'Domingo 18 de Mayo · 8:00 AM',
    capacity: 20,
    attendeeCount: 15,
    creatorName: 'Valentina R.',
    universityId: 1,
    status: 0,
    description: '',
    imageUrl: null,
  },
  {
    id: 3,
    title: 'Noche de estudio parciales',
    location: 'Biblioteca EAFIT - Sala grupal',
    eventDate: 'Viernes 16 de Mayo · 6:00 PM',
    capacity: 10,
    attendeeCount: 10,
    creatorName: 'Daniela C.',
    universityId: 1,
    status: 0,
    description: '',
    imageUrl: null,
  },
]

const usarMock = true

export const obtenerParches = async () => {
  if (usarMock) return parchesMock

  const response = await fetch(`${BASE_URL}/events`)
  const data = await response.json()
  return data.data
}

export const crearParche = async (nuevoParche) => {
  if (usarMock) return { ...nuevoParche, id: Date.now(), attendeeCount: 0, status: 0 }

  const response = await fetch(`${BASE_URL}/events`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(nuevoParche),
  })
  const data = await response.json()
  return data.data
}

export const unirseAParche = async (eventId, userId = 1) => {
  if (usarMock) return { id: Date.now(), eventId, userId, status: 0 }

  const response = await fetch(`${BASE_URL}/eventattendees`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ eventId, userId, status: 0 }),
  })
  const data = await response.json()
  return data.data
}