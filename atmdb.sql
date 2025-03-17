-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 13, 2025 at 03:26 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `atmdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `atmusers`
--

CREATE TABLE `atmusers` (
  `Id` int(11) NOT NULL,
  `CardNumber` varchar(16) NOT NULL,
  `PIN` varchar(4) NOT NULL,
  `Balance` decimal(18,2) DEFAULT 0.00,
  `FullName` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `atmusers`
--

INSERT INTO `atmusers` (`Id`, `CardNumber`, `PIN`, `Balance`, `FullName`) VALUES
(1, '9876543210987654', '4321', 8225696.00, 'Pedro Santos'),
(2, '123456789', '456', 1000.00, 'Paul Andrei Pangan'),
(3, '123456', '234', 2000.00, 'Rein');

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `Id` int(11) NOT NULL,
  `CardNumber` varchar(16) NOT NULL,
  `TransactionType` varchar(20) NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `RecipientCardNumber` varchar(16) DEFAULT NULL,
  `TransactionDate` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transactions`
--

INSERT INTO `transactions` (`Id`, `CardNumber`, `TransactionType`, `Amount`, `RecipientCardNumber`, `TransactionDate`) VALUES
(1, '123456789', 'Transfer', 1000.00, '123456789', '2025-03-12 20:28:43'),
(2, '123456', 'Transfer', 1000.00, '123456789', '2025-03-12 20:30:52'),
(3, '123456789', 'Transfer', 2000.00, '123456', '2025-03-12 20:31:44');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `atmusers`
--
ALTER TABLE `atmusers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `CardNumber` (`CardNumber`);

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `atmusers`
--
ALTER TABLE `atmusers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
