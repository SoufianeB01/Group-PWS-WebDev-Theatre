import React from "react";
import { LoginState, initLoginState } from "./login.state";

export class Login extends React.Component<{}, LoginState> {
  constructor(props: {}) {
    super(props);
    this.state = initLoginState;
  }

  render(): JSX.Element {
    switch (this.state.view) {
      case "login":
        return (
          <div>
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
            <button onClick={() => this.setState(this.state.updateView("home"))}>
              Login
            </button>
            <button onClick={() => this.setState(this.state.updateView("home"))}>
              Back to Home
            </button>
          </div>
        );

      case "home":
        return <div>Welcome back to Home Page</div>;

      default:
        return <div>Something went wrong!</div>;
    }
  }
}
