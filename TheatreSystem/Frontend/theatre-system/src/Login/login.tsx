import React, { useState } from 'react';
import { login } from './login.api';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = () => {
    login(username, password)
      .then((data) => {
        if (data.success) {
          window.location.href = '/dashboard';
        } else {
          alert(data.message || 'Invalid credentials');
        }
      })
      .catch(() => {
        alert('An error occurred');
      });
  };

  return (
    <div>
      <h1>Login</h1>
      <input value={username} onChange={(e) => setUsername(e.target.value)} />
      <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default Login;
