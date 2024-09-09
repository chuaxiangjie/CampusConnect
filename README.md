# CampusConnect

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development.

### Prerequisites

What things you need to install the software and how to install them

#### Windows Environment (With Visual Studio)

1. Install Visual Studio 2022 Community 
(https://visualstudio.microsoft.com/vs/community/)
> [!NOTE]  
> Must be above v17.8+ to support .Net 8

2. Install Microsoft SQL Server Management Studio (SSMS) <br>
Follow tutorial : https://www.c-sharpcorner.com/article/how-to-install-sql-server-20222/

3. Clone git repository via visual studio
   
4. Verify database connection string in application
> [!NOTE]
> No need to change anything


5. Open command prompt, enter
```
dotnet tool install --global dotnet-ef
```

6. In the same command prompt, cd to CampusConnect.Repository folder, enter to execute db migration
```
dotnet ef database update
```

7. Verify in Microsoft SQL Server Management Studio (SSMS), database CampusConnect is created with seeded data

8. Build and Run using Visual Studio 2022
```
-> Clone repository using VS, build and run application via IIS express
```

#### Execute Rest Apis via swagger

Browse swagger url : [http://localhost:6600/swagger/index.html](https://localhost:44374/swagger/index.html)
