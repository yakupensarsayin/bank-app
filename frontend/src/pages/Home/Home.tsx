import Navbar from '../Navbar';
import Carousel from './Carousel';
import ScrollText from './ScrollText';
import Login from '../Login/Login';
import Register from '../Register/Register';
import { BrowserRouter as Router,  Route, Routes } from "react-router-dom";
import '../../css/home.css';

function Home() {
  return (
    <>
      <Router>
        <Navbar />

        <div className="pages">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/accounts" />
            <Route path="/" element={<Login />}/>
            <Route path="/register" element={<Register />}/>
          </Routes>
        </div>
      </Router>
      <Carousel />
      <div className='presentation'>
        IrSay Bank, müşteri odaklı yaklaşımı ve teknolojiyi en üst düzeyde kullanarak finansal çözümler sunan öncü bir bankadır. 
        Güvenilirlikleri ve kullanıcı dostu hizmet anlayışları ile tanınan IrSay Bank, her zaman müşteri memnuniyetini ön planda tutar ve 
        yenilikçi çözümleriyle fark yaratır. Finansal işlemlerdeki hassasiyetleri ve aşkla yapılan hizmetleri ile 
        IrSay Bank, müşterilerine güvenilirlik ve kalite sunar, geleceğe güvenle bakmalarını sağlar.
      </div>
      <ScrollText />
    </>
  );
};

export default Home;