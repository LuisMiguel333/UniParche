const BASE_URL = 'http://localhost:5292/api'

const gruposMock = [
  {
    id: 1,
    name: 'Cálculo III - ITM',
    subject: 'Cálculo',
    universityId: 1,
    memberCount: 12,
    creatorName: 'Valentina Ríos',
    description: '',
    type: 0,
  },
  {
    id: 2,
    name: 'Anatomía Primer Semestre',
    subject: 'Anatomía',
    universityId: 2,
    memberCount: 8,
    creatorName: 'Sebastián Mora',
    description: '',
    type: 0,
  },
  {
    id: 3,
    name: 'Finanzas Corporativas EAFIT',
    subject: 'Finanzas',
    universityId: 3,
    memberCount: 5,
    creatorName: 'Daniela Castro',
    description: '',
    type: 0,
  },
]

const usarMock = true

export const obtenerGrupos = async () => {
  if (usarMock) return gruposMock

  const response = await fetch(`${BASE_URL}/groups`)
  const data = await response.json()
  return data.data
}

export const crearGrupo = async (nuevoGrupo) => {
  if (usarMock) return { ...nuevoGrupo, id: Date.now(), memberCount: 1 }

  const response = await fetch(`${BASE_URL}/groups`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(nuevoGrupo),
  })
  const data = await response.json()
  return data.data
}

export const unirseAGrupo = async (groupId, userId = 1) => {
  if (usarMock) return { id: Date.now(), groupId, userId, role: 0 }

  const response = await fetch(`${BASE_URL}/groupmembers`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ groupId, userId, role: 0 }),
  })
  const data = await response.json()
  return data.data
}