import Carousel from '@components/ui/Carousel';
import ScrollText from '@components/ui/ScrollText';
import '@css/home.css';

function Home() {
  return (
    <>
      <Carousel />
      <div className='presentation'>
        &nbsp;&nbsp;&nbsp;&nbsp;IrSay Bank is a pioneering bank that offers financial solutions with a customer-focused approach and the highest level of technology.
        Known for their reliability and user-friendly service approach, IrSay Bank always prioritizes customer satisfaction and stands out 
        with its innovative solutions. With their sensitivity in financial transactions and services performed with passion, 
        IrSay Bank provides reliability and quality to its customers, ensuring they look to the future with confidence.
      </div>
      <ScrollText />
    </>
  );
};

export default Home;