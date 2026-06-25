(function () {
    'use strict';

    // { "11": { "V": 1 }, "36": { "*": 3 } }
    // Estados: 0=Sano, 1=Caries, 2=Obturado, 3=Ausente, 4=ExtraccionIndicada, 5=Corona
    let odEstado = {};
    let toolbarNum = null;
    let toolbarSup = null;

    const PERMANENTES = [];
    for (const q of [1, 2, 3, 4]) for (let p = 1; p <= 8; p++) PERMANENTES.push(q * 10 + p);

    const TEMPORARIOS = [];
    for (const q of [5, 6, 7, 8]) for (let p = 1; p <= 5; p++) TEMPORARIOS.push(q * 10 + p);

    document.addEventListener('DOMContentLoaded', init);

    function init() {
        const wrapEditable = document.getElementById('odontogramaEditable');
        const fuenteEstado = wrapEditable ||
            document.getElementById('odontogramaCard') ||
            document.getElementById('odontogramaReadonly');
        if (!fuenteEstado) return;

        const raw = fuenteEstado.dataset.estados;
        if (raw && raw.length > 2) {
            try { cargarEstados(JSON.parse(raw)); } catch (_) { }
        }

        renderizar();
        if (wrapEditable) setupInteracciones(wrapEditable);
    }

    function cargarEstados(lista) {
        odEstado = {};
        for (const item of lista) {
            const k = String(item.numeroDiente);
            if (!odEstado[k]) odEstado[k] = {};
            odEstado[k][item.superficie] = item.estado;
        }
    }

    // ——— RENDER ———

    function renderizar() {
        document.querySelectorAll('.diente-svg').forEach(svg => {
            svg.querySelectorAll('.superficie').forEach(p => {
                p.classList.remove('sup-caries', 'sup-obturado', 'sup-corona');
            });
            svg.classList.remove('diente-bloqueado', 'diente-ausente', 'diente-extraccion');
            svg.querySelectorAll('.odo-x-line').forEach(l => l.remove());
        });

        for (const [numStr, sups] of Object.entries(odEstado)) {
            document.querySelectorAll(`.diente-svg[data-numero="${numStr}"]`).forEach(svg => {
                if (sups['*'] === 3) {
                    svg.classList.add('diente-bloqueado', 'diente-ausente');
                } else if (sups['*'] === 4) {
                    svg.classList.add('diente-bloqueado', 'diente-extraccion');
                    dibujarX(svg);
                } else {
                    for (const [sup, estado] of Object.entries(sups)) {
                        if (estado === 0) continue;
                        const pol = svg.querySelector(`[data-sup="${sup}"]`);
                        if (!pol) continue;
                        if (estado === 1) pol.classList.add('sup-caries');
                        else if (estado === 2) pol.classList.add('sup-obturado');
                        else if (estado === 5) pol.classList.add('sup-corona');
                    }
                }
            });
        }
    }

    function dibujarX(svg) {
        for (const [x1, y1, x2, y2] of [['6', '6', '38', '38'], ['38', '6', '6', '38']]) {
            const ln = document.createElementNS('http://www.w3.org/2000/svg', 'line');
            ln.setAttribute('x1', x1); ln.setAttribute('y1', y1);
            ln.setAttribute('x2', x2); ln.setAttribute('y2', y2);
            ln.classList.add('odo-x-line');
            svg.appendChild(ln);
        }
    }

    // ——— INTERACCIONES ———

    function setupInteracciones(wrap) {
        const toolbar = document.getElementById('odoMiniToolbar');
        if (!toolbar) return;

        wrap.addEventListener('click', e => {
            const svg = e.target.closest('.diente-svg');
            if (!svg) { cerrarToolbar(); return; }

            const bloqueado = svg.classList.contains('diente-bloqueado');
            const pol = e.target.closest('.superficie');

            toolbarNum = svg.dataset.numero;
            toolbarSup = bloqueado ? '*' : (pol ? pol.dataset.sup : null);
            if (!toolbarSup) { cerrarToolbar(); return; }

            wrap.querySelectorAll('.diente-svg.activo').forEach(s => s.classList.remove('activo'));
            svg.classList.add('activo');
            posicionarToolbar(toolbar, svg);
            toolbar.removeAttribute('hidden');
            e.stopPropagation();
        });

        toolbar.querySelectorAll('.odo-tb-btn').forEach(btn => {
            btn.addEventListener('click', e => {
                e.stopPropagation();
                aplicarEstado(parseInt(btn.dataset.estado));
            });
        });

        document.addEventListener('click', cerrarToolbar);
        toolbar.addEventListener('click', e => e.stopPropagation());
    }

    function posicionarToolbar(toolbar, svgEl) {
        const rect = svgEl.getBoundingClientRect();
        const tbW = 268;
        let left = Math.min(rect.left, window.innerWidth - tbW - 8);
        let top = rect.bottom + 8;
        if (top + 72 > window.innerHeight) top = rect.top - 72;
        toolbar.style.left = left + 'px';
        toolbar.style.top = top + 'px';
    }

    // ——— APLICAR ESTADO ———

    function aplicarEstado(estado) {
        if (!toolbarNum) return;
        const k = toolbarNum;
        if (!odEstado[k]) odEstado[k] = {};

        if (estado === 3 || estado === 4) {
            // Diente completo: Ausente o Extracción indicada
            odEstado[k] = { '*': estado };

        } else if (estado === 0) {
            // Sano: borrar superficie (o todo el diente si estaba bloqueado)
            if (toolbarSup === '*') {
                delete odEstado[k];
            } else {
                delete odEstado[k][toolbarSup];
                delete odEstado[k]['*'];
                if (Object.keys(odEstado[k]).length === 0) delete odEstado[k];
            }

        } else {
            // Caries (1), Obturado (2), Corona (5) — desbloquea si era ausente/extraccion
            delete odEstado[k]['*'];
            const sup = toolbarSup === '*' ? 'O' : toolbarSup;
            odEstado[k][sup] = estado;
        }

        cerrarToolbar();
        renderizar();
        calcularCPO();
        actualizarHidden();
    }

    function cerrarToolbar() {
        const toolbar = document.getElementById('odoMiniToolbar');
        if (toolbar) toolbar.setAttribute('hidden', '');
        document.querySelectorAll('.diente-svg.activo').forEach(s => s.classList.remove('activo'));
        toolbarNum = null;
        toolbarSup = null;
    }

    // ——— CPO / ceo ———

    function calcularCPO() {
        let C = 0, P = 0, O = 0, c = 0, e = 0, o = 0;

        for (const num of PERMANENTES) {
            const sups = odEstado[String(num)];
            if (!sups) continue;
            if (sups['*'] === 3) { P++; continue; }
            if (sups['*'] === 4) continue; // indicada no cuenta en permanentes
            const vals = Object.values(sups).filter(v => v > 0);
            if (vals.includes(1)) C++;
            else if (vals.some(v => v === 2 || v === 5)) O++;
        }

        for (const num of TEMPORARIOS) {
            const sups = odEstado[String(num)];
            if (!sups) continue;
            if (sups['*'] === 3) continue; // ausente en temporarios no cuenta
            if (sups['*'] === 4) { e++; continue; }
            const vals = Object.values(sups).filter(v => v > 0);
            if (vals.includes(1)) c++;
            else if (vals.some(v => v === 2 || v === 5)) o++;
        }

        fijarInput('Valoracion_CariesPerm', C);
        fijarInput('Valoracion_PerdidosPerm', P);
        fijarInput('Valoracion_ObturadosPerm', O);
        fijarInput('Valoracion_CariesTemp', c);
        fijarInput('Valoracion_ExtraccionTemp', e);
        fijarInput('Valoracion_ObturadosTemp', o);
    }

    function fijarInput(id, val) {
        const el = document.getElementById(id);
        if (el) el.value = val;
    }

    // ——— HIDDEN INPUT ———

    function actualizarHidden() {
        const lista = [];
        for (const [numStr, sups] of Object.entries(odEstado)) {
            for (const [sup, estado] of Object.entries(sups)) {
                if (estado > 0) lista.push({
                    numeroDiente: parseInt(numStr), superficie: sup, estado
                });
            }
        }
        const inp = document.getElementById('odontogramaJson');
        if (inp) inp.value = JSON.stringify(lista);
    }

    window.OdontogramaJS = { cargarEstados, renderizar, calcularCPO, actualizarHidden };

})();