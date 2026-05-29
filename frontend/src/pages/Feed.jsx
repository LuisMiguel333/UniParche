import { useState, useEffect } from 'react'
import { obtenerPublicaciones, crearPublicacion, obtenerComentarios, crearComentario, darLike } from '../services/publicaciones'

const MAX_CARACTERES = 280

const imagenesDemo = [
  'https://images.unsplash.com/photo-1541339907198-e08756dedf3f?w=600&q=80',
  'https://images.unsplash.com/photo-1523050854058-8df90110c9f1?w=600&q=80',
]

function SeccionComentarios({ postId, comentariosIniciales, onAgregarComentario }) {
  const [comentarios, setComentarios] = useState(comentariosIniciales)
  const [texto, setTexto] = useState('')
  const [mostrar, setMostrar] = useState(false)

  const handleAgregar = async () => {
    if (!texto.trim()) return
    const nuevo = await crearComentario(postId, texto.trim())
    setComentarios([...comentarios, nuevo])
    onAgregarComentario(postId)
    setTexto('')
  }

  return (
    <div className="flex flex-col gap-2">
      <button
        onClick={() => setMostrar(!mostrar)}
        className="self-start text-gray-500 text-xs hover:text-purple-400 transition-colors"
      >
        💬 {mostrar ? 'Ocultar comentarios' : `${comentarios.length} comentarios`}
      </button>

      {mostrar && (
        <div className="flex flex-col gap-3 mt-1">
          {comentarios.length === 0 && (
            <p className="text-gray-600 text-xs">Sin comentarios aún. Sé el primero.</p>
          )}
          {comentarios.map(comentario => (
            <div key={comentario.id} className="flex gap-2">
              <div className="w-7 h-7 rounded-full bg-gray-700 flex items-center justify-center text-white text-xs font-bold flex-shrink-0">
                {comentario.userName[0]}
              </div>
              <div className="bg-gray-800 rounded-xl px-3 py-2 flex flex-col gap-0.5 flex-1">
                <p className="text-white text-xs font-semibold">{comentario.userName}</p>
                <p className="text-gray-300 text-xs">{comentario.content}</p>
                <p className="text-gray-600 text-xs">{typeof comentario.createdAt === 'string' ? comentario.createdAt : new Date(comentario.createdAt).toLocaleDateString('es-CO')}</p>
              </div>
            </div>
          ))}
          <div className="flex gap-2 mt-1">
            <input
              value={texto}
              onChange={(e) => setTexto(e.target.value)}
              onKeyDown={(e) => e.key === 'Enter' && handleAgregar()}
              placeholder="Escribe un comentario..."
              className="flex-1 bg-gray-800 text-white text-xs rounded-lg px-3 py-2 outline-none border border-gray-700 focus:border-purple-500"
            />
            <button
              onClick={handleAgregar}
              className="text-xs px-3 py-2 rounded-lg bg-purple-600 hover:bg-purple-700 text-white transition-colors"
            >
              Enviar
            </button>
          </div>
        </div>
      )}
    </div>
  )
}

function TarjetaPublicacion({ publicacion, onLike, onAgregarComentario, comentariosIniciales }) {
  const [liked, setLiked] = useState(publicacion.isLikedByCurrentUser)
  const [likes, setLikes] = useState(publicacion.likeCount)

  const handleLike = async () => {
    setLiked(!liked)
    setLikes(liked ? likes - 1 : likes + 1)
    await darLike(publicacion.id)
    onLike(publicacion.id)
  }

  const fechaFormateada = typeof publicacion.createdAt === 'string'
    ? publicacion.createdAt
    : new Date(publicacion.createdAt).toLocaleDateString('es-CO')

  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl overflow-hidden hover:border-gray-700 transition-colors">
      <div className="p-5 flex flex-col gap-3">
        <div className="flex items-center gap-3">
          <div className="w-10 h-10 rounded-full bg-purple-600 flex items-center justify-center text-white font-bold flex-shrink-0">
            {publicacion.userName[0]}
          </div>
          <div>
            <p className="text-white font-semibold text-sm">{publicacion.userName}</p>
            <p className="text-gray-500 text-xs">{publicacion.universityName} · {publicacion.careerName}</p>
          </div>
          <span className="ml-auto text-gray-600 text-xs">{fechaFormateada}</span>
        </div>
        <p className="text-gray-300 text-sm">{publicacion.content}</p>
      </div>

      {publicacion.imageUrl && (
        <img
          src={publicacion.imageUrl}
          alt="Imagen de la publicación"
          className="w-full max-h-72 object-cover"
          onError={(e) => e.target.style.display = 'none'}
        />
      )}

      <div className="px-5 py-3 flex gap-4 border-t border-gray-800">
        <button
          onClick={handleLike}
          className={`flex items-center gap-1.5 text-xs transition-colors ${
            liked ? 'text-purple-400' : 'text-gray-500 hover:text-purple-400'
          }`}
        >
          {liked ? '♥' : '♡'} {likes} likes
        </button>
      </div>

      <div className="px-5 pb-4">
        <SeccionComentarios
          postId={publicacion.id}
          comentariosIniciales={comentariosIniciales}
          onAgregarComentario={onAgregarComentario}
        />
      </div>
    </div>
  )
}

function FormularioPublicacion({ onPublicar }) {
  const [contenido, setContenido] = useState('')
  const [conImagen, setConImagen] = useState(false)
  const [error, setError] = useState('')

  const handlePublicar = async () => {
    if (!contenido.trim()) {
      setError('Escribe algo antes de publicar')
      return
    }
    if (contenido.trim().length < 5) {
      setError('La publicación debe tener al menos 5 caracteres')
      return
    }
    await onPublicar(contenido.trim(), conImagen)
    setContenido('')
    setConImagen(false)
    setError('')
  }

  const restantes = MAX_CARACTERES - contenido.length
  const casiLleno = restantes <= 30
  const lleno = restantes < 0

  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3">
      <div className="flex items-start gap-3">
        <div className="w-10 h-10 rounded-full bg-purple-600 flex items-center justify-center text-white font-bold flex-shrink-0">
          F
        </div>
        <textarea
          value={contenido}
          onChange={(e) => {
            if (e.target.value.length <= MAX_CARACTERES) {
              setContenido(e.target.value)
              setError('')
            }
          }}
          placeholder="¿Qué está pasando en tu universidad?"
          rows={3}
          className="flex-1 bg-gray-800 text-white text-sm rounded-xl px-4 py-3 outline-none border border-gray-700 focus:border-purple-500 resize-none transition-colors"
        />
      </div>
      <div className="flex items-center gap-3">
        <button
          onClick={() => setConImagen(!conImagen)}
          className={`text-xs px-3 py-1.5 rounded-lg border transition-colors ${
            conImagen
              ? 'border-purple-500 text-purple-400 bg-purple-900'
              : 'border-gray-700 text-gray-500 hover:border-gray-500'
          }`}
        >
          📷 {conImagen ? 'Foto incluida' : 'Agregar foto'}
        </button>
        {conImagen && (
          <p className="text-gray-600 text-xs">Se agregará una foto de ejemplo</p>
        )}
      </div>
      {error && <p className="text-red-400 text-xs">{error}</p>}
      <div className="flex items-center justify-between">
        <span className={`text-xs ${lleno ? 'text-red-400' : casiLleno ? 'text-amber-400' : 'text-gray-600'}`}>
          {restantes} caracteres restantes
        </span>
        <button
          onClick={handlePublicar}
          disabled={lleno}
          className={`text-sm px-5 py-2 rounded-lg text-white transition-colors font-medium ${
            lleno ? 'bg-gray-700 cursor-not-allowed' : 'bg-purple-600 hover:bg-purple-700'
          }`}
        >
          Publicar
        </button>
      </div>
    </div>
  )
}

function Feed() {
  const [publicaciones, setPublicaciones] = useState([])
  const [comentariosPorPost, setComentariosPorPost] = useState({})
  const [cargando, setCargando] = useState(true)

  useEffect(() => {
    const cargarDatos = async () => {
  try {
    const posts = await obtenerPublicaciones()
    const lista = posts || []
    setPublicaciones(lista)
    const comentariosMap = {}
    for (const post of lista) {
      comentariosMap[post.id] = await obtenerComentarios(post.id)
    }
    setComentariosPorPost(comentariosMap)
  } catch (error) {
    console.error('Error cargando datos:', error)
  } finally {
    setCargando(false)
  }
}
    cargarDatos()
  }, [])

  const handleLike = (id) => {
    setPublicaciones(publicaciones.map(p =>
      p.id === id ? { ...p, likeCount: p.likeCount + 1 } : p
    ))
  }

  const handleAgregarComentario = (postId) => {
    setPublicaciones(publicaciones.map(p =>
      p.id === postId ? { ...p, commentCount: p.commentCount + 1 } : p
    ))
  }

  const handleCrearPublicacion = async (contenido, conImagen) => {
  const imagen = conImagen ? imagenesDemo[Math.floor(Math.random() * imagenesDemo.length)] : null
  const nueva = await crearPublicacion(contenido, imagen)
  if (nueva) {
    setPublicaciones([nueva, ...publicaciones])
    setComentariosPorPost({ [nueva.id]: [], ...comentariosPorPost })
  }
}

  if (cargando) return (
    <div className="flex items-center justify-center py-20">
      <p className="text-gray-500 text-sm">Cargando publicaciones...</p>
    </div>
  )

  return (
    <div className="flex flex-col gap-4">
      <h1 className="text-2xl font-bold text-white">Feed</h1>
      <FormularioPublicacion onPublicar={handleCrearPublicacion} />
      {publicaciones.map(publicacion => (
        <TarjetaPublicacion
          key={publicacion.id}
          publicacion={publicacion}
          onLike={handleLike}
          onAgregarComentario={handleAgregarComentario}
          comentariosIniciales={comentariosPorPost[publicacion.id] || []}
        />
      ))}
    </div>
  )
}

export default Feed