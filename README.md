# 🏋️‍♂️ FitVision – AI-Powered Fitness & Nutrition Tracker

A full-stack fitness and health app built with **.NET 8**, **CQRS architecture**, **EF Core**, **Azure**, **React Native (Expo)**, and **Supabase**.  
The goal: combine clean architecture with cloud technologies and practical AI features (food recognition, recommendations, etc.).

---

## 🧠 Overview

FitVision helps users track their meals, workouts, and progress through:
- 📸 **AI food recognition** (powered by Azure Vision)
- 🥗 **Nutrition tracking** with Supabase as a cloud database
- 🏋️‍♀️ **Workout and meal logging** synced to your account
- ☁️ **Cloud-ready .NET backend** with CQRS and clean architecture
- 📱 **Cross-platform mobile app** (iOS/Android via Expo)

This project demonstrates **enterprise-grade backend structure** and **scalable front-end design** suitable for consulting or portfolio presentation.

---

## 🏗️ Tech Stack

### Backend (API)
- **.NET 8** + **CQRS** (MediatR)
- **Entity Framework Core** (Npgsql provider)
- **Supabase (PostgreSQL)** for persistent storage
- **Serilog** for structured logging
- **Clean Architecture** with:
  - `Domain` – entities, value objects
  - `Application` – commands, queries, handlers
  - `Infrastructure` – EF Core, repositories, persistence
  - `API` – endpoints, middleware, DI

### Mobile App
- **Expo (React Native)** + **TypeScript**
- **Zustand** for state management
- **Axios** for API communication
- **Supabase Auth** for user accounts
- **Expo-router** navigation

### Cloud & DevOps
- **Azure App Service / Container Apps**
- **Azure Blob Storage** (for images)
- **Pulumi / Bicep** for IaC (in progress)
- **GitHub Actions** CI/CD (planned)

---

## 📂 Monorepo Structure
```bash
/fitvision
│
├── backend/ # .NET 8 Web API
│ ├── FitVision.Application/ # CQRS (commands, queries, DTOs)
│ ├── FitVision.Domain/ # Entities & core logic
│ ├── FitVision.Infrastructure/ # EF Core, Supabase integration
| ├── FitVision.Tests/ # Tests
│ ├── FitVision.API/ # Controllers, middleware, startup
│   └── appsettings.json
│
├── mobile/FitVision # Expo React Native App
│ ├── app/ # Screens (expo-router)
│ ├── components/ # Shared UI
│ ├── lib/ # API, Supabase client
│ ├── store/ # Zustand state management
│ ├── assets/ # Icons, images
│ └── App.tsx
│
└── README.md
```


---

## ⚙️ Getting Started

### 1️⃣ Clone & Setup
```bash
git clone https://github.com/<yourusername>/fitvision.git
cd fitvision
```

### 2️⃣ API Setup
```bash
cd api
dotnet restore
dotnet ef database update
dotnet run
```

### 3️⃣ Mobile Setup
```bash
cd mobile
npm install
npx expo start
```

Create a .env file for Supabase keys:
```bash
EXPO_PUBLIC_SUPABASE_URL=...
EXPO_PUBLIC_SUPABASE_ANON_KEY=...
```

### 🧩 Architecture

The backend follows Clean Architecture + CQRS principles:

Controller → Command/Query → Handler → Repository → DbContext

    Commands (POST, PUT, DELETE) → mutate data

    Queries (GET) → read data

    Handlers contain business logic

    Repositories encapsulate EF Core operations

    Middleware handles logging & exceptions globally

### 🧠 Example CQRS Flow
```bash
POST /api/meals
  ↓
CreateMealCommand
  ↓
CreateMealHandler (validates + saves via repository)
  ↓
MealRepository.AddAsync()
  ↓
Supabase (PostgreSQL)
```

### 🧱 Roadmap

- [x] Setup Clean Architecture base (.NET 8)

- [x] EF Core + Supabase integration

- [ ] Implement extra API Functions

- [ ] Expo mobile app scaffold

- [ ] Supabase Auth integration

- [ ] AI food recognition (Azure Vision)

- [ ] OpenAI fitness recommendations

- [ ] Azure CI/CD pipeline

- [ ] Cloud deployment (App Service + Blob Storage)

### 🔒 Security Notes

This repository is public for educational and portfolio purposes only.
All secrets are stored locally in .env files and excluded from version control via .gitignore.

### 🧰 Local Development Tools

    VS Studio
    
    VS Code / Rider

    Azure Data Studio (for Postgres)

    Postman

    Expo Go (for mobile testing)

### 💬 Contact

Darwin Gutierrez
🌐 LinkedIn

### 📜 License

MIT License – see LICENSE
for details.