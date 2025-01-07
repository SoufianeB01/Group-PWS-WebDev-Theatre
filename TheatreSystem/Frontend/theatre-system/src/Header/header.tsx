import React from "react";

type HeaderProps = {
  setView: (newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll" | "Login") => void;
  isLoggedIn: boolean;
  username: string | null;
  onLogout: () => void;
};

export const Header: React.FC<HeaderProps> = ({ setView, isLoggedIn, username, onLogout }) => {
  return (
    <div className="header">
      <div className="header-logo">
        <p>PWS</p>
      </div>
      <div className="header-item" onClick={() => setView("Home")}>
        Home
      </div>
      <div className="header-item" onClick={() => setView("OverviewShows")}>
        Overview Shows
      </div>
      <div className="header-item" onClick={() => setView("OverviewVenues")}>
        Overview Venues
      </div>
      <div className="header-item" onClick={() => setView("Contact")}>
        Contact
      </div>
      <div className="header-item" onClick={() => setView("Poll")}>
        Poll
      </div>
      <div className="header-item">
        {isLoggedIn ? (
          <div>
            <p>{username}</p>
            <button onClick={onLogout}>Uitloggen</button>
          </div>
        ) : (
          <button onClick={() => setView("Login")}>Inloggen</button>
        )}
      </div>
    </div>
  );
};
