const quickAddButtons = document.querySelectorAll('.quick-add-btn');

quickAddButtons.forEach(btn => {
    btn.addEventListener('click', function() {
        if (this.disabled) return;

        const gameId = this.getAttribute('data-game-id');
        const currentButton = this;
        currentButton.innerHTML = '<span class="spinner-border spinner-border-sm"></span> Dodawanie...';
        currentButton.disabled = true;
        fetch(`/UserGames/QuickAdd?gameId=${gameId}`, {
            method: 'POST'
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    currentButton.classList.remove('btn-outline-success');
                    currentButton.classList.add('btn-success'); 
                    currentButton.innerHTML = '<i class="fa-solid fa-check"></i> Dodano';
                } else {
                    alert(data.message);
                    currentButton.innerHTML = '+ Do biblioteki';
                    currentButton.disabled = false;
                }
            })
            .catch(error => {
                console.error('Błąd:', error);
                currentButton.innerHTML = '+ Do biblioteki';
                currentButton.disabled = false;
            });
    });
});