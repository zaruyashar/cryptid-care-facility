document.addEventListener("DOMContentLoaded", function () {
    const imgInput = document.getElementById('imageUrlInput');
    const imgPreview = document.getElementById('imagePreview');
    const imgHolder = document.getElementById('imagePlaceholder');

    window.updatePreview = function (url) {
        if (!imgPreview || !imgHolder) return;
        if (url) {
            imgPreview.src = url;
            imgPreview.classList.add('visible');
            imgPreview.style.display = 'block';
            imgHolder.style.display = 'none';
        } else {
            imgPreview.classList.remove('visible');
            imgPreview.style.display = 'none';
            imgHolder.style.display = 'flex';
        }
    };

    if (imgInput) {
        imgInput.addEventListener('input', e => window.updatePreview(e.target.value));
    }

    if (imgPreview) {
        imgPreview.addEventListener('error', () => {
            imgPreview.classList.remove('visible');
            imgPreview.style.display = 'none';
            if (imgHolder) imgHolder.style.display = 'flex';
        });
        imgPreview.addEventListener('load', () => {
            imgPreview.classList.add('visible');
            imgPreview.style.display = 'block';
            if (imgHolder) imgHolder.style.display = 'none';
        });
    }

    const expungeModal = document.getElementById('expungeModal');
    const expungeBox = document.getElementById('expungeBox');

    window.confirmExpunge = function (id, name, endpoint, customWarning) {
        const textEl = document.getElementById('expungeText');
        const linkEl = document.getElementById('expungeLink');

        if (textEl) {
            const warningMsg = customWarning ? customWarning : 'from all records.';
            textEl.innerHTML = `This will permanently expunge <strong style="color:#c84b31">${name}</strong> ${warningMsg}`;
        }
        if (linkEl) {
            linkEl.href = `${endpoint}/${id}`;
        }
        if (expungeModal) {
            expungeModal.style.opacity = '1';
            expungeModal.style.pointerEvents = 'all';
        }
        if (expungeBox) {
            expungeBox.style.transform = 'scale(1)';
        }
    };

    window.closeExpunge = function () {
        if (expungeModal) {
            expungeModal.style.opacity = '0';
            expungeModal.style.pointerEvents = 'none';
        }
        if (expungeBox) {
            expungeBox.style.transform = 'scale(0.95)';
        }
    };

    if (expungeModal) {
        expungeModal.addEventListener('click', e => {
            if (e.target === expungeModal) window.closeExpunge();
        });
    }

    document.querySelectorAll('.field-input, .field-select, .field-textarea').forEach(el => {
        el.addEventListener('focus', () => {
            const label = el.closest('.field-group')?.querySelector('.field-label');
            if (label) label.style.setProperty('color', '#7b2d8b');
        });
        el.addEventListener('blur', () => {
            const label = el.closest('.field-group')?.querySelector('.field-label');
            if (label) label.style.setProperty('color', '#6a5a8a');
        });
    });
});