import { useState, useEffect } from 'react'
import { obtenerParches, crearParche, unirseAParche } from '../services/parches'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import { registerLocale } from 'react-datepicker'
import { es } from 'date-fns/locale'

registerLocale('es', es)

const formularioVacio = {
  titulo: '',
  lugar: '',
  fecha: null,
  cupos: '',
}

const erroresVacios = {
  titulo: '',
  lugar: '',
  fecha: '',
  cupos: '',
}

function TarjetaParche({ parche, onUnirse }) {
  const [inscrito, setInscrito] = useState(false)
  const lleno = parche.attendeeCount >= parche.capacity

  const handleClick = async () => {
    if (inscrito) {
      setInscrito(false)
    } else {
      await onUnirse(parche.id)
      setInscrito(true)
    }
  }

  const fechaFormateada = typeof parche.eventDate === 'string'
    ? parche.eventDate
    : new Date(parche.eventDate).toLocaleDateString('es-CO', {
        weekday: 'long', day: 'numeric', month: 'long',
        hour: '2-digit', minute: '2-digit',
      })

  return (
    <div className="bg-gray-900 border border-gray-800 rounded-xl p-5 flex flex-col gap-3 hover:border-gray-700 transition-colors">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-white font-semibold">{parche.title}</p>
          <p className="text-gray-500 text-xs mt-1">Organiza {parche.creatorName || 'UniParche'}</p>
        </div>
        <span className={`text-xs px-2 py-1 rounded-full ${lleno ? 'bg-red-900 text-red-400' : 'bg-green-900 text-green-400'}`}>
          {lleno ? 'Lleno' : 'Disponible'}
        </span>
      </div>
      <div className="flex flex-col gap-1">
        <p className="text-gray-400 text-sm">📍 {parche.location}</p>
        <p className="text-gray-400 text-sm">📅 {fechaFormateada}</p>
        <p className="text-gray-400 text-sm">👥 {parche.attendeeCount} / {parche.capacity} cupos</p>
      </div>
      <button
        disabled={lleno && !inscrito}
        onClick={handleClick}
        className={`self-start text-sm px-4 py-2 rounded-lg transition-colors ${
          inscrito
            ? 'bg-gray-700 text-gray-300 hover:bg-red-900 hover:text-red-400'
            : lleno
            ? 'bg-gray-800 text-gray-600 cursor-not-allowed'
            : 'bg-purple-600 hover:bg-purple-700 text-white'
        }`}
      >
        {inscrito ? 'Salir del parche' : lleno ? 'Sin cupos' : 'Unirme al parche'}
      </button>
    </div>
  )
}

function CampoFormulario({ label, name, value, onChange, error, type = 'text', placeholder }) {
  return (
    <div className="flex flex-col gap-1">
      <label className="text-gray-400 text-xs">{label}</label>
      <input
        name={name}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        type={type}
        className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
          error ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
        }`}
      />
      {error && <p className="text-red-400 text-xs">{error}</p>}
    </div>
  )
}

function Parches() {
  const [listaParches, setListaParches] = useState([])
  const [mostrarFormulario, setMostrarFormulario] = useState(false)
  const [formulario, setFormulario] = useState(formularioVacio)
  const [errores, setErrores] = useState(erroresVacios)
  const [cargando, setCargando] = useState(true)

  useEffect(() => {
    obtenerParches().then(data => {
      setListaParches(data)
      setCargando(false)
    })
  }, [])

  const handleChange = (e) => {
    setFormulario({ ...formulario, [e.target.name]: e.target.value })
    setErrores({ ...errores, [e.target.name]: '' })
  }

  const validar = () => {
    const nuevosErrores = { ...erroresVacios }
    let valido = true

    if (!formulario.titulo.trim()) {
      nuevosErrores.titulo = 'El título es obligatorio'
      valido = false
    } else if (formulario.titulo.trim().length < 5) {
      nuevosErrores.titulo = 'El título debe tener al menos 5 caracteres'
      valido = false
    }

    if (!formulario.lugar.trim()) {
      nuevosErrores.lugar = 'El lugar es obligatorio'
      valido = false
    }

    if (!formulario.fecha) {
      nuevosErrores.fecha = 'La fecha es obligatoria'
      valido = false
    }

    if (!formulario.cupos) {
      nuevosErrores.cupos = 'Los cupos son obligatorios'
      valido = false
    } else if (parseInt(formulario.cupos) < 2) {
      nuevosErrores.cupos = 'Debe haber mínimo 2 cupos'
      valido = false
    } else if (parseInt(formulario.cupos) > 100) {
      nuevosErrores.cupos = 'No puede superar 100 cupos'
      valido = false
    }

    setErrores(nuevosErrores)
    return valido
  }

  const handleCrear = async () => {
  if (!validar()) return

  const nuevoParche = {
    Title: formulario.titulo.trim(),
    Description: 'Sin descripción',
    Location: formulario.lugar.trim(),
    EventDate: formulario.fecha.toISOString(),
    Capacity: parseInt(formulario.cupos),
    ImageUrl: '',
    UniversityId: 1,
    CreatorId: 1,
  }

  try {
    const creado = await crearParche(nuevoParche)
    if (creado) {
      setListaParches([creado, ...listaParches])
    }
    setFormulario(formularioVacio)
    setErrores(erroresVacios)
    setMostrarFormulario(false)
  } catch (error) {
    console.error('Error creando parche:', error)
  }
}

  const handleUnirse = async (id) => {
  try {
    await unirseAParche(id)
    setListaParches(listaParches.map(p =>
      p.id === id && p.attendeeCount < p.capacity
        ? { ...p, attendeeCount: p.attendeeCount + 1 }
        : p
    ))
  } catch (error) {
    console.error('Error al unirse:', error)
  }
}

  if (cargando) return (
    <div className="flex items-center justify-center py-20">
      <p className="text-gray-500 text-sm">Cargando parches...</p>
    </div>
  )

  return (
    <div className="flex flex-col gap-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-white">Parches</h1>
        <button
          onClick={() => {
            setMostrarFormulario(!mostrarFormulario)
            setErrores(erroresVacios)
            setFormulario(formularioVacio)
          }}
          className="text-sm px-4 py-2 rounded-lg bg-purple-600 hover:bg-purple-700 text-white transition-colors"
        >
          {mostrarFormulario ? 'Cancelar' : '+ Crear parche'}
        </button>
      </div>

      {mostrarFormulario && (
        <div className="bg-gray-900 border border-purple-800 rounded-xl p-5 flex flex-col gap-4">
          <p className="text-white font-semibold">Nuevo parche</p>
          <CampoFormulario
            label="Título"
            name="titulo"
            value={formulario.titulo}
            onChange={handleChange}
            error={errores.titulo}
            placeholder="Ej: Torneo de FIFA en el ITM"
          />
          <CampoFormulario
            label="Lugar"
            name="lugar"
            value={formulario.lugar}
            onChange={handleChange}
            error={errores.lugar}
            placeholder="Ej: Bloque 1 - Sala de sistemas"
          />
          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Fecha y hora</label>
            <DatePicker
              selected={formulario.fecha}
              onChange={(date) => {
                setFormulario({ ...formulario, fecha: date })
                setErrores({ ...errores, fecha: '' })
              }}
              showTimeSelect
              timeFormat="HH:mm"
              timeIntervals={30}
              dateFormat="dd/MM/yyyy HH:mm"
              minDate={new Date()}
              locale="es"
              placeholderText="Selecciona fecha y hora"
              shouldCloseOnSelect={false}
              showPopperArrow={false}
              className="bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border border-gray-700 w-full"
            />
            {errores.fecha && <p className="text-red-400 text-xs">{errores.fecha}</p>}
          </div>
          <CampoFormulario
            label="Cupos disponibles"
            name="cupos"
            value={formulario.cupos}
            onChange={handleChange}
            error={errores.cupos}
            type="number"
            placeholder="Ej: 20"
          />
          <button
            onClick={handleCrear}
            className="self-start text-sm px-4 py-2 rounded-lg bg-purple-600 hover:bg-purple-700 text-white transition-colors"
          >
            Publicar parche
          </button>
        </div>
      )}

      {listaParches.map(parche => (
        <TarjetaParche
          key={parche.id}
          parche={parche}
          onUnirse={handleUnirse}
        />
      ))}
    </div>
  )
}

export default Parches