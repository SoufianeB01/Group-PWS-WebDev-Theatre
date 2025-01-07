export type DashboardState = {
    isAdmin: boolean;
    userinfo: string | null;
  };
  
  export const initDashboardState: DashboardState = {
    isAdmin: true,
    userinfo: null,
  };
  