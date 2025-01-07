import React, { useState } from 'react';

const Contact = () => {
  const [users, setUsers] = useState<any[]>([]);
  const [error, setError] = useState<string | null>(null);

  // Functie die de GET-aanroep doet naar de API
  const getAllUsers = () => {
    fetch("https://localhost:5000/api/auth/users", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Failed to fetch users");
        }
        return response.json();
      })
      .then((data) => {
        setUsers(data); // Zet de ontvangen data in de state
        setError(null); // Clear any previous errors
      })
      .catch((err) => {
        setError(err.message); // Stel de fout in als er iets misgaat
        setUsers([]); // Leeg de gebruikerslijst bij een fout
      });
  };

  return (
    <div>
      <h1>Contact Page</h1>
      {/* De knop om de API-aanroep uit te voeren */}
      <button onClick={getAllUsers}>Get All Users</button>

      {/* Foutmelding als er iets mis is */}
      {error && <p style={{ color: 'red' }}>Error: {error}</p>}

      {/* Lijst van gebruikers */}
      {users.length > 0 ? (
        <ul>
          {users.map((user, index) => (
            <li key={index}>{user.username}</li> // Dit zou moeten overeenkomen met de structuur van de ontvangen data
          ))}
        </ul>
      ) : (
        <p>No users found</p>
      )}
    </div>
  );
};

export default Contact;
