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
- De startcode werkt; studenten hoeven hier dus geen fouten uit te halen omdat de code het niet zou doen. Studenten verbeteren code uitsluitend op basis van de gestelde kwaliteitseisen. Deze verbeteringen vinden zowel plaats in de XAML als in de .cs code.


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
|Opdracht03Ontwikkelomgeving|Script voor een lege MySql database (losPolloshermanos.sql)|
||wpf applicatie|
||folder Documentatie met daarin de templates voor Visio (ERD, Use Case, Class Diagram) en msWord (Datamodel)|


### Opdracht 4: Userstory RestaurantDisplay
In deze opdracht gaat de student een eenvoudige use case en een datamodel maken. Veel wordt voorgedaan. Het gaat erom dat de student meedoet, zodat hij dit bij een volgende opdracht zelf kan doen. Student moet zo snel mogelijk aan de slag met programmeren. Belangrijkste is de _databaseclass_, de _domain class_ en _binding_. In deze opdracht wordt veel voorgedaan (gezamenlijk programmeren). Studenten zorgen dat ze alles begrijpen, bij blijven en hun persoonlijke leerdoelen noteren.

|***Branch***||
|:---|---|
|Opdracht04.1.UseCase     |Bevat bijgewerkte Use Case |
|Opdracht04.2.Datamodel   |Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database |
|Opdracht04.3.WpfDatabase |Bevat WPF applicatie met uitgewerkte database class |
|Opdtacht04.4.WpfDisPlay  |Bevat WPF applicatie met data gebind aan de XAML |


### Opdracht 5: Userstory IngrediëntenInzicht
In deze opdracht gaat de student geheel zelfstandig een eenvoudige use case en datamodel maken. De bestaande documentatie wordt uitgebreid. Daarnaast wordt ook het programma uitgebreid. De gebruiker zal afhankelijk van zijn rol een keuze moeten maken tussen het Display en het Ingredientenoverzicht. De student moet begrijpen dat dit niet automatisch betekent dat hij een inlogscherm of iets dergelijk gaat maken, maar dat hij de mogelijkheden met de productowner bespreekt en hem een keuze laat maken. In dit geval zal de productowner kiezen voor een eenvoudig keuzescherm, waarbij procedureel wordt geregeld dat actors alleen die functies gebruiken die zij nodig hebben. 


|***Branch***||
|:---|---|
| Opdracht05.1.UseCase          |Bevat bijgewerkte Use Case   |
| Opdracht05.2.Datamodel        |Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database|
| Opdracht05.3.WpfDatabase      |Bevat WPF applicatie met uitgewerkte database class: Ingredient toegevoegd|
| Opdtacht05.4.WpfKeuzescherm   |Bevat WPF applicatie om een keuze te maken tussen het Display of ingredientenwindow|
| Opdtacht05.4.WpfIngredienten  |Bevat WPF applicatie met data gebind aan de XAML|

### Opdracht 6: Userstory IngredientEenheden|
Doel van deze opdracht i|s te oefenen met relaties en met de SQL innerjoin. De eenheden worden aan het datamodel toegevoegd. Wat betekent dit voor de database class. Ook hier zal weer veel gezamenlij|k geprogrammeerd worden. |||

||***Branch***||
|:---|---|
| Opdracht06.1.Datamodel        |Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database|
| Opdracht06.2.WpfDatabase      |Bevat WPF applicatie met uitgewerkte database class: Unit toegevoegd, Ingredient aangepast|
| Opdtacht06.3.WpfIngredienten  |Bevat WPF applicatie met data gebind aan de XAML: aangepast|

### Opdracht 7: Userstory MenuIngredienten
In deze opdracht wordt eerst ingegaan op de m:n relaties en koppelentiteiten. Het model wordt samen gemaakt, maar daarna gaan de studenten zelf de rest uitwerken

|***Branch***||
|:---|---|
| Opdracht07.1.UseCase              |Bevat de bijgewerkte use case|
| Opdracht07.2.Datamodel            |Bevat bijgewerkte Datamodel: MenuIngredienten is toegevoegd|
| Opdracht07.2.WpfDatabase          |Databaseclass is uitgebreid: Model class MenuIngredient toegevoegd, extra methods om de ingredienten van een menu bij te selecteren|
| Opdtacht07.3.WpfMenuIngredienten  |Bevat beheerfunctie voor de menuingredienten|

