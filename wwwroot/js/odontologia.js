// === TOAST ÉXITO ===
const alertaEl = document.getElementById('alertaExito');
if (alertaEl?.dataset.mensaje) {
    Swal.fire({
        toast: true, position: 'top-end', icon: 'success',
        title: alertaEl.dataset.mensaje,
        showConfirmButton: false, timer: 3000, timerProgressBar: true
    });
}

// === INIT: leer datos del servidor ===
const datosCalculo = document.getElementById('datosCalculo');
let pacienteFechaNac = datosCalculo?.dataset.fechaNac || null;

// Calcular edad
function calcularEdad() {
    const edadInput = document.getElementById('Edad');
    const fechaInput = document.getElementById('inputFecha');
    if (!edadInput || !fechaInput || !pacienteFechaNac) return;
    const nac = new Date(pacienteFechaNac);
    const aten = new Date(fechaInput.value);
    if (isNaN(nac) || isNaN(aten)) return;
    let edad = aten.getFullYear() - nac.getFullYear();
    const m = aten.getMonth() - nac.getMonth();
    if (m < 0 || (m === 0 && aten.getDate() < nac.getDate())) edad--;
    edadInput.value = edad >= 0 ? edad : 0;
}

document.getElementById('inputFecha')?.addEventListener('change', calcularEdad);
calcularEdad();

// === PRESTACIONES ===
function actualizarResumen() {
    const checks = document.querySelectorAll('.prestacion-check:checked');
    const lista = document.getElementById('resumenLista');
    const vacio = document.getElementById('resumenVacio');
    if (!lista) return;

    const items = Array.from(checks).map(check => {
        const row = check.closest('.prestacion-item');
        const cantidad = row.querySelector('.prestacion-cantidad').value || 1;
        return { nombre: check.dataset.nombre, cantidad };
    });

    if (items.length === 0) {
        lista.innerHTML = '';
        if (vacio) { vacio.hidden = false; lista.appendChild(vacio); }
        return;
    }

    if (vacio) vacio.hidden = true;
    lista.textContent = '';
    items.forEach(i => {
        const row = document.createElement('div');
        row.className = 'resumen-prest-item';
        const nombre = document.createElement('span');
        nombre.className = 'resumen-prest-nombre';
        nombre.textContent = i.nombre;
        const cant = document.createElement('span');
        cant.className = 'resumen-prest-cant';
        cant.textContent = `× ${i.cantidad}`;
        row.appendChild(nombre);
        row.appendChild(cant);
        lista.appendChild(row);
    });
}

document.querySelectorAll('.prestacion-check').forEach(check => {
    check.addEventListener('change', () => {
        const row = check.closest('.prestacion-item');
        const cantInput = row.querySelector('.prestacion-cantidad');
        if (check.checked) {
            cantInput.disabled = false;
            row.classList.add('seleccionada');
        } else {
            cantInput.disabled = true;
            cantInput.value = 1;
            row.classList.remove('seleccionada');
        }
        actualizarResumen();
    });
});

document.querySelectorAll('.prestacion-cantidad').forEach(input => {
    input.addEventListener('input', actualizarResumen);
});

// === PRE-CARGA EN EDIT ===
if (datosCalculo) {
    const prestacionesEdit = JSON.parse(datosCalculo.dataset.prestacionesEdit || '[]');
    prestacionesEdit.forEach(p => {
        const check = document.querySelector(`.prestacion-check[data-id="${p.tipoPrestacionId}"]`);
        if (!check) return;
        check.checked = true;
        const row = check.closest('.prestacion-item');
        const cantInput = row.querySelector('.prestacion-cantidad');
        cantInput.disabled = false;
        cantInput.value = p.cantidad;
        row.classList.add('seleccionada');
    });
    actualizarResumen();
}

// === FORM SUBMIT ===
document.getElementById('formAtencion')?.addEventListener('submit', e => {
    const checks = document.querySelectorAll('.prestacion-check:checked');
    const errorEl = document.getElementById('errorPrestaciones');

    if (checks.length === 0) {
        e.preventDefault();
        if (errorEl) { errorEl.textContent = 'Agrega al menos una prestación'; errorEl.hidden = false; }
        document.getElementById('resumenCard')?.scrollIntoView({ behavior: 'smooth' });
        return;
    }
    if (errorEl) errorEl.hidden = true;

    const container = document.getElementById('prestacionesHidden');
    container.innerHTML = '';
    checks.forEach((check, i) => {
        const cantidad = check.closest('.prestacion-item').querySelector('.prestacion-cantidad').value || 1;
        const hId = document.createElement('input');
        hId.type = 'hidden'; hId.name = `Prestaciones[${i}].TipoPrestacionId`; hId.value = check.dataset.id;
        const hCant = document.createElement('input');
        hCant.type = 'hidden'; hCant.name = `Prestaciones[${i}].Cantidad`; hCant.value = cantidad;
        container.appendChild(hId);
        container.appendChild(hCant);
    });
});

// === OBRA SOCIAL INLINE ===
const sinOsCheck = document.getElementById('SinObraSocial');
const osContainer = document.getElementById('osInlineContainer');
const osInput = document.getElementById('NuevaObraSocial');

sinOsCheck?.addEventListener('change', () => {
    if (!sinOsCheck.checked) {
        if (osContainer) { osContainer.style.display = 'block'; }
        if (osInput) { osInput.value = ''; osInput.focus(); }
    } else {
        if (osContainer) { osContainer.style.display = 'none'; }
        if (osInput) { osInput.value = ''; }
    }
});

// === CONFIRMAR ELIMINAR (Index) ===
function confirmarEliminar(id, nombre) {
    Swal.fire({
        title: '¿Eliminar atención?',
        text: `Atención de ${nombre}`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#e53e3e'
    }).then(r => { if (r.isConfirmed) document.getElementById(`formDel-${id}`).submit(); });
}
