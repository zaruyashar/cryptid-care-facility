document.addEventListener("DOMContentLoaded", function () {
    const tableBody = document.getElementById('tableBody');
    if (!tableBody) return;

    let allRows = Array.from(tableBody.querySelectorAll('tr[data-id]'));
    let pageSize = parseInt(document.getElementById('pageSizeSelect')?.value || 5);
    let currentPage = 1;
    let totalPages = 1;

    window.renderTable = function () {
        totalPages = Math.max(1, Math.ceil(allRows.length / pageSize));
        currentPage = Math.min(currentPage, totalPages);

        const start = (currentPage - 1) * pageSize;
        const end = Math.min(start + pageSize, allRows.length);

        allRows.forEach((row, i) => {
            row.style.display = (i >= start && i < end) ? '' : 'none';
            if (i >= start && i < end) {
                const idxCol = row.querySelector('.col-index');
                if (idxCol) idxCol.textContent = i + 1;
            }
        });

        const showingFrom = document.getElementById('showingFrom');
        if (showingFrom) showingFrom.textContent = allRows.length ? start + 1 : 0;
        const showingTo = document.getElementById('showingTo');
        if (showingTo) showingTo.textContent = end;
        const totalCount = document.getElementById('totalCount');
        if (totalCount) totalCount.textContent = allRows.length;

        renderPageNumbers();

        const btnFirst = document.getElementById('btnFirst');
        const btnPrev = document.getElementById('btnPrev');
        const btnNext = document.getElementById('btnNext');
        const btnLast = document.getElementById('btnLast');

        if (btnFirst) btnFirst.disabled = currentPage === 1;
        if (btnPrev) btnPrev.disabled = currentPage === 1;
        if (btnNext) btnNext.disabled = currentPage === totalPages;
        if (btnLast) btnLast.disabled = currentPage === totalPages;
    };

    function renderPageNumbers() {
        const container = document.getElementById('pageNumbers');
        if (!container) return;
        container.innerHTML = '';
        const range = 2;
        const pages = new Set();
        pages.add(1);
        pages.add(totalPages);
        for (let p = currentPage - range; p <= currentPage + range; p++) {
            if (p >= 1 && p <= totalPages) pages.add(p);
        }
        let sorted = Array.from(pages).sort((a, b) => a - b);
        let prev = 0;
        sorted.forEach(p => {
            if (p - prev > 1) {
                const dots = document.createElement('span');
                dots.style.cssText = 'color:#3a2a5a;padding:0 4px;align-self:center;font-size:12px;';
                dots.textContent = '…';
                container.appendChild(dots);
            }
            const btn = document.createElement('button');
            btn.className = 'page-btn' + (p === currentPage ? ' active' : '');
            btn.textContent = p;
            btn.onclick = () => window.goPage(p);
            container.appendChild(btn);
            prev = p;
        });
    }

    window.goPage = function (p) {
        currentPage = Math.max(1, Math.min(p, totalPages));
        window.renderTable();
    };

    const pageSizeSelect = document.getElementById('pageSizeSelect');
    if (pageSizeSelect) {
        pageSizeSelect.addEventListener('change', e => {
            pageSize = parseInt(e.target.value);
            currentPage = 1;
            window.renderTable();
        });
    }

    let sortCol = -1;
    let sortAsc = true;
    document.querySelectorAll('th.sortable').forEach(th => {
        th.addEventListener('click', () => {
            const col = parseInt(th.dataset.col);
            const type = th.dataset.type;

            if (sortCol === col) {
                sortAsc = !sortAsc;
            } else {
                sortCol = col;
                sortAsc = true;
            }

            document.querySelectorAll('th.sortable').forEach(h => {
                h.classList.remove('active');
                const icon = h.querySelector('.sort-icon');
                if (icon) icon.className = 'bi bi-arrow-down-up sort-icon';
            });

            th.classList.add('active');
            const thIcon = th.querySelector('.sort-icon');
            if (thIcon) thIcon.className = sortAsc ? 'bi bi-arrow-up sort-icon' : 'bi bi-arrow-down sort-icon';

            allRows.sort((a, b) => {
                let valA = a.cells[col]?.innerText.trim() || '';
                let valB = b.cells[col]?.innerText.trim() || '';

                if (type === 'num') {
                    let numA = parseFloat(valA.replace(/[^0-9.-]+/g, "")) || 0;
                    let numB = parseFloat(valB.replace(/[^0-9.-]+/g, "")) || 0;
                    return sortAsc ? (numA - numB) : (numB - numA);
                } else {
                    return sortAsc ? valA.localeCompare(valB) : valB.localeCompare(valA);
                }
            });

            allRows.forEach(row => tableBody.appendChild(row));
            currentPage = 1;
            window.renderTable();
        });
    });

    window.renderTable();

    const deleteModal = document.getElementById('deleteModal');
    window.confirmDelete = function (id, name, endpoint, customWarning) {
        const linkEl = document.getElementById('deleteConfirmLink');
        const textEl = document.getElementById('deleteModalText');

        if (linkEl) linkEl.href = endpoint + '/' + id;

        if (textEl && customWarning) {
            textEl.innerHTML = `This action will permanently demolish <strong id="deleteEntityName">${name}</strong>. ${customWarning}`;
        } else if (textEl) {
            textEl.innerHTML = `This action will permanently expunge <strong id="deleteEntityName">${name}</strong> from the registry. The void does not return what it takes.`;
        }

        if (deleteModal) deleteModal.classList.add('open');
    };

    window.closeDeleteModal = function () {
        if (deleteModal) deleteModal.classList.remove('open');
    };

    if (deleteModal) {
        deleteModal.addEventListener('click', e => {
            if (e.target === deleteModal) window.closeDeleteModal();
        });
    }

    window.exportPdf = function () {
        window.print();
    };
});