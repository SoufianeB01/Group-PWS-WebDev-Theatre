export interface LoginState {
  username: string;
  password: string;
}

export const initLoginState: LoginState = {
  username: '',
  password: '',
};
