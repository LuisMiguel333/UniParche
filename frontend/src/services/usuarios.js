const BASE_URL = 'http://localhost:5000/api'

const usarMock = true

export const registrarUsuario = async (datos) => {
  if (usarMock) {
    return {
      id: 1,
      userName: datos.userName,
      email: datos.email,
      careerName: datos.careerName,
      semester: datos.semester,
      universityId: datos.universityId,
      universityName: 'ITM',
      profilePictureUrl: '',
      registerDate: new Date().toISOString(),
    }
  }

  const response = await fetch(`${BASE_URL}/users`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(datos),
  })
  const data = await response.json()
  return data.data
}

export const iniciarSesion = async (email, password) => {
  // Este endpoint no existe aún — se conectará cuando Moisés lo suba
  // El endpoint esperado será: POST /api/auth/login
  if (usarMock) {
    return {
      id: 1,
      userName: 'Felipe Garces',
      email,
      careerName: 'Programación Web',
      semester: 5,
      universityName: 'ITM',
      profilePictureUrl: '',
    }
  }

  const response = await fetch(`${BASE_URL}/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  })
  const data = await response.json()
  return data.data
}

export const obtenerUsuarioPorId = async (id) => {
  if (usarMock) return null

  const response = await fetch(`${BASE_URL}/users/${id}`)
  const data = await response.json()
  return data.data
}