# ASP.NET-WebAPI-App
Repo for contemporary programming final project.

If running windows install docker desktop.
If linux or mac make sure to have docker set up.
Run DB with "docker-compose up -d"
You may need to use "dotnet ef database update"
Ensure mysql is running and ready "docker ps"
Ensure you are looking for the proper endpoint on the proper port using https (fixed error where https redirect failed), may need to trust certificate
All required libraries and dependencies should be there. I had to ensure EF.Design was referenced in csproj
There should be no need to run migrations initialcreate / add unless new db changes
Feel free to make changes / additions / fixes as needed. 
