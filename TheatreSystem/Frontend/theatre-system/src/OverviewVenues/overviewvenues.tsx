import React from "react";
import { HomeState, initHomeState } from "../Home/home.state";
import { Header } from "../Header/header";

export class OverviewVenues extends React.Component<{}, HomeState> {
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

  setView = (newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll") => {
    this.setState(this.state.updateView(newView));
  }

  renderContent(): JSX.Element {
    return <div>This is the Overview Venues page</div>;
  }
}
