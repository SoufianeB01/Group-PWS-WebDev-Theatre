export type ViewState = "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll" | "Login";

export type HomeState = {
  view: ViewState;
  isLoggedIn: boolean;
  username: string | null;
  updateView: (newView: ViewState) => (currentState: HomeState) => HomeState;
};

export const initHomeState: HomeState = {
  view: "Home",
  isLoggedIn: false,
  username: null,

  updateView: (newView: ViewState) => (currentState: HomeState) => ({
    ...currentState,
    view: newView,
  }),
};
