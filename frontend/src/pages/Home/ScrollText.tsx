import { useState, useEffect } from 'react';
import ab from '../../assets/aligulbag.jpeg';
import mfa from '../../assets/mfa.jpeg';
import '../../css/scrolltext.css';

function ScrollText() {
  const [word, setWord] = useState(0);
  const words = ["reliably.", "user friendly.", "with love.", "punctually."];

  const [pic, setPic] = useState(0);
  const pics = [ab, mfa];

  useEffect(() => {
    const interval = setInterval(() => {
      setWord((prevWord) => (prevWord + 1) % words.length);
    }, 2000);
    return () => clearInterval(interval);
  }, [words.length]);

  useEffect(() => {
    const interval = setInterval(() => {
      setPic((prevPic) => (prevPic + 1) % pics.length);
    }, 3000);
    return () => clearInterval(interval);
  }, [pics.length]);

  return (
    <>
        <div className='container'>
            <div className="scroll-text">
                We do our job<br/><div className="scroll-text-highlight">{words[word]}</div>
            </div>

            <div className="scroll-carousel">
                {pics.map((p, j) => (
                    <div key={j} className={`scroll-carousel-slide ${j === pic ? 'active' : ''}`}>
                        <img src={p} alt={`Slide ${j + 1}`} />
                    </div>
                ))}
            </div>
        </div>
    </>
  );
};

export default ScrollText;