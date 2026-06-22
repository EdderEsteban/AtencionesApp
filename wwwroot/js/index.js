const el = document.getElementById('dashboard-data');
  const labels7   = JSON.parse(el.dataset.labels7);
  const serieEnf  = JSON.parse(el.dataset.serieenf);
  const serieOdo  = JSON.parse(el.dataset.serieodo);
  const donutLbls = JSON.parse(el.dataset.donutlbls);
  const donutVals = JSON.parse(el.dataset.donutvals);
  const labels6m  = JSON.parse(el.dataset.labels6m);
  const serieM    = JSON.parse(el.dataset.seriem);
  const top10nom  = JSON.parse(el.dataset.top10nom);
  const top10cant = JSON.parse(el.dataset.top10cant);

  const accent  = '#7c6af7';
  const accent2 = '#22d3ee';
  const accentA = 'rgba(124,106,247,0.15)';
  const hayOdo  = serieOdo.length > 0;

  const datasetsBarras = [
      { label: 'Enfermería', data: serieEnf, backgroundColor: accent, borderRadius: 4 }
  ];
  if (hayOdo) {
      datasetsBarras.push({ label: 'Odontología', data: serieOdo, backgroundColor: accent2,
  borderRadius: 4 });
  }
  new Chart(document.getElementById('chartBarras'), {
      type: 'bar',
      data: { labels: labels7, datasets: datasetsBarras },
      options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: { legend: { display: hayOdo } },
          scales: {
              x: { stacked: hayOdo, grid: { display: false } },
              y: { stacked: hayOdo, beginAtZero: true, ticks: { precision: 0 } }
          }
      }
  });

  new Chart(document.getElementById('chartDonut'), {
      type: 'doughnut',
      data: {
          labels: donutLbls,
          datasets: [{ data: donutVals, backgroundColor: [accent, accent2], hoverOffset: 6 }]
      },
      options: {
          responsive: true,
          maintainAspectRatio: false,
          cutout: '65%',
          plugins: { legend: { position: 'bottom' } }
      }
  });

  new Chart(document.getElementById('chartLinea'), {
      type: 'line',
      data: {
          labels: labels6m,
          datasets: [{
              label: 'Atenciones',
              data: serieM,
              borderColor: accent,
              backgroundColor: accentA,
              fill: true,
              tension: 0.4,
              pointBackgroundColor: accent
          }]
      },
      options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: { legend: { display: false } },
          scales: {
              x: { grid: { display: false } },
              y: { beginAtZero: true, ticks: { precision: 0 } }
          }
      }
  });

  new Chart(document.getElementById('chartTop10'), {
      type: 'bar',
      data: {
          labels: top10nom,
          datasets: [{ label: 'Cantidad', data: top10cant, backgroundColor: accent, borderRadius: 4
  }]
      },
      options: {
          indexAxis: 'y',
          responsive: true,
          maintainAspectRatio: false,
          plugins: { legend: { display: false } },
          scales: {
              x: { beginAtZero: true, ticks: { precision: 0 } },
              y: { grid: { display: false } }
          }
      }
  });