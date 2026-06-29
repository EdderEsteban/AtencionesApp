document.addEventListener("DOMContentLoaded", function () {
    // ── FILTROS: botones de período ──
    const inputPeriodo = document.getElementById("inputPeriodo");
    const formFiltros = document.getElementById("formFiltros");
    const rangoPers = document.getElementById("rangoPersonalizado");

    document.querySelectorAll(".periodo-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const p = this.dataset.periodo;
            if (p === "personalizado") {
                rangoPers.classList.remove("d-none");
                inputPeriodo.value = "personalizado";
                document.querySelectorAll(".periodo-btn").forEach(b => b.classList.remove("active"));
                this.classList.add("active");
            } else {
                inputPeriodo.value = p;
                formFiltros.submit();
            }
        });
    });

    // ── DATOS ──
    const el = document.getElementById("reporteData");
    if (!el || !document.getElementById("chartActividad")) return;

    const parse = k => JSON.parse(el.dataset[k]);
    const num = k => parseInt(el.dataset[k] || "0", 10);

    const css = getComputedStyle(document.body);
    const colorText = css.getPropertyValue("--content-text").trim() || "#333";
    const gridColor = "rgba(128,128,128,0.15)";

    Chart.defaults.color = colorText;
    Chart.defaults.font.family = "'DM Sans', sans-serif";

    // acento según el tipo de reporte (data-accent: "enf" | "odo")
    const esOdo = el.dataset.accent === "odo";
    const ACCENT = esOdo ? "#8b5cf6" : "#10b981";

    const donutOpts = { responsive: true, plugins: { legend: { position: "bottom" } } };
    const barOpts = {
        responsive: true,
        plugins: { legend: { display: false } },
        scales: {
            x: { grid: { display: false } },
            y: { beginAtZero: true, ticks: { precision: 0 }, grid: { color: gridColor } }
        }
    };

    // ── 1. Actividad en el tiempo ──
    new Chart(document.getElementById("chartActividad"), {
        type: "bar",
        data: {
            labels: parse("labelsActividad"),
            datasets: [{
                data: parse("serieActividad"), backgroundColor: ACCENT, borderRadius: 5,
                maxBarThickness: 40
            }]
        },
        options: barOpts
    });

    // ── 2. Tipo (atención / consulta) — donut ──
    const tipoCanvas = document.getElementById("chartTipo");
    if (tipoCanvas) {
        new Chart(tipoCanvas, {
            type: "doughnut",
            data: {
                labels: parse("tipoLabels"),
                datasets: [{ data: parse("tipoValores"), backgroundColor: ["#3b82f6", "#f59e0b"] }]
            },
            options: donutOpts
        });
    }

    // ── 3. Cobertura OS — donut ──
    new Chart(document.getElementById("chartOS"), {
        type: "doughnut",
        data: {
            labels: ["Con Obra Social", "Sin Obra Social"],
            datasets: [{
                data: [num("conOs"), num("sinOs")], backgroundColor: ["#10b981", "#ef4444"]
            }]
        },
        options: donutOpts
    });

    // ── 4. Sexo — donut ──
    new Chart(document.getElementById("chartSexo"), {
        type: "doughnut",
        data: {
            labels: ["Masculino", "Femenino", "No especificado"],
            datasets: [{
                data: [num("sexoM"), num("sexoF"), num("sexoNoEsp")], backgroundColor:
                    ["#3b82f6", "#ec4899", "#94a3b8"]
            }]
        },
        options: donutOpts
    });

    // ── 5. Franjas etarias — barras ──
    new Chart(document.getElementById("chartEdad"), {
        type: "bar",
        data: {
            labels: parse("labelsEdad"),
            datasets: [{
                data: parse("serieEdad"), backgroundColor: "#8b5cf6", borderRadius: 5,
                maxBarThickness: 50
            }]
        },
        options: barOpts
    });

    // ── 6. Top 10 prestaciones — barras horizontales ──
    new Chart(document.getElementById("chartTop10"), {
        type: "bar",
        data: {
            labels: parse("top10Nombres"),
            datasets: [{ data: parse("top10Cantidades"), backgroundColor: ACCENT, borderRadius: 5 }]
        },
        options: {
            indexAxis: "y",
            responsive: true,
            plugins: { legend: { display: false } },
            scales: {
                x: { beginAtZero: true, ticks: { precision: 0 }, grid: { color: gridColor } },
                y: { grid: { display: false } }
            }
        }
    });

    // ── 7. Tipo de turno (solo odonto) — barras ──
    const turnoCanvas = document.getElementById("chartTurno");
    if (turnoCanvas) {
        new Chart(turnoCanvas, {
            type: "bar",
            data: {
                labels: ["Ventanilla", "Profesional", "Demanda", "Interdisc."],
                datasets: [{
                    data: parse("turnoValores"), backgroundColor: "#06b6d4", borderRadius:
                        5, maxBarThickness: 50
                }]
            },
            options: barOpts
        });
    }

    // ── 8. Top 10 diagnósticos (solo odonto) — barras horizontales ──
    const diagCanvas = document.getElementById("chartDiag");
    if (diagCanvas) {
        new Chart(diagCanvas, {
            type: "bar",
            data: {
                labels: parse("top10DiagNombres"),
                datasets: [{
                    data: parse("top10DiagCantidades"), backgroundColor: "#8b5cf6",
                    borderRadius: 5
                }]
            },
            options: {
                indexAxis: "y",
                responsive: true,
                plugins: { legend: { display: false } },
                scales: {
                    x: { beginAtZero: true, ticks: { precision: 0 }, grid: { color: gridColor } },
                    y: { grid: { display: false } }
                }
            }
        });
    }
});
