let savedUrl = '';
document.getElementById('usePlaceholderCheck').addEventListener('change', function () {
    var urlInput = document.getElementById('imageUrlInput');

    if (this.checked) {
        savedUrl = urlInput.value;
        urlInput.value = '/lib/Images/placeholder.jpg';
        urlInput.readOnly = true;
    } else {
        urlInput.value = savedUrl;

        urlInput.readOnly = false;
    }
});