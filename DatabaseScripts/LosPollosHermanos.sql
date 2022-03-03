-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 03 mrt 2022 om 08:00
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
-- Tabelstructuur voor tabel `ingredients`
--

CREATE TABLE `ingredients` (
  `ingredientId` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `price` decimal(18,2) NOT NULL,
  `unitId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Gegevens worden geëxporteerd voor tabel `ingredients`
--

INSERT INTO `ingredients` (`ingredientId`, `name`, `price`, `unitId`) VALUES
(1, 'Rundvlees burger', '3.23', 8),
(2, 'Sla', '0.12', 3),
(3, 'cheddar smeltkaas', '0.35', 5),
(4, 'verse uitjes', '0.15', 6),
(5, 'augurk', '0.13', 7),
(6, 'Big Mac saus', '0.30', 6),
(7, '3-delig sesambroodje', '0.50', 9),
(8, 'ketchup', '0.20', 6),
(9, 'getoast sesambroodje', '0.45', 8),
(10, 'geroosterd sesambroodje', '0.45', 8),
(11, 'Mosterd', '0.25', 4),
(12, 'ragout', '4.20', 2),
(13, 'kipfilet', '4.15', 8),
(14, 'sandwichsaus', '0.18', 6),
(15, 'crunchy bacon', '0.28', 1),
(16, 'emmentaler smeltkaas', '0.45', 7),
(17, 'grillsaus', '0.35', 6),
(18, 'tomaat', '0.65', 7);

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
-- Gegevens worden geëxporteerd voor tabel `meals`
--

INSERT INTO `meals` (`MealId`, `Name`, `Description`, `Price`) VALUES
(1, 'Los Dos', '2 Burgers van 100% rundvlees met verse sla, cheddar smeltkaas, verse uitjes en fijne slices van augurk. Met legendarische Big Mac saus op een 3-delig sesambroodje.', '7.10'),
(2, 'El Grande', 'Een royale burger van 100% rundvlees, cheddar smeltkaas, verse uitjes en fijne augurk slices. Afgemaakt met een lekker laagje ketchup en mosterd op een getoast sesambroodje.', '8.25'),
(3, 'El Pollo Picanto', 'De enige echte kipburger. Een burger van malse kipfilet, omhuld met een krokant laagje. Daarbovenop verse sla en een frisse sandwichsaus op een getoast sesambroodje.', '7.95'),
(4, 'El Sabroso Doble Grande', 'Twee grootse burgers van 100% rundvlees met crunchy bacon, emmentaler smeltkaas, juicy tomaat, verse uitjes en frisse sla. Met een speciale grillsaus op een geroosterd sesambroodje.', '9.35'),
(5, 'Croqueta Hollanda', 'De echte oer-Hollandse burger. Knapperig vanbuiten en zacht van binnen. Met een vulling van ragout met stukjes puur rundvlees. Afgemaakt met een lekkere laag mosterd.', '6.25');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `units`
--

CREATE TABLE `units` (
  `unitId` int(11) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Gegevens worden geëxporteerd voor tabel `units`
--

INSERT INTO `units` (`unitId`, `name`) VALUES
(1, '120 gram'),
(2, '300 gram'),
(3, 'Blaadjes'),
(4, 'Kuipje'),
(5, 'Plakken'),
(6, 'Portie'),
(7, 'Schijfjes'),
(8, 'Stuks'),
(9, 'Suks');

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `ingredients`
--
ALTER TABLE `ingredients`
  ADD PRIMARY KEY (`ingredientId`),
  ADD UNIQUE KEY `name` (`name`),
  ADD KEY `FK_UNIT` (`unitId`);

--
-- Indexen voor tabel `meals`
--
ALTER TABLE `meals`
  ADD PRIMARY KEY (`MealId`),
  ADD UNIQUE KEY `Name` (`Name`);

--
-- Indexen voor tabel `units`
--
ALTER TABLE `units`
  ADD PRIMARY KEY (`unitId`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `ingredients`
--
ALTER TABLE `ingredients`
  MODIFY `ingredientId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT voor een tabel `meals`
--
ALTER TABLE `meals`
  MODIFY `MealId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT voor een tabel `units`
--
ALTER TABLE `units`
  MODIFY `unitId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `ingredients`
--
ALTER TABLE `ingredients`
  ADD CONSTRAINT `FK_UNIT` FOREIGN KEY (`unitId`) REFERENCES `units` (`unitId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
