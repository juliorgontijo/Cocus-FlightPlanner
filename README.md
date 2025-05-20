# CocusFlightPlanner

![Screenshot 1](/Screenshot1.png)

## Technical considerations and assumptions

* As requested, the project was created using .Net MVC architecture. However, my personal preference is to use a more modern client technology, like Angular or React;
  
* Since .net MVC was the chosen architecture, and for the sake of simplicity of this test, no separated API was developed. The main reasons were:
    * There were no requirement to expose external endpoints to be used in external systems;
    * The use of an API would generate extra costs in real life, as most cloud providers (like Azure) charges the incoming and/or outgoing traffic. And this is a significant cost for most projects;
    * Exposing endpoints add an extra risk in terms of security;
      
* However, there are some drawbacks when not choosing an API implementation:
    * The project turns into a more monolith approach;
    * Both Frontend and Backend should use the same technology;

* My personal chosen technology in a real life project would be an Angular Frontend calling a .net API. This is a more distributed approach, making the system easier to maintain and to be scaled.

## Business assumptions used to create the solution

* Calculating the takeoff effort involves many parameters. For the sake of simplicity, I choose the 2 main factors that affect takeoff effort: airport altitude and aircraft Maximum Take-off weight (MTOW). The airport altitude is maintained in the Airport entity, while aircraft Mtow is maintained in the aircraft type entity. Then the system does an arbitrary calculatiuon using these 2 factors;
  
* When creating the airport, an external api with real data is called to populate airport data. Simply add the IATA (or ICAO code) and click on "Get Airport Data" button. The Iata code is the 3 letters code used when we search for flights in all travel websites (e.g. OPO for Porto, CDG for Paris Charles de Gaulle, etc.). This external api uses a free plan with 100 requests/month to retrieve data. I used some of them on May 2025 to develop the system, so this might stop working eventually;
  
* When generating the database, some aircraft types are already created to make it easier to play around with the system;

## How to run the project
1. Clone this repository in your computer;
2. The solution uses local MS-SQL Express as a database provider. Create a database in your local SQL Server named CocusFlightPlanner. Windows Authentication will be used to access it;
3. Open the solution in Visual Studio
4. In the package manager console, run database-update command
5. Run the project using Visual Studio
