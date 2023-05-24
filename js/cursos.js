$(function(){
    $("#task-result").hide()
    fetchTasks()
    let edit = false

    $("#search").keyup(()=>{
    let search = $("#search").val().trim();
    if(search !== ""){
    $.ajax({
        url: "php/buscar-tarea.php",
        data: { search },
        type: "POST",
        success: function (response) {
            if(!response.error) {
                let tasks = JSON.parse(response);
                let template = ``;
                tasks.forEach(task => {
                    template += `<tr taskId="${task.id}">
                    <td>${task.name}</td>
                    <td>${task.description}</td>
                    <td>${task.temario}</td>
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

    })

    $("#task-form").submit(e => {
        e.preventDefault();
        const postData = {
            name: $("#nombre").val(),
            description: $("#descripcion").val(),
            temario: $("#temario").val(),
            id: $("#taskId").val()
        }

        const url = edit === false ? "php/agregar-tarea.php" : "php/editar-tarea.php";

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
            url: "php/listar-tareas.php",
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
                            <td>${task.description}</td>
                            <td>${task.temario}</td>
                            <td class="d-flex justify-content-center">
                            <button class="btn btn-danger task-delete">Eliminar</button>
                            <button class="btn btn-warning task-item ms-2">Modificar</button>
                            </td>
                        </tr>
                        `;
                    })
                $("#tasks").html(template);
            }
        })
    }

    $(document).on("click", ".task-delete", ()=>{
        if(confirm("¿seguro de eliminar?")){
            const element = $(this)[0].activeElement.parentElement.parentElement;
            const id = $(element).attr("taskId")
            $.post("php/eliminar-tarea.php", { id }, () => {
                fetchTasks()
            })
        }
    })

    $(document).on("click", ".task-item", ()=>{
        const element = $(this)[0].activeElement.parentElement.parentElement;
        const id = $(element).attr("taskId")
        let url = "php/obtener-una-tarea.php"
        $.ajax({
            url,
            data: {id},
            type: "POST",
            success: function(response){
                if(!response.error){
                    const task = JSON.parse(response)
                    $("#nombre").val(task.name)
                    $("#descripcion").val(task.description)
                    $("#temario").val(task.temario)
                    $("#taskId").val(task.id)
                    edit = true
                }
            }
        })
    })

})