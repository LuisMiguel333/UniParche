const parches = [
  {
    id: 1,
    titulo: 'Torneo de FIFA en el ITM',
    lugar: 'Bloque 1 - Sala de sistemas',
    fecha: 'Sábado 17 de Mayo · 2:00 PM',
    cupos: 16,
    inscritos: 9,
    creador: 'Luis M.',
    universidad: 'ITM',
  },
  {
    id: 2,
    titulo: 'Salida al Parque Arví',
    lugar: 'Metro Acevedo - Punto de encuentro',
    fecha: 'Domingo 18 de Mayo · 8:00 AM',
    cupos: 20,
    inscritos: 15,
    creador: 'Valentina R.',
    universidad: 'UdeA',
  },
  {
    id: 3,
    titulo: 'Noche de estudio parciales',
    lugar: 'Biblioteca EAFIT - Sala grupal',
    fecha: 'Viernes 16 de Mayo · 6:00 PM',
    cupos: 10,
    inscritos: 10,
    creador: 'Daniela C.',
    universidad: 'EAFIT',
  },
]

function TarjetaParche({ parche }) {
  const lleno = parche.inscritos >= parche.cupos

  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-white font-semibold">{parche.titulo}</p>
          <p className="text-gray-500 text-xs mt-1">{parche.universidad} · Organiza {parche.creador}</p>
        </div>
        <span className={`text-xs px-2 py-1 rounded-full ${lleno ? 'bg-red-900 text-red-400' : 'bg-green-900 text-green-400'}`}>
          {lleno ? 'Lleno' : 'Disponible'}
        </span>
      </div>
      <div className="flex flex-col gap-1">
        <p className="text-gray-400 text-sm">📍 {parche.lugar}</p>
        <p className="text-gray-400 text-sm">📅 {parche.fecha}</p>
        <p className="text-gray-400 text-sm">👥 {parche.inscritos} / {parche.cupos} cupos</p>
      </div>
      <button
        disabled={lleno}
        className={`self-start text-sm px-4 py-2 rounded-lg transition-colors ${
          lleno
            ? 'bg-gray-800 text-gray-600 cursor-not-allowed'
            : 'bg-purple-600 hover:bg-purple-700 text-white'
        }`}
      >
        {lleno ? 'Sin cupos' : 'Unirme al parche'}
      </button>
    </div>
  )
}

function Parches() {
  return (
    <div className="flex flex-col gap-4">
      <h1 className="text-2xl font-bold text-white">Parches</h1>
      {parches.map(parche => (
        <TarjetaParche key={parche.id} parche={parche} />
      ))}
    </div>
  )
}

export default Parches