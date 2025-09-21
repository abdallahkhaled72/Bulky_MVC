# Bulky - E-Commerce Platform
A fully functional, production-ready Online Book Store built with ASP.NET Core MVC. This project demonstrates a modern, scalable backend architecture for an e-commerce platform that can be adapted to sell a diverse range of products online.

---

#  Features
- User Authentication & Authorization
  * User Registration and Login with ASP.NET Core Identity
     * External login: Facebook  
  * Email Verification during sign-up with expiring confirmation tokens
  * Role-based UI rendering (e.g., Admin vs. Customer)


- E-Commerce Functionality
  * Browse and search through the product categories
  * Track your order progress
  * Secure Online Payments powered by Stripe integration
  * Order history and summary

- Admin Management
  * CRUD operations for Products, Categories, etc
  * Lock users
  * Create new user
  * Manage user roles and orders


- Responsive Design
  * Built with Bootstrap for a clean
---
###  Role-Based Access Control (RBAC) & Payment Terms

The application implements a sophisticated authentication system with four distinct user roles. A key feature is the **differentiated payment processing logic** based on the user's role:

| Role | Responsibilities | Payment Terms |
| :--- | :--- | :--- |
| **Admin** | Global system administrator. Manages all products, categories, users, and companies. | N/A |
| **Company** | Represents a business selling on the platform. Manages their own product catalog. | **✅ Delayed Payment Approved.** Orders are approved immediately but payment can be delayed for up to 30 days. |
| **Employee** | Works for a **Company**. Can update order statuses and manage products for their company. | **✅ Delayed Payment Approved.** Inherits the payment terms of their associated company. |
| **Customer** | An individual shopper. Browses and purchases items for personal use. | **❌ Immediate Payment Required.** Payment must be completed successfully at the time of checkout using Stripe. |
---
# Tech Stack
- **Backend Framework:** ASP.NET Core MVC (.NET 8)
- **Programming Language:** C#
- **Database:** MS SQL Server
- **ORM:** Entity Framework Core
- **Architecture:**
    * Repository Pattern
    * Unit of Work Pattern
- **Authentication & Authorization:** ASP.NET Core Identity, Facebook OAuth 2.0
- **Payment Processing:** Stripe API
- **Other Services:** Custom Email Service (SMTP for email verification), Session Management
- **Frontend:** HTML5, CSS3, JavaScript, Bootstrap 5, Razor Views
- **Tools:** jQuery, AJAX

  ---
  #  Solution Architecture
  This solution is structured into multiple Class Library projects to enforce a clear **Separation of Concerns** and promote **maintainability**. The architecture follows a layered principle, where each layer has a distinct responsibility and references only the layers it depends on.

```text
Bulky.sln (Solution)
│
├──  Bulky.Web (Main ASP.NET Core MVC Project)
│   ├── Areas/
│   │   ├── Admin/
│   │   ├── Customer/
│   │   └── Identity/
│   ├── Controllers/
│   ├── Views/
│   ├── wwwroot/
│   └── Program.cs (Configures services from other libraries)
│
├──  Bulky.DataAccess (Class Library)
│   ├── Data/ (Contains ApplicationDbContext)
│   ├── Repository/ (IRepository, IUnitOfWork, and their implementations)
│   ├── DbInitializer/ (IDbInitializer and DbInitializer)
│   └── Migrations/ (Entity Framework Core Migrations)
│
├──  Bulky.Models (Class Library)
│   ├── ViewModels/ (All ViewModels, e.g., ShoppingCartVM)
│   ├── ApplicationUser.cs
│   ├── Category.cs
│   ├── Product.cs
│   └── ... (All other Domain Models)
│
└──  Bulky.Utility (Class Library)
    ├── SD.cs (Static Details - Constants for roles, order statuses, etc.)
    ├── StripeSettings.cs (Stripe configuration DTO)
    └── EmailSender.cs (Implementation of IEmailSender)
```

# Project Dependencies & References
The dependency graph illustrates the clean separation between layers:

```text
Bulky.Web
    ├── Depends on: Bulky.DataAccess
    ├── Depends on: Bulky.Models
    └── Depends on: Bulky.Utility

Bulky.DataAccess
    └── Depends on: Bulky.Models (The DataAccess layer knows about the Models)

Bulky.Utility
    └── Depends on: Bulky.Models (e.g., if EmailSender uses a Model)

Bulky.Models
    └── Has no dependencies. (This is the core of the application)
```

# Layer Responsibilities

| Layer                | Purpose                                                    | Key Components                                          |
|----------------------|------------------------------------------------------------|---------------------------------------------------------|
| **Bulky.Web**        | **Presentation Layer**<br>Handles HTTP requests, UI logic, and serves views. | Controllers, Razor Views, JavaScript, CSS               |
| **Bulky.Models**     | **Domain Layer**<br>Contains the core business entities and data contracts. | Entity Classes, ViewModels, Enums                       |
| **Bulky.DataAccess** | **Data Access Layer**<br>Abstracts all database interactions. | DbContext, Repository Pattern, Unit of Work, Migrations |
| **Bulky.Utility**    | **Cross-Cutting Concerns Layer**<br>Holds common services and helpers used across other layers. | Email Service, Payment Gateway Config, Constants        |


The Repository Pattern and Unit of Work are used to abstract the data layer, making the application more flexible and easier to test

# Installation & Setup
Follow these steps to run this project locally on your machine.
- Prerequisites
   * [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
   * [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express Edition is sufficient)
   * [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

 ## Steps

1. **Clone the repository:**
```bash
git clone https://github.com/abdallahkhaled72/Bulky_MVC.git
cd Bulky
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
4. **Run the Application:**
   ```bash
   dotnet run
   ```
---
# Usage
1. **Register a new account.** You will receive a verification email
2. **Log in** and browse the book catalog
3. **Log in** by Facebook account
4. **Add books** to your shopping cart
5. **Proceed to checkout** and use Stripe's test card to simulate a payment:
   * Card Number: `4242 4242 4242 4242`
   * Expiry: Any future date
   * CVC: Any 3 digits
6. **Admins** can access management features via the navigation bar

---
# Screenshots
## Home Page
<img width="600" height="400" alt="UserInterface" src="https://github.com/user-attachments/assets/dd5b0727-3bef-4f6f-9fc0-a5bc399687ff" />

## Order Summary For User
<img width="600" height="400" alt="OrderSummaryForUserToPlaceOrder" src="https://github.com/user-attachments/assets/e915ab84-2b43-46d4-9bdc-6d17524908db" />

## Payment Status
<img width="600" height="400" alt="PaymentStatus" src="https://github.com/user-attachments/assets/853676c2-da6d-4467-be54-8b7df56fb98c" />


---
# Contributing
This is a personal portfolio project. However, **suggestions and feedback** are always welcome! Feel free to fork the repo and submit pull requests for any improvements

---
# Contact
   - Email: [abdullakhalid7857166@gmail.com](mailto:abdullakhalid7857166@gmail.com)
   - LinkedIn: [Abdullah Khaled](https://www.linkedin.com/in/abdullah-khaled-393a84233)  
   - GitHub: [abdallahkhaled72](https://github.com/abdallahkhaled72)

---
  # **If you found this project helpful or interesting, please give it a star on GitHub!** 
