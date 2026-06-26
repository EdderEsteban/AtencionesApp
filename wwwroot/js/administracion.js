// ── FILTRO TABLA USUARIOS ──────────────────────────────────────────────────
const inputBusqueda = document.getElementById('inputBusqueda');
if (inputBusqueda) {
    const tabla = document.getElementById('tablaUsuarios');
    const filas = tabla.querySelectorAll('tbody tr');
    const contador = document.getElementById('contadorUsuarios');
    const sinResultados = document.getElementById('sinResultados');
    const totalStr = filas.length === 1 ? '1 usuario' : `${filas.length} usuarios`;

    inputBusqueda.addEventListener('input', () => {
        const q = inputBusqueda.value.toLowerCase().trim();
        let visibles = 0;
        filas.forEach(fila => {
            const texto = fila.textContent.toLowerCase();
            const mostrar = !q || texto.includes(q);
            fila.style.display = mostrar ? '' : 'none';
            if (mostrar) visibles++;
        });
        if (q) {
            contador.textContent = `${visibles} de ${filas.length} usuario${filas.length !== 1 ? 's' : ''}`;
        } else {
            contador.textContent = totalStr;
        }
        sinResultados.style.display = visibles === 0 ? '' : 'none';
    });
}

// ── TOM SELECT INSTITUCIONES ───────────────────────────────────────────────
const selectEl = document.getElementById('selectInstituciones');
if (selectEl) {
    new TomSelect(selectEl, {
        plugins: ['remove_button'],
        maxOptions: 600,
        placeholder: 'Buscar institución…',
        create: false
    });
}
