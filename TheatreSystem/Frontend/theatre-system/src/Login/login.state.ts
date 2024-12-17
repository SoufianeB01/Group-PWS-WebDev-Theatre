export type ViewState = "login" | "home";

export interface LoginState {
  view: ViewState;
  username: string;
  password: string;
  updateView: (view: ViewState) => (state: LoginState) => LoginState;
  updateUsername: (username: string) => (state: LoginState) => LoginState;
  updatePassword: (password: string) => (state: LoginState) => LoginState;
}

export const initLoginState: LoginState = {
  view: "login",
  username: "",
  password: "",
  updateView: (view: ViewState) => (state: LoginState): LoginState => ({
    ...state,
    view: view,
  }),
  updateUsername: (username: string) => (state: LoginState): LoginState => ({
    ...state,
    username: username,
  }),
  updatePassword: (password: string) => (state: LoginState): LoginState => ({
    ...state,
    password: password,
  }),
};
