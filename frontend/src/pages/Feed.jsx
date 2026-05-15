const publicaciones = [
  {
    id: 1,
    autor: 'Valentina Ríos',
    universidad: 'ITM',
    carrera: 'Ingeniería de Sistemas',
    contenido: 'Alguien más tiene el parcial de cálculo mañana? 😭',
    fecha: 'Hace 10 minutos',
    likes: 24,
  },
  {
    id: 2,
    autor: 'Sebastián Mora',
    universidad: 'UdeA',
    carrera: 'Medicina',
    contenido: 'Terminé el semestre con todas las materias. No lo puedo creer.',
    fecha: 'Hace 1 hora',
    likes: 87,
  },
  {
    id: 3,
    autor: 'Daniela Castro',
    universidad: 'EAFIT',
    carrera: 'Administración',
    contenido: 'Buscando grupo para el proyecto de finanzas. Somos 2, necesitamos 1 más.',
    fecha: 'Hace 3 horas',
    likes: 5,
  },
]

function TarjetaPublicacion({ publicacion }) {
  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3">
      <div className="flex items-center gap-3">
        <div className="w-10 h-10 rounded-full bg-purple-600 flex items-center justify-center text-white font-bold">
          {publicacion.autor[0]}
        </div>
        <div>
          <p className="text-white font-semibold text-sm">{publicacion.autor}</p>
          <p className="text-gray-500 text-xs">{publicacion.universidad} · {publicacion.carrera}</p>
        </div>
        <span className="ml-auto text-gray-600 text-xs">{publicacion.fecha}</span>
      </div>
      <p className="text-gray-300 text-sm">{publicacion.contenido}</p>
      <button className="self-start text-gray-500 text-xs hover:text-purple-400 transition-colors">
        {publicacion.likes} likes
      </button>
    </div>
  )
}

function Feed() {
  return (
    <div className="flex flex-col gap-4">
      <h1 className="text-2xl font-bold text-white">Feed</h1>
      {publicaciones.map(publicacion => (
        <TarjetaPublicacion key={publicacion.id} publicacion={publicacion} />
      ))}
    </div>
  )
}

export default Feed