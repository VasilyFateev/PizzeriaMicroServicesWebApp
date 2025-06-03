document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.product-card [data-product-id]').forEach(card => {
        const productId = card.dataset.productId;

        card.addEventListener('click', function (e) {
            e.preventDefault();
            openProductModal(productId);
        });
    });
});

function openProductModal(productId) {
    fetch(`/Storefront/GetProductDetalisation?id=${productId}`)
        .then(response => response.text())
        .then(html => {
            document.body.insertAdjacentHTML('beforeend', html);
            const modal = document.querySelector('.product-modal:last-of-type');
            initModal(modal);
        });
}

function initModal(modal) {
    modal.style.display = 'flex';

    modal.querySelector('.close-modal').addEventListener('click', () => {
        modal.style.display = 'none';
        setTimeout(() => modal.remove(), 300);
    });

    modal.querySelector('.modal-overlay').addEventListener('click', () => {
        modal.style.display = 'none';
        setTimeout(() => modal.remove(), 300);
    });

    initOptionSelection(modal);
}

function initOptionSelection(modal) {
    const optionItems = modal.querySelectorAll('.option-item:not(.disabled)');

    optionItems.forEach(item => {
        item.addEventListener('click', () => {
            const variationId = item.dataset.variationId;
            const optionId = item.dataset.optionId;

            modal.querySelectorAll(`.option-item[data-variation-id="${variationId}"]`)
                .forEach(opt => opt.classList.remove('selected'));

            item.classList.add('selected');

            checkCompleteSelection(modal);
        });
    });
}

function checkCompleteSelection(modal) {
    const selectedOptions = modal.querySelectorAll('.option-item.selected').length;
    const totalVariations = modal.querySelectorAll('.variation-section').length;
    const addButton = modal.querySelector('.add-to-cart');

    addButton.disabled = selectedOptions !== totalVariations;
}