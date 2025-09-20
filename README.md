# BookStore - E-Commerce Platform
A fully functional, production-ready Online Book Store built with ASP.NET Core MVC. This project demonstrates a modern, scalable backend architecture for an e-commerce platform that can be adapted to sell any type of product online.

---

# ✨Features
- User Authentication & Authorization
  * User Registration and Login with ASP.NET Core Identity
  * Email Verification during sign-up with expiring confirmation tokens
  * Role-based UI rendering (e.g., Admin vs. Customer)


- E-Commerce Functionality
  * Browse and search through the product categories
  * Secure Online Payments powered by Stripe integration
  * Order history and summary

- Admin Management
  * CRUD operations for Products, Categories, etc
  * Manage user roles and orders


- Responsive Design
  * Built with Bootstrap for a clean
---
# 🛠️ Tech Stack
- Backend Framework: ASP.NET Core MVC (.NET 8)
- Programming Language: C#
- Database: MS SQL Server
- ORM: Entity Framework Core
- Architecture:
    * Repository Pattern
    * Unit of Work Pattern
- Authentication & Authorization: ASP.NET Core Identity
- Payment Gateway: Stripe API
- Email Service: SMTP (for email verification)
- Frontend: HTML5, CSS3, JavaScript, Bootstrap 5
- Tools: jQuery, AJAX

  ---
  # 🗄️ Project Architecture
  This project follows a structured architecture to ensure separation of concerns, testability, and maintainability.

```text
BookStore/
├── Controllers/   # MVC Controllers
├── Models/        # Domain Models & ViewModels
├── Views/         # Razor Views
├── Data/          # ApplicationDbContext and Migrations
├── Repository/    # Repository Interfaces and Implementations
├── Utility/       # Helper Classes (e.g, Stripe Service, Email Sender)
├── wwwroot/       # Static files (JS, CSS, Images)
```
The Repository Pattern and Unit of Work are used to abstract the data layer, making the application more flexible and easier to test

# 📦Installation & Setup
Follow these steps to run this project locally on your machine.
- Prerequisites
   * [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
   * [SQL Server] (https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express Edition is sufficient)
   * [Visual Studio 2022] (https://visualstudio.microsoft.com/vs/)
   * 
