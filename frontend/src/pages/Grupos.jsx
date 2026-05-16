import { useState } from 'react'

const gruposMock = [
  {
    id: 1,
    nombre: 'Cálculo III - ITM',
    materia: 'Cálculo',
    universidad: 'ITM',
    miembros: 12,
    creador: 'Valentina Ríos',
    unido: false,
    rol: null,
  },
  {
    id: 2,
    nombre: 'Anatomía Primer Semestre',
    materia: 'Anatomía',
    universidad: 'UdeA',
    miembros: 8,
    creador: 'Sebastián Mora',
    unido: false,
    rol: null,
  },
  {
    id: 3,
    nombre: 'Finanzas Corporativas EAFIT',
    materia: 'Finanzas',
    universidad: 'EAFIT',
    miembros: 5,
    creador: 'Daniela Castro',
    unido: false,
    rol: null,
  },
]

function TarjetaGrupo({ grupo, onUnirse }) {
  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3 hover:border-gray-700 transition-colors">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-white font-semibold">{grupo.nombre}</p>
          <p className="text-gray-500 text-xs mt-1">{grupo.universidad} · {grupo.materia}</p>
        </div>
        <div className="flex flex-col items-end gap-1">
          <span className="text-xs text-gray-400 bg-gray-800 px-2 py-1 rounded-full">
            👥 {grupo.miembros} miembros
          </span>
          {grupo.unido && (
            <span className="text-xs px-2 py-1 rounded-full bg-purple-900 text-purple-300">
              {grupo.rol === 'Administrador' ? '👑 Administrador' : '🎓 Miembro'}
            </span>
          )}
        </div>
      </div>
      <p className="text-gray-500 text-xs">Creado por {grupo.creador}</p>
      <button
        onClick={() => onUnirse(grupo.id)}
        className={`self-start text-sm px-4 py-2 rounded-lg transition-colors ${
          grupo.unido
            ? 'bg-gray-700 text-gray-300 hover:bg-red-900 hover:text-red-400'
            : 'bg-purple-600 hover:bg-purple-700 text-white'
        }`}
      >
        {grupo.unido ? 'Salir del grupo' : 'Unirme al grupo'}
      </button>
    </div>
  )
}

function Grupos() {
  const [listaGrupos, setListaGrupos] = useState(gruposMock)

  const toggleGrupo = (id) => {
    setListaGrupos(listaGrupos.map(g =>
      g.id === id
        ? {
            ...g,
            unido: !g.unido,
            miembros: g.unido ? g.miembros - 1 : g.miembros + 1,
            rol: g.unido ? null : 'Miembro',
          }
        : g
    ))
  }

  return (
    <div className="flex flex-col gap-4">
      <h1 className="text-2xl font-bold text-white">Grupos</h1>
      {listaGrupos.map(grupo => (
        <TarjetaGrupo
          key={grupo.id}
          grupo={grupo}
          onUnirse={toggleGrupo}
        />
      ))}
    </div>
  )
}

export default Grupos