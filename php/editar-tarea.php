<?php
include("database.php");

if(isset($_POST["id"])) {
    $task_id = $_POST["id"];
    $task_name = $_POST["name"];
    $task_description = $_POST["description"];
    $task_temario = $_POST["temario"];

    $query = "UPDATE cursos SET nombre = '$task_name', descripcion = '$task_description' , temario = '$task_temario' WHERE id = '$task_id'";
    $result = mysqli_query($connecction, $query);

    if(!$result){
        die("Hubo un error en la consulta" . mysqli_error($connecction));
    }
}