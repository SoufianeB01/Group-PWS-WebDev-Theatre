import React, { useState } from 'react';
import InputField from './inputfield';

interface ReservationProps {
  selectedSeats: Seat[];
  firstName: string;
  lastName: string;
  email: string;
  setEmail: (email: string) => void;
  setLastName: (lastName: string) => void;
  setFirstName: (firstName: string) => void;
  setSelectedSeats: (seats: Seat[]) => void;
  setShoppingCard?: (value: boolean) => void;
}

interface Seat {
  row: number;
  col: number;
}

const Reservation: React.FC<ReservationProps> = ({
  selectedSeats,
  firstName,
  lastName,
  email,
  setEmail,
  setLastName,
  setFirstName,
  setSelectedSeats,
  setShoppingCard,
}) => {
  const [seatError, setSeatError] = useState('');
  const [firstNameError, setFirstNameError] = useState('');
  const [lastNameError, setLastNameError] = useState('');
  const [emailError, setEmailError] = useState('');

  const handleSeatClick = (row: number, col: number) => {
    if (selectedSeats.some(seat => seat.row === row && seat.col === col)) {
      setSelectedSeats(selectedSeats.filter(seat => !(seat.row === row && seat.col === col)));
    } else {
      setSelectedSeats([...selectedSeats, { row, col }]);
    }
  };

  const handleSubmit = () => {
    setSeatError(selectedSeats.length === 0 ? 'Please select one or more seats' : '');
    setFirstNameError(firstName.trim().length === 0 ? 'Please fill in First Name' : '');
    setLastNameError(lastName.trim().length === 0 ? 'Please fill in Last Name' : '');
    setEmailError(email.trim().length === 0 ? 'Please fill in Email' : '');

    if (selectedSeats.length > 0 && firstName.trim() && lastName.trim() && email.trim()) {
      setShoppingCard && setShoppingCard(true);
    }
  };

  return (
    <div className="container mt-5">
      <h2 className="text-center mb-4 text-primary">Reservation</h2>

      {/* Film screen representation */}
      <div className="alert alert-info text-center my-3">
        <strong>SCREEN</strong>
      </div>

      {/* Seat selection table */}
      <div className="card shadow-lg mb-4">
        <div className="card-header bg-secondary text-white text-center">
          <h5>Select Your Seats</h5>
        </div>
        <div className="card-body">
          <table className="table table-bordered text-center">
            <tbody>
              {Array.from({ length: 11 }, (_, row) => (
                <tr key={row}>
                  {Array.from({ length: 11 }, (_, col) => (
                    <td
                      key={`${row}-${col}`}
                      className={`square-seat p-2 border-2 rounded-2 ${
                        selectedSeats.some(seat => seat.row === row && seat.col === col)
                          ? 'bg-secondary text-white border-primary'
                          : 'bg-light'
                      }`}
                      onClick={() => handleSeatClick(row, col)}
                      style={{ cursor: 'pointer', width: 30, height: 30 }}
                      aria-label={`Seat at row ${row + 1}, column ${col + 1}`}
                    ></td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
          {seatError && <div className="text-danger text-center mt-2">{seatError}</div>}
        </div>
      </div>

      {/* Input fields for personal information */}
      <div className="card shadow-lg mb-4">
        <div className="card-header bg-primary text-white">
          <h5 className="mb-0">Personal Information</h5>
        </div>
        <div className="card-body">
          <InputField label="First Name" initialValue={firstName} setValue={setFirstName} errorMessage={firstNameError} />
          <InputField label="Last Name" initialValue={lastName} setValue={setLastName} errorMessage={lastNameError} />
          <InputField label="Email" initialValue={email} setValue={setEmail} errorMessage={emailError} />
        </div>
      </div>

      {/* Submit button */}
      <div className="text-center mt-4">
        <button onClick={handleSubmit} className="btn btn-success btn-lg px-5">
          Submit
        </button>
      </div>
    </div>
  );
};

export default Reservation;
