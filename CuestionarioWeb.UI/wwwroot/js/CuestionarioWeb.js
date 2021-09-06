function MostrarOcultarTextAreaContainer(button) {
    if (button.innerHTML === "Contraer") {
        document.getElementById("text-area-container").style.display = "none";
        button.innerHTML = "Unirme a la discusión";
    } else {
        document.getElementById("text-area-container").style.display = "initial";
        button.innerHTML = "Contraer";
    }
}
//modal para emojis
function modalEmoji (id) {
    if (document.getElementById("tipos-reacciones-" + id).style.display === "inline-block") {
        //let elem = document.getElementsByClassName("tipos-reacciones");
        //for (let i = 0; i < elem.length; i++) {
        //    elem[i].style.display = "none";
        //}
        document.getElementById("tipos-reacciones-" + id).style.display = "none";
    } else {
        let elem = document.getElementsByClassName("tipos-reacciones");
        for (let i = 0; i < elem.length; i++) {
            elem[i].style.display = "none";
        }
        document.getElementById("tipos-reacciones-" + id).style.display = "inline-block";
    }
}

function revelarModal() {
    document.getElementById("modal-pregunta").style.display = "initial";
    document.getElementById("modal-block").style.display = "initial";
}

function ocultarModal() {
    document.getElementById("modal-pregunta").style.display = "none";
    document.getElementById("modal-block").style.display = "none";
}

function mencionarDialogo(idRespuesta) {
    let dialog = document.getElementById("mencionar-dialogo-" + idRespuesta);
    if (dialog.style.display === "none" || dialog.style.display === "") {
        //nos aseguramos para cerrar todas las menciones abiertas
        //y eliminar sus hijos
        let elem = document.getElementsByClassName("mencionar-dialogo");
        for (let i = 0; i < elem.length; i++) {
            try {
                let idElem = elem[i].id;
                const info = idElem.split("-");
                const idPadre = `header-mencionar-dialogo-${info[2]}`;
                const idHijo = `container-respuesta-${info[2]}-child`;

                const hijo = document.getElementById(idHijo);
                if (hijo !== null) {
                    document.getElementById(idPadre).removeChild(hijo);
                }
            } catch {
                console.log("error")
            }
            elem[i].style.display = "none";
        }
        const mencion = document.getElementById("container-respuesta-" + idRespuesta).cloneNode(true);
        mencion.id = `container-respuesta-${idRespuesta}-child`;
        document.getElementById("header-mencionar-dialogo-" + idRespuesta).appendChild(mencion);

        dialog.style.display = "block";

    } else {
        const mencion = document.getElementById(`container-respuesta-${idRespuesta}-child`);
        document.getElementById("header-mencionar-dialogo-" + idRespuesta).removeChild(mencion);
        dialog.style.display = "none";
    } 
}

//peticion Ajax
const obtenerComentarios = (idRespuesta) => new Promise((resolve, reject) => {
    const ajax = new XMLHttpRequest();
    ajax.onload = function () {
        if (ajax.status == 200) {
            resolve(JSON.parse(ajax.responseText));
        } else {
            reject(Error(ajax.statusText));
        }
    }
    ajax.open("GET", `/Preguntas/ComentariosDeRespuestas?id=${idRespuesta}`, true);
    ajax.onerror = (error) => reject(error);
    ajax.send();
});


function verComentarios(idRespuesta) {
    const contenedorComentarios = document.getElementById(`comentarios-sobre-respuesta-${idRespuesta}`);
    if (contenedorComentarios.style.display === "" || contenedorComentarios.style.display === "none") {
        contenedorComentarios.style.display = "block"
        obtenerComentarios(idRespuesta)
        .then(
            res => respuestaHTML(res, idRespuesta),
            err => respuestaHTML(err)
        )
    } else {
        contenedorComentarios.style.display = "none"
    }
   
}

//html que se enviara a la vista
function respuestaHTML(data, idRespuesta) {
    const contenedorComentarios = document.getElementById(`comentarios-sobre-respuesta-${idRespuesta}`);
    let html="";
    data.forEach((item) => {
        html += `
            <div id="container-comentarios-${item.idRespuesta}" class="container-comentarios">
                <span class="usuario-respuesta">${item.usuario.nombre} ${item.usuario.apellido}</span>
                ${item.respuestaEmitida}
                <span class="hora-respuesta">${item.fechaDeRespuesta}</span>
            </div>
        `;
    });
    if (html === "") {
        html="Sin comentarios..."
    }
    contenedorComentarios.innerHTML = html;
}