# CampusConnect
Your One-Stop University Portal

## Technologies

Architecture
   * Database - Microsoft SQL Server
   * Rest Api - Develop using .Net 8

Library
   * EntityFrameworkCore 8
   * Mediatr
   * Swashbuckle (swagger)

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
   
4. Verify database connection string in `appsettings.json`
> [!NOTE]
> No need to change anything

![image](https://github.com/user-attachments/assets/3a99eccf-7b94-4f84-8a8e-ed33ff195a88)


5. Open command prompt (can be any directory), enter
```
dotnet tool install --global dotnet-ef
```

6. In the same command prompt, cd to CampusConnect.Repository folder, enter to execute db migration
```
dotnet ef database update
```

![image](https://github.com/user-attachments/assets/3c4181bb-fd61-4b3f-b119-29a9937937c7)


7. Verify in Microsoft SQL Server Management Studio (SSMS), database CampusConnect is created with seeded data

- Country table (seeded)
- Universities table (seeded)

![image](https://github.com/user-attachments/assets/5f4e44f4-cdfc-4a93-83d1-5200fa0f03f4)


8. Build and Run using Visual Studio 2022

-> Clone repository using VS, build and run application via IIS express


#### Execute Rest Apis via swagger

Browse swagger url : [(https://localhost:44374/swagger/index.html)](https://localhost:44374/swagger/index.html)

![image](https://github.com/user-attachments/assets/dcf8c35e-a600-4636-a845-d5bd4459e8c1)
