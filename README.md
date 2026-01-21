# Mobile App Project

A full-stack mobile application built with React Native (Expo) and .NET Core.

## Technology Stack

### Frontend
- **React Native** with **Expo** SDK
- **TypeScript** for type safety
- **React Navigation** for navigation
- **Axios** for API calls
- **AsyncStorage** for local storage
- **Context API** for state management

### Backend
- **.NET 8** Web API
- **Entity Framework Core** with PostgreSQL
- **JWT Authentication** (BCrypt password hashing)
- **Swagger/OpenAPI** documentation
- **Clean Architecture** (API, Core, Infrastructure layers)

### Database
- **PostgreSQL 15**

### DevOps
- **Docker** and **Docker Compose**
- Multi-stage Dockerfile for optimized builds

## Project Structure

```
claude_project_example/
├── backend/                      # .NET Core Web API
│   ├── src/
│   │   ├── API/                 # Web API Layer
│   │   │   ├── Controllers/     # API Controllers
│   │   │   ├── Program.cs       # App configuration
│   │   │   └── appsettings.json
│   │   ├── Core/                # Business Logic Layer
│   │   │   ├── Entities/        # Domain entities
│   │   │   ├── Interfaces/      # Service interfaces
│   │   │   └── DTOs/            # Data transfer objects
│   │   └── Infrastructure/      # Data Access Layer
│   │       ├── Data/            # DbContext
│   │       └── Services/        # Service implementations
│   ├── Dockerfile
│   └── backend.sln
├── frontend/                     # React Native + Expo
│   ├── src/
│   │   ├── api/                 # API Client
│   │   ├── components/          # Reusable components
│   │   ├── screens/             # Screen components
│   │   ├── navigation/          # React Navigation
│   │   ├── context/             # Context providers
│   │   └── types/               # TypeScript types
│   ├── App.tsx
│   └── package.json
├── docker-compose.yml
├── .gitignore
└── README.md
```

## Prerequisites

- **Node.js** 18+ and **npm**
- **.NET SDK 8.0**
- **Docker** and **Docker Compose** (for containerized deployment)
- **PostgreSQL** (or use Docker)
- **Expo Go** app on your mobile device (iOS/Android)

## Getting Started

### Option 1: Docker (Recommended)

1. **Start all services:**
   ```bash
   docker-compose up --build
   ```

2. **Access the API:**
   - API: http://localhost:5000
   - Swagger UI: http://localhost:5000/swagger

### Option 2: Local Development

#### Backend Setup

1. **Navigate to backend directory:**
   ```bash
   cd backend
   ```

2. **Restore packages:**
   ```bash
   dotnet restore
   ```

3. **Update connection string** in `src/API/appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=mobileappdb;Username=postgres;Password=your_password"
     }
   }
   ```

4. **Install EF Core tools** (if not installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```

5. **Create and apply database migrations:**
   ```bash
   cd src/API
   dotnet ef migrations add InitialCreate --project ../Infrastructure
   dotnet ef database update
   ```

6. **Run the API:**
   ```bash
   dotnet run
   ```

   The API will be available at:
   - https://localhost:7XXX (HTTPS)
   - http://localhost:5000 (HTTP)
   - Swagger UI: https://localhost:7XXX/swagger

#### Frontend Setup

1. **Navigate to frontend directory:**
   ```bash
   cd frontend
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Create `.env` file:**
   ```env
   API_BASE_URL=http://localhost:5000/api/v1
   ```

   **Note:** For testing on physical devices, replace `localhost` with your computer's local IP address (e.g., `http://192.168.1.100:5000/api/v1`)

4. **Start Expo development server:**
   ```bash
   npm start
   ```

5. **Run on device:**
   - Scan the QR code with **Expo Go** app (iOS/Android)
   - Or press `w` to open in web browser

## API Endpoints

### Authentication
- `POST /api/v1/auth/register` - Register new user
- `POST /api/v1/auth/login` - Login user

### Items (Protected - requires JWT token)
- `GET /api/v1/items` - Get all items for current user
- `GET /api/v1/items/{id}` - Get item by ID
- `POST /api/v1/items` - Create new item
- `PUT /api/v1/items/{id}` - Update item
- `DELETE /api/v1/items/{id}` - Delete item

### Health Check
- `GET /api/health` - API health status

## Features

### Implemented
✅ User registration and authentication (JWT)
✅ Password hashing with BCrypt
✅ Protected CRUD operations
✅ Input validation
✅ Global error handling
✅ Swagger API documentation
✅ CORS configuration
✅ Docker support
✅ Clean Architecture

### Authentication Flow
1. User registers with email, password, and full name
2. Password is hashed using BCrypt (work factor: 12)
3. User logs in with credentials
4. Server returns JWT token (expires in 60 minutes)
5. Client stores token securely (AsyncStorage)
6. Token is attached to all subsequent API requests
7. Protected endpoints verify token validity

## Environment Variables

### Backend (.NET)
Configure in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=mobileappdb;Username=postgres;Password=postgres"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyMinimum32CharactersLongForHS256Algorithm",
    "Issuer": "MobileApp",
    "Audience": "MobileAppClient",
    "ExpirationMinutes": "60"
  },
  "AllowedOrigins": [
    "http://localhost:19006",
    "http://localhost:8081"
  ]
}
```

### Frontend (Expo)
Create `.env` file:
```env
API_BASE_URL=http://localhost:5000/api/v1
```

## Database Migrations

### Create a new migration:
```bash
cd backend/src/API
dotnet ef migrations add MigrationName --project ../Infrastructure
```

### Apply migrations:
```bash
dotnet ef database update
```

### Remove last migration:
```bash
dotnet ef migrations remove --project ../Infrastructure
```

## Testing

### Test Backend API

1. **Start the backend**
2. **Navigate to Swagger UI:** http://localhost:5000/swagger
3. **Test authentication:**
   - Register a new user via `/api/v1/auth/register`
   - Login via `/api/v1/auth/login` (copy the token)
4. **Authorize in Swagger:**
   - Click "Authorize" button
   - Enter: `Bearer YOUR_TOKEN_HERE`
5. **Test protected endpoints** (Items CRUD operations)

### Test Frontend

1. **Ensure backend is running**
2. **Update API_BASE_URL** in `.env` with correct URL
3. **Start Expo:** `npm start`
4. **Test on device/emulator:**
   - Register a new account
   - Login
   - Create, view, edit, and delete items
   - Logout and verify auto-login on app restart

## Troubleshooting

### Cannot connect to API from Expo Go
- **Solution:** Replace `localhost` in `.env` with your computer's IP address
- Find your IP: `ipconfig` (Windows) or `ifconfig` (Mac/Linux)
- Example: `API_BASE_URL=http://192.168.1.100:5000/api/v1`

### CORS errors
- **Solution:** Add your Expo development server URL to `AllowedOrigins` in `appsettings.json`

### Database connection issues
- **Solution:** Ensure PostgreSQL is running and connection string is correct
- Test connection: `psql -h localhost -U postgres -d mobileappdb`

### Migration issues
- **Solution:** Ensure you're in the correct directory and using `--project ../Infrastructure` flag
- Delete database and recreate: `dotnet ef database drop && dotnet ef database update`

## Security Best Practices

🔒 **Implemented security measures:**
- Password hashing with BCrypt (work factor: 12)
- JWT with expiration (60 minutes)
- HTTPS redirect in production
- Input validation with Data Annotations
- SQL injection protection (EF Core parameterization)
- CORS restrictions
- No sensitive data in error messages

⚠️ **For production:**
- Change JWT SecretKey to a strong random value (32+ characters)
- Use environment variables for all secrets
- Enable HTTPS only
- Configure proper CORS (not wildcard)
- Implement rate limiting
- Add refresh token mechanism
- Use secure database credentials
- Enable logging and monitoring

## License

MIT

## Author

Your Name

---

Built with ❤️ using React Native, Expo, and .NET Core
