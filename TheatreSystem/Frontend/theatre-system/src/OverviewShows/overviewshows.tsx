import React, { ChangeEvent, useEffect, useState } from "react";
import { HomeState, initHomeState } from "../Home/home.state";
import { Header } from "../Header/header";
import { fetchAllShows, TheaterShow, TheaterShowDate } from "../Admin/EditshowState";
import { fetchFilteredShows, sortAndFilterShows, Show, SortCriteria } from "../Sort";
import ReservationMain from "../Reservation/ReservationMain";
import Reservation from "../Reservation/Reservation";
import { Seat, ReservationProps } from "./overviewshows.state";
import { getFilteredShowDates } from "./overviewshows.state";
import { initReservationState, ReservationState } from "../Reservation/Reservation.state";
import ShoppingCard from "../ShoppingCart/ShoppingCart";

export class OverviewShows extends React.Component<{}, HomeState> {
  constructor(props: {}) {
    super(props);
    this.state = {
      ...initHomeState,
      isLoggedIn: false,
      username: null,
    };
  }

  // React state declarations moved inside the class component as properties
  filter = {
    id: 0,
    date: '',
    time: '',
    theaterShowId: 0,
    sortBy: 'date',
    ascending: true,
  };

  stateHooks = {
    filters: {
      title: "",
      description: "",
      sortBy: "A-Z" as SortCriteria,
      ascending: true,
    },
    loading: false,
    error: null as string | null,
    shows: [] as Show[],
    showDates: [] as TheaterShowDate[],
    shoppingCard: false,
    showReservation: false,
    reservationState: initReservationState,
  };

  // Methods to manage state hooks
  setFilters = (filters: Partial<typeof this.stateHooks.filters>) => {
    this.stateHooks.filters = { ...this.stateHooks.filters, ...filters };
  };

  setLoading = (loading: boolean) => {
    this.stateHooks.loading = loading;
  };

  setError = (error: string | null) => {
    this.stateHooks.error = error;
  };

  setShows = (shows: Show[]) => {
    this.stateHooks.shows = shows;
  };

  setShowDates = (showDates: TheaterShowDate[]) => {
    this.stateHooks.showDates = showDates;
  };

  setShoppingCard = (shoppingCard: boolean) => {
    this.stateHooks.shoppingCard = shoppingCard;
  };

  setShowReservation = (showReservation: boolean) => {
    this.stateHooks.showReservation = showReservation;
  };

  // Function to update the reservation state
  updateReservationState = (newState: Partial<ReservationState>) => {
    this.stateHooks.reservationState = {
      ...this.stateHooks.reservationState,
      ...newState,
    };
  };

  handleSetSelectedSeats = (selectedSeats: Seat[]) => {
    this.updateReservationState({ selectedSeats });
  };

  handleSetFirstName = (firstName: string) => {
    this.updateReservationState({ firstName });
  };

  handleSetLastName = (lastName: string) => {
    this.updateReservationState({ lastName });
  };

  handleSetEmail = (email: string) => {
    this.updateReservationState({ email });
  };

  fetchShowDates = async () => {
    try {
      const data = await getFilteredShowDates(
        this.filter.id,
        this.filter.date,
        this.filter.time,
        this.filter.theaterShowId,
        this.filter.sortBy,
        this.filter.ascending
      );
      this.setShowDates(data);
    } catch (error) {
      console.error('Error fetching show dates:', error);
    }
  };

  loadTheaterShows = async () => {
    try {
      const theaterShows = await fetchAllShows();
      this.setShows(theaterShows);
    } catch (error) {
      console.error('Error fetching shows:', error);
    }
  };

  handleFetchFilteredShows = async () => {
    this.setLoading(true);
    this.setError(null);

    try {
      const fetchedShows = await fetchFilteredShows(this.stateHooks.filters);
      const sortedShows = sortAndFilterShows(fetchedShows, this.stateHooks.filters.sortBy);
      this.setShows(sortedShows);
    } catch (err: any) {
      this.setError(err.message || "Failed to fetch shows.");
    } finally {
      this.setLoading(false);
    }
  };

  componentDidMount() {
    this.loadTheaterShows();
    this.fetchShowDates();
  }

  handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    this.setFilters({
      ...this.stateHooks.filters,
      [name]: name === "ascending" ? value === "true" : value,
    });
  };

  handleLogout = () => {
    this.setState({ isLoggedIn: false, username: null });
  };

  renderContent(): JSX.Element {
    const {
      shows,
      loading,
      error,
      filters,
    } = this.stateHooks;

    return (
      <div>
        <h1>Filter and Sort Theater Shows</h1>
        <form
          onSubmit={(e) => {
            e.preventDefault();
            this.handleFetchFilteredShows();
          }}
        >
          <div>
            <label>
              Title:
              <input
                type="text"
                name="title"
                value={filters.title || ""}
                onChange={this.handleInputChange}
              />
            </label>
          </div>
          <div>
            <label>
              Description:
              <input
                type="text"
                name="description"
                value={filters.description || ""}
                onChange={this.handleInputChange}
              />
            </label>
          </div>
          <div>
            <label>
              Sort By:
              <select
                name="sortBy"
                value={filters.sortBy}
                onChange={this.handleInputChange}
              >
                <option value="A-Z">A-Z</option>
                <option value="Z-A">Z-A</option>
                <option value="Price Lowest">Price Lowest</option>
                <option value="Price Highest">Price Highest</option>
                <option value="Date Ascending">Date Ascending</option>
                <option value="Date Descending">Date Descending</option>
              </select>
            </label>
          </div>
          <div>
            <label>
              Order:
              <select
                name="ascending"
                value={filters.ascending ? "true" : "false"}
                onChange={this.handleInputChange}
              >
                <option value="true">Ascending</option>
                <option value="false">Descending</option>
              </select>
            </label>
          </div>
          <button type="submit">Apply Filters</button>
        </form>

        {loading && <p>Loading...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}

        <h1>Filtered Shows</h1>
        {shows.length > 0 ? (
          <ul>
            {shows.map((show) => (
              <li key={show.TheaterShowID}>
                <h2>{show.Title}</h2>
                <p>{show.Description}</p>
                <p>Price: ${show.Price}</p>
                <p>Date: {new Date(show.Date).toLocaleDateString()}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p>No shows available.</p>
        )}
      </div>
    );
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
}

export default OverviewShows;
