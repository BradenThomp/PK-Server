DROP DATABASE IF EXISTS `pk_rentals`;
CREATE DATABASE `pk_rentals`;
USE `pk_rentals`;

CREATE TABLE `serialized_event` (
	`aggregate_id` varchar(50) NOT NULL,
    `version` int NOT NULL,
    `data` LONGTEXT NOT NULL,
    `type` LONGTEXT NOT NULL
);
