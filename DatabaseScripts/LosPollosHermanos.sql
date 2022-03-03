-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 03 mrt 2022 om 21:04
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
-- Tabelstructuur voor tabel `mealingredients`
--

CREATE TABLE `mealingredients` (
  `mealIngredientId` int(11) NOT NULL,
  `mealId` int(11) NOT NULL,
  `ingredientId` int(11) NOT NULL,
  `quantity` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Gegevens worden geëxporteerd voor tabel `mealingredients`
--

INSERT INTO `mealingredients` (`mealIngredientId`, `mealId`, `ingredientId`, `quantity`) VALUES
(1, 1, 1, 2),
(2, 2, 1, 1),
(3, 1, 9, 1),
(4, 2, 10, 1);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `meals`
--

CREATE TABLE `meals` (
  `MealId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Gegevens worden geëxporteerd voor tabel `meals`
--

INSERT INTO `meals` (`MealId`, `Name`) VALUES
(5, 'Croqueta Hollanda'),
(2, 'El Grande'),
(3, 'El Pollo Picanto'),
(4, 'El Sabroso Doble Grande'),
(1, 'Los Dos');

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
-- Indexen voor tabel `mealingredients`
--
ALTER TABLE `mealingredients`
  ADD PRIMARY KEY (`mealIngredientId`),
  ADD UNIQUE KEY `mealId` (`mealId`,`ingredientId`),
  ADD KEY `FK_INGREDIENT` (`ingredientId`);

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
  MODIFY `ingredientId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT voor een tabel `mealingredients`
--
ALTER TABLE `mealingredients`
  MODIFY `mealIngredientId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT voor een tabel `meals`
--
ALTER TABLE `meals`
  MODIFY `MealId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT voor een tabel `units`
--
ALTER TABLE `units`
  MODIFY `unitId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `ingredients`
--
ALTER TABLE `ingredients`
  ADD CONSTRAINT `FK_UNIT` FOREIGN KEY (`unitId`) REFERENCES `units` (`unitId`);

--
-- Beperkingen voor tabel `mealingredients`
--
ALTER TABLE `mealingredients`
  ADD CONSTRAINT `FK_INGREDIENT` FOREIGN KEY (`ingredientId`) REFERENCES `ingredients` (`ingredientId`),
  ADD CONSTRAINT `FK_MAALTIJD` FOREIGN KEY (`mealId`) REFERENCES `meals` (`MealId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
