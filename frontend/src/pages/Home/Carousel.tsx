import { useEffect, useState } from 'react';
import cumberland from '../../assets/cumberland.jpg';
import lights from '../../assets/lights.jpg';
import slider from '../../assets/slider.png';
import volume from '../../assets/volume.jpg';
import wool from '../../assets/wool.jpg';
import '../../css/carousel.css';

function Carousel() {
  const [slideIndex, setSlideIndex] = useState(0);
  const slides = [cumberland, lights, slider, volume, wool];

  useEffect(() => {
    const interval = setInterval(() => {
      setSlideIndex((prevIndex) => (prevIndex + 1) % slides.length);
    }, 3000);
    return () => clearInterval(interval);
  }, [slides.length]);

  const nextSlide = () => {
    setSlideIndex((prevIndex) => (prevIndex + 1) % slides.length);
  };

  const prevSlide = () => {
    setSlideIndex((prevIndex) => (prevIndex - 1 + slides.length) % slides.length);
  };

  const setCurrentSlide = (index: number) => {
    setSlideIndex(index);
  };

  return (
    <div className="carousel">
      <button className="carousel-button prev" onClick={prevSlide}>❮</button>
      <button className="carousel-button next" onClick={nextSlide}>❯</button>
      {slides.map((slide, index) => (
        <div key={index} className={`carousel-slide ${index === slideIndex ? 'active' : ''}`}>
          <img src={slide} alt={`Slide ${index + 1}`} />
        </div>
      ))}
      <div className="carousel-indicators">
        {slides.map((_, index) => (
          <span
            key={index}
            className={`indicator ${index === slideIndex ? 'active' : ''}`}
            onClick={() => setCurrentSlide(index)}
          ></span>
        ))}
      </div>
    </div>
  );
}

export default Carousel;
