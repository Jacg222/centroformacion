<?php

include("database.php");

    $search = $_POST["search"];
    if(!empty($search)) {
    $query = "SELECT alumnos.id, alumnos.nombre, alumnos.apellido, alumnos.codigo, alumnos.telefono, GROUP_CONCAT(cursos.nombre SEPARATOR ', ') as curso FROM alumnos
    JOIN matriculas ON alumnos.id = matriculas.id_alumno
    JOIN cursos ON matriculas.id_curso = cursos.id
    WHERE alumnos.nombre LIKE '$search%'
    GROUP BY alumnos.id";
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
            "phone"=>$row["telefono"],
            "code"=>$row["codigo"],
            "course"=>$row["curso"]
        );
    }
    $jsonstring = json_encode($json);
    echo $jsonstring;
}