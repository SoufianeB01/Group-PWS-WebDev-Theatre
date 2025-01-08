import React, { ChangeEvent, useEffect, useState } from "react";
import { HomeState, initHomeState } from "../Home/home.state";
import { Header } from "../Header/header";
import { fetchAllShows, TheaterShow } from "../Admin/EditshowState";
import { fetchFilteredShows, sortAndFilterShows, Show, SortCriteria } from "../Sort";


const loadTheaterShows = async () => {
  try {
    const theaterShows = await fetchAllShows(); 
    setShows(theaterShows); 
  } catch (error) {
    console.error('Error fetching shows:', error);
  }
};

useEffect(() => {
  loadTheaterShows();
}, []); 
const [shows, setShows] = useState<Show[]>([]);
  const [filters, setFilters] = useState({
    title: "",
    description: "",
    sortBy: "A-Z" as SortCriteria,
    ascending: true,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Handle input change for filters
  const handleInputChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFilters((prevFilters) => ({
      ...prevFilters,
      [name]: name === "ascending" ? value === "true" : value,
    }));
  };

  // Fetch and update the shows list
  const handleFetchFilteredShows = async () => {
    setLoading(true);
    setError(null);

    try {
      const fetchedShows = await fetchFilteredShows(filters);
      const sortedShows = sortAndFilterShows(fetchedShows, filters.sortBy);
      setShows(sortedShows);
    } catch (err: any) {
      setError(err.message || "Failed to fetch shows.");
    } finally {
      setLoading(false);
    }
  };

export class OverviewShows extends React.Component<{}, HomeState> {
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
    return (
<div>
      <h1>Filter and Sort Theater Shows</h1>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleFetchFilteredShows();
        }}
      >
        <div>
          <label>
            Title:
            <input
              type="text"
              name="title"
              value={filters.title || ""}
              onChange={handleInputChange}
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
              onChange={handleInputChange}
            />
          </label>
        </div>
        <div>
          <label>
            Sort By:
            <select
              name="sortBy"
              value={filters.sortBy}
              onChange={handleInputChange}
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
              onChange={handleInputChange}
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


      <div>
        
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
      </div>
    )}
}
