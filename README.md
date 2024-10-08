# E-Commerce Platform
This E-Commerce project is a full-featured online shopping platform built with ASP.NET Core and follows the Onion Architecture pattern. The system targets three main roles (Admin, Brand Owner, and Customer) and supports essential functionalities for online retail, including authentication, payment integration, and product management.

# Features
Secure user authentication and authorization with JWT.<br>
Payment processing integration using Stripe.
Product management with categories including Baby, Women, and Men clothing.
Supports role-based access for Admin, Brand Owner, and Customer.
Automapper integration for streamlined object mapping.
Dependency injection for decoupling and improved testability.
Uses MSSQL Server with EF Core for database management.
LINQ for efficient data querying.

# Technologies Used
ASP.NET Core Web API
JWT (JSON Web Tokens) for Authentication
Automapper for object-to-object mapping
Dependency Injection (DI)
Entity Framework Core (EF Core)
MS SQL Server for data storage
LINQ for data manipulation
Stripe API for payment processing
Onion Architecture for maintaining a clean code structure

# System Roles
The platform serves the following user roles:

Admin: Manages the overall system, including users and settings.
Brand Owner: Can manage their own products and view orders related to their brand.
Customer: Can browse products, add them to their cart, and proceed to checkout and payment.

# Categories
The product catalog is divided into three categories:

Baby Clothes
Women Clothes
Men Clothes



