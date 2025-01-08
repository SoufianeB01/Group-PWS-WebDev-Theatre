import React from "react";
import { LoginState, initLoginState } from "./login.state";
import Dashboard from "../AdminPages/Dashboard/dashboard";

export class Login extends React.Component<{}, LoginState> {
  constructor(props: {}) {
    super(props);
    this.state = initLoginState;
  }

  handleLogin = async () => {
    const { username, password } = this.state;

    try {
      const response = await fetch("https://localhost:5000/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        throw new Error("Failed to login");
      }

      if (!response.ok) {
        throw new Error('Failed to login');
      }

        alert("Login successful!");
        this.setState(this.state.updateView("home"));
    } catch (error) {
      console.error("Error:", error);
      alert("An error occurred during login. Please try again later.");
    }
  };

  render(): JSX.Element {
    if (this.state.view === "home") {
      return <Dashboard />;
    }

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
