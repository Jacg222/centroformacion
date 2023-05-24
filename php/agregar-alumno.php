<?php
include("database.php");

if(isset($_POST["name"])){
    $task_name = $_POST["name"];
    $task_lname = $_POST["lname"];
    $task_phone = $_POST["phone"];

    $query = "INSERT into alumnos (nombre, apellido, telefono) VALUES ('$task_name', '$task_lname', '$task_phone')";
    $result = mysqli_query($connecction, $query);
    if(!$result) {
        die("Hubo un error en la consulta". mysqli_error($connecction));
    }
}