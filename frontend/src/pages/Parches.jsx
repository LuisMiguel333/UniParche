import { useState } from 'react'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import { registerLocale } from 'react-datepicker'
import { es } from 'date-fns/locale'

registerLocale('es', es)

const parchesMock = [
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
        onClick={() => onUnirse(parche.id)}
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
  const [listaParches, setListaParches] = useState(parchesMock)
  const [mostrarFormulario, setMostrarFormulario] = useState(false)
  const [formulario, setFormulario] = useState(formularioVacio)
  const [errores, setErrores] = useState(erroresVacios)

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

  const crearParche = () => {
    if (!validar()) return

    const nuevoParche = {
      id: listaParches.length + 1,
      titulo: formulario.titulo.trim(),
      lugar: formulario.lugar.trim(),
      fecha: formulario.fecha.toLocaleDateString('es-CO', {
        weekday: 'long',
        day: 'numeric',
        month: 'long',
        hour: '2-digit',
        minute: '2-digit',
      }),
      cupos: parseInt(formulario.cupos),
      inscritos: 0,
      creador: 'Tú',
      universidad: 'ITM',
    }

    setListaParches([nuevoParche, ...listaParches])
    setFormulario(formularioVacio)
    setErrores(erroresVacios)
    setMostrarFormulario(false)
  }

  const unirseAlParche = (id) => {
    setListaParches(listaParches.map(p =>
      p.id === id && p.inscritos < p.cupos
        ? { ...p, inscritos: p.inscritos + 1 }
        : p
    ))
  }

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
            onClick={crearParche}
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
          onUnirse={unirseAlParche}
        />
      ))}
    </div>
  )
}

export default Parches