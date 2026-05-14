import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Navbar from './components/Navbar'
import Feed from './pages/Feed'
import Parches from './pages/Parches'
import Grupos from './pages/Grupos'

function App() {
  return (
    <BrowserRouter>
      <div className="min-h-screen bg-gray-950">
        <Navbar />
        <main className="max-w-4xl mx-auto p-6">
          <Routes>
            <Route path="/" element={<Feed />} />
            <Route path="/parches" element={<Parches />} />
            <Route path="/grupos" element={<Grupos />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  )
}

export default App