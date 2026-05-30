const BASE_URL = 'http://localhost:5292/api'

const usarMock = false

export const obtenerPublicaciones = async () => {
  if (usarMock) return []

  const response = await fetch(`${BASE_URL}/posts`)
  const data = await response.json()
  return data.data
}

export const crearPublicacion = async (contenido, imageUrl = null, userId = 1) => {
  if (usarMock) return null

  const response = await fetch(`${BASE_URL}/posts?userId=${userId}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      title: contenido.substring(0, 100),
      content: contenido,
      imageUrl: imageUrl || '',
    }),
  })
  const data = await response.json()
  return data.data
}

export const obtenerComentarios = async (postId) => {
  if (usarMock) return []

  const response = await fetch(`${BASE_URL}/comments/post/${postId}`)
  const data = await response.json()
  return data.data || []
}

export const crearComentario = async (postId, content, userId = 1) => {
  if (usarMock) return null

  const response = await fetch(`${BASE_URL}/comments?userId=${userId}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      Content: content,
      PostId: postId,
    }),
  })
  const data = await response.json()
  return data.data
}

export const darLike = async (postId, userId = 1) => {
  if (usarMock) return null

  const response = await fetch(`${BASE_URL}/likes`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ postId, userId, reactionType: 0 }),
  })
  const data = await response.json()
  return data.data
}