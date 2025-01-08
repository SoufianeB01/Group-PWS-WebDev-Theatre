import React from "react";
import { HomeState, initHomeState } from "./home.state";
import { Header } from "../Header/header";
import { Login } from "../Login/login";
import Reservation from "../Reservation/Reservation";
import ReservationMain from "../Reservation/ReservationMain";

export class Home extends React.Component<{}, HomeState> {
  constructor(props: {}) {
    super(props);
    this.state = initHomeState;
  }

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

  handleLogout = () => {
    this.setState({
      isLoggedIn: false,
      username: null,
    });
  };

  renderContent(): JSX.Element {
    switch (this.state.view) {
      case "Home":
        return <div>This is the Home page</div>;
      case "OverviewShows":
        return <div>This is the Overview Shows page</div>;
      case "OverviewVenues":
        return <div>This is the Overview Venues page</div>;
      case "Contact":
        return <ReservationMain />;
      case "Poll":
        return <div>This is the Poll page</div>;
      case "Login":
        return <Login />;
      default:
        return <div>Something went wrong</div>;
    }
  }
}