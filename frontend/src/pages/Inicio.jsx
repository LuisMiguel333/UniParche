import { Link } from 'react-router-dom'

function Inicio() {
  const crearUsuariosPrueba = async () => {
    const usuarios = [
      { UserName: 'Felipe Garces', Email: 'felipegarces@correo.com', Password: 'abcd1234', CareerName: 'Programación Web', Semester: 5, UniversityId: 1 },
      { UserName: 'Moises Gonzalez', Email: 'moisesgonzalez@correo.com', Password: 'abcd1234', CareerName: 'Programación Web', Semester: 5, UniversityId: 1 },
      { UserName: 'Miguel Cardona', Email: 'miguelcardona@correo.com', Password: 'abcd1234', CareerName: 'Programación Web', Semester: 5, UniversityId: 1 },
      { UserName: 'Usuario Prueba', Email: 'usuarioprueba@correo.com', Password: 'abcd1234', CareerName: 'Arquitectura', Semester: 3, UniversityId: 4 },
    ]

    let creados = 0
    for (const usuario of usuarios) {
      try {
        const response = await fetch('http://localhost:5292/api/users', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(usuario),
        })
        const data = await response.json()
        if (data.success) creados++
      } catch (e) {
        console.log('Error creando usuario:', usuario.UserName)
      }
    }
    alert(`✅ ${creados} usuarios creados exitosamente`)
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-950 via-gray-900 to-purple-950 flex flex-col items-center justify-center px-4 gap-10">
      <div className="text-center flex flex-col gap-4">
        <h1 className="text-5xl font-bold">
          <span className="text-white">Uni</span><span className="text-purple-400">Parche</span>
        </h1>
        <p className="text-gray-400 text-lg max-w-md">
          La red social exclusiva para estudiantes universitarios colombianos.
          Conecta, organiza parches y estudia en grupo.
        </p>
      </div>

      <div className="flex gap-4">
        <Link
          to="/feed"
          className="px-6 py-3 rounded-xl bg-purple-600 hover:bg-purple-700 text-white font-medium transition-colors"
        >
          Explorar
        </Link>
        <Link
          to="/login"
          className="px-6 py-3 rounded-xl border border-gray-700 hover:border-purple-500 text-gray-300 hover:text-white font-medium transition-colors"
        >
          Iniciar sesión
        </Link>
      </div>

      <div className="grid grid-cols-3 gap-4 max-w-lg w-full">
        <div className="bg-gray-900 bg-opacity-60 border border-gray-800 rounded-xl p-4 text-center flex flex-col gap-2">
          <span className="text-2xl">📰</span>
          <p className="text-white text-sm font-medium">Feed social</p>
          <p className="text-gray-500 text-xs">Publicaciones de tu universidad</p>
        </div>
        <div className="bg-gray-900 bg-opacity-60 border border-gray-800 rounded-xl p-4 text-center flex flex-col gap-2">
          <span className="text-2xl">🎉</span>
          <p className="text-white text-sm font-medium">Parches</p>
          <p className="text-gray-500 text-xs">Eventos y salidas universitarias</p>
        </div>
        <div className="bg-gray-900 bg-opacity-60 border border-gray-800 rounded-xl p-4 text-center flex flex-col gap-2">
          <span className="text-2xl">👥</span>
          <p className="text-white text-sm font-medium">Grupos</p>
          <p className="text-gray-500 text-xs">Estudia con otros estudiantes</p>
        </div>
      </div>

      <button
        onClick={crearUsuariosPrueba}
        className="text-gray-600 text-xs border border-gray-800 px-3 py-1.5 rounded-lg hover:border-gray-600 hover:text-gray-400 transition-colors"
      >
        Crear usuarios de prueba
      </button>
    </div>
  )
}

export default Inicio