CREATE TABLE Admin (
    AdminID INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);
CREATE TABLE Reservation (
    ReservationID INT PRIMARY KEY AUTO_INCREMENT,
    CustomerID INT,
    TheaterShowDate DATETIME NOT NULL,
    AmountOfTickets INT NOT NULL,
    Used BOOLEAN NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
CREATE TABLE TheaterShow (
    TheaterShowID INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    Description TEXT,
    Price FLOAT NOT NULL,
    VenueID INT,
    FOREIGN KEY (VenueID) REFERENCES Venue(VenueID)
);
CREATE TABLE TheaterShowDate (
    TheaterShowDateID INT PRIMARY KEY AUTO_INCREMENT,
    Date DATE NOT NULL,
    Time TIME NOT NULL,
    TheaterShowID INT,
    FOREIGN KEY (TheaterShowID) REFERENCES TheaterShow(TheaterShowID)
);
CREATE TABLE Venue (
    VenueID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Capacity INT NOT NULL
);
