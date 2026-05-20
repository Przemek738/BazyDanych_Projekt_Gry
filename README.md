# BazyDanych_Projekt_Gry

Cel projektu:
Naszym celem jest zaprojektowanie relacyjnej bazy danych oraz stworzenie opierającej się na niej aplikacji webowej, pełniącej rolę osobistego menedżera biblioteki gier. System pozwoli użytkownikom na budowanie własnego katalogu posiadanych tytułów, śledzenie postępów w rozgrywce oraz dzielenie się opiniami.

Kluczowe funkcjonalności, które planujemy wdrożyć:
- Przeglądanie globalnego katalogu gier wideo.
- Dodawanie gier do własnej biblioteki z przypisaniem platformy.
- System ocen i dodawania krótkich recenzji tekstowych do ukończonych tytułów.
- Wyszukiwanie i filtrowanie gier po gatunkach, platformach i statusach.

Uruchomienie Aplikacji:
1. Pobranie plików.
2. Zbudowanie obrazu Dockera przez konsole: docker build -t backlog-manager .
3. Uruchomienie kontenera: docker run -d -p 8080:8080 --name proj-backlog backlog-manager,
   (Opcjonalnie do zapisywanaia bazy): docker run -d -p 8080:8080 -v backlog_data:/app/Data --name proj-backlog backlog-manager
