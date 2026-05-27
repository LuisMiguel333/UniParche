import { useState, useEffect } from 'react'
import { obtenerGrupos, crearGrupo, unirseAGrupo } from '../services/grupos'

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

const universidades = [
  { id: 1, nombre: 'ITM' },
  { id: 2, nombre: 'UdeA' },
  { id: 3, nombre: 'EAFIT' },
  { id: 4, nombre: 'UPB' },
  { id: 5, nombre: 'Unal' },
]

function TarjetaGrupo({ grupo, onUnirse }) {
  const [unido, setUnido] = useState(false)
  const [rol, setRol] = useState(null)
  const [miembros, setMiembros] = useState(grupo.memberCount)

  const handleToggle = async () => {
    if (!unido) {
      await onUnirse(grupo.id)
      setUnido(true)
      setRol('Miembro')
      setMiembros(miembros + 1)
    } else {
      setUnido(false)
      setRol(null)
      setMiembros(miembros - 1)
    }
  }

  const universidadNombre = universidades.find(u => u.id === grupo.universityId)?.nombre || 'Universidad'

  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3 hover:border-gray-700 transition-colors">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-white font-semibold">{grupo.name}</p>
          <p className="text-gray-500 text-xs mt-1">{universidadNombre} · {grupo.subject}</p>
        </div>
        <div className="flex flex-col items-end gap-1">
          <span className="text-xs text-gray-400 bg-gray-800 px-2 py-1 rounded-full">
            👥 {miembros} miembros
          </span>
          {unido && (
            <span className="text-xs px-2 py-1 rounded-full bg-purple-900 text-purple-300">
              {rol === 'Administrador' ? '👑 Administrador' : '🎓 Miembro'}
            </span>
          )}
        </div>
      </div>
      <p className="text-gray-500 text-xs">Creado por {grupo.creatorName}</p>
      <button
        onClick={handleToggle}
        className={`self-start text-sm px-4 py-2 rounded-lg transition-colors ${
          unido
            ? 'bg-gray-700 text-gray-300 hover:bg-red-900 hover:text-red-400'
            : 'bg-purple-600 hover:bg-purple-700 text-white'
        }`}
      >
        {unido ? 'Salir del grupo' : 'Unirme al grupo'}
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
  const [listaGrupos, setListaGrupos] = useState([])
  const [mostrarFormulario, setMostrarFormulario] = useState(false)
  const [formulario, setFormulario] = useState(formularioVacio)
  const [errores, setErrores] = useState(erroresVacios)
  const [cargando, setCargando] = useState(true)
  const [filtroUniversidad, setFiltroUniversidad] = useState('')
  const [filtroMateria, setFiltroMateria] = useState('')

  useEffect(() => {
    obtenerGrupos().then(data => {
      setListaGrupos(data)
      setCargando(false)
    })
  }, [])

  const gruposFiltrados = listaGrupos.filter(g => {
    const porUniversidad = filtroUniversidad
      ? g.universityId === parseInt(filtroUniversidad)
      : true
    const porMateria = filtroMateria
      ? g.subject.toLowerCase().includes(filtroMateria.toLowerCase())
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

  const handleCrear = async () => {
    if (!validar()) return

    const nuevoGrupo = {
      name: formulario.nombre.trim(),
      subject: formulario.materia.trim(),
      universityId: parseInt(formulario.universidad),
      memberCount: 1,
      creatorName: 'Felipe Garces',
      description: '',
      type: 0,
    }

    const creado = await crearGrupo(nuevoGrupo)
    setListaGrupos([{ ...creado, unido: true, rol: 'Administrador' }, ...listaGrupos])
    setFormulario(formularioVacio)
    setErrores(erroresVacios)
    setMostrarFormulario(false)
  }

  const handleUnirse = async (id) => {
    await unirseAGrupo(id)
  }

  if (cargando) return (
    <div className="flex items-center justify-center py-20">
      <p className="text-gray-500 text-sm">Cargando grupos...</p>
    </div>
  )

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
          {universidades.map(u => (
            <option key={u.id} value={u.id}>{u.nombre}</option>
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
                <option key={u.id} value={u.id}>{u.nombre}</option>
              ))}
            </select>
            {errores.universidad && <p className="text-red-400 text-xs">{errores.universidad}</p>}
          </div>
          <button
            onClick={handleCrear}
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
          onUnirse={handleUnirse}
        />
      ))}
    </div>
  )
}

export default Grupos