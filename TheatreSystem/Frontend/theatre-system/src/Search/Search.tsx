

import { getFilteredShows } from "./Search"; 
import { Header } from "../Header/header";
import React, { ChangeEvent } from "react";
import { ViewState } from "../Home/home.state";

interface Show {
  TheaterShowID: number;
  Title: string;
  Description: string;
}

interface FilterParams {
  id?: number;
  title?: string;
  description?: string;
  venueId?: number;
  startDate?: string;
  endDate?: string;
  sortBy?: string;
  ascending?: boolean;
}

interface HomeState {
  isLoggedIn: boolean;
  username: string | null;
  shows: Show[];
  filters: FilterParams;
  loading: boolean;
  error: string | null;
  updateView: (newView: ViewState) => (currentState: HomeState) => HomeState;
  
}

const initHomeState: HomeState = {
  isLoggedIn: false,
  username: null,
  shows: [],
  filters: {
    sortBy: "title",
    ascending: true,
  },
  loading: false,
  error: null,
  updateView: (newView: ViewState) => (currentState: HomeState) => initHomeState
};

export class search extends React.Component<{}, HomeState> {
  constructor(props: {}) {
    super(props);
    this.state = {
      ...initHomeState,
    };
  }
  

  handleLogout = () => {
    this.setState({ isLoggedIn: false, username: null });
  };

  handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    this.setState((prevState) => ({
      filters: {
        ...prevState.filters,
        [name]: value,
      },
    }));
  };

  fetchFilteredShows = async () => {
    this.setState({ loading: true, error: null });

    const { filters } = this.state;
    const url = new URL("/filter", window.location.origin);

    Object.entries(filters).forEach(([key, value]) => {
      if (value !== undefined && value !== "") {
        url.searchParams.append(key, value.toString());
      }
    });

    try {
      const response = await fetch(url.toString(), {
        method: "GET",
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const shows = await response.json();
      this.setState({ shows });
    } catch (err: any) {
      this.setState({ error: err.message || "Failed to fetch shows." });
    } finally {
      this.setState({ loading: false });
    }
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
     
  setView = (
    newView:
      | "Home"
      | "OverviewShows"
      | "OverviewVenues"
      | "Contact"
      | "Poll"
      | "Login"
  ) => {
    
    this.setState(this.state.updateView(newView));
  };

  renderContent(): JSX.Element {
    const { shows, filters, loading, error } = this.state;

    return (
      <div>
        <h1>Filter Theater Shows</h1>
        <form
          onSubmit={(e) => {
            e.preventDefault();
            this.fetchFilteredShows();
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
                value={filters.sortBy || ""}
                onChange={this.handleInputChange}
              >
                <option value="title">Title</option>
                <option value="startDate">Start Date</option>
                <option value="endDate">End Date</option>
              </select>
            </label>
          </div>
          <div>
            <label>
              Ascending:
              <select
                name="ascending"
                value={filters.ascending ? "true" : "false"}
                onChange={(e) =>
                  this.setState((prevState) => ({
                    filters: {
                      ...prevState.filters,
                      ascending: e.target.value === "true",
                    },
                  }))
                }
              >
                <option value="true">True</option>
                <option value="false">False</option>
              </select>
            </label>
          </div>
          <button type="submit">Apply Filters</button>
        </form>

        {loading && <p>Loading...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}

        <h1>Theater Shows</h1>
        {shows.length > 0 ? (
          <ul>
            {shows.map((show) => (
              <li key={show.TheaterShowID}>
                <h2>{show.Title}</h2>
                <p>{show.Description}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p>No shows available.</p>
        )}
      </div>
    );
  }
}


export default search;


