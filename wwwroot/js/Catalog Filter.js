document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const genreFilter = document.getElementById('genreFilter');
    const gameCards = document.querySelectorAll('.game-card');
    const noResultsMessage = document.getElementById('noResultsMessage');

    function filterGames() {
        const searchValue = searchInput.value.toLowerCase().trim();
        const selectedGenre = genreFilter.value;
        let visibleCount = 0;
        gameCards.forEach(card => {

            const title = card.getAttribute('data-title');
            const genre = card.getAttribute('data-genre');
            const matchesSearch = title.includes(searchValue);
            const matchesGenre = (selectedGenre === 'ALL' || genre === selectedGenre);

            if (matchesSearch && matchesGenre) {
                card.style.display = 'block'; 
                visibleCount++;
            } else {
                card.style.display = 'none'; 
            }
        });
        if (visibleCount === 0) {
            noResultsMessage.classList.remove('d-none');
        } else {
            noResultsMessage.classList.add('d-none');
        }
    }
    searchInput.addEventListener('input', filterGames);
    
    genreFilter.addEventListener('change', filterGames);
});