# RoadStatus
Building the code:  

I have marked the repo as Public so either clone the code or download as a zip file from the master branch.  

.net core 8 SDK is used.  

      - update the appsettings.json with the appkey. now it is blank.

## How to build  

  From the root run  
       
        - dotnet build
        
## How to run

      - dotnet run -- A2
      - dotnet run -- A233
      
## How to test

      - dotnet test

# Assumptions

      - new TfL API DOES NOT require APP_ID hence only APP_KEY is used. THIS INFO IS GIVEN IN THE TFL BLOG.

# More info

      - TDD and BDD are used
      - separation of concerns
      - code will do 3 retries with 10 secs timeout of HTTP APIs.
      - Git tags and branches are created for easy code review.
      - branches are quash merged with master to maintain linear commit history.
      - As the execution format is given as "dotnet run -- A2", I havent used environment variables and used appsettings.json
      
      
