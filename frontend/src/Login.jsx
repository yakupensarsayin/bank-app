import React, { useState } from "react";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const response = await fetch('NessesaryLink', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          username,
          password
        })
      });
      const data = await response.json();
      if (response.ok) {
        console.log('Login successful:', data);
      } else {
        console.error('Login failed:', data);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <div className="log-container">
      <div className="log-box">
        <span>User Name: </span>
        <input
          className="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <br />
        <span>Password: </span>
        <input
          className="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <br />
        <button className="log-button" onClick={handleLogin}>
          Login
        </button>
      </div>
    </div>
  );
}

export default Login;