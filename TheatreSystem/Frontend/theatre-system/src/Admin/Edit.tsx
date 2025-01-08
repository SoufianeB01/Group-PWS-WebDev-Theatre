import React, { ChangeEventHandler, useEffect, useState } from "react";
import { propTypes } from "react-bootstrap/esm/Image";
import {fetchAllShows, TheaterShow,TheaterShowFormProps,ShowEntry,Show,Delete} from "./EditshowState";


const TheaterShowForm: React.FC<TheaterShowFormProps> = ({
      initialData,
      onSubmit,
      }) => {
      const [formData, setFormData] = useState<TheaterShow>(
      initialData || {
        TheaterShowID: 0,
        Title: "",
        Description: "",
        Price: 0,
        VenueID: 0,
      }
      );
  
const handleChange = (
      e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
      ) => {
      const { name, value } = e.target;
      setFormData({
        ...formData,
        [name]:
          name === "Price" || name === "TheaterShowID" || name === "VenueID"
            ? +value
            : value,
      });
    };
  
const handleSubmit = (e: React.FormEvent) => {
      e.preventDefault();
      onSubmit(formData);
      };
  
    return (
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="TheaterShowID">Theater Show ID:</label>
          <input
            type="number"
            id="TheaterShowID"
            name="TheaterShowID"
            value={formData.TheaterShowID}
            onChange={handleChange}
            required
          />
        </div>
  
        <div>
          <label htmlFor="Title">Title:</label>
          <input
            type="text"
            id="Title"
            name="Title"
            value={formData.Title}
            onChange={handleChange}
            required
          />
        </div>
  
        <div>
          <label htmlFor="Description">Description:</label>
          <textarea
            id="Description"
            name="Description"
            value={formData.Description}
            onChange={handleChange}
            required
          />
        </div>
  
        <div>
          <label htmlFor="Price">Price:</label>
          <input
            type="number"
            id="Price"
            name="Price"
            value={formData.Price}
            onChange={handleChange}
            step="0.01"
            required
          />
        </div>
  
        <div>
          <label htmlFor="VenueID">Venue ID:</label>
          <input
            type="number"
            id="VenueID"
            name="VenueID"
            value={formData.VenueID}
            onChange={handleChange}
            required
          />
        </div>
  
        <button type="submit">Submit</button>
      </form>
    );
  };
  
  
  const Overview: React.FC = () => {
    const [shows, setShows] = useState<TheaterShow[]>([]);
    const [selectedShow, setSelectedShow] = useState<TheaterShow | null>(null);
    const [reservations, setReservations] = useState<any[]>([]);
    const handleEdit = (show: TheaterShow) => {
      setSelectedShow(show);
    };
    const loadTheaterShows = async () => {
      try {
        const theaterShows = await fetchAllShows(); 
        setShows(theaterShows); 
      } catch (error) {
        console.error('Error fetching shows:', error);
      }
    };
  
    const handleFormSubmit = (updatedShow: TheaterShow) => {
      setShows((prevShows) =>
        prevShows.map((show) =>
          show.TheaterShowID === updatedShow.TheaterShowID ? updatedShow : show
        )
      );
      setSelectedShow(null);
    };
    useEffect(() => {
      loadTheaterShows();
    }, []); 
    return (
      <div>
        <h1>Theater Shows</h1>
        {shows.length > 0 ? (
          <ul>
            {shows.map((show) => (
              <li key={show.TheaterShowID}>
                <h2>{show.Title}</h2>
                <p>{show.Description}</p>
                <p>Price: ${show.Price}</p>
                <p>Venue ID: {show.VenueID}</p>
                <button onClick={() => handleEdit(show)}>Edit</button>
                <button onClick={() => Delete(show.TheaterShowID)}>Delete</button>
              </li>
            ))}
          </ul>
        ) : (
          <p>No shows available.</p>
        )}
  
        {selectedShow && (
          <TheaterShowForm
            initialData={selectedShow}
            onSubmit={handleFormSubmit}
          />
        )}
        <h1>Reservations</h1>
        {reservations.length > 0 ? (
          <ul>
            {reservations.map((reservation, index) => (
              <li key={index}>
                <pre>{JSON.stringify(reservation, null, 2)}</pre>
              </li>
            ))}
          </ul>
        ) : (
          <>
            <p>No reservations available.</p>
            
          </>
        )}
      </div>
    );
  };
  export default Overview;
  