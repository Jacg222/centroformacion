$(function(){
    $("#task-result").hide()
    fetchTasks()
    let edit = false

    $("#search").keyup(()=>{
        let search = $("#search").val().trim();
        if(search !== ""){
            $.ajax({
                url: "php/buscar-alumno.php",
                data: { search },
                type: "POST",
                success: function (response) {
                    if(!response.error) {
                        let tasks = JSON.parse(response);
                        let template = ``;
                        tasks.forEach(task => {
                            template += `<tr taskId="${task.id}">
                                <td>${task.name}</td>
                                <td>${task.lname}</td>
                                <td>${task.code}</td>
                                <td>${task.phone}</td>
                                <td>${task.course}</td>
                                <td class="d-flex justify-content-center">
                                    <button class="btn btn-danger task-delete">Eliminar</button>
                                    <button class="btn btn-warning task-item ms-2">Modificar</button>
                                </td>`
                        });
                        $("#task-result").show();
                        $("#container").html(template);
                    }
                }
            })
        } else {
            $("#task-result").hide();
            $("#container").html("");
        }
    });
    

    $("#task-form").submit(e => {
        e.preventDefault();
        const postData = {
            name: $("#nombre").val(),
            lname: $("#apellido").val(),
            code: $("#codigo").val(),
            phone: $("#telefono").val(),
            id: $("#taskId").val()
        }

        const url = edit === false ? "php/agregar-alumno.php" : "php/editar-alumno.php";

        $.ajax({
            url,
            data: postData,
            type: "POST",
            success: function (response) {
                if(!response.error){
                    fetchTasks();
                    $("#task-form").trigger("reset")
                    edit = false
                }
            }
        })
    })

    function fetchTasks() {
        $.ajax({
            url: "php/listar-alumnos.php",
            type: "GET",
            success: function(response){
                const tasks = JSON.parse(response);
                let template = ``;
                tasks.forEach(task => 
                    {
                        template += `
                        <tr taskId="${task.id}">
                        <td>${task.id}</td>
                        <td>${task.name}</td>
                        <td>${task.lname}</td>
                        <td>${task.code}</td>
                        <td>${task.phone}</td>
                        <td>${task.course}</td>
                        <td class="d-flex justify-content-center">
                          <button class="btn btn-danger task-delete">Eliminar</button>
                          <button class="btn btn-warning task-item ms-2">Modificar</button>
                        </td>
                      </tr>`;
                    })
                $("#tasks").html(template);
            }
        })
    }

    $(document).on("click", ".task-delete", ()=>{
        if(confirm("¿seguro de eliminar?")){
            const element = $(this)[0].activeElement.parentElement.parentElement;
            const id = $(element).attr("taskId")
            $.post("php/eliminar-alumno.php", { id }, () => {
                fetchTasks()
            })
        }
    })

    $(document).on("click", ".task-item", ()=>{
        const element = $(this)[0].activeElement.parentElement.parentElement;
        const id = $(element).attr("taskId")
        let url = "php/obtener-un-alumno.php"
        $.ajax({
            url,
            data: {id},
            type: "POST",
            success: function(response){
                if(!response.error){
                    const task = JSON.parse(response)
                    $("#nombre").val(task.name)
                    $("#apellido").val(task.lname)
                    $("#code").val(task.codigo)
                    $("#telefono").val(task.phone)
                    $("#taskId").val(task.id)
                    edit = true
                }
            }
        })
    })

})