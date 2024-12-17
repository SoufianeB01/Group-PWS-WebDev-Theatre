import React from "react";

type HeaderProps = {
  setView: (newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll") => void;
};

export const Header: React.FC<HeaderProps> = ({ setView }) => {
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
    </div>
  );
};