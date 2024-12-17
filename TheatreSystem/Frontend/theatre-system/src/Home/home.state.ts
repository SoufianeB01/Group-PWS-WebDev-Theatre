export type ViewState = "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll";

export type HomeState = {
  view: ViewState;
  updateView: (newView: ViewState) => (currentState: HomeState) => HomeState;
};

export const initHomeState: HomeState = {
  view: "Home",

  updateView: (newView: ViewState) => (currentState: HomeState) => ({
    ...currentState,
    view: newView,
  }),
};
