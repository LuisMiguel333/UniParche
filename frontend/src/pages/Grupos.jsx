const grupos = [
  {
    id: 1,
    nombre: 'Cálculo III - ITM',
    materia: 'Cálculo',
    universidad: 'ITM',
    miembros: 12,
    creador: 'Valentina Ríos',
  },
  {
    id: 2,
    nombre: 'Anatomía Primer Semestre',
    materia: 'Anatomía',
    universidad: 'UdeA',
    miembros: 8,
    creador: 'Sebastián Mora',
  },
  {
    id: 3,
    nombre: 'Finanzas Corporativas EAFIT',
    materia: 'Finanzas',
    universidad: 'EAFIT',
    miembros: 5,
    creador: 'Daniela Castro',
  },
]

function TarjetaGrupo({ grupo }) {
  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-white font-semibold">{grupo.nombre}</p>
          <p className="text-gray-500 text-xs mt-1">{grupo.universidad} · {grupo.materia}</p>
        </div>
        <span className="text-xs text-gray-400 bg-gray-800 px-2 py-1 rounded-full">
          {grupo.miembros} miembros
        </span>
      </div>
      <p className="text-gray-500 text-xs">Creado por {grupo.creador}</p>
      <button className="self-start text-sm px-4 py-2 rounded-lg bg-purple-600 hover:bg-purple-700 text-white transition-colors">
        Unirme al grupo
      </button>
    </div>
  )
}

function Grupos() {
  return (
    <div className="flex flex-col gap-4">
      <h1 className="text-2xl font-bold text-white">Grupos</h1>
      {grupos.map(grupo => (
        <TarjetaGrupo key={grupo.id} grupo={grupo} />
      ))}
    </div>
  )
}

export default Grupos