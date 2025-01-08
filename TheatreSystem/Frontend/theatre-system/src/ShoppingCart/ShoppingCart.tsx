import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

interface Seat {
    row: number;
    col: number;
}

interface Reservation {
    ReservationID: number;
    CustomerID: number;
    theatereShowDate: {
        TheaterShowDateID: number;
        date: string;
        time: string;
        TheaterShowID: number;
    };
    tickets: Seat[];
    amountOfTickets: number;
    used: boolean;  
}

interface ShoppingCartProps {
    setShoppingCard: (value: boolean) => void;
}

const ShoppingCard: React.FC<ShoppingCartProps> = ({ setShoppingCard }) => {
    const [data, setData] = useState<Reservation[]>([]);

    useEffect(() => {
        const fetchSeats = async () => {
            try {
                const response = await fetch('/reservation/ShoppingCart');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                setData(await response.json());
            } catch (error) {
                console.error('Error fetching reservations:', error);
            }
        };

        fetchSeats();
    }, []);

    const handleConfirm = async () => {
        // Add your confirmation logic here
    };

    return (
        <div className="container mt-5">
            <div className="card shadow-lg">
                <div className="card-header bg-primary text-white text-center">
                    <h2>Shopping Cart</h2>
                </div>
                <div className="card-body">
                    {data?.length > 0 ? (
                        data.map((order) => (
                            <div key={order.ReservationID} className="card mb-3 shadow-sm">
                                <div className="card-header bg-secondary text-white">
                                    <h5>
                                        Reservation ID: {order.ReservationID} - {order.amountOfTickets} Tickets
                                    </h5>
                                </div>
                                <div className="card-body">
                                    <p>
                                        <strong>Date:</strong> {order.theatereShowDate.date}{' '}
                                        <strong>Time:</strong> {order.theatereShowDate.time}
                                    </p>
                                    <h6>Seats:</h6>
                                    {order.tickets.length > 0 ? (
                                        <div className="d-flex flex-wrap">
                                            {order.tickets.map((seat, index) => (
                                                <span
                                                    key={index}
                                                    className="badge bg-primary me-2 mb-2"
                                                >
                                                    Row {seat.row}, Col {seat.col}
                                                </span>
                                            ))}
                                        </div>
                                    ) : (
                                        <p className="text-danger">No seats selected.</p>
                                    )}
                                    <p>
                                        <strong>Used:</strong>{' '}
                                        {order.used ? (
                                            <span className="text-success">Yes</span>
                                        ) : (
                                            <span className="text-danger">No</span>
                                        )}
                                    </p>
                                </div>
                            </div>
                        ))
                    ) : (
                        <p className="text-center text-danger">No reservations in the shopping cart.</p>
                    )}
                </div>
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
