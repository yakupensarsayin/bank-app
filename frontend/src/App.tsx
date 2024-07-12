import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navbar from "@components/ui/Navbar";
import Home from "@pages/Home";
import Login from "@pages/Login"
import Register from "@pages/Register";
import Account from '@pages/Account';
import '@css/app.css';

function App() {

  return (
    <BrowserRouter>
        <Navbar />

        <Routes>
          <Route path="/" Component={Home} />
          <Route path="/login" Component={Login} />
          <Route path="/register" Component={Register} />
          <Route path="/account" Component={Account} />
        </Routes>

    </BrowserRouter>
  )
}

export default App
