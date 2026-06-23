const alertaEl = document.getElementById('alertaExito');
if (alertaEl?.dataset.mensaje) {
    Swal.fire({
        icon: 'success',
        title: alertaEl.dataset.mensaje,
        timer: 3000,
        showConfirmButton: false,
        toast: true,
        position: 'top-end',
        timerProgressBar: true
    });
}

function confirmarEliminar(id, nombre) {
    Swal.fire({
        title: '¿Eliminar paciente?',
        text: nombre,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#ef4444'
    }).then(result => {
        if (result.isConfirmed) {
            document.getElementById('formDel-' + id).submit();
        }
    });
}