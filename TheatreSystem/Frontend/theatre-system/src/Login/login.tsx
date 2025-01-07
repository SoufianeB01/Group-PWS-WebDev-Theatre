import React from "react";
import { LoginState, initLoginState } from "./login.state";

export class Login extends React.Component<{}, LoginState> {
  constructor(props: {}) {
    super(props);
    this.state = initLoginState;
  }

  handleLogin = () => {
    const { username, password } = this.state;

    fetch("https://localhost:5000/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password }),
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.success) {
          alert("Login successful!");
          window.location.href = "/dashboard";
        } else {
          alert("Invalid data, please try again.");
        }
      });
  };

  render(): JSX.Element {
    return (
      <div className="login-box">
        <h1>Admin Login</h1>
        <div>
          <input
            placeholder="Username"
            type="text"
            value={this.state.username}
            onChange={(e) =>
              this.setState(this.state.updateUsername(e.target.value))
            }
          />
        </div>
        <div>
          <input
            placeholder="Password"
            type="password"
            value={this.state.password}
            onChange={(e) =>
              this.setState(this.state.updatePassword(e.target.value))
            }
          />
        </div>
        <button onClick={this.handleLogin}>Login</button>
      </div>
    );
  }
}
