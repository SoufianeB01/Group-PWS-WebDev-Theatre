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
  
  interface Show {
    id: number;
    title: string;
    description: string;
    venueId: number;
    startDate: string; // ISO date string
    endDate: string;   // ISO date string
  }

  export const getFilteredShows = async (params: FilterParams): Promise<Show[]> => {
    const url = new URL("/filter", window.location.origin);
  
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== "") {
        url.searchParams.append(key, value.toString());
      }
    });
  
    const response = await fetch(url.toString(), {
      method: "GET",
    });
  
    if (!response.ok) {
      throw new Error(`Error: ${response.statusText}`);
    }
  
    return response.json();
  };

  