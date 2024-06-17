Aplikacja do szacowania kosztów. 

## Wymagania

- .NET SDK 6.0 SDK 
- SQL Server
- SQL Server Management Studio (SSMS) lub inny klient SQL

1. Klonowanie repozytorium:
1. Aby sklonować repozytorium należy, użyć poniższego polecenia:
git clone https://github.com/Opalinsky/CostEstimationApp.git

2. Konfiguracja bazy danych:
2.1. Po otwarciu pliku w visual studio, otwarcie pliku "appsettings.json"
2.2 Dostosowanie connectionstring dodając dane połączenia do swojej bazy danych

Przykład:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CostEstimationApp;Trusted_Connection=True;"
}

3. Pobranie bazy danych
3.1 Pobranie pliku kopii zapasowej CostEstimationDb.bak
3.2 Zalogowanie się do do SQL Managment Studio (SSMS)
3.3. Przywrócenie bazy danych 
  3.3.1 Kliknięcie prawym przyciskiem myszy na węzeł "Database" i wybranie "Restore database" aby wskazać plik .bak
  3.3.2. W oknie `Restore Database', wybranie `Device` i kliknięcie przycisku `...`, aby wskazać lokalizację pliku `.bak`.
  3.3.2 Kliknięcie Add i wskazanie na plik CostEstimationDb.bak i kliknięcie ok
  3.3.3 Upewnienie się, że zaznaczono opcję "Restore" i kliknięcie ok aby rozpocząc przywracanie bazy danych
  3.3.4 W celu sprawdzenia czy baza danych została przywrócona poprawnie trzeba upewnić się, że jest widoczna pod węzłem "Databases"

5. Uruchomienie aplikacji
  4.1 W celu zaktualizowania bazy danych wpisanie:
   dotnet ef database update
4.2 Projekt jest gotowy do Zdebuggowania. 
