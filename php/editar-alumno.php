<?php

include("database.php");

if(isset($_POST["id"])) {
    $task_id = $_POST["id"];
    $task_name = $_POST["name"];
    $task_lname = $_POST["lname"];
    $task_phone = $_POST["phone"];
    $query = "UPDATE alumnos SET nombre = '$task_name', apellido = '$task_lname' , telefono = '$task_phone' WHERE id = '$task_id'";
    $result = mysqli_query($connecction, $query);
    if(!$result){
        die("Hubo un error en la consulta" . mysqli_error($connecction));
    }
}
