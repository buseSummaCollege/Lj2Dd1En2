-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 02 mrt 2022 om 14:08
-- Serverversie: 10.4.22-MariaDB
-- PHP-versie: 8.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `lospolloshermanos`
--
CREATE DATABASE IF NOT EXISTS `lospolloshermanos` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `lospolloshermanos`;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `meals`
--

CREATE TABLE `meals` (
  `MealId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` text DEFAULT NULL,
  `Price` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `meals`
--
ALTER TABLE `meals`
  ADD PRIMARY KEY (`MealId`),
  ADD UNIQUE KEY `Name` (`Name`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `meals`
--
ALTER TABLE `meals`
  MODIFY `MealId` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

INSERT INTO `meals` (`mealId`, `name`, `description`, `price`) VALUES
(1, 'Los Dos', '2 Burgers van 100% rundvlees met verse sla, cheddar smeltkaas, verse uitjes en fijne slices van augurk. Met legendarische Big Mac saus op een 3-delig sesambroodje.', '7.10'),
(2, 'El Grande', 'Een royale burger van 100% rundvlees, cheddar smeltkaas, verse uitjes en fijne augurk slices. Afgemaakt met een lekker laagje ketchup en mosterd op een getoast sesambroodje.', '8.25'),
(3, 'El Pollo Picanto', 'De enige echte kipburger. Een burger van malse kipfilet, omhuld met een krokant laagje. Daarbovenop verse sla en een frisse sandwichsaus op een getoast sesambroodje.', '7.95'),
(4, 'El Sabroso Doble Grande', 'Twee grootse burgers van 100% rundvlees met crunchy bacon, emmentaler smeltkaas, juicy tomaat, verse uitjes en frisse sla. Met een speciale grillsaus op een geroosterd sesambroodje.', '9.35'),
(5, 'Croqueta Hollanda', 'De echte oer-Hollandse burger. Knapperig vanbuiten en zacht van binnen. Met een vulling van ragout met stukjes puur rundvlees. Afgemaakt met een lekkere laag mosterd.', '6.25');
