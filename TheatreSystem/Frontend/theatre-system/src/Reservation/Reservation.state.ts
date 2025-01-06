export type ViewState = "Reservation" | "home";

interface Seat {
  row: number;
  col: number;
}

export interface ReservationState {
  view?: ViewState;
  selectedSeats?: Seat[];
  firstName?: string;
  lastName?: string;
  email?: string;
  updateView?: (view: ViewState) => (state: ReservationState) => ReservationState;
}

export const initReservationState: ReservationState = {
  view: "Reservation",
  selectedSeats: [],
  firstName: "",
  lastName: "",
  email: "",
  updateView: (view: ViewState) => (state: ReservationState): ReservationState => ({
    ...state,
    view: view,
  }),

};
