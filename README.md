Hier ist eine angepasste Version der README, die die von dir genannten Änderungen berücksichtigt:

# Datenbank-Benchmarking-Projekt

## Projektbeschreibung

Dieses Projekt wurde im Rahmen unserer Ausbildung zum **Fachinformatiker für Anwendungsentwicklung** während der Blockwochen-Phase an der Berufsschule erstellt. Unsere Aufgabe besteht darin, ein Programm zu entwickeln, das die **Performance von PostgreSQL-Datenbanken** benchmarkt. Konkret soll der **Unterschied in der Performance** gemessen werden, wenn Datensätze entweder als **Objekte** (verschachtelte Strukturen) oder **relational** in Form von **verschiedenen Tabellen** gespeichert werden.

## Ziel des Projekts

Das Ziel dieses Projekts ist es, die Effizienz der beiden verschiedenen Datenmodellierungsansätze zu vergleichen:

- **Objektbasierte Speicherung**: Datensätze werden als verschachtelte Objekte (z. B. JSON-Datenstrukturen) in einer einzelnen Tabelle gespeichert.
- **Relationale Speicherung**: Die gleiche Information wird über mehrere Tabellen verteilt und mit Fremdschlüsseln verbunden.

Anhand von **Messungen** wie der Einfügezeit, Abrufzeit und der Speichergröße wollen wir herausfinden, welcher Ansatz unter verschiedenen Bedingungen besser performt.

## Voraussetzungen

Für die erfolgreiche Ausführung des Programms werden die folgenden Technologien und Tools benötigt:

- **PostgreSQL**: Wir verwenden zwei separate PostgreSQL-Datenbanken, die jeweils in einem eigenen Docker-Container laufen.
- **Entity Framework Core**: Wird intern zur Kommunikation mit den beiden Datenbanken verwendet.
- **Docker**: Zum Starten und Verwalten der PostgreSQL-Datenbanken.
- **JSON-Datei**: Testdatensätze sind vordefiniert und in einer JSON-Datei gespeichert.

### Technologie-Stack:
- **Programmiersprache**: C# 
- **Framework**: .NET Core
- **ORM**: Entity Framework Core
- **Datenbank**: PostgreSQL (in Docker-Containern)
- **Container-Orchestrierung**: Docker

## Funktionsumfang

Unser Programm umfasst folgende Funktionen:

1. **Datenbankverbindung herstellen**: Das Programm stellt über Entity Framework Core eine Verbindung zu zwei getrennten PostgreSQL-Datenbanken her, die jeweils in einem Docker-Container laufen. Eine Datenbank speichert die Testdaten **objektbasiert**, die andere **relational**.

2. **Testdatensätze laden**: 
   - Die Testdatensätze sind in einer **JSON-Datei** gespeichert, die beim Start des Programms geladen wird.
   - Es handelt sich dabei um verschachtelte Datensätze, die sowohl für die objektbasierte als auch für die relationale Speicherung genutzt werden.

3. **Daten speichern**: 
   - Die geladenen Datensätze werden entweder in einer einzelnen Tabelle (objektbasierte Speicherung) oder in mehreren Tabellen (relationale Speicherung) abgelegt.
   - Entity Framework Core sorgt dabei für die Kommunikation mit den jeweiligen Datenbanken und führt die entsprechenden **Insert**-Operationen aus.

4. **Performance-Benchmarking**: 
   - **Einfügezeit**: Wie lange dauert es, eine große Anzahl von Datensätzen zu speichern?
   - **Abrufzeit**: Wie lange dauert es, diese Datensätze wieder aus der Datenbank abzufragen?
   - **Speicherplatz**: Wie viel Speicherplatz wird für die Daten benötigt?

5. **Ergebnisse vergleichen**: 
   - Nach Durchführung der Benchmarks werden die Ergebnisse in einer übersichtlichen Form präsentiert, damit der Unterschied zwischen den beiden Speicheransätzen (objektbasiert vs. relational) erkennbar wird.

## Aufbau und Nutzung des Programms

### 1. **Docker-Container starten**
   - Stelle sicher, dass **Docker** installiert ist.
   - Die beiden PostgreSQL-Datenbanken werden in separaten Docker-Containern ausgeführt. Um diese zu starten, navigiere zum Verzeichnis, das die Docker-Konfigurationen enthält, und führe den Befehl aus:

     ```bash
     docker-compose up
     ```

   - Dies startet die beiden Container für die relationalen und objektbasierten Datenbanken.

### 2. **Programm ausführen**
   - Bringe die lokalen Datenbanken in den vorgesehenen Ausgangszustand:

     ```bash
     dotnet ef database update -
     ```

   - Das Programm lädt die Datensätze aus der JSON-Datei und führt die Benchmarks gegen die beiden Datenbanken durch.

### 3. **Programm ausführen**
   - Stelle sicher, dass die **JSON-Datei** mit den Testdatensätzen im Programmverzeichnis vorhanden ist.
   - Starte das Benchmark-Programm über die Kommandozeile oder eine IDE (wie Visual Studio oder JetBrains Rider):

     ```bash
     dotnet run
     ```

   - Das Programm lädt die Datensätze aus der JSON-Datei und speichert diese entsprechend in den Datenbanken.

### 4. **Tests ausführen**
   - Nach dem Setup können beliebige Tests aus dem Test-Projekt ausgeführt, und die Ergebnisse interpretiert werden.

## Projektstruktur

```
/Projektverzeichnis
├── /Bücherei.Cli                # CLI-Projekt, lädt JSON-Daten und speichert diese in die Datenbanken
    ├── Program.cs               # Hauptprogramm
    ├── sample-data.json         # Beispiel-Datensatz
├── /Bücherei.Lib                # Bibliothek mit Geschäftslogik
    ├── Contexts                 # Entity Framework Core Datenbankkontexte
    ├── EntitiesDocument         # Entitäten für die dokumentenbasierte Speicherung
    ├── EntitiesRelational       # Entitäten für die relationale Speicherung
    ├── Migrations               # EF Core Migrations
    ├── Services                 # Logik für Datenoperationen
├── /Bücherei.Tests              # Tests für die Anwendung
    ├── DocumentTests.cs         # xUnit-Tests für die dokumentenbasierte Speicherung
    ├── RelationalTests.cs       # xUnit-Tests für die relationale Speicherung
├── Bücherei.sln                 # Solution-Datei des Projekts
└── README.md                    # Diese README-Datei
```

## Fazit

Mit diesem Projekt wollen wir ein tieferes Verständnis für die Unterschiede zwischen **objektbasierter** und **relationaler** Datenhaltung in PostgreSQL entwickeln. Wir lernen, wie sich unterschiedliche Datenmodellierungsansätze auf die **Performance** auswirken und welche Technologien sich für welche Anwendungsfälle besser eignen.

## Team

- **Teammitglieder**: [Name 1], [Name 2], [Name 3]  
- **Berufsschule**: Robert-Bosch-Berufskolleg, Dortmund
- **Ausbildung**: Fachinformatiker/in für Anwendungsentwicklung

## Lizenz

Dieses Projekt wurde im Rahmen einer schulischen Aufgabe erstellt und steht unter keiner bestimmten Lizenz.
