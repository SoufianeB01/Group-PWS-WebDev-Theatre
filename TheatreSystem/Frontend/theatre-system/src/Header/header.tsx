import React from "react";

type HeaderProps = {
  setView: (newView: "Home" | "OverviewShows" | "OverviewVenues" | "Contact" | "Poll") => void;
};

export const Header: React.FC<HeaderProps> = ({ setView }) => {
  return (
    <div>
      <button onClick={() => setView("Home")}>Home</button>
      <button onClick={() => setView("OverviewShows")}>Overview Shows</button>
      <button onClick={() => setView("OverviewVenues")}>Overview Venues</button>
      <button onClick={() => setView("Contact")}>Contact</button>
      <button onClick={() => setView("Poll")}>Poll</button>
    </div>
  );
};