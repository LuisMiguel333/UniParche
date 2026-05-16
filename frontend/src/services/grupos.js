const BASE_URL = 'http://localhost:5292/api'

const gruposMock = [
  {
    id: 1,
    nombre: 'Cálculo III - ITM',
    materia: 'Cálculo',
    universidad: 'ITM',
    miembros: 12,
    creador: 'Valentina Ríos',
    unido: false,
  },
  {
    id: 2,
    nombre: 'Anatomía Primer Semestre',
    materia: 'Anatomía',
    universidad: 'UdeA',
    miembros: 8,
    creador: 'Sebastián Mora',
    unido: false,
  },
  {
    id: 3,
    nombre: 'Finanzas Corporativas EAFIT',
    materia: 'Finanzas',
    universidad: 'EAFIT',
    miembros: 5,
    creador: 'Daniela Castro',
    unido: false,
  },
]

const usarMock = true

export const obtenerGrupos = async () => {
  if (usarMock) return gruposMock

  const response = await fetch(`${BASE_URL}/grupos`)
  return response.json()
}