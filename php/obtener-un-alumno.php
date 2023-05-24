<?php

include("database.php");

if(isset($_POST["id"])) {
    $id = $_POST["id"];
    $query = "SELECT * FROM alumnos WHERE id = {$id} ";
    $result = mysqli_query($connecction, $query);
    if(!$result) {
        die("Hubo un error en la consulta". mysqli_error($connecction));
    }
    $json = array();
    while($row = mysqli_fetch_array($result)){
        $json[] = array(
            "id"=>$row["id"],
            "name"=>$row["nombre"],
            "lname"=>$row["apellido"],
            "code"=>$row["codigo"],
            "phone"=>$row["telefono"]
        );
    }
    $jsonstring = json_encode($json[0]);
    echo $jsonstring;
}