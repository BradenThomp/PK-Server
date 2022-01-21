DROP DATABASE IF EXISTS `pk_rentals`;
CREATE DATABASE `pk_rentals`;
USE `pk_rentals`;

CREATE TABLE `customer` (
	`Id` char(36) primary key NOT NULL,
	`Name` varchar(50),
    `Phone` varchar(50),
    `Email` varchar(50)
);

CREATE TABLE `location` (
	`Id` char(36) primary key NOT NULL,
	`Longitude` double NOT NULL,
    `Latitude` double NOT NULL
);

CREATE TABLE `venue` (
	`Id` char(36) primary key NOT NULL,
    `Address` varchar(50) NOT NULL,
	`City` varchar(50) NOT NULL,
	`Province` varchar(50) NOT NULL,
	`PostalCode` varchar(50) NOT NULL
);

CREATE TABLE `tracker` (
	`HardwareId` varchar(50) primary key NOT NULL,
    `LastUpdate` datetime NOT NULL,
    `LocationId` char(36) NOT NULL,
    FOREIGN KEY (`LocationId`) REFERENCES location (`Id`)
);

CREATE TABLE `rental` (
	`id` char(36) primary key NOT NULL,
	`rental_date` datetime NOT NULL,
    `expected_return_date` datetime NOT NULL,
    `CustomerId` char(36) NOT NULL,
    `DestinationId` char(36) NOT NULL,
    FOREIGN KEY (`CustomerId`) REFERENCES customer (`Id`),
    FOREIGN KEY (`DestinationId`) REFERENCES venue (`Id`)
);

CREATE TABLE `speaker` (
	`SerialNumber` varchar(50) primary key NOT NULL,
    `Model` varchar(50) NOT NULL,
    `TrackerId` varchar(50),
    `RentalId`char(36), 
    FOREIGN KEY (`TrackerId`) REFERENCES tracker (`HardwareId`),
    FOREIGN KEY (`RentalId`) REFERENCES rental (`Id`)
);