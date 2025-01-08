import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

interface Seat {
    row: number;
    col: number;
}

interface ShoppingCardProps {
    movieId: number,
    theathreShowDateId: number,
    selectedSeats: Seat[];
    firstName: string;
    lastName: string;
    email: string;
    setShoppingCard: (value: boolean) => void;
}

const ShoppingCard: React.FC<ShoppingCardProps> = ({
    movieId,
    theathreShowDateId,
    selectedSeats,
    firstName,
    lastName,
    email,
    setShoppingCard,
}) => {
    const handleConfirm = async () => {
        // Prepare the request payload
        const requestData = {
            Customer: {
                CustomerId: 0, // You should replace this with the actual customer ID
                FirstName: firstName,
                LastName: lastName,
                Email: email,
            },
            Reservation: {
                ReservationID: 0,
                CustomerID: 0,
                TheaterShowDate: {
                    TheaterShowDateID: theathreShowDateId,
                    Date: "2024-12-25", 
                    Time: "16:00:00", 
                    TheaterShowID: movieId,
                },
                tickets: selectedSeats.map((seat) => ({
                    row: seat.row,
                    col: seat.col,
                })),
                amountOfTickets: selectedSeats.length,
                used: false,
            },
        };

        try {
            // Make the POST request
            const response = await fetch('https://localhost:5000/reservation', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(requestData),
            });

            if (!response.ok) {
                throw new Error('Failed to submit the reservation');
            }

            // Handle successful submission
            alert('Reservation confirmed!');
            setShoppingCard(false); // Close the shopping card after successful submission

        } catch (error) {
            // Handle errors
            console.error('Error:', error);
            alert('An error occurred while confirming the reservation.');
        }
    };

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
                                        Row {seat.row}, Col {seat.col}
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
                    <button className="btn btn-success" onClick={handleConfirm}>
                        Confirm
                    </button>
                </div>
            </div>
        </div>
    );
};

export default ShoppingCard;
