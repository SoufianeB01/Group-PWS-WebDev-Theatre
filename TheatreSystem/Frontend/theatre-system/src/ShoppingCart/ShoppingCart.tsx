import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

interface Seat {
  row: number;
  col: number;
}

interface ShoppingCardProps {
  selectedSeats: Seat[];
  firstName: string;
  lastName: string;
  email: string;
  setShoppingCard: (value: boolean) => void;
}

const ShoppingCard: React.FC<ShoppingCardProps> = ({
  selectedSeats,
  firstName,
  lastName,
  email,
  setShoppingCard,
}) => {
  return (
    <div className="container mt-5">
      <div className="card shadow-lg">
        <div className="card-header bg-primary text-white text-center">
          <h2>Order Summary</h2>
        </div>
        <div className="card-body">
          {/* Seat Details */}
          <div className="mb-4">
            <h5 className="card-title">Selected Seats</h5>
            {selectedSeats.length > 0 ? (
              <div>
                {selectedSeats.map((seat, index) => (
                  <span
                    key={index}
                    className="badge bg-secondary me-2 mb-2"
                  >
                    Row {seat.row + 1}, Col {seat.col + 1}
                  </span>
                ))}
              </div>
            ) : (
              <p className="text-danger">No seats selected.</p>
            )}
          </div>

          {/* Personal Information */}
          <div className="mb-4">
            <h5 className="card-title">Personal Information</h5>
            <p>
              <strong>First Name:</strong> {firstName || <span className="text-danger">Not provided</span>}
            </p>
            <p>
              <strong>Last Name:</strong> {lastName || <span className="text-danger">Not provided</span>}
            </p>
            <p>
              <strong>Email:</strong> {email || <span className="text-danger">Not provided</span>}
            </p>
          </div>
        </div>

        {/* Card Footer */}
        <div className="card-footer text-center">
          <button
            className="btn btn-outline-primary me-2"
            onClick={() => setShoppingCard(false)}
          >
            Edit
          </button>
          <button className="btn btn-success">Confirm</button>
        </div>
      </div>
    </div>
  );
};

export default ShoppingCard;
