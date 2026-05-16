import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Navbar from './components/Navbar'
import Inicio from './pages/Inicio'
import Feed from './pages/Feed'
import Parches from './pages/Parches'
import Grupos from './pages/Grupos'
import Login from './pages/Login'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Inicio />} />
        <Route
          path="/*"
          element={
            <div className="min-h-screen bg-gray-950">
              <Navbar />
              <main className="max-w-4xl mx-auto p-6">
                <Routes>
                  <Route path="/feed" element={<Feed />} />
                  <Route path="/parches" element={<Parches />} />
                  <Route path="/grupos" element={<Grupos />} />
                  <Route path="/login" element={<Login />} />
                </Routes>
              </main>
            </div>
          }
        />
      </Routes>
    </BrowserRouter>
  )
}

export default App