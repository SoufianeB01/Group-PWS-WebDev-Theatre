import React, { useState } from 'react';
import Reservation from './Reservation';
import ShoppingCard from '../ShoppingCart/ShoppingCart';

interface Seat {
    row: number;
    col: number;
}

const ReservationMain: React.FC = () => {
    const [selectedSeats, setSelectedSeats] = useState<Seat[]>([]);
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');

    const [shoppingcard, setShoppingCard] = useState(false);

    return (
        shoppingcard
            ? <ShoppingCard
                selectedSeats={selectedSeats}
                firstName={firstName}
                lastName={lastName}
                email={email}
                setShoppingCard={setShoppingCard}
                 />
            : <Reservation
                selectedSeats={selectedSeats}
                setSelectedSeats={setSelectedSeats}
                firstName={firstName}
                setFirstName={setFirstName}
                lastName={lastName}
                setLastName={setLastName}
                email={email}
                setEmail={setEmail}
                setShoppingCard={setShoppingCard}
            />
    );
};

export default ReservationMain;
