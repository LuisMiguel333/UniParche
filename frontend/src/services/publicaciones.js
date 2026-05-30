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

export const obtenerPublicaciones = async () => {
  if (usarMock) return []

  const response = await fetch(`${BASE_URL}/posts`)
  const data = await response.json()
  return data.data
}

export const crearPublicacion = async (contenido, imageUrl = null) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/posts?userId=${usuario.id}`, {
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

export const crearComentario = async (postId, content) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/comments?userId=${usuario.id}`, {
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

export const darLike = async (postId) => {
  if (usarMock) return null

  const usuario = getUsuarioActual()

  const response = await fetch(`${BASE_URL}/likes`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ postId, userId: usuario.id, reactionType: 0 }),
  })
  const data = await response.json()
  return data.data
}