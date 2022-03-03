# Lj2Dd1En2
## Introductie
Deze repository bevat alle programmacode en documentatie die gebruikt en gemaakt worden voor de vakken DD1 en DD2 van leerjaar 2 Software Development.

De code en documentatie is in een aantal branches ondergebracht. Deze branches komen overeen met de opdrachten uit de reader.

### Opdracht1: Heractiveren kennis Leerjaar 1. 
Studenten gaan zelfstandig aan de slag met een CRUD applicatie, op basis van de kennis die ze nog hebben. Na de vakantie zal deze kennis vaak weggezakt zijn.  

|***Branch***   ||
|:---           |---|
|Opdracht01Cars |Script voor een MySql database (carsdb.sql). |
|               |Startcode voor de te maken WPF applicatie. Wijs studenten erop dat het hier om een .**Net Core** applicatie gaat. |

**Opmerkingen:**
- Het database script maakt ook de database aan met de naam carsdb. Levert dit problemen op, dan maakt de student zelf een database met deze naam. Uit het script wordt dan alleen het aanmaken van de tabellen en indexen overgenomen.
- De startcode voor de WPF applicatie bevat de XAML lay-out. De .cs code is gedeeltelijk gegeven (lege event handlers, en code om een afbeelding in te lezen) 

### Opdracht2: Oefenen met kwaliteitstandaarden.
Studenten verbeteren het programma, zodat het voldoet aan de kwaliteitseisen. Het gaat hierbij om de regels waarmee de studenten in leerjaar 1 kennis hebben gemaakt, zoals het toepassen van logische namen, gebruik van hoofd- en kleine letters, toepassen van een database class, juiste types toepassen, duidelijke instructies bij fouten, commentaar etc. Ook deze regels moeten weer geactiveerd worden. 

|***Branch***         ||
|:---                 |---|
|Opdracht02Start      |Bevat de code die de studenten moeten beoordelen en verbeteren|
|Opdracht02Uitwerking |De voorbeelduitwerking|

**Opmerkingen:**
- De code maakt gebruik van een MySql database (NuGet package MySql.Data). 
- De startcode werkt. Studenten hoeven hier dus geen fouten uit te halen omdat de code het niet zou doen. Studenten verbeteren code uitsluitend op basis van de gestelde kwaliteitseisen. Deze verbeteringen vinden zowel plaats in de XAML als in de .cs code. 
- Er treden om diverse redenen exceptions plaats. Bijvoorbeeld als gegevens niet goed door de gebruiker zijn ingevuld. De student zal deze met het oog op de kwaliteit wel moeten aanpassen.
- Het refreshen gebeurt door het MainWindow te sluiten en opnieuw te starten. Deze aanpak zie ik vaak bij studenten; hij geeft een onrustig beeld wat ze anders moeten gaan oplossen.


## Los Pollos Hermanos
Na deze eerste 2 opdrachten gaan de studenten aan de slag met een opdracht voor restaurant Los Pollos Hermanos. Doel is dat de studenten volgens een Scrum aanpak een applicatie maken. Belangrijk is dus dat ze niet eerst een compleet ontwerp en programma maken, maar dit ontwerp en programma laten ontstaan volgens de volgorde waarin de product owner, user story's in de product backlog heeft gezet. Deze user story's staan in een KanBan in deze repository. 

Studenten leren het volgende:
- Documentatie (ontwerpen)
  - Maken van een use cases diagram.
  - Maken van een genormaliseerd datamodellen voor de applicatie, bestaande uit een beschrijving en ERD.
 - Databases
   - Een database maken, inclusief tabellen met kolommen, indexen en relaties tussen de tabellen.
   - SQL toepassen: SELECT (inclusief INNER JOIN), INSERT, UPDATE en DELETE.
- Programmeren
  - Database class: connection strings, foutafhandeling, dataconversie (database types - c# types).
  - Model classes : properties, afleidbare gegevens (zoals leeftijd).
  - Binding       : properties, ObservableCollection, INotifyPropertyChanged.

### Opdracht 3: Inrichten ontwikkelomgeving
Dit is een opdracht die ervoor zorgt dat de studenten over alle templates e.d. beschikken die nodig zijn voor het maken van de de vervolgopdrachten. 
  - Voor Visio worden templates voor een class diagram, een use case diagram en een ERD diagram klaargezte
  - In msWord wordt een template voor een datamodelbeschrijving klaargezet

|***Branch***||
|:---|---|
|Opdracht03Ontwikkelomgeving|Visio|
||Databaseprogramma voor MySql, bijvoorbeeld XAMPP|
||VisualStudio 2022|
||Folder met naam Documentatie. Hierin worden de Visio (ERD, Use Case, Class Diagram) en MsWord (Datamodel) templates gezet.|
||Script voor een lege MySql database (losPolloshermanos.sql)|
||Lege WPF Core applicatie met de naam Lj2Dd1En2.|

### Opdracht 4: Maken Configbestand
Met deze opdracht leert de student een config bestand te maken voor de connectionstring van een database.

|***Branch***||
|:---|---|
|Opdracht04Configbestand|Config bestand wordt toegevoegd|
||Databaseclass voor een MySql database die gebruik maakt van het ConfigBestand|

**Opmerking:**
- Het Config bestand moet de student zelf toevoegen. Is dat niet het geval, dan werkt hij niet met de core versie van WPF. Hij zal dan een nieuwe WPF programma moeten maken (oude kan weg, is nog niks aan gedaan) 

### Opdracht 5: Userstory RestaurantDisplay
In deze opdracht gaat de student een eenvoudige use case en een datamodel maken. Veel wordt voorgedaan. Het gaat erom dat de student meedoet, zodat hij dit bij een volgende opdracht zelf kan doen. Student moet zo snel mogelijk aan de slag met programmeren. Belangrijkste is de _databaseclass_, de _domain class_ en _binding_. In deze opdracht wordt veel voorgedaan (gezamenlijk programmeren). Studenten zorgen dat ze alles begrijpen, bij blijven en hun persoonlijke leerdoelen noteren.

|***Branch***||
|:---|---|
|Opdracht05.1.UseCase     |Bevat bijgewerkte Use Case |
|Opdracht05.2.Datamodel   |Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database |
|Opdracht05.3.WpfDatabase |Bevat WPF applicatie met uitgewerkte database class |
|Opdracht05.4.WpfDisPlay  |Bevat WPF applicatie met data gebind aan de XAML |


### Opdracht 6: Userstory IngrediëntenInzicht
In deze opdracht gaat de student geheel zelfstandig een eenvoudige use case en datamodel maken. De bestaande documentatie wordt uitgebreid. Daarnaast wordt ook het programma uitgebreid. De gebruiker zal afhankelijk van zijn rol een keuze moeten maken tussen het Display en het Ingredientenoverzicht. De student moet begrijpen dat dit niet automatisch betekent dat hij een inlogscherm of iets dergelijk gaat maken, maar dat hij de mogelijkheden met de productowner bespreekt en hem een keuze laat maken. In dit geval zal de productowner kiezen voor een eenvoudig keuzescherm, waarbij procedureel wordt geregeld dat actors alleen die functies gebruiken die zij nodig hebben. 


|***Branch***||
|:---|---|
| Opdracht06.1.UseCase          |Bevat bijgewerkte Use Case   |
| Opdracht06.2.Datamodel        |Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database|
| Opdracht06.3.WpfDatabase      |Bevat WPF applicatie met uitgewerkte database class: Ingredient toegevoegd|
| Opdracht06.4.WpfKeuzescherm   |Bevat WPF applicatie om een keuze te maken tussen het Display of ingredientenwindow|
| Opdracht06.5.WpfIngredienten  |Bevat WPF applicatie met data gebind aan de XAML|

### Opdracht 7: Userstory IngredientEenheden
Doel van deze opdracht is te oefenen met relaties en met de SQL innerjoin. De eenheden worden aan het datamodel toegevoegd. Wat betekent dit voor de database class. Ook hier zal weer veel gezamenlijk geprogrammeerd worden.

|***Branch***||
|:---|---|
| Opdracht07.1.Datamodel        |Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database|
| Opdracht07.2.WpfDatabase      |Bevat WPF applicatie met uitgewerkte database class: Unit toegevoegd, Ingredient aangepast|
| Opdracht07.3.WpfIngredienten  |Bevat WPF applicatie met data gebind aan de XAML: aangepast|

### Opdracht 8: Userstory Ingredientenbeheren
De manager moet ook ingredienten kunnen toevoegen, wijzigen en verwijderen. Student leert out parameter te gebruiken

|***Branch***||
|:---|---|
| Opdracht08.1.UseCaseCrud       |Bevat de bijgewerkte use case|
| Opdracht08.2.DatabaseCrud      |Databaseclass is uitgebreid: methods Create, GetById, Update en Delete zijn toegevoegd|
| Opdracht08.3.IngredientenCrud  |IngredientWindow is aangepast: Ingredienten kunnen toegevoegd, gewijzigd en verwijderd worden|


### Opdracht 9: UserstoryDisplayWindow
In deze opdracht wordt eerst ingegaan op de m:n relaties en koppelentiteiten. Het model en ERD worden gemaakt, waarna de database wordt aangepast
Tenslotte wordt het DisplayWindow aangepast, zodat de beschrijving en prijs van een maaltijd automatisch worden samengesteld

|***Branch***||
|:---|---|
| Opdracht09.1.Datamodel            |Bevat bijgewerkte Datamodel: MenuIngredienten is toegevoegd. Daarnaast is de databaseclass aangepast en zijn Description en Price van de Meal class afleidbare gegevens. |

### Opdracht 10: Userstory MaaltijdIngredienten
In deze opdracht wordt eerst ingegaan op de m:n relaties en koppelentiteiten. Het model wordt samen gemaakt, maar daarna gaan de studenten zelf de rest uitwerken

|***Branch***||
|:---|---|
| Opdracht10.1.UseCase              |Bevat de bijgewerkte use case. Daarnaast is het beheerwindow voor meals toegevoegd (inclusief toevoegen en verwijderen van gekoppelde ingredienten)|

### Opdracht 11: Optimalisatie
In deze opdracht wordt de databaseclass geoptimaliseerd

|***Branch***||
|:---|---|
| Opdracht11.1.WpfDatabase          |Databaseclass geoptimaliseerd|

