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
	`SpeakerSerialNumber` varchar(50) DEFAULT NULL,
    `LocationId` char(36) NOT NULL,
    FOREIGN KEY (`LocationId`) REFERENCES location (`Id`)
);

CREATE TABLE `rental` (
	`Id` char(36) primary key NOT NULL,
	`CustomerId` char(36) NOT NULL,
	`RentalDate` datetime NOT NULL,
	`DestinationId` char(36) NOT NULL,
    `ExpectedReturnDate` datetime NOT NULL,
    `DateReturned` datetime 
);

CREATE TABLE `returned_speaker` (
	`SerialNumber` varchar(50) NOT NULL,
    `Model` varchar(50) NOT NULL,
    `RentalId`char(36) NOT NULL,
    `DateReturned` datetime NOT NULL,
    FOREIGN KEY (`RentalId`) REFERENCES rental (`Id`),
    PRIMARY KEY (`SerialNumber`, `RentalId`)
);

CREATE TABLE `speaker` (
	`SerialNumber` varchar(50) primary key NOT NULL,
    `Model` varchar(50) NOT NULL,
    `TrackerId` varchar(50) UNIQUE,
    `RentalId`char(36), 
    FOREIGN KEY (`TrackerId`) REFERENCES tracker (`HardwareId`),
    FOREIGN KEY (`RentalId`) REFERENCES rental (`Id`)
);
