const BASE_URL = 'http://localhost:5292/api'

const publicacionesMock = [
  {
    id: 1,
    userName: 'Valentina Ríos',
    universityName: 'ITM',
    careerName: 'Ingeniería de Sistemas',
    content: 'Alguien más tiene el parcial de cálculo mañana? 😭',
    createdAt: 'Hace 10 minutos',
    likeCount: 24,
    imageUrl: null,
    commentCount: 1,
    isLikedByCurrentUser: false,
  },
  {
    id: 2,
    userName: 'Sebastián Mora',
    universityName: 'UdeA',
    careerName: 'Medicina',
    content: 'Terminé el semestre con todas las materias. No lo puedo creer.',
    createdAt: 'Hace 1 hora',
    likeCount: 87,
    imageUrl: 'https://images.unsplash.com/photo-1541339907198-e08756dedf3f?w=600&q=80',
    commentCount: 0,
    isLikedByCurrentUser: false,
  },
  {
    id: 3,
    userName: 'Daniela Castro',
    universityName: 'EAFIT',
    careerName: 'Administración',
    content: 'Buscando grupo para el proyecto de finanzas. Somos 2, necesitamos 1 más.',
    createdAt: 'Hace 3 horas',
    likeCount: 5,
    imageUrl: null,
    commentCount: 2,
    isLikedByCurrentUser: false,
  },
]

const comentariosMock = {
  1: [
    { id: 1, userName: 'Sebastián Mora', content: 'Yo también! Qué parcial tan difícil.', createdAt: 'Hace 5 minutos', userProfilePictureUrl: '' },
  ],
  2: [],
  3: [
    { id: 1, userName: 'Luis M.', content: 'Yo me uno! Mándame mensaje.', createdAt: 'Hace 2 horas', userProfilePictureUrl: '' },
    { id: 2, userName: 'Valentina Ríos', content: 'También estoy interesada.', createdAt: 'Hace 1 hora', userProfilePictureUrl: '' },
  ],
}

const usarMock = true

export const obtenerPublicaciones = async () => {
  if (usarMock) return publicacionesMock

  const response = await fetch(`${BASE_URL}/posts`)
  const data = await response.json()
  return data.data
}

export const crearPublicacion = async (contenido, imageUrl = null, userId = 1) => {
  if (usarMock) {
    return {
      id: Date.now(),
      userName: 'Felipe Garces',
      universityName: 'ITM',
      careerName: 'Programación Web',
      content: contenido,
      createdAt: 'Ahora',
      likeCount: 0,
      imageUrl,
      commentCount: 0,
      isLikedByCurrentUser: false,
    }
  }

  const response = await fetch(`${BASE_URL}/posts`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ content: contenido, imageUrl, userId }),
  })
  const data = await response.json()
  return data.data
}

export const obtenerComentarios = async (postId) => {
  if (usarMock) return comentariosMock[postId] || []

  const response = await fetch(`${BASE_URL}/comments/post/${postId}`)
  const data = await response.json()
  return data.data
}

export const crearComentario = async (postId, content, userId = 1) => {
  if (usarMock) {
    return {
      id: Date.now(),
      userName: 'Tú',
      content,
      createdAt: 'Ahora',
      postId,
      userProfilePictureUrl: '',
    }
  }

  const response = await fetch(`${BASE_URL}/comments`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ content, postId, userId }),
  })
  const data = await response.json()
  return data.data
}

export const darLike = async (postId, userId = 1) => {
  if (usarMock) return { id: Date.now(), postId, userId, reactionType: 0 }

  const response = await fetch(`${BASE_URL}/likes`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ postId, userId, reactionType: 0 }),
  })
  const data = await response.json()
  return data.data
}