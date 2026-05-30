import { useState, useEffect } from 'react'
import { Link, useNavigate } from 'react-router-dom'

const erroresVacios = {
  email: '',
  password: '',
}

function Login() {
  const [formulario, setFormulario] = useState({ email: '', password: '' })
  const [errores, setErrores] = useState(erroresVacios)
  const [cargando, setCargando] = useState(false)
  const [errorGeneral, setErrorGeneral] = useState('')
  const [usuarios, setUsuarios] = useState([])
  const navigate = useNavigate()

  useEffect(() => {
    fetch('http://localhost:5292/api/users')
      .then(r => r.json())
      .then(data => setUsuarios(data.data || []))
      .catch(() => {})
  }, [])

  const handleChange = (e) => {
    setFormulario({ ...formulario, [e.target.name]: e.target.value })
    setErrores({ ...errores, [e.target.name]: '' })
    setErrorGeneral('')
  }

  const validar = () => {
    const nuevosErrores = { ...erroresVacios }
    let valido = true

    if (!formulario.email.trim()) {
      nuevosErrores.email = 'El correo es obligatorio'
      valido = false
    } else if (!formulario.email.includes('@')) {
      nuevosErrores.email = 'Ingresa un correo válido'
      valido = false
    }

    if (!formulario.password) {
      nuevosErrores.password = 'La contraseña es obligatoria'
      valido = false
    } else if (formulario.password.length < 6) {
      nuevosErrores.password = 'La contraseña debe tener al menos 6 caracteres'
      valido = false
    }

    setErrores(nuevosErrores)
    return valido
  }

  const handleLogin = async () => {
    if (!validar()) return
    setCargando(true)
    setErrorGeneral('')

    try {
      const response = await fetch('http://localhost:5292/api/users')
      const data = await response.json()
      const lista = data.data || []
      const usuario = lista.find(u => u.email === formulario.email)

      if (!usuario) {
        setErrorGeneral('No existe una cuenta con ese correo')
        setCargando(false)
        return
      }

      localStorage.setItem('usuario', JSON.stringify(usuario))
      navigate('/feed')
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
          <p className="text-gray-500 text-sm">Red social universitaria colombiana</p>
        </div>

        <div className="bg-gray-900 border border-gray-800 rounded-xl p-6 flex flex-col gap-4">
          <p className="text-white font-semibold">Iniciar sesión</p>

          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Correo institucional</label>
            <input
              name="email"
              value={formulario.email}
              onChange={handleChange}
              placeholder="correo@udea.edu.co"
              className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
                errores.email ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
              }`}
            />
            {errores.email && <p className="text-red-400 text-xs">{errores.email}</p>}
          </div>

          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Contraseña</label>
            <input
              name="password"
              value={formulario.password}
              onChange={handleChange}
              placeholder="Mínimo 6 caracteres"
              type="password"
              onKeyDown={(e) => e.key === 'Enter' && handleLogin()}
              className={`bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border transition-colors ${
                errores.password ? 'border-red-500' : 'border-gray-700 focus:border-purple-500'
              }`}
            />
            {errores.password && <p className="text-red-400 text-xs">{errores.password}</p>}
          </div>

          {errorGeneral && (
            <p className="text-red-400 text-xs text-center">{errorGeneral}</p>
          )}

          <button
            onClick={handleLogin}
            disabled={cargando}
            className={`w-full text-sm py-2 rounded-lg text-white font-medium transition-colors ${
              cargando ? 'bg-gray-700 cursor-not-allowed' : 'bg-purple-600 hover:bg-purple-700'
            }`}
          >
            {cargando ? 'Verificando...' : 'Entrar'}
          </button>

          <p className="text-center text-gray-500 text-xs">
            ¿No tienes cuenta?{' '}
            <Link to="/registro" className="text-purple-400 hover:underline">
              Regístrate
            </Link>
          </p>
        </div>

        {usuarios.length > 0 && (
          <div className="bg-gray-900 border border-gray-800 rounded-xl p-4 flex flex-col gap-3">
            <p className="text-gray-500 text-xs font-medium">Usuarios disponibles</p>
            <div className="flex flex-col gap-2 max-h-48 overflow-y-auto">
              {usuarios.map(u => (
                <button
                  key={u.id}
                  onClick={() => setFormulario({ email: u.email, password: 'abcd1234' })}
                  className="flex items-center justify-between bg-gray-800 hover:bg-gray-700 rounded-lg px-3 py-2 transition-colors text-left"
                >
                  <div>
                    <p className="text-white text-xs font-medium">{u.userName}</p>
                    <p className="text-gray-500 text-xs">{u.email}</p>
                  </div>
                  <span className="text-gray-600 text-xs">→</span>
                </button>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  )
}

export default Login