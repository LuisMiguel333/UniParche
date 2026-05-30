import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'

const universidades = [
  { id: 1, nombre: 'Universidad de Antioquia' },
  { id: 2, nombre: 'Universidad Nacional' },
  { id: 3, nombre: 'EAFIT' },
  { id: 4, nombre: 'Universidad Pontificia Bolivariana' },
]

const erroresVacios = {
  userName: '',
  email: '',
  password: '',
  careerName: '',
  semester: '',
  universityId: '',
}

function Registro() {
  const [formulario, setFormulario] = useState({
    userName: '',
    email: '',
    password: '',
    careerName: '',
    semester: '',
    universityId: '',
  })
  const [errores, setErrores] = useState(erroresVacios)
  const [cargando, setCargando] = useState(false)
  const [errorGeneral, setErrorGeneral] = useState('')
  const navigate = useNavigate()

  const handleChange = (e) => {
    setFormulario({ ...formulario, [e.target.name]: e.target.value })
    setErrores({ ...errores, [e.target.name]: '' })
    setErrorGeneral('')
  }

  const validar = () => {
    const nuevosErrores = { ...erroresVacios }
    let valido = true

    if (!formulario.userName.trim()) {
      nuevosErrores.userName = 'El nombre de usuario es obligatorio'
      valido = false
    }
    if (!formulario.email.trim() || !formulario.email.includes('@')) {
      nuevosErrores.email = 'Ingresa un correo válido'
      valido = false
    }
    if (!formulario.password || formulario.password.length < 6) {
      nuevosErrores.password = 'La contraseña debe tener al menos 6 caracteres'
      valido = false
    }
    if (!formulario.careerName.trim()) {
      nuevosErrores.careerName = 'La carrera es obligatoria'
      valido = false
    }
    if (!formulario.semester || parseInt(formulario.semester) < 1) {
      nuevosErrores.semester = 'Ingresa un semestre válido'
      valido = false
    }
    if (!formulario.universityId) {
      nuevosErrores.universityId = 'Selecciona una universidad'
      valido = false
    }

    setErrores(nuevosErrores)
    return valido
  }

  const handleRegistro = async () => {
    if (!validar()) return
    setCargando(true)

    try {
      const response = await fetch('http://localhost:5292/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          UserName: formulario.userName.trim(),
          Email: formulario.email.trim(),
          Password: formulario.password,
          CareerName: formulario.careerName.trim(),
          Semester: parseInt(formulario.semester),
          UniversityId: parseInt(formulario.universityId),
        }),
      })

      const data = await response.json()

      if (data.success) {
        navigate('/login')
      } else {
        setErrorGeneral(data.message || 'Error al crear la cuenta')
      }
    } catch (error) {
      setErrorGeneral('Error al conectar con el servidor')
    } finally {
      setCargando(false)
    }
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-950 via-gray-900 to-purple-950 flex items-center justify-center px-4 py-8">
      <div className="w-full max-w-sm flex flex-col gap-6">
        <div className="text-center flex flex-col gap-2">
          <Link to="/" className="text-3xl font-bold">
            <span className="text-white">Uni</span><span className="text-purple-400">Parche</span>
          </Link>
          <p className="text-gray-500 text-sm">Crea tu cuenta universitaria</p>
        </div>

        <div className="bg-gray-900 border border-gray-800 rounded-xl p-6 flex flex-col gap-4">
          <p className="text-white font-semibold">Crear cuenta</p>

          {[
            { label: 'Nombre de usuario', name: 'userName', placeholder: 'Ej: carlos_dev', type: 'text' },
            { label: 'Correo institucional', name: 'email', placeholder: 'correo@udea.edu.co', type: 'email' },
            { label: 'Contraseña', name: 'password', placeholder: 'Mínimo 6 caracteres', type: 'password' },
            { label: 'Carrera', name: 'careerName', placeholder: 'Ej: Ingeniería de Sistemas', type: 'text' },
            { label: 'Semestre', name: 'semester', placeholder: 'Ej: 5', type: 'number' },
          ].map(campo => (
            <div key={campo.name} className="flex flex-col gap-1">
              <label className="text-gray-400 text-xs">{campo.label}</label>
              <input
                name={campo.name}
                value={formulario[campo.name]}
                onChange={handleChange}
                placeholder={campo.placeholder}
                type={campo.type}
                className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
                  errores[campo.name] ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
                }`}
              />
              {errores[campo.name] && <p className="text-red-400 text-xs">{errores[campo.name]}</p>}
            </div>
          ))}

          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Universidad</label>
            <select
              name="universityId"
              value={formulario.universityId}
              onChange={handleChange}
              className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
                errores.universityId ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
              }`}
            >
              <option value="">Selecciona tu universidad</option>
              {universidades.map(u => (
                <option key={u.id} value={u.id}>{u.nombre}</option>
              ))}
            </select>
            {errores.universityId && <p className="text-red-400 text-xs">{errores.universityId}</p>}
          </div>

          {errorGeneral && (
            <p className="text-red-400 text-xs text-center">{errorGeneral}</p>
          )}

          <button
            onClick={handleRegistro}
            disabled={cargando}
            className={`w-full text-sm py-2 rounded-lg text-white font-medium transition-colors ${
              cargando ? 'bg-gray-700 cursor-not-allowed' : 'bg-purple-600 hover:bg-purple-700'
            }`}
          >
            {cargando ? 'Creando cuenta...' : 'Crear cuenta'}
          </button>

          <p className="text-center text-gray-500 text-xs">
            ¿Ya tienes cuenta?{' '}
            <Link to="/login" className="text-purple-400 hover:underline">
              Inicia sesión
            </Link>
          </p>
        </div>
      </div>
    </div>
  )
}

export default Registro