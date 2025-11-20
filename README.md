# ASP.NET-WebAPI-App

## Prerequisites (Windows)
- Docker Desktop (running)
- .NET SDK 8.0+ (check with: `dotnet --version`)
- Git (optional if you already have the repo)

## Quick Start (copy/paste into PowerShell)
```powershell
# 1) Go to the API project folder (compose file lives here)
cd c:\Users\aiden\Documents\GitHub\ASP.NET-WebAPI-App\MyWebApi

# 2) Start MySQL in Docker
docker compose up -d

# 3) Wait until MySQL is ready (Ctrl+C to stop tailing when you see 'ready for connections')
docker logs -f mywebapi_mysql

# 4) Trust HTTPS dev cert (one-time on your machine)
dotnet dev-certs https --trust

# 5) Restore, build, and run the API (HTTPS profile)
dotnet restore
dotnet build
dotnet run --launch-profile https
```

The API will listen on:
- HTTPS: https://localhost:7160
- HTTP:  http://localhost:5235

```bash
# List first 5 students (use -k if you did not trust the cert)
curl -k https://localhost:7160/api/students

# Get a specific student by id
curl -k https://localhost:7160/api/students/1
```

## Resetting the Database (fresh seed)
```powershell
cd c:\Users\aiden\Documents\GitHub\ASP.NET-WebAPI-App\MyWebApi
docker compose down -v   # removes the volume (all data)
docker compose up -d
# run the API again; migrations + seed will rerun