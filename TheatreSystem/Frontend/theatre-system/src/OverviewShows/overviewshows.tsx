import React, { ChangeEvent, useEffect, useState } from "react";
import { HomeState, initHomeState } from "../Home/home.state";
import { Header } from "../Header/header";
import { fetchAllShows, TheaterShow, TheaterShowDate } from "../Admin/EditshowState";
import { fetchFilteredShows, sortAndFilterShows, Show, SortCriteria } from "../Sort";
import ReservationMain from "../Reservation/ReservationMain";
import Reservation from "../Reservation/Reservation";
import { Seat,ReservationProps } from "./overviewshows.state";
import{ getFilteredShowDates} from"./overviewshows.state"
import { __String } from "typescript";
const [showDates, setShowDates] = useState<TheaterShowDate[]>([]);
    
const [filter, setFilter] = useState({
      id: 0,
      date: '',
      time: '',
      theaterShowId: 0,
      sortBy: 'date',
      ascending: true,
    });
  
    const fetchShowDates = async () => {
      try {
        const data = await getFilteredShowDates(
          filter.id,
          filter.date,
          filter.time,
          filter.theaterShowId,
          filter.sortBy,
          filter.ascending
        );
        setShowDates(data);
      } catch (error) {
        console.error('Error fetching show dates:', error);
      }
    };

const loadTheaterShows = async () => {
  try {
    const theaterShows = await fetchAllShows(); 
    setShows(theaterShows); 
  } catch (error) {
    console.error('Error fetching shows:', error);
  }
};
const [showReservation, setShowReservation] = useState(false);
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
  
  useEffect(() => {
    fetchShowDates();
  }, [filters]);
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
  prop = <Reservation theathreShowDateId={0} movieId={0} selectedSeats={[]} firstName={""} lastName={""} email={""} setEmail={function (email: string): void {
    throw new Error("Function not implemented.");
  } } setLastName={function (lastName: string): void {
    throw new Error("Function not implemented.");
  } } setFirstName={function (firstName: string): void {
    throw new Error("Function not implemented.");
  } } setSelectedSeats={function (seats: Seat[]): void {
    throw new Error("Function not implemented.");
  } }>
  </Reservation>
  
  showing=fetchAllShows(); 
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
                <li key = {this.prop.key}>
                {showReservation ? (
                <ReservationMain movieId={show.TheaterShowID} theathreShowDateId={456} />
            ) : (
                <button onClick={() => this.rendersecondcontent(show) }>
                    make reservation
                </button>
            )}
                </li>
              </li>
            ))}
          </ul>
        ) : (
          <p>No shows available.</p>
        )}
      </div>
      </div>
    )}

rendersecondcontent(showw:Show): JSX.Element {
  return (
    <div>
      <h1>Filterable Theater Show Dates</h1>
      <div>
        <label>
          ID:
          <input
            type="number"
            value={filter.id}
            onChange={(e) => setFilter({ ...filter, id :Number(e.target.value )})}
          />
        </label>
        <label>
          Date:
          <input
            type="date"
            value={filter.date}
            onChange={(e) => setFilter({ ...filter, date: e.target.value })}
          />
        </label>
        <label>
          Time:
          <input
            type="time"
            value={filter.time}
            onChange={(e) => setFilter({ ...filter, time: e.target.value })}
          />
        </label>
        <label>
          Theater Show ID:
          <input
            type="number"
            value={filter.theaterShowId || ''}
            onChange={(e) => setFilter({ ...filter, theaterShowId:Number (e.target.value) })}
          />
        </label>
        <label>
          Sort By:
          <select
            value={filter.sortBy}
            onChange={(e) => setFilter({ ...filter, sortBy: e.target.value })}
          >
            <option value="date">Date</option>
            <option value="time">Time</option>
            <option value="theaterShowId">Theater Show ID</option>
          </select>
        </label>
        <label>
          Ascending:
          <input
            type="checkbox"
            checked={filter.ascending}
            onChange={(e) => setFilter({ ...filter, ascending: e.target.checked })}
          />
        </label>
        <button onClick={fetchShowDates}>Fetch Show Dates</button>
      </div>
      <ul>
        {showDates.map((show) => (
          <li key={show.TheaterShowDateID}>
            {show.Date} - {show.Time} (Show ID: {show.TheathershowID})
            <div>
            {showReservation ? (
                <ReservationMain movieId={showw.TheaterShowID} theathreShowDateId={show.TheaterShowDateID} />
            ) : (
                <button onClick={() => setShowReservation(true)}>
                    Open Reservation
                </button>
            )}
        </div>
          </li>
        ))}
      </ul>
    </div>
  );}}
