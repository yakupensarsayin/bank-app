import Login from "./pages/Login/Login"
import Register from "./pages/Register/Register";
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register.html" element={<Register />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
