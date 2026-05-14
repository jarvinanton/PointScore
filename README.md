# PointScore API

This API provides services for feature scoring, project management, and license administration. It is designed to integrate with various modules such as AI, MBSE, and external systems.

## 🚀 Features

- **Score Calculation**: Advanced algorithms to calculate technical, functional, schedule, user impact, and cost scores.
- **Project Management**: CRUD endpoints for project administration.
- **Licensing**: JWT-based license system for access control and usage limits.
- **Automatic Documentation**: Full Swagger integration for API exploration and testing.
- **Automatic Redirection**: When the server runs, the root (`/`) automatically redirects to `/swagger`.

## 🛠️ Technologies

- **Core**: .NET Core
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Documentation**: Swagger (OpenAPI)

## 📋 Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher recommended)
- SQL Server

## ⚙️ Configuration

1. Clone the repository.
2. Configure the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=PointScoreDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
3. Configure the JWT secret:
   ```json
   "Jwt": {
     "Secret": "YOUR_SUPER_SECURE_SECRET",
     "Issuer": "PointScoreAPI",
     "Audience": "PointScoreUsers"
   }
   ```
4. Run migrations (if applicable):
   ```bash
   dotnet ef database update
   ```

## 🏃 Execution

To start the server:

```bash
dotnet run
```

Once running, the API will be available at `http://localhost:5288` (or your configured port) and will automatically open the **Swagger** interface at `/swagger`.

## 🛣️ Main Endpoints

- **Dashboard**: `GET /api/dashboard` - Retrieves a summary of recent scores.
- **Scoring**:
  - `POST /api/wsm-feature` - Calculates and saves new scores.
  - `GET /api/wsm-feature/{id}/calculate-technical-score` - Detailed breakdown of the technical score.
- **Projects**: `GET /api/Projects` - Project listing and management.
- **Licenses**: `GET /api/licenses` - License administration (requires Admin role).

---
Developed for the Jarvin platform.
