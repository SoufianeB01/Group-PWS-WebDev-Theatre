
import { getFilteredShows } from "./Search"; 
import { Header } from "../Header/header";
import React, { ChangeEvent } from "react";
import { ViewState } from "../Home/home.state";
export type Reservation = {
  ReservationID: number;
  CustomerID: number;
  amountOfTickets: number;
  TheaterShowDate: TheaterShowDate;
  used: boolean;
  VenueID: number;
};

export type TheaterShowDate = {
  TheaterShowDateID: number;
  Date: string;
  Time: string;
  TheaterShowID: number;
};

interface HomeState {
  isLoggedIn: boolean;
  username: string | null;
  reservations: Reservation[];
  loading: boolean;
  error: string | null;
  updateView: (newView: ViewState) => (currentState: HomeState) => HomeState;
}

const initHomeState: HomeState = {
  isLoggedIn: false,
  username: null,
  reservations: [],
  loading: false,
  error: null,
  updateView: (newView: ViewState) => (currentState: HomeState) => initHomeState
};

export const fetchAllReservations = async (): Promise<Reservation[]> => {
  try {
    const response = await fetch("reservation"); // Replace with the full API endpoint if needed
    if (!response.ok) {
      throw new Error("Failed to fetch reservations.");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching reservations:", error);
    throw error;
  }
};

export class SearchReservation extends React.Component<{}, HomeState> {
  constructor(props: {}) {
    super(props);
    this.state = {
      ...initHomeState,
    };
  }

  componentDidMount() {
    this.loadReservations();
  }

  handleLogout = () => {
    this.setState({ isLoggedIn: false, username: null });
  };

  async loadReservations() {
    this.setState({ loading: true, error: null });
    try {
      const reservations = await fetchAllReservations();
      this.setState({ reservations });
    } catch (error: any) {
      this.setState({
        error: error.message || "Failed to load reservations.",
      });
    } finally {
      this.setState({ loading: false });
    }
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

  setView = (
    newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll" | "Login"
  ) => {
    this.setState(this.state.updateView(newView));
  };

  renderContent(): JSX.Element {
    const { reservations, loading, error } = this.state;

    return (
      <div>
        <h1>Customer Reservations</h1>
        {loading && <p>Loading reservations...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}
        {reservations.length > 0 ? (
          <ul>
            {reservations.map((reservation) => (
              <li key={reservation.ReservationID}>
                <h2>Reservation #{reservation.ReservationID}</h2>
                <p>Customer ID: {reservation.CustomerID}</p>
                <p>Amount of Tickets: {reservation.amountOfTickets}</p>
                <p>
                  Date: {reservation.TheaterShowDate.Date} - Time:{" "}
                  {reservation.TheaterShowDate.Time}
                </p>
                <p>Venue ID: {reservation.VenueID}</p>
                <p>Used: {reservation.used ? "Yes" : "No"}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p>No reservations available.</p>
        )}
      </div>
    );
  }
}
