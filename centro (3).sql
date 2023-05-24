-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 16-05-2023 a las 00:26:29
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `centro`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alumnos`
--

CREATE TABLE `alumnos` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `codigo` varchar(10) NOT NULL,
  `telefono` varchar(20) NOT NULL,
  `correo` varchar(255) NOT NULL,
  `contraseña` varchar(255) NOT NULL,
  `tipo` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `alumnos`
--

INSERT INTO `alumnos` (`id`, `nombre`, `apellido`, `codigo`, `telefono`, `correo`, `contraseña`, `tipo`) VALUES
(1, 'Juan', 'Pérez', '', '1234567890', '', '', ''),
(2, 'María', 'García', '', '2345678901', '', '', ''),
(3, 'Pedro', 'Martínez', '', '3456789012', '', '', ''),
(4, 'Laura', 'Sánchez', '', '4567890123', '', '', ''),
(5, 'Carlos', 'González', '', '5678901234', '', '', ''),
(6, 'Ana', 'López', '', '6789012345', '', '', ''),
(7, 'Javier', 'Hernández', '', '7890123456', '', '', ''),
(8, 'Mónica', 'Romero', '', '8901234567', '', '', ''),
(9, 'Sergio', 'Torres', '', '9012345678', '', '', ''),
(10, 'Elena', 'Fernández', '', '0123456788', '', '', ''),
(11, 'Julian', 'Cortes', '', '3192371420', '', '', ''),
(12, 'Alvaro', 'Llanez', '', '3145687936', '', '', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cursos`
--

CREATE TABLE `cursos` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `descripcion` text NOT NULL,
  `temario` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `cursos`
--

INSERT INTO `cursos` (`id`, `nombre`, `descripcion`, `temario`) VALUES
(1, 'musica', 'curso de musica', 'Introducción a la música\nHistoria de la música\nTeoría musical\nTécnicas de interpretación\nComposición y arreglo'),
(2, 'Física', 'Curso de física clásica para estudiantes de nivel medio superior', ''),
(3, 'Química', 'Curso de química básica para estudiantes de nivel preparatoria', ''),
(4, 'Programación', 'Curso de programación en C++ para principiantes', ''),
(5, 'Inglés', 'Curso de inglés avanzado para negocios', ''),
(6, 'Marketing', 'Curso de marketing digital para emprendedores', ''),
(7, 'Finanzas', 'Curso de finanzas para inversionistas', ''),
(8, 'Arte contemporáneo', 'Curso de arte contemporáneo para entusiastas', ''),
(9, 'Diseño gráfico', 'Curso de diseño gráfico para principiantes', ''),
(10, 'Historia', 'Curso de historia mundial para curiosos', ''),
(11, 'Frances', 'Curso de frances avanzado para negocios', ''),
(24, 'Biologia', 'curso de biologia basica', ''),
(28, 'electronica', 'curso electronica digital ', ''),
(29, 'Fotografia', 'curso de fotografia avanzada', ''),
(32, 'aleman', 'curso de aleman', ''),
(42, 'geografia', 'curso de geografia', 'mapas');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `matriculas`
--

CREATE TABLE `matriculas` (
  `id` int(11) NOT NULL,
  `id_alumno` int(11) NOT NULL,
  `id_curso` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `matriculas`
--

INSERT INTO `matriculas` (`id`, `id_alumno`, `id_curso`) VALUES
(1, 6, 8),
(2, 6, 6),
(3, 6, 9),
(4, 6, 5),
(5, 5, 2),
(6, 5, 1),
(7, 5, 4),
(8, 10, 3),
(9, 10, 1),
(10, 10, 2),
(11, 10, 8),
(12, 10, 9),
(13, 1, 1),
(14, 1, 2),
(15, 1, 3),
(16, 1, 4),
(17, 1, 5),
(18, 2, 2),
(19, 2, 3),
(20, 2, 4),
(21, 2, 5),
(22, 3, 3),
(23, 3, 8),
(25, 4, 4),
(26, 4, 5),
(27, 4, 9),
(28, 4, 6),
(29, 4, 3),
(30, 7, 2),
(31, 7, 8),
(32, 7, 4),
(33, 7, 9),
(34, 8, 1),
(35, 8, 6),
(36, 8, 4),
(37, 8, 5),
(38, 9, 9),
(39, 9, 10),
(40, 9, 1),
(41, 9, 2);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `alumnos`
--
ALTER TABLE `alumnos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `cursos`
--
ALTER TABLE `cursos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `matriculas`
--
ALTER TABLE `matriculas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_alumno` (`id_alumno`),
  ADD KEY `id_curso` (`id_curso`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `alumnos`
--
ALTER TABLE `alumnos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `cursos`
--
ALTER TABLE `cursos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- AUTO_INCREMENT de la tabla `matriculas`
--
ALTER TABLE `matriculas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `matriculas`
--
ALTER TABLE `matriculas`
  ADD CONSTRAINT `matriculas_ibfk_1` FOREIGN KEY (`id_alumno`) REFERENCES `alumnos` (`id`),
  ADD CONSTRAINT `matriculas_ibfk_2` FOREIGN KEY (`id_curso`) REFERENCES `cursos` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
