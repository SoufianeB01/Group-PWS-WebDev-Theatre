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
  