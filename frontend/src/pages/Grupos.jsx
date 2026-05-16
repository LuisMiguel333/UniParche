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
  {
    id: 4,
    nombre: 'Programación Web - ITM',
    materia: 'Programación',
    universidad: 'ITM',
    miembros: 9,
    creador: 'Felipe Garces',
    unido: false,
    rol: null,
  },
  {
    id: 5,
    nombre: 'Estadística UdeA',
    materia: 'Estadística',
    universidad: 'UdeA',
    miembros: 6,
    creador: 'Sebastián Mora',
    unido: false,
    rol: null,
  },
]

const formularioVacio = {
  nombre: '',
  materia: '',
  universidad: '',
}

const erroresVacios = {
  nombre: '',
  materia: '',
  universidad: '',
}

const universidades = ['ITM', 'UdeA', 'EAFIT', 'UPB', 'CES', 'Unal', 'Otra']

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

function CampoFormulario({ label, name, value, onChange, error, placeholder }) {
  return (
    <div className="flex flex-col gap-1">
      <label className="text-gray-400 text-xs">{label}</label>
      <input
        name={name}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
          error ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
        }`}
      />
      {error && <p className="text-red-400 text-xs">{error}</p>}
    </div>
  )
}

function Grupos() {
  const [listaGrupos, setListaGrupos] = useState(gruposMock)
  const [mostrarFormulario, setMostrarFormulario] = useState(false)
  const [formulario, setFormulario] = useState(formularioVacio)
  const [errores, setErrores] = useState(erroresVacios)
  const [filtroUniversidad, setFiltroUniversidad] = useState('')
  const [filtroMateria, setFiltroMateria] = useState('')

  const gruposFiltrados = listaGrupos.filter(g => {
    const porUniversidad = filtroUniversidad ? g.universidad === filtroUniversidad : true
    const porMateria = filtroMateria
      ? g.materia.toLowerCase().includes(filtroMateria.toLowerCase())
      : true
    return porUniversidad && porMateria
  })

  const handleChange = (e) => {
    setFormulario({ ...formulario, [e.target.name]: e.target.value })
    setErrores({ ...errores, [e.target.name]: '' })
  }

  const validar = () => {
    const nuevosErrores = { ...erroresVacios }
    let valido = true

    if (!formulario.nombre.trim()) {
      nuevosErrores.nombre = 'El nombre es obligatorio'
      valido = false
    } else if (formulario.nombre.trim().length < 5) {
      nuevosErrores.nombre = 'El nombre debe tener al menos 5 caracteres'
      valido = false
    }

    if (!formulario.materia.trim()) {
      nuevosErrores.materia = 'La materia es obligatoria'
      valido = false
    }

    if (!formulario.universidad) {
      nuevosErrores.universidad = 'Selecciona una universidad'
      valido = false
    }

    setErrores(nuevosErrores)
    return valido
  }

  const crearGrupo = () => {
    if (!validar()) return

    const nuevoGrupo = {
      id: listaGrupos.length + 1,
      nombre: formulario.nombre.trim(),
      materia: formulario.materia.trim(),
      universidad: formulario.universidad,
      miembros: 1,
      creador: 'Felipe Garces',
      unido: true,
      rol: 'Administrador',
    }

    setListaGrupos([nuevoGrupo, ...listaGrupos])
    setFormulario(formularioVacio)
    setErrores(erroresVacios)
    setMostrarFormulario(false)
  }

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

  const universidadesUnicas = [...new Set(listaGrupos.map(g => g.universidad))]

  return (
    <div className="flex flex-col gap-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-white">Grupos</h1>
        <button
          onClick={() => {
            setMostrarFormulario(!mostrarFormulario)
            setErrores(erroresVacios)
            setFormulario(formularioVacio)
          }}
          className="text-sm px-4 py-2 rounded-lg bg-purple-600 hover:bg-purple-700 text-white transition-colors"
        >
          {mostrarFormulario ? 'Cancelar' : '+ Crear grupo'}
        </button>
      </div>

      <div className="flex gap-3">
        <select
          value={filtroUniversidad}
          onChange={(e) => setFiltroUniversidad(e.target.value)}
          className="bg-gray-800 text-gray-300 text-sm rounded-lg px-3 py-2 outline-none border border-gray-700 focus:border-purple-500"
        >
          <option value="">Todas las universidades</option>
          {universidadesUnicas.map(u => (
            <option key={u} value={u}>{u}</option>
          ))}
        </select>
        <input
          value={filtroMateria}
          onChange={(e) => setFiltroMateria(e.target.value)}
          placeholder="Buscar por materia..."
          className="flex-1 bg-gray-800 text-gray-300 text-sm rounded-lg px-3 py-2 outline-none border border-gray-700 focus:border-purple-500"
        />
        {(filtroUniversidad || filtroMateria) && (
          <button
            onClick={() => {
              setFiltroUniversidad('')
              setFiltroMateria('')
            }}
            className="text-sm px-3 py-2 rounded-lg bg-gray-700 hover:bg-gray-600 text-gray-300 transition-colors"
          >
            Limpiar
          </button>
        )}
      </div>

      {gruposFiltrados.length === 0 && (
        <div className="bg-gray-900 border border-gray-800 rounded-xl p-8 text-center">
          <p className="text-gray-500 text-sm">No hay grupos que coincidan con tu búsqueda.</p>
          <button
            onClick={() => {
              setFiltroUniversidad('')
              setFiltroMateria('')
            }}
            className="mt-3 text-purple-400 text-xs hover:underline"
          >
            Ver todos los grupos
          </button>
        </div>
      )}

      {mostrarFormulario && (
        <div className="bg-gray-900 border border-purple-800 rounded-xl p-5 flex flex-col gap-4">
          <p className="text-white font-semibold">Nuevo grupo de estudio</p>
          <CampoFormulario
            label="Nombre del grupo"
            name="nombre"
            value={formulario.nombre}
            onChange={handleChange}
            error={errores.nombre}
            placeholder="Ej: Cálculo III - Grupo Tarde"
          />
          <CampoFormulario
            label="Materia"
            name="materia"
            value={formulario.materia}
            onChange={handleChange}
            error={errores.materia}
            placeholder="Ej: Cálculo, Anatomía, Finanzas..."
          />
          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Universidad</label>
            <select
              name="universidad"
              value={formulario.universidad}
              onChange={handleChange}
              className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
                errores.universidad ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
              }`}
            >
              <option value="">Selecciona tu universidad</option>
              {universidades.map(u => (
                <option key={u} value={u}>{u}</option>
              ))}
            </select>
            {errores.universidad && <p className="text-red-400 text-xs">{errores.universidad}</p>}
          </div>
          <button
            onClick={crearGrupo}
            className="self-start text-sm px-4 py-2 rounded-lg bg-purple-600 hover:bg-purple-700 text-white transition-colors"
          >
            Crear grupo
          </button>
        </div>
      )}

      {gruposFiltrados.map(grupo => (
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