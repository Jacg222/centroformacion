<?php

include("database.php");

if(isset($_POST["name"])){
    $task_name = $_POST["name"];
    $task_description = $_POST["description"];
    $task_temario = $_POST["temario"];


    $query = "INSERT into cursos (nombre, descripcion, temario) VALUES ('$task_name', '$task_description', '$task_temario')";
    $result = mysqli_query($connecction, $query);

    if(!$result) {
        die("Hubo un error en la consulta". mysqli_error($connecction));
    }
}