# 🎉 EventEase Booking System – Complete Project (Parts 1–3)

## 👋 Introduction

This is a full-stack web application developed for **EventEase**, an event management company. The **EventEase Booking System** enables users to create, manage, and book venues and events efficiently.

The system is built using **ASP.NET Core MVC**, **Entity Framework Core**, and is deployed to **Microsoft Azure**. The project is divided into **three parts**, each expanding on the previous with more functionality, better UX, and cloud capabilities.

---

## 📂 Project Parts Overview

### ✅ Part 1 – Core Functionality

> **Objective**: Build the foundational booking system with basic CRUD operations.

**Key Features**:
- CRUD operations for Venues, Events, and Bookings.
- Relational data model with Entity Framework Core.
- Razor Views and Controllers for MVC separation.
- Initial validations and UI setup.
- Local database using SQL Server or LocalDB.

---

### 🔄 Part 2 – Enhanced Functionality & Azure Prep

> **Objective**: Improve the system’s usability, add data views, and prepare for Azure deployment.

**Enhancements**:
- Added a **BookingView** to show joined data (Booking + Event + Venue).
- Input validation and improved form UI.
- Search bar and filtered data display.
- Connected Azure SQL Database and deployed initial version to **Azure Web App**.
- Integration with **Azure Blob Storage** (optional image/file handling).
- GitHub integration and version control setup.

---

### 🚀 Part 3 – Final Deployment & Filtering

> **Objective**: Complete system features and fully deploy the system on Azure.

**New Additions**:
- Advanced filtering: search bookings by venue, date, or event type.
- Azure Web App full deployment.
- `.gitignore` cleanup (excluding `bin/` and `obj/` directories).
- Bug fixes and performance optimization.
- Polished UI and error handling.

---

## 🧰 Technologies Used

- ✅ ASP.NET Core MVC (latest .NET version)
- ✅ Entity Framework Core
- ✅ Microsoft SQL Server & Azure SQL
- ✅ Visual Studio Code (macOS development)
- ✅ Azure Web App & Azure Portal
- ✅ Git & GitHub

---

## 🗂️ Project Structure

EventEaseBookingSystem/
│
├── Controllers/
│ ├── BookingController.cs
│ ├── EventController.cs
│ └── VenueController.cs
│
├── Models/
│ ├── AppDbContext.cs
│ ├── Booking.cs
│ ├── Event.cs
│ ├── Venue.cs
│ └── BookingView.cs
│
├── Views/
│ ├── Booking/
│ ├── Event/
│ └── Venue/
│
├── wwwroot/
│ └── site.css, js/
│
├── Program.cs
├── appsettings.json
├── EventEaseBookingSystem.csproj
└── README.md

yaml
Copy
Edit

---

## 💻 How to Run Locally

1. **Clone the repository**:
   ```bash
   git clone https://github.com/VCCT-CLDV6211-2025-G3/ST10457455_CLDV6211_Part1.git
Navigate to the directory:

bash
Copy
Edit
cd EventEaseBookingSystem
Restore dependencies:

bash
Copy
Edit
dotnet restore
Build and run the project:

bash
Copy
Edit
dotnet build
dotnet run
Access locally at:
https://localhost:5001

☁️ Azure Deployment
Hosted on Azure Web App

Connected to Azure SQL Database

Connection strings managed via Azure App Settings

✅ Git & Version Control Notes
Used .gitignore to exclude unnecessary files:

markdown
Copy
Edit
bin/
obj/
*.user
*.vs
Used branches and commits to track progress:

main: production-ready deployment

part2-updates: new features for Part 2

part3-final: final filtering and cloud deployment updates

📅 Timeline Summary
Phase	Focus	Completed ✅
Part 1	Core CRUD Functionality	✅
Part 2	Enhanced UX, Azure Prep, Views	✅
Part 3	Advanced Filtering + Azure Deployment	✅

🙌 Acknowledgements
Special thanks to the instructors, peers, and Microsoft Learn documentation that supported the development and deployment of this project.

📜 License
This project is for academic purposes and submitted as part of the CLDV6211 Portfolio of Evidence.
