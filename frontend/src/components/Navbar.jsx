import { Link, useLocation } from 'react-router-dom'

function Navbar() {
  const location = useLocation()

  const links = [
    { to: '/feed', label: 'Feed' },
    { to: '/parches', label: 'Parches' },
    { to: '/grupos', label: 'Grupos' },
  ]

  return (
    <nav className="bg-gray-900 border-b border-gray-800 px-6 py-3 flex items-center justify-between sticky top-0 z-10">
      <div className="flex items-center gap-8">
        <Link to="/" className="font-bold text-xl tracking-tight">
          <span className="text-white">Uni</span><span className="text-purple-400">Parche</span>
        </Link>
        <div className="flex gap-6">
          {links.map(link => (
            <Link
              key={link.to}
              to={link.to}
              className={`text-sm font-medium transition-colors ${
                location.pathname === link.to
                  ? 'text-purple-400'
                  : 'text-gray-400 hover:text-white'
              }`}
            >
              {link.label}
            </Link>
          ))}
        </div>
      </div>
      <div className="flex items-center gap-3">
        <Link
          to="/login"
          className="text-sm px-4 py-1.5 rounded-lg border border-purple-600 text-purple-400 hover:bg-purple-600 hover:text-white transition-colors"
        >
          Iniciar sesión
        </Link>
        <div className="w-8 h-8 rounded-full bg-purple-600 flex items-center justify-center text-white text-sm font-bold">
          F
        </div>
      </div>
    </nav>
  )
}

export default Navbar