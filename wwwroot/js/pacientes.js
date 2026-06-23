const alertaEl = document.getElementById('alertaExito');
if (alertaEl) {
    Swal.fire({
        icon: 'success',
        title: 'Listo',
        text: alertaEl.dataset.mensaje,
        timer: 3000,
        showConfirmButton: false,
        toast: true,
        position: 'top-end'
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