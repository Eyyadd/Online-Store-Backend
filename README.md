# E-Commerce Platform
This E-Commerce project is a full-featured online shopping platform built with ASP.NET Core and follows the Onion Architecture pattern. The system targets three main roles (Admin, Brand Owner, and Customer) and supports essential functionalities for online retail, including authentication, payment integration, and product management.

# Features
Secure user authentication and authorization with JWT.<br>
Payment processing integration using Stripe.<br>
Product management with categories including Baby, Women, and Men clothing.<br>
Supports role-based access for Admin, Brand Owner, and Customer.<br>
Automapper integration for streamlined object mapping.<br>
Dependency injection for decoupling and improved testability.<br>
Uses MSSQL Server with EF Core for database management.<br>
LINQ for efficient data querying.<br>

# Technologies Used
ASP.NET Core Web API <br>
JWT (JSON Web Tokens) for Authentication <br>
Automapper for object-to-object mapping <br>
Dependency Injection (DI)<br>
Entity Framework Core (EF Core)<br>
MS SQL Server for data storage<br>
LINQ for data manipulation<br>
Stripe API for payment processing<br>
Onion Architecture for maintaining a clean code structure<br>

# System Roles
The platform serves the following user roles:<br>
Admin: Manages the overall system, including users and settings.<br>
Brand Owner: Can manage their own products and view orders related to their brand.<br>
Customer: Can browse products, add them to their cart, and proceed to checkout and payment.<br>

# Categories
The product catalog is divided into three categories:<br>
Baby Clothes<br>
Women Clothes<br>
Men Clothes



