# Hardship Application System

A full-stack web application for creating and managing hardship applications.

---

## Overview

The Hardship Application System is a full-stack application that allows users to:

- Create hardship applications
- Edit existing applications
- View a list of submitted applications

The project demonstrates:

- Clean architecture principles
- CQRS pattern using MediatR
- RESTful API development
- React frontend integration
- Environment-based configuration
- Proper Git and project hygiene

---

# Technology Stack

## Backend

- **Framework:** ASP.NET Core Web API (.NET 8)
- **Architecture Pattern:** CQRS
- **Mediator:** MediatR
- **ORM:** Entity Framework Core
- **Database:** SQLite
- **API Documentation:** Swagger

## Frontend

- **Framework:** React (TypeScript)
- **Build Tool:** Vite
- **Routing:** React Router
- **HTTP Client:** Axios
- **Notifications:** React Hot Toast
- **Styling:** Tailwind CSS

---

# Project Structure

hardship-app/
│
├── backend/
│ └── Hardship.Api
│
├── frontend/
│ └── src/
│
├── README.md
└── .gitignore


---

# Prerequisites

Ensure the following tools are installed:

- .NET 8 SDK
- Node.js (v18+ recommended)
- npm (comes with Node.js)
- Git

---

# Backend Setup

## 1 Navigate to backend folder
        cd backend
## 2 Restore dependencies
        dotnet restore
## 3    Restore dependencies
        dotnet ef database update 
        (This will automatically create the SQLite database file.)
## 4    Run the API
        dotnet run 
        (Once running, Swagger will be available at: https://localhost:<port>/swagger)

---

# Frontend Setup

## Navigate to frontend folder
        cd frontend
## Install dependencies
        npm install
## Configure environment variables
        Create a .env file inside the frontend root and add:

            VITE_API_BASE_URL=https://localhost:<backend-port>

        Ensure the port matches your backend API port.

## Start the frontend
        npm run dev

---

# rchitecture Notes

## Backend

    - CQRS separation between Commands and Queries
    - MediatR used for request handling
    - DTOs used for response shaping
    - EF Core for data persistence

## Frontend

    - Feature-based folder structure:
        features/
        hardships/
            api/
            components/
            pages/
            types/

    - Centralized API layer
    - Environment-driven base URL
    - Reusable form components

## Future Enhancements

    - **Authentication & Authorization**  
    Implement JWT-based authentication with role-based access control.
    - **Production-Ready Database**  
    Replace SQLite with PostgreSQL or SQL Server for scalable deployment.
    - **Automated Testing**  
    Add backend unit/integration tests and frontend component testing.
    - **Containerization & CI/CD**  
    Dockerize the application and implement a CI/CD pipeline for deployment.
