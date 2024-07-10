import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navbar from "./pages/Navbar/Navbar";
import Home from "./pages/Home/Home";
import Login from "./pages/Login/Login"
import Register from "./pages/Register/Register";
import './css/app.css';

function App() {

  return (
    <BrowserRouter>
        <Navbar />

        <Routes>
          <Route path="/" Component={Home} />
          <Route path="/login" Component={Login} />
          <Route path="/register" Component={Register} />
        </Routes>
    </BrowserRouter>
  )
}

export default App
