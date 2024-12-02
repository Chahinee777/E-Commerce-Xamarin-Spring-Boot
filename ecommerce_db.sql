-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 02, 2024 at 02:29 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ecommerce_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `cart`
--

CREATE TABLE `cart` (
  `id` bigint(20) NOT NULL,
  `quantite` int(11) NOT NULL,
  `prix_total` int(11) NOT NULL,
  `product_id` bigint(20) NOT NULL,
  `user_id` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cart`
--

INSERT INTO `cart` (`id`, `quantite`, `prix_total`, `product_id`, `user_id`) VALUES
(9, 1, 789, 6, 1),
(10, 1, 697, 7, 1);

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `id` bigint(20) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`id`, `name`, `description`) VALUES
(1, 'Watches', 'Various types of watches'),
(2, 'Electronics', 'Electronic gadgets and devices'),
(3, 'Clothing', 'Apparel and clothing items');

-- --------------------------------------------------------

--
-- Table structure for table `order`
--

CREATE TABLE `order` (
  `id` bigint(20) NOT NULL,
  `CommandeId` varchar(255) DEFAULT NULL,
  `DateCommande` datetime DEFAULT NULL,
  `Product_id` bigint(20) DEFAULT NULL,
  `prix` int(11) DEFAULT NULL,
  `quantite` int(11) DEFAULT NULL,
  `User_id` bigint(20) DEFAULT NULL,
  `Statue` varchar(255) DEFAULT NULL,
  `typePayment` varchar(255) DEFAULT NULL,
  `adresse` varchar(255) DEFAULT NULL,
  `commande_id` varchar(255) DEFAULT NULL,
  `date_commande` datetime(6) DEFAULT NULL,
  `type_payment` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `order`
--

INSERT INTO `order` (`id`, `CommandeId`, `DateCommande`, `Product_id`, `prix`, `quantite`, `User_id`, `Statue`, `typePayment`, `adresse`, `commande_id`, `date_commande`, `type_payment`) VALUES
(1, NULL, NULL, 6, 1486, 1, 2, 'Pending', NULL, 'User Address', '06e1bf10-4137-44d8-8980-3e2feea41501', '2024-12-02 12:47:57.000000', 'Credit Card'),
(2, NULL, NULL, 8, 897, 1, 2, 'Pending', NULL, 'User Address', '1f1070ca-040f-48a5-8ed7-a70eb0231c50', '2024-12-02 12:53:38.000000', 'Credit Card'),
(3, NULL, NULL, 7, 697, 1, 2, 'Pending', NULL, 'User Address', '7a3ef3dd-b123-473a-ad61-a1a41f982ab3', '2024-12-02 12:57:14.000000', 'Credit Card'),
(4, NULL, NULL, 7, 697, 1, 2, 'Pending', NULL, 'User Address', '04284ec2-07c1-472f-a5a9-4c7f2816ad00', '2024-12-02 13:02:36.000000', 'Credit Card'),
(5, NULL, NULL, 6, 789, 1, 2, 'Pending', NULL, 'User Address', 'c64efb6c-348c-4053-a554-e375ac7b1871', '2024-12-02 13:09:43.000000', 'Credit Card');

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `id` bigint(20) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `price` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `image_name` varchar(255) DEFAULT NULL,
  `image_type` varchar(255) DEFAULT NULL,
  `image_date` longblob DEFAULT NULL,
  `category_id` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`id`, `name`, `description`, `price`, `quantity`, `image_name`, `image_type`, `image_date`, `category_id`) VALUES
(6, 'Hilfi Watch', 'Elite', 789, 10, 'charlesWatch.png', 'image/png', NULL, 1),
(7, 'Gold Watch', 'Golden', 697, 15, 'johnWatch.png', 'image/png', NULL, 1),
(8, 'Pierre LD Watch', 'Luxury', 897, 5, 'marekWatch.png', 'image/png', NULL, 1),
(9, 'Omega RD Watch', 'Omega', 567, 20, 'rutgeWatch.png', 'image/png', NULL, 1),
(10, 'iPhone', '16 pro max', 1550, 20, 'iPhone.png', 'image/png', NULL, 2),
(11, 'PS5', 'Sony', 1000, 15, 'ps5.png', 'image/png', NULL, 2),
(12, 'Air Jordan 1', 'Nike', 300, 50, 'nike.png', 'image/png', NULL, 3),
(13, 'T-Shirt', 'Black', 12, 47, 't-shirt.png', 'image/png', NULL, 3);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` bigint(20) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `email`, `password`) VALUES
(1, 'test@example.com', 'password123'),
(2, 'chahine@gmail.com', '1234567');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cart`
--
ALTER TABLE `cart`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_cart_product` (`product_id`),
  ADD KEY `fk_cart_user` (`user_id`);

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `order`
--
ALTER TABLE `order`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Product_id` (`Product_id`),
  ADD KEY `User_id` (`User_id`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_product_category` (`category_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cart`
--
ALTER TABLE `cart`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `order`
--
ALTER TABLE `order`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cart`
--
ALTER TABLE `cart`
  ADD CONSTRAINT `fk_cart_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_cart_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `order`
--
ALTER TABLE `order`
  ADD CONSTRAINT `order_ibfk_1` FOREIGN KEY (`Product_id`) REFERENCES `product` (`id`),
  ADD CONSTRAINT `order_ibfk_2` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`);

--
-- Constraints for table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `fk_product_category` FOREIGN KEY (`category_id`) REFERENCES `category` (`id`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
