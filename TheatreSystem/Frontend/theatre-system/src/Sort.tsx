// filterShows.ts
export interface Show {
    TheaterShowID: number;
    Title: string;
    Description: string;
    Price: number;  
    Date: string;  
  }
  
  export type SortCriteria = "A-Z" | "Z-A" | "Price Lowest" | "Price Highest" | "Date Ascending" | "Date Descending";
  
  export interface FilterParams {
    id?: number;
    title?: string;
    description?: string;
    venueId?: number;
    startDate?: string;
    endDate?: string;
    sortBy?: SortCriteria;
    ascending?: boolean;
  }

  export const sortAndFilterShows = (shows: Show[], criteria: SortCriteria): Show[] => {
    return [...shows].sort((a, b) => {
      switch (criteria) {
        case "A-Z":
          return a.Title.localeCompare(b.Title);
        case "Z-A":
          return b.Title.localeCompare(a.Title);
        case "Price Lowest":
          return a.Price - b.Price;
        case "Price Highest":
          return b.Price - a.Price;
        case "Date Ascending":
          return new Date(a.Date).getTime() - new Date(b.Date).getTime();
        case "Date Descending":
          return new Date(b.Date).getTime() - new Date(a.Date).getTime();
        default:
          return 0;
      }
    });
  };
  

  export const fetchFilteredShows = async (filters: FilterParams): Promise<Show[]> => {
    const url = new URL("/filter", window.location.origin);

    Object.entries(filters).forEach(([key, value]) => {
      if (value !== undefined && value !== "") {
        url.searchParams.append(key, value.toString());
      }
    });
  
    try {
      const response = await fetch(url.toString(), { method: "GET" });
      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }
      return await response.json();
    } catch (error) {
      console.error("Failed to fetch shows:", error);
      throw error;
    }
  };
  