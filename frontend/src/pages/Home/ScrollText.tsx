import { useState, useEffect } from 'react';
import ab from '../../assets/aligulbag.jpeg';
import mfa from '../../assets/mfa.jpeg';
import '../../css/scrolltext.css';

function ScrollText() {
  const [index, setIndex] = useState(0);
  const words = ["reliably", "user friendly", "with love", "punctually"];

  const [slideIndex, setSlideIndex] = useState(0);
  const slides = [ab, mfa];

  useEffect(() => {
    const interval = setInterval(() => {
      setIndex((prevIndex) => (prevIndex + 1) % words.length);
    }, 2000);
    return () => clearInterval(interval);
  }, [words.length]);

  useEffect(() => {
    const interval = setInterval(() => {
      setSlideIndex((prevIndex) => (prevIndex + 1) % slides.length);
    }, 3000);
    return () => clearInterval(interval);
  }, [slides.length]);

  return (
    <>
        <div className='container'>
            <div className="scroll-text">
                We do our job<br/><span className="scroll-text-highlight">{words[index]}</span>.
            </div>

            <div className="scroll-carousel">
                {slides.map((slide, index) => (
                    <div key={index} className={`scroll-carousel-slide ${index === slideIndex ? 'active' : ''}`}>
                        <img src={slide} alt={`Slide ${index + 1}`} />
                    </div>
                ))}
            </div>
        </div>
    </>
  );
};

export default ScrollText;