import React, { useEffect, useState } from 'react';
import Reservation from './Reservation';
import ShoppingCard from '../ShoppingCart/ShoppingCart';
import { ReservationState, initReservationState } from './Reservation.state';

interface Seat {
    row: number;
    col: number;
}

const ReservationMain: React.FC = () => {
    const [shoppingCard, setShoppingCard] = useState(false);

    const [state, setState] = useState<ReservationState>(initReservationState); // op hoger niveau

    // Update function to modify the state using the functional updates
    const updateState = (newState: Partial<ReservationState>) => {
        setState((prevState) => ({
            ...prevState,
            ...newState,
        }));
    };

    // Handle method calls for updating individual properties
    const handleSetSelectedSeats = (selectedSeats: Seat[]) => {
        updateState({ selectedSeats });
    };

    const handleSetFirstName = (firstName: string) => {
        updateState({ firstName });
    };

    const handleSetLastName = (lastName: string) => {
        updateState({ lastName });
    };

    const handleSetEmail = (email: string) => {
        updateState({ email });
    };

    return (
        shoppingCard
            ? <ShoppingCard
                selectedSeats={state.selectedSeats || []}
                firstName={state.firstName || ''}
                lastName={state.lastName || ''}
                email={state.email || ''}
                setShoppingCard={setShoppingCard}
            />
            : <Reservation
                selectedSeats={state.selectedSeats || []}
                setSelectedSeats={handleSetSelectedSeats}
                firstName={state.firstName || ''}
                setFirstName={handleSetFirstName}
                lastName={state.lastName || ''}
                setLastName={handleSetLastName}
                email={state.email || ''}
                setEmail={handleSetEmail}
                setShoppingCard={setShoppingCard}
            />
    );
};

export default ReservationMain;
