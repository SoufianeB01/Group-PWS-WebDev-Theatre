import { TheaterShowDate } from "../Admin/EditshowState";

export type OverviewShowsState = {
    view: string;
  };
  export const search = async (need:string) =>
    {
      const response = await fetch("api/[controller]/filter", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(need),
      });
  
    }

  export const initOverviewShowsState: OverviewShowsState = {
    view: "OverviewShows",
  };
  
  export interface Seat {
    row: number;
    col: number;
  }
  export interface ReservationProps {
    theathreShowDateId: number,
    movieId: number,
    selectedSeats: Seat[];
    firstName: string;
    lastName: string;
    email: string;
    setEmail: (email: string) => void;
    setLastName: (lastName: string) => void;
    setFirstName: (firstName: string) => void;
    setSelectedSeats: (seats: Seat[]) => void;
    setShoppingCard?: (value: boolean) => void;
  }
  export const getFilteredShowDates = async (
    id?: number,
    date?: string,
    time?: string,
    theaterShowId?: number,
    sortBy: string = 'date',
    ascending: boolean = true
  ): Promise<TheaterShowDate[]> => {
    const params = new URLSearchParams();
  
    if (id !== undefined) params.append('id', id.toString());
    if (date) params.append('date', date);
    if (time) params.append('time', time);
    if (theaterShowId !== undefined) params.append('theaterShowId', theaterShowId.toString());
    params.append('sortBy', sortBy);
    params.append('ascending', ascending.toString());
  
    const response = await fetch(`/filter?${params.toString()}`);
    if (!response.ok) {
      throw new Error(`Error fetching data: ${response.statusText}`);
    }
  
    const data = await response.json();
    return data;
  };