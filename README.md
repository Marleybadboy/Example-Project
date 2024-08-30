# Example-Project

## About
Example Project to prosta gra o zbieraniu zasobów i craftowaniu przedmiotów. Gracz może zbierać zasoby za pomocą siekiery oraz kilofa. Można swobonie ścinać drzewa w grze, które opadają po ścięciu w sposób generyczny.

### Input
1. WASD - poruszanie postacią
2. TAB - Otwarcie ekwipunku
3. ESC - Zamknięcie Ekwipunku
4. R - Usuwanie przedmiotu z ekwipunku, tylo kiedy kursor znajduje sie nad ikoną przedmiotu
5. LPM - Akcja czylu użycie przedmiotu lub/gdy postać ma wole rece podnosi przedmiot
6. Klawisze Numeryczne 1 - Siekiera 2 - Kilof 3 - chowanie przedmiotu

### Klasa State
Klasa State jest klasą abstrakcyjną, która definiuje podstawowy interfejs dla różnych stanów gry. Jest to część wzorca stanowego, który umożliwia zarządzanie różnymi trybami gry (takimi jak tryb inwentarza, tryb domyślny itp.).

#### Pola:

_controlInput: Obiekt typu InputControl, który jest serializowany do Unity i kontroluje wejścia użytkownika w danym stanie.

#### Metody:
1. ExecuteState(): Metoda wywołująca funkcję Execute na obiekcie _controlInput. Może być nadpisana przez klasy dziedziczące w celu dodania specyficznego zachowania dla każdego stanu.
2. ResetState(): Abstrakcyjna metoda odpowiedzialna za resetowanie stanu.
3. BindState(DiContainer container): Abstrakcyjna metoda służąca do bindowania stanu z kontenerem Zenject.
4. AssignValue(object value): Abstrakcyjna metoda używana do przypisywania wartości do stanu, np. inicjalizowania menadżera inventory.

> [!NOTE]
> Wszystkie klasy używają frameworka Zenject do wstrzykiwania zależności i zarządzania inicjalizacją. BindState, AssignValue oraz klasy pomocnicze jak ControleInjection używają Zenject do integracji stanu gry i innych komponentów.
 #### Baza danych stanów

<p align="center">
  <img src="Readme_Files/Unity_vriC9n1rnn.png" alt="Main Menu" width="300"/>
</p>
