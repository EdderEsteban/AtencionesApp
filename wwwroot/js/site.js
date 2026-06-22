const html = document.documentElement;
const saved = localStorage.getItem('theme') || 'light';
html.setAttribute('data-bs-theme', saved);

document.getElementById('themeToggle').addEventListener('click', () => {
    const current = html.getAttribute('data-bs-theme');
    const next = current === 'dark' ? 'light' : 'dark';
    html.setAttribute('data-bs-theme', next);
    localStorage.setItem('theme', next);
});

// Avatar con iniciales
const avatarEl = document.getElementById('userAvatar');
if (avatarEl) {
    const name = document.querySelector('.navbar-user-name')?.textContent?.trim() || '';
    const parts = name.split(' ');
    const initials = parts.length >= 2
        ? parts[0][0] + parts[1][0]
        : name.substring(0, 2);
    avatarEl.textContent = initials.toUpperCase();
}