# Lj2Dd1En2
## Introductie
Deze repository bevat alle code die gebruikt wordt in de vakken DD1 en DD2 van leerjaar 2 Software Development.

De code is in een aantal branches ondergebracht. Deze branches komen overeen met de opdrachten uit de reader.

### Opdracht1: Heractiveren kennis Leerjaar 1. 
Studenten gaan zelfstandig aan de slag met een CRUD applicatie, op basis van de kennis die ze nog hebben. Na de vakantie zal deze kennis vaak weggezakt zijn. Laat studenten in groepen werken en hun opdracht aftekenen. Laat ze an de hand van deze opdracht hun persoonlijke leerdoelen noteren.

***Branch***:
- Opdracht01Cars   
  - carsdb.sql      Script voor een MySql database
  - wpf applicatie  Let op: is .**Net Core**. Wijs studenten hierop.  

### Opdracht2: Oefenen met kwaliteitstandaarden.
Studenten verbeteren het programma, zodat het voldoet aan de kwaliteitseisen. Het gaat hierbij om de regels waarmee de studenten in leerjaar 1 kennis hebben gemaakt, zoals het toepassen van logische namen, gebruik van hood- en kleine letters, toepassen van een database class, juiste types toepassen, duidelijke instructies bij fouten, etc. Ook deze regels moeten ze weer activeren. Belang van de regels moet benadrukt worden. 

***Branches***: 
- Opdracht02Start      : Bevat de code die de studenten moeten beoordelen en verbeteren,
- Opdracht02Uitwerking : De voorbeelduitwerking. 

## Los Pollos Hermanos
Na deze eerste 2 opdrachten gaan de studenten aan de slag met een opdracht voor restaurant Los Pollos Hermanos. Doel is dat de studenten volgens Scrum een applicatie maken. Belangrijk is dus dat ze niet eerst een compleet ontwerp en programma maken, maar dit ontwerp en programma laten ontstaan volgens de volgorde waarin de product owner user stories heeft gemaakt. Alle user stories staan in een KanBan in deze opdracht. 

Van belang is dat de studenten weten wat een databaseprogramma is en dat ze ook al eens een databaseclass hebben gemaakt. Studenten leren het volgende:
- documentatie
  - use cases maken
  - maken van een genormaliseerd datamodellen voor de applicatie, bestaande uit een beschrijving en ERD
 - databases
   - een database maken, inclusief tabellen met kolommen, indexen en relaties tussen de tabellen
   - SQL toepassen: SELECT, INSERT, UPDATE en DELETE. Daarnaast ook INNER JOIN
- programmeren
  - Database class: connection strings, foutafhandeling, dataconversie (database types - c# types)
  - Model classes : properties, afleidbare gegevens (zoals leeftijd)
  - Binding       : properties, ObservableCollection, INotifyPropertyChanged

### Opdracht 3: Inrichten ontwikkelomgeving
Dit is een tussendooropdracht die ervoor zorgt dat de studenten voor het vervolg over alle templates e.d. beschikken die nodig zijn voor het maken van de opdrachten. 
  - Voor Visio worden templates voor een class diagram, een use case diagram en een ERD diagram klaargezte
  - In msWord wordt een template voor een datamodelbeschrijving klaargezet

***Branch***:
- Opdracht03Ontwikkelomgeving   
  - losPolloshermanos.sql      Script voor een MySql database (leeg)
  - wpf applicatie             
  - folder Documentatie met daarin de templates voor Visio (ERD, Use Case, Class Diagram) en msWord (Datamodel)


### Opdracht 4: Userstory RestaurantDisplay
In deze opdracht gaat de student een eenvoudige use case en datamodel maken. Veel wordt voorgedaan. Het gaat erom dat de student meedoet, zodat hij dit bij een volgende opdracht zelf kan doen. Student moet zo snel mogelijk aan de slag met programmeren. Belangrijkste is de structuur van de databaseclass de domain class en binding. In deze opdracht wordt veel voorgedaan (gezamenlijk programmeren) en getoets of eenieder bij is en het begrijpt.

***Branch***:
- Opdracht04.1.UseCase      Bevat bijgewerkte Use Case   
- Opdracht04.2.Datamodel    Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database
- Opdracht04.3.WpfDatabase  Bevat WPF applicatie met uitgewerkte database class
- Opdtacht04.4.WpfDisPlay   Bevat WPF applicatie met data gebind aan de XAML


### Opdracht 5: Userstory IngrediëntenInzicht
In deze opdracht gaat de student geheel zelfstandig een eenvoudige use case en datamodel maken. De bestaande documentatie wordt uitgebreid. Daarnaast wordt ook het programma uitgebreid. De gebruiker zal afhankelijk van zijn rol een keuze moeten maken tussen het Display en het Ingredientenoverzicht. De student moet begrijpen dat dit niet automatisch betekent dat hij een inlogscherm of iets dergelijk gaat maken, maar dat hij de mogelijkheden met de productowner bespreekt en hem een keuze laat maken. In dit geval zal de productowner kiezen voor een eenvoudig keuzescherm, waarbij procedureel wordt geregeld dat actors alleen die functies gebruiken die zij nodig hebben. 


***Branch***:
- Opdracht05.1.UseCase          Bevat bijgewerkte Use Case   
- Opdracht05.2.Datamodel        Bevat bijgewerkte Datamodel, ERD en SQL Script voor de database
- Opdracht05.3.WpfDatabase      Bevat WPF applicatie met uitgewerkte database class
- Opdtacht05.4.WpfKeuzescherm   Bevat WPF applicatie om een keuze te maken tussen het Display of ingredientenwindow
- Opdtacht05.4.WpfIngredienten  Bevat WPF applicatie met data gebind aan de XAML


