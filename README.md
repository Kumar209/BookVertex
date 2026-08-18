# 📚 BookVertex — Full Stack Bookstore Platform

<p align="center">
  <img src="./previews/book.png" alt="BookVertex Logo" width="130"/>

</p>

<p align="center">
  <strong>✨ Your Gateway to Endless Stories ✨</strong>
</p>

<p align="center">
  A modern full-stack e-commerce bookstore built with ASP.NET Core MVC, Entity Framework Core, SQL Server, Stripe, Tailwind CSS, and DaisyUI.
</p>

---

## 🚀 Overview

**BookVertex** is a modern full-stack bookstore platform designed to provide a complete online shopping experience for browsing, purchasing, and managing books.

The application combines a responsive **ASP.NET Core MVC** frontend with a structured **N-Tier backend architecture** and includes:

* 📚 Product & Category Management
* 🛒 Shopping Cart & Checkout
* 💳 Stripe Payment Integration
* 🔐 Secure Authentication & Authorization
* 👥 Role-Based Administration
* 📦 Order Processing & Tracking
* 👤 User & Role Management
* 🎨 Responsive Multi-Theme UI

---

# 📸 Preview

## 🏠 Home Page
![alt text](/previews/home.png)

## 📚 Product Catalog
![alt text](/previews/products.png)
![alt text](/previews/product.png)


## 🛒 Shopping Cart
![alt text](/previews/cart.png)


## 💳 Checkout
![alt text](/previews/checkout.png)


## 📦 Order Management
![alt text](/previews/userOrder.png)
![alt text](/previews/userOrderDetails.png)


## 📊 Administration Dashboard
![alt text](/previews/admin.png)

## Product Management
![alt text](/previews/productsManagement.png)
![alt text](/previews/editProductManagement.png)

## Admin Order Management
![alt text](/previews/adminOrder.png)

## 👥 User Management
![alt text](/previews/userManagement.png)
![alt text](/previews/userRegister.png)


# ✨ Core Features

BookVertex delivers a complete e-commerce bookstore experience focused on **product discovery, secure purchasing, order management, and administrative control**.

### 🛍️ Customer Experience

* Browse books and categories
* View detailed product information
* Add and manage products in the shopping cart
* Proceed through checkout
* Complete secure payments through Stripe
* View order history
* Track order status

### 🔐 Authentication & Authorization

* ASP.NET Core Identity authentication
* Secure password hashing
* Role-based authorization
* Customer, Employee, and Admin roles
* User and role management
* Protected administrative operations

### ⚙️ Administration

* Product management
* Category management
* Order management
* Shipping information management
* User management
* Role management
* Password management
* Dashboard analytics

### 🎨 UI & Experience

* Responsive design
* Tailwind CSS
* DaisyUI components
* Multi-theme interface
* Mobile, tablet, laptop, and desktop support

---

# 🏗️ Technology Stack

## 🎨 Frontend

The frontend is built with:

* **ASP.NET Core MVC**
* **Razor Views**
* **Tailwind CSS**
* **DaisyUI**
* **Bootstrap Icons**
* **jQuery**

This provides a responsive, server-rendered architecture with a modern user interface.

---

## ⚙️ Backend

The backend is powered by:

* **ASP.NET Core**
* **C#**
* **Entity Framework Core**
* **ASP.NET Identity**
* **N-Tier Architecture**

The architecture provides structured business logic, data access, authentication, authorization, and application services.

---

## 🗄️ Database

**Microsoft SQL Server** is used as the primary database for storing:

* Users
* Products
* Categories
* Orders
* Order Details
* Payments
* Application Data

---

## 💳 Payment

**Stripe** is integrated into the checkout workflow for secure online payment processing and payment intent management.

---

## ☁️ Hosting

BookVertex is configured for deployment on **MonsterASP.NET** with SQL Server hosting support.

---

# 🔌 Third-Party Integrations

## 💳 Stripe

Stripe powers the application's online payment functionality, handling payment intents and secure checkout processing before orders are completed.

## ☁️ Cloudinary

Cloudinary is used for secure cloud-based storage and delivery of book and product images, providing efficient image management and optimized media delivery throughout the application.

## 📊 Chart.js

Chart.js powers the administration dashboard analytics, including:

* Revenue trends
* Monthly order activity
* Order status distribution
* Product category statistics

---

# 🏛️ Architecture

BookVertex follows a modular **N-Tier Architecture** with clear separation between presentation, business services, data access, models, and shared utilities.

```text
                    ASP.NET Core MVC
                           │
                           ▼
                  ┌─────────────────┐
                  │ Presentation    │
                  │     Layer       │
                  └────────┬────────┘
                           │
                           ▼
                  ┌─────────────────┐
                  │ Business        │
                  │ Service Layer   │
                  └────────┬────────┘
                           │
                           ▼
                  ┌─────────────────┐
                  │ Data Access     │
                  │     Layer       │
                  └────────┬────────┘
                           │
                           ▼
                  ┌─────────────────┐
                  │ Entity          │
                  │ Framework Core  │
                  └────────┬────────┘
                           │
                           ▼
                  ┌─────────────────┐
                  │ SQL Server      │
                  │    Database     │
                  └─────────────────┘
```

The application separates business logic from database operations through service interfaces and implementations, helping maintain a clean and scalable codebase.

Authentication and authorization are handled through **ASP.NET Identity**, while **Stripe** is integrated into the payment workflow.

---

# 🛒 E-Commerce Experience

BookVertex provides a complete shopping workflow from **product discovery to order completion**.

```text
Browse Books
     │
     ▼
Product Details
     │
     ▼
Add to Cart
     │
     ▼
Shopping Cart
     │
     ▼
Checkout
     │
     ▼
Stripe Payment
     │
     ▼
Order Confirmation
     │
     ▼
Track Order
```

Customers can browse available books, explore categories, view product details, manage quantities in their shopping cart, proceed through checkout, and complete payments through Stripe.

After successful checkout, orders are created with their associated order details and payment information. Customers can then view their orders and track their current order status.

---

# 📦 Order Management

BookVertex provides a complete order lifecycle for administrators and employees.

### 🔄 Order Lifecycle

```text
┌─────────┐
│ Pending │
└────┬────┘
     │
     ▼
┌──────────┐
│ Approved │
└────┬─────┘
     │
     ▼
┌────────────┐
│ Processing │
└──────┬─────┘
       │
       ▼
┌─────────┐
│ Shipped │
└─────────┘
```

Orders can also be cancelled according to the application's business rules.

### 🚚 Shipping Management

Administrators and employees can manage:

* Carrier
* Tracking Number
* Shipping Date
* Order Status

Customers can view their order information and track the current status of their purchases.

---

# 🔐 Security

Security is a core component of BookVertex's architecture.

The application uses:

* **ASP.NET Core Identity**
* Password hashing
* Role-based authorization
* Protected controller actions
* Server-side validation
* Secure user management workflows

### 👥 User Roles

| Role               | Access                                                                                       |
| ------------------ | -------------------------------------------------------------------------------------------- |
| 👤 **Customer**    | Browse products, manage cart, checkout, and view orders                                      |
| 👨‍💼 **Employee** | Manage products and orders                                                                   |
| 🛡️ **Admin**      | Full administrative access including users, roles, products, orders, and dashboard analytics |

Administrative operations are protected using role-based authorization policies.

---

# 🚢 Deployment

BookVertex is designed to be deployed as an **ASP.NET Core MVC application with SQL Server database hosting**.

### Deployment Stack

* ☁️ MonsterASP.NET Hosting
* 🗄️ SQL Server Database
* 💳 Stripe Payment Processing
* 🐙 GitHub Source Control

The application can be deployed as a single ASP.NET Core application containing the Razor-based frontend, backend services, business logic, and data access components.

---

# 🔮 Future Enhancements

## 🤖 AI Features

* AI-Powered Book Recommendations
* Personalized Reading Suggestions
* AI Book Summaries
* Intelligent Book Search

---

## 🛍️ Shopping Experience

* Wishlist
* Product Reviews & Ratings
* Discount & Coupon System
* Advanced Product Filtering

---

## 📊 Administration

* Advanced Sales Analytics
* Inventory Management
* Customer Activity Insights
* Sales Reports & Export

---

# 👨‍💻 Developer

## Prashant Kumar Verma

**Full Stack Developer** specializing in modern web applications built with ASP.NET Core, C#, SQL Server, Entity Framework Core, Angular, React, and cloud-based technologies.

BookVertex was developed as a complete end-to-end project demonstrating:

* E-commerce development
* Secure authentication
* Payment integration
* N-Tier architecture
* Responsive UI/UX design
* Order processing
* Role-based administration
* Database-driven application development

### 🧰 Core Expertise

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server
* REST APIs
* ASP.NET Identity
* Stripe Integration
* N-Tier Architecture
* Authentication & Authorization
* Tailwind CSS
* DaisyUI
* Responsive UI/UX

---

<p align="center">
  <strong>Built with ❤️ using ASP.NET Core MVC, C#, Entity Framework Core, SQL Server, Stripe, Tailwind CSS, and DaisyUI.</strong>
</p>
