const BASE_URL = 'http://localhost:5292/api'

const getUsuarioActual = () => {
  try {
    const usuario = localStorage.getItem('usuario')
    return usuario ? JSON.parse(usuario) : { id: 1 }
  } catch {
    return { id: 1 }
  }
}

const usarMock = false

export const obtenerParches = async () => {
  if (usarMock) return []

  const response = await fetch(`${BASE_URL}/events`)
  const data = await response.json()
  return data.data
}

export const crearParche = async (nuevoParche) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/events`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      ...nuevoParche,
      CreatorId: usuario.id,
      EventDate: nuevoParche.EventDate.replace('Z', ''),
    }),
  })
  const data = await response.json()
  return data.data
}

export const unirseAParche = async (eventId) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/EventAttendees/event/${eventId}/user/${usuario.id}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
  })
  const data = await response.json()
  return data.data
}