# OnlineClientRegistrationWebApplication

## Overview

This project is a web application for registering clients for cosmetic services, specifically nail treatments. It features both frontend and backend components, built using C#, .NET, JavaScript, HTML, CSS, AJAX, Bootstrap, and SQLite. The application is designed to run directly from the code and does not require a separate build or deployment process.

## Features

- **User Registration and Authentication**: Allows clients to register and secure login for managing access to the application.
- **Appointment Scheduling**: Schedule and manage appointments for cosmetic procedures.
- **Responsive Design**: Utilizes Bootstrap for a responsive and user-friendly interface.

## Technologies

- **Backend**: C#, .NET Core, ASP.NET(Razor Pages)
- **Frontend**: HTML, CSS, JavaScript, AJAX, Bootstrap
- **Database**: SQLite

## Setup

To get started with this project, follow these steps:

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [SQLite](https://www.sqlite.org/download.html) (for database management)

### Installation

1. **Clone the Repository**

    ```bash
    git clone https://github.com/yourusername/your-repo-name.git
    cd your-repo-name
    ```
2. **Restore NuGet Packages**

    ```bash
    dotnet restore
    ```
3. **Run the Application**

    The application can be started directly from the code using the .NET CLI:
    ```bash
    dotnet run
    ```
    This command will start the application on the default port (usually http://localhost:5000).

4. **Setup the Database**

    The application uses SQLite for data storage. The database is initialized automatically when the application runs for the first time.

### Usage
-  Navigate to http://localhost:5000 to access the application.
-  Use the registration form to create new client profiles and schedule appointments.
-  Admin users can manage clients and appointments via the admin panel.
