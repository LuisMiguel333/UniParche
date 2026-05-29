const BASE_URL = 'http://localhost:5292/api'

const usarMock = false

export const obtenerParches = async () => {
  if (usarMock) return parchesMock

  const response = await fetch(`${BASE_URL}/events`)
  const data = await response.json()
  return data.data
}

export const crearParche = async (nuevoParche) => {
  if (usarMock) return { ...nuevoParche, id: Date.now(), attendeeCount: 0, status: 0 }

  const body = {
    ...nuevoParche,
    EventDate: nuevoParche.EventDate.replace('Z', ''),
  }

  console.log('Enviando al backend:', JSON.stringify(body))

  const response = await fetch(`${BASE_URL}/events`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  })

  console.log('Status:', response.status)
  const data = await response.json()
  console.log('Respuesta:', JSON.stringify(data))
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