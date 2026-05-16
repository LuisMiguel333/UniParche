import { useState } from 'react'

const MAX_CARACTERES = 280

const imagenesDemo = [
  'https://images.unsplash.com/photo-1541339907198-e08756dedf3f?w=600&q=80',
  'https://images.unsplash.com/photo-1523050854058-8df90110c9f1?w=600&q=80',
  'https://images.unsplash.com/photo-1498243691581-b145c3f54a5a?w=600&q=80',
]

const publicacionesIniciales = [
  {
    id: 1,
    autor: 'Valentina Ríos',
    universidad: 'ITM',
    carrera: 'Ingeniería de Sistemas',
    contenido: 'Alguien más tiene el parcial de cálculo mañana? 😭',
    fecha: 'Hace 10 minutos',
    likes: 24,
    imagen: null,
    comentarios: [
      { id: 1, autor: 'Sebastián Mora', texto: 'Yo también! Qué parcial tan difícil.', fecha: 'Hace 5 minutos' },
    ],
  },
  {
    id: 2,
    autor: 'Sebastián Mora',
    universidad: 'UdeA',
    carrera: 'Medicina',
    contenido: 'Terminé el semestre con todas las materias. No lo puedo creer.',
    fecha: 'Hace 1 hora',
    likes: 87,
    imagen: imagenesDemo[0],
    comentarios: [],
  },
  {
    id: 3,
    autor: 'Daniela Castro',
    universidad: 'EAFIT',
    carrera: 'Administración',
    contenido: 'Buscando grupo para el proyecto de finanzas. Somos 2, necesitamos 1 más.',
    fecha: 'Hace 3 horas',
    likes: 5,
    imagen: imagenesDemo[1],
    comentarios: [
      { id: 1, autor: 'Luis M.', texto: 'Yo me uno! Mándame mensaje.', fecha: 'Hace 2 horas' },
      { id: 2, autor: 'Valentina Ríos', texto: 'También estoy interesada.', fecha: 'Hace 1 hora' },
    ],
  },
]

function SeccionComentarios({ comentarios, onAgregarComentario }) {
  const [texto, setTexto] = useState('')
  const [mostrar, setMostrar] = useState(false)

  const handleAgregar = () => {
    if (!texto.trim()) return
    onAgregarComentario(texto.trim())
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
                {comentario.autor[0]}
              </div>
              <div className="bg-gray-800 rounded-xl px-3 py-2 flex flex-col gap-0.5 flex-1">
                <p className="text-white text-xs font-semibold">{comentario.autor}</p>
                <p className="text-gray-300 text-xs">{comentario.texto}</p>
                <p className="text-gray-600 text-xs">{comentario.fecha}</p>
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

function TarjetaPublicacion({ publicacion, onLike, onAgregarComentario }) {
  const [liked, setLiked] = useState(false)

  const handleLike = () => {
    setLiked(!liked)
    onLike(publicacion.id)
  }

  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl overflow-hidden hover:border-gray-700 transition-colors">
      <div className="p-5 flex flex-col gap-3">
        <div className="flex items-center gap-3">
          <div className="w-10 h-10 rounded-full bg-purple-600 flex items-center justify-center text-white font-bold flex-shrink-0">
            {publicacion.autor[0]}
          </div>
          <div>
            <p className="text-white font-semibold text-sm">{publicacion.autor}</p>
            <p className="text-gray-500 text-xs">{publicacion.universidad} · {publicacion.carrera}</p>
          </div>
          <span className="ml-auto text-gray-600 text-xs">{publicacion.fecha}</span>
        </div>
        <p className="text-gray-300 text-sm">{publicacion.contenido}</p>
      </div>

      {publicacion.imagen && (
        <img
          src={publicacion.imagen}
          alt="Imagen de la publicación"
          className="w-full max-h-72 object-cover"
        />
      )}

      <div className="px-5 py-3 flex gap-4 border-t border-gray-800">
        <button
          onClick={handleLike}
          className={`flex items-center gap-1.5 text-xs transition-colors ${
            liked ? 'text-purple-400' : 'text-gray-500 hover:text-purple-400'
          }`}
        >
          {liked ? '♥' : '♡'} {publicacion.likes} likes
        </button>
      </div>

      <div className="px-5 pb-4">
        <SeccionComentarios
          comentarios={publicacion.comentarios}
          onAgregarComentario={(texto) => onAgregarComentario(publicacion.id, texto)}
        />
      </div>
    </div>
  )
}

function FormularioPublicacion({ onPublicar }) {
  const [contenido, setContenido] = useState('')
  const [conImagen, setConImagen] = useState(false)
  const [error, setError] = useState('')

  const handlePublicar = () => {
    if (!contenido.trim()) {
      setError('Escribe algo antes de publicar')
      return
    }
    if (contenido.trim().length < 5) {
      setError('La publicación debe tener al menos 5 caracteres')
      return
    }
    onPublicar(contenido.trim(), conImagen)
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

      <div className="flex items-center gap-3 ml-13">
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
  const [publicaciones, setPublicaciones] = useState(publicacionesIniciales)

  const darLike = (id) => {
    setPublicaciones(publicaciones.map(p =>
      p.id === id ? { ...p, likes: p.likes + 1 } : p
    ))
  }

  const agregarComentario = (idPublicacion, texto) => {
    setPublicaciones(publicaciones.map(p =>
      p.id === idPublicacion
        ? {
            ...p,
            comentarios: [
              ...p.comentarios,
              { id: p.comentarios.length + 1, autor: 'Tú', texto, fecha: 'Ahora' },
            ],
          }
        : p
    ))
  }

  const crearPublicacion = (contenido, conImagen) => {
    const nueva = {
      id: publicaciones.length + 1,
      autor: 'Felipe Garces',
      universidad: 'ITM',
      carrera: 'Programación Web',
      contenido,
      fecha: 'Ahora',
      likes: 0,
      imagen: conImagen ? imagenesDemo[2] : null,
      comentarios: [],
    }
    setPublicaciones([nueva, ...publicaciones])
  }

  return (
    <div className="flex flex-col gap-4">
      <h1 className="text-2xl font-bold text-white">Feed</h1>
      <FormularioPublicacion onPublicar={crearPublicacion} />
      {publicaciones.map(publicacion => (
        <TarjetaPublicacion
          key={publicacion.id}
          publicacion={publicacion}
          onLike={darLike}
          onAgregarComentario={agregarComentario}
        />
      ))}
    </div>
  )
}

export default Feed