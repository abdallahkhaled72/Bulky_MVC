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
   * [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express Edition is sufficient)
   * [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

 ## Steps

1. **Clone the repository:**
```bash
git clone https://github.com/abdallahkhaled72/Bulky_MVC.git
cd BookStore
```

2. **Database Setup:**
   
   * Update the connection string in `appsettings.json` to point to your local SQL Server instance
   * Run the following commands in the Package Manager Console (PMC) to create the database:
     
   ```update-database
   Update-Database
   ```

   3. **Configure Secrets (Stripe & Email):**
      * For local development, use the Secret Manager tool:
        ```bash
        dotnet user-secrets set "Stripe:SecretKey" "your_stripe_secret_key_here"
        dotnet user-secrets set "Stripe:PublishableKey" "your_stripe_publishable_key_here"
        dotnet user-secrets set "Gmail:Password" "your_app_specific_gmail_password_here"
        ```
