import React from "react";
import { HomeState, initHomeState } from "../Home/home.state";
import { Header } from "../Header/header";

export class Poll extends React.Component<{}, HomeState> {
  constructor(props: {}) {
    super(props);
    this.state = {
      ...initHomeState,
      isLoggedIn: false,
      username: null,
    };
  }

  handleLogout = () => {
    this.setState({ isLoggedIn: false, username: null });
  };

  render(): JSX.Element {
    return (
      <div>
        <Header 
          setView={this.setView} 
          isLoggedIn={this.state.isLoggedIn} 
          username={this.state.username} 
          onLogout={this.handleLogout} 
        />
        {this.renderContent()}
      </div>
    );
  }

  setView = (newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll" | "Login") => {
    this.setState(this.state.updateView(newView));
  };

  renderContent(): JSX.Element {
    return <div>This is the Poll page</div>;
  }
}