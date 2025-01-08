import React from "react";
import { HomeState, initHomeState } from "./home.state";
import { Header } from "../Header/header";
import Reservation from "../Reservation/Reservation";
import ReservationMain from "../Reservation/ReservationMain";
import ShoppingCard from "../ShoppingCart/ShoppingCart";

export class Home extends React.Component<{}, HomeState> {
  constructor(props: {}) {
    super(props);
    this.state = initHomeState;
  }

  render(): JSX.Element {
    return (
      <div>
        <Header setView={this.setView} />
        {this.renderContent()}
      </div>
    );
  }

  setView = (newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll" | "Shoppingcard") => {
    this.setState(this.state.updateView(newView));
  }

  renderContent(): JSX.Element {
    switch (this.state.view) {
      case "Home":
        return <div>This is the Home page</div>;
      case "OverviewShows":
        return <div>This is the Overview Shows page</div>;
      case "OverviewVenues":
        return <div>This is the Overview Venues page</div>;
      case "Contact":
        return <ReservationMain movieId={2} theathreShowDateId={2} />;
      case "Poll":
        return <div>This is the Poll page</div>;
      case "Shoppingcard":
        return <ShoppingCard
          setShoppingCard={() => { }}
          clearselectedSeats={() => { }}

        />
      default:
        return <div>Something went wrong</div>;
    }
  }
}
