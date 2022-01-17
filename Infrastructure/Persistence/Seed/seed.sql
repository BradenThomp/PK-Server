DROP DATABASE IF EXISTS `pk_rentals`;
CREATE DATABASE `pk_rentals`;
USE `pk_rentals`;

CREATE TABLE `serialized_event` (
	`aggregate_id` varchar(50) NOT NULL,
    `version` int NOT NULL,
    `data` LONGTEXT NOT NULL,
    `type` LONGTEXT NOT NULL
);

CREATE TABLE `speaker` (
	`SerialNumber` varchar(50) primary key NOT NULL,
    `Model` varchar(50) NOT NULL
);

CREATE TABLE `tracker_projection` (
	`MACAddress` varchar(50) primary key NOT NULL,
    `Longitude` double NOT NULL,
    `Latitude` double NOT NULL,
    `LastUpdate` datetime NOT NULL
);