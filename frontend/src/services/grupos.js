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

export const obtenerGrupos = async () => {
  if (usarMock) return []

  const response = await fetch(`${BASE_URL}/groups`)
  const data = await response.json()
  return data.data
}

export const crearGrupo = async (nuevoGrupo) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/groups`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      Name: nuevoGrupo.name,
      Description: nuevoGrupo.description || 'Sin descripción',
      Subject: nuevoGrupo.subject,
      UniversityId: nuevoGrupo.universityId || 1,
      CreatorId: usuario.id,
      Type: 0,
    }),
  })
  const data = await response.json()
  return data.data
}

export const unirseAGrupo = async (groupId) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/GroupMembers/group/${groupId}/user/${usuario.id}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
  })
  const data = await response.json()
  return data.data
}