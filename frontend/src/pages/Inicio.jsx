import { Link } from 'react-router-dom'

function Inicio() {
  return (
    <div className="min-h-screen bg-gray-950 flex flex-col items-center justify-center px-4 gap-10">
      <div className="text-center flex flex-col gap-4">
        <h1 className="text-5xl font-bold text-white">
          Uni<span className="text-purple-400">Parche</span>
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
        <div className="bg-gray-900 border border-gray-800 rounded-xl p-4 text-center flex flex-col gap-2">
          <span className="text-2xl">📰</span>
          <p className="text-white text-sm font-medium">Feed social</p>
          <p className="text-gray-500 text-xs">Publicaciones de tu universidad</p>
        </div>
        <div className="bg-gray-900 border border-gray-800 rounded-xl p-4 text-center flex flex-col gap-2">
          <span className="text-2xl">🎉</span>
          <p className="text-white text-sm font-medium">Parches</p>
          <p className="text-gray-500 text-xs">Eventos y salidas universitarias</p>
        </div>
        <div className="bg-gray-900 border border-gray-800 rounded-xl p-4 text-center flex flex-col gap-2">
          <span className="text-2xl">👥</span>
          <p className="text-white text-sm font-medium">Grupos</p>
          <p className="text-gray-500 text-xs">Estudia con otros estudiantes</p>
        </div>
      </div>
    </div>
  )
}

export default Inicio