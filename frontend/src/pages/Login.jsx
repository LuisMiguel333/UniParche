function Login() {
  return (
    <div className="min-h-screen bg-gray-950 flex items-center justify-center px-4">
      <div className="w-full max-w-sm flex flex-col gap-6">
        <div className="text-center flex flex-col gap-4">
          <h1 className="text-3xl font-bold text-purple-400">UniParche</h1>
          <p className="text-gray-500 text-sm mt-2">Red social universitaria colombiana</p>
        </div>
        <div className="bg-gray-900 border border-gray-800 rounded-xl p-6 flex flex-col gap-4">
          <p className="text-white font-semibold">Iniciar sesión</p>
          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Correo institucional</label>
            <input
              placeholder="correo@itm.edu.co"
              className="bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border border-gray-700"
            />
          </div>
          <div className="flex flex-col gap-1">
            <label className="text-gray-400 text-xs">Contraseña</label>
            <input
              placeholder="Mínimo 6 caracteres"
              type="password"
              className="bg-gray-800 text-white text-sm rounded-lg px-4 py-2 outline-none border border-gray-700"
            />
          </div>
          <button className="w-full text-sm py-2 rounded-lg bg-purple-600 text-white font-medium opacity-50 cursor-not-allowed">
            Próximamente disponible
          </button>
          <p className="text-center text-gray-600 text-xs">
            La autenticación estará disponible cuando el backend esté listo.
          </p>
        </div>
      </div>
    </div>
  )
}

export default Login