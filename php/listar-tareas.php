<?php
include("database.php");

$query = "SELECT * FROM cursos";
$result = mysqli_query($connecction, $query);

if(!$result) {
    die("Hubo un error en la consulta". mysqli_error($connecction));
}

$json = array();

while($row = mysqli_fetch_array($result)){
    $json[] = array(
        "id"=>$row["id"],
        "name"=>$row["nombre"],
        "description"=>$row["descripcion"],
        "temario"=>$row["temario"]
    );
}

$jsonstring = json_encode($json);
echo $jsonstring;
