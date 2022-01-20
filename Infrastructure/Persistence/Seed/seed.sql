DROP DATABASE IF EXISTS `pk_rentals`;
CREATE DATABASE `pk_rentals`;
USE `pk_rentals`;

CREATE TABLE `tracker_event_stream` (
	`aggregate_id` varchar(50) NOT NULL,
    `version` int NOT NULL,
    `data` LONGTEXT NOT NULL,
    `type` LONGTEXT NOT NULL,
    PRIMARY KEY (`aggregate_id`, `version`)
);

CREATE TABLE `rental_event_stream` (
	`aggregate_id` varchar(50) NOT NULL,
    `version` int NOT NULL,
    `data` LONGTEXT NOT NULL,
    `type` LONGTEXT NOT NULL,
    PRIMARY KEY (`aggregate_id`, `version`)
);

CREATE TABLE `speaker` (
	`SerialNumber` varchar(50) primary key NOT NULL,
    `Model` varchar(50) NOT NULL,
    `TrackerId` varchar(50)
);

CREATE TABLE `tracker_projection` (
	`MACAddress` varchar(50) primary key NOT NULL,
    `Longitude` double NOT NULL,
    `Latitude` double NOT NULL,
    `LastUpdate` datetime NOT NULL,
    `SpeakerSerialNumber` varchar(50)
);

CREATE TABLE `rental_projection` (
	`Id` char(36) primary key NOT NULL,
    `CustomerId` char(36) NOT NULL,
    `VenueId` char(36) NOT NULL,
    `RentalDate` datetime NOT NULL,
    `ExpectedReturnDate` datetime NOT NULL
);