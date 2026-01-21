# Quick Start Guide

## ⚡ เริ่มต้นใช้งานด่วน

### วิธีที่ 1: Docker (แนะนำสำหรับทดสอบรวดเร็ว)

```bash
# เริ่มทั้งระบบ (PostgreSQL + Backend)
docker-compose up --build

# เปิดหน้าต่างใหม่สำหรับ Frontend
cd frontend
npm start
```

จากนั้น scan QR code ด้วย Expo Go app

---

### วิธีที่ 2: Local Development (แนะนำสำหรับพัฒนา)

#### 1️⃣ ติดตั้ง PostgreSQL

**Windows:**
- ดาวน์โหลดจาก https://www.postgresql.org/download/windows/
- หรือใช้ Docker: `docker run -d -p 5432:5432 -e POSTGRES_PASSWORD=postgres postgres:15-alpine`

**Mac:**
```bash
brew install postgresql@15
brew services start postgresql@15
```

**Linux:**
```bash
sudo apt-get install postgresql-15
sudo systemctl start postgresql
```

#### 2️⃣ Backend Setup

```bash
cd backend/src/API

# สร้าง Database Migration
dotnet ef migrations add InitialCreate --project ../Infrastructure

# Apply Migration (สร้าง tables)
dotnet ef database update

# Run Backend
dotnet run
```

✅ Backend พร้อมใช้งาน:
- API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

#### 3️⃣ Frontend Setup

```bash
cd frontend

# ตั้งค่า Environment
# แก้ไข .env ให้ใช้ IP ของเครื่องคุณ (สำหรับ physical device)
# สำหรับ emulator ใช้ localhost ได้

# เริ่ม Expo
npm start
```

**สำหรับ Physical Device:**
1. หา IP ของคอมพิวเตอร์: `ipconfig` (Windows) หรือ `ifconfig` (Mac/Linux)
2. แก้ไข `.env`:
   ```
   API_BASE_URL=http://192.168.1.100:5000/api/v1
   ```
3. Scan QR code ด้วย Expo Go app

**สำหรับ Emulator/Simulator:**
- iOS Simulator: ใช้ `http://localhost:5000/api/v1`
- Android Emulator: ใช้ `http://10.0.2.2:5000/api/v1`

---

## 🧪 ทดสอบระบบ

### Test Backend ผ่าน Swagger

1. เปิด http://localhost:5000/swagger
2. **Register User:**
   - `POST /api/v1/auth/register`
   - Body:
     ```json
     {
       "email": "test@example.com",
       "password": "Password123",
       "fullName": "Test User"
     }
     ```
3. **Login:**
   - `POST /api/v1/auth/login`
   - Copy `token` ที่ได้รับ
4. **Authorize:**
   - Click ปุ่ม "Authorize" 🔒
   - ใส่: `Bearer YOUR_TOKEN_HERE`
   - Click "Authorize"
5. **Test CRUD:**
   - `GET /api/v1/items` - ดูรายการ
   - `POST /api/v1/items` - สร้างรายการใหม่
   - `PUT /api/v1/items/{id}` - แก้ไข
   - `DELETE /api/v1/items/{id}` - ลบ

### Test Frontend

1. Register account ใหม่
2. Login
3. ทดสอบ CRUD operations:
   - สร้าง Item ใหม่ (กด + button)
   - ดูรายการ Items
   - Edit Item (กดที่ item)
   - Delete Item
4. Logout
5. Login อีกครั้ง (ทดสอบ auto-login)

---

## 📁 โครงสร้างโปรเจกต์

```
claude_project_example/
├── backend/
│   ├── src/
│   │   ├── API/              # Controllers, Program.cs
│   │   ├── Core/             # Entities, DTOs, Interfaces
│   │   └── Infrastructure/   # DbContext, Services
│   └── Dockerfile
├── frontend/
│   ├── src/
│   │   ├── api/              # API Client
│   │   ├── context/          # AuthContext
│   │   ├── navigation/       # AppNavigator
│   │   └── screens/          # All screens
│   ├── App.tsx
│   └── .env
├── docker-compose.yml
└── README.md
```

---

## 🔧 คำสั่งที่ใช้บ่อย

### Backend

```bash
# Build
dotnet build

# Run
dotnet run

# Run with watch (auto-reload)
dotnet watch run

# Create migration
dotnet ef migrations add MigrationName --project ../Infrastructure

# Apply migrations
dotnet ef database update

# Remove last migration
dotnet ef migrations remove --project ../Infrastructure
```

### Frontend

```bash
# Start development server
npm start

# Clear cache
npm start --clear

# Run on specific platform
npm run android
npm run ios
npm run web
```

### Docker

```bash
# Start all services
docker-compose up

# Start in background
docker-compose up -d

# Rebuild
docker-compose up --build

# Stop all services
docker-compose down

# View logs
docker-compose logs -f

# Stop and remove volumes
docker-compose down -v
```

---

## ⚠️ Troubleshooting

### ❌ Cannot connect to API from mobile
**แก้ไข:** ใช้ IP address ของคอมพิวเตอร์แทน localhost ใน `.env`
```
API_BASE_URL=http://192.168.1.100:5000/api/v1
```

### ❌ CORS error
**แก้ไข:** เพิ่ม origin ใน `backend/src/API/appsettings.json`
```json
"AllowedOrigins": [
  "http://localhost:19006",
  "http://localhost:8081",
  "YOUR_EXPO_DEV_SERVER_URL"
]
```

### ❌ Database connection failed
**ตรวจสอบ:**
1. PostgreSQL running: `psql -h localhost -U postgres`
2. Connection string ใน `appsettings.json`
3. Database exists: `dotnet ef database update`

### ❌ JWT token invalid
**แก้ไข:** ตรวจสอบว่า SecretKey ใน `appsettings.json` มีความยาวอย่างน้อย 32 characters

### ❌ Expo not loading
**แก้ไข:**
```bash
cd frontend
npm start --clear
```

---

## 📝 Default Credentials

ไม่มี default user - ต้อง register ผ่าน app หรือ Swagger

**Database:**
- Host: localhost
- Port: 5432
- Database: mobileappdb
- Username: postgres
- Password: postgres

---

## 🚀 Next Steps

หลังจากทดสอบเรียบร้อยแล้ว คุณสามารถ:

1. **เพิ่ม Features:**
   - Profile management
   - Image upload
   - Push notifications
   - Real-time updates (SignalR)
   - Pagination
   - Search & Filter

2. **ปรับปรุง Security:**
   - Refresh token
   - Rate limiting
   - Email verification
   - Two-factor authentication

3. **Deploy:**
   - Backend: Azure App Service, AWS, Heroku
   - Database: Managed PostgreSQL
   - Frontend: EAS Build → App Stores

---

## 📚 เอกสารเพิ่มเติม

- [README.md](./README.md) - เอกสารฉบับเต็ม
- Swagger UI: http://localhost:5000/swagger
- React Navigation: https://reactnavigation.org/
- Expo Docs: https://docs.expo.dev/

---

สร้างโดย Claude Code 🤖
