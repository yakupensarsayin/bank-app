import { useState } from "react";
import { NavLink } from "react-router-dom";
import "../../css/navbar.css";
import { CodeIcon, HamburgerMenuClose, HamburgerMenuOpen } from "./Icons";

function Navbar() {
  const [click, setClick] = useState(false);

  const handleClick = () => setClick(!click);
  return (
    <>
      <nav className="navbar">
        <div className="nav-container">
          <NavLink to="/" className="nav-logo">
            <span>IrSay Bank</span>
            <span className="icon">
              <CodeIcon />
            </span>
          </NavLink>

          <ul className={click ? "nav-menu active" : "nav-menu"}>
            <li className="nav-item">
              <NavLink
                to="/"
                className={location.pathname === "/" ? "nav-links active" : "nav-links"}
                onClick={handleClick}
              >
                Home
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink
                to="/accounts"
                className={location.pathname === "/" ? "nav-links active" : "nav-links"}
                onClick={handleClick}
              >
                Accounts
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink
                to="/login"
                className={location.pathname === "/" ? "nav-links active" : "nav-links"}
                onClick={handleClick}
              >
                Login
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink
                to="/register"
                className={location.pathname === "/" ? "nav-links active" : "nav-links"}
                onClick={handleClick}
              >
                Register
              </NavLink>
            </li>
          </ul>
          <div className="nav-icon" onClick={handleClick}>

            {click ? (
              <span className="icon">
                <HamburgerMenuClose />{" "}
              </span>
            ) : (
              <span className="icon">
                <HamburgerMenuOpen />
              </span>
            )}
          </div>
        </div>
      </nav>
    </>
  );
}

export default Navbar;