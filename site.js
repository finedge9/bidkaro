// BidKaro - site.js

document.addEventListener('DOMContentLoaded', () => {

    // ---- Countdown Timers ----
    function parseTime(str) {
        const parts = str.split(':').map(Number);
        return parts[0] * 3600 + parts[1] * 60 + parts[2];
    }
    function formatTime(s) {
        const h = Math.floor(s / 3600);
        const m = Math.floor((s % 3600) / 60);
        const sec = s % 60;
        return `${String(h).padStart(2,'0')}:${String(m).padStart(2,'0')}:${String(sec).padStart(2,'0')}`;
    }
    document.querySelectorAll('.timer').forEach(el => {
        let secs = parseTime(el.getAttribute('data-time') || el.textContent);
        setInterval(() => {
            secs = Math.max(0, secs - 1);
            el.textContent = formatTime(secs);
            if (secs < 300) { el.style.color = '#dc2626'; el.style.animation = 'pulse 1s infinite'; }
            if (secs === 0) { el.textContent = 'ENDED'; el.style.color = '#6b7280'; }
        }, 1000);
    });

    // ---- Price Range Slider ----
    const priceRange = document.getElementById('priceRange');
    const priceVal = document.getElementById('priceVal');
    if (priceRange && priceVal) {
        priceRange.addEventListener('input', () => {
            const v = parseInt(priceRange.value);
            priceVal.textContent = '₹' + (v >= 100000 ? (v/100000).toFixed(1) + ' L' : v.toLocaleString('en-IN'));
        });
    }

    // ---- Grid / List Toggle ----
    const gridBtn = document.getElementById('gridView');
    const listBtn = document.getElementById('listView');
    const grid = document.getElementById('auctionGrid');
    if (gridBtn && listBtn && grid) {
        gridBtn.addEventListener('click', () => {
            grid.classList.remove('list-mode');
            gridBtn.classList.add('active');
            listBtn.classList.remove('active');
        });
        listBtn.addEventListener('click', () => {
            grid.classList.add('list-mode');
            listBtn.classList.add('active');
            gridBtn.classList.remove('active');
        });
    }

    // ---- Mobile Nav ----
    const hamburger = document.getElementById('hamburger');
    if (hamburger) {
        hamburger.addEventListener('click', () => {
            const navLinks = document.querySelector('.nav-links');
            if (navLinks) {
                navLinks.style.display = navLinks.style.display === 'flex' ? 'none' : 'flex';
                navLinks.style.flexDirection = 'column';
                navLinks.style.position = 'absolute';
                navLinks.style.top = '68px';
                navLinks.style.left = '0';
                navLinks.style.width = '100%';
                navLinks.style.background = '#fff';
                navLinks.style.padding = '16px';
                navLinks.style.boxShadow = '0 8px 24px rgba(0,0,0,.1)';
                navLinks.style.zIndex = '999';
            }
        });
    }

    // ---- Sticky Nav shadow ----
    window.addEventListener('scroll', () => {
        const nav = document.querySelector('.navbar');
        if (nav) {
            nav.style.boxShadow = window.scrollY > 10
                ? '0 4px 24px rgba(0,0,0,.15)'
                : '0 2px 12px rgba(0,0,0,.08)';
        }
    });

    // ---- Watchlist hearts ----
    document.querySelectorAll('.btn-watchlist').forEach(btn => {
        btn.addEventListener('click', () => {
            const icon = btn.querySelector('i');
            if (icon.classList.contains('far')) {
                icon.classList.replace('far', 'fas');
                btn.style.color = '#dc2626';
                btn.style.background = '#fee2e2';
            } else {
                icon.classList.replace('fas', 'far');
                btn.style.color = '';
                btn.style.background = '';
            }
        });
    });

    // ---- Password Toggle ----
    window.togglePass = (id) => {
        const inp = document.getElementById(id);
        if (inp) inp.type = inp.type === 'password' ? 'text' : 'password';
    };

    // ---- Tab switcher ----
    document.querySelectorAll('.tab-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
        });
    });

    // ---- Animate stats on scroll ----
    const statsObserver = new IntersectionObserver((entries) => {
        entries.forEach(e => {
            if (e.isIntersecting) {
                e.target.style.opacity = '1';
                e.target.style.transform = 'translateY(0)';
            }
        });
    }, { threshold: 0.1 });

    document.querySelectorAll('.step-card, .testi-card, .cat-card, .auction-card').forEach(el => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(20px)';
        el.style.transition = 'opacity .5s ease, transform .5s ease';
        statsObserver.observe(el);
    });
});
