# Pizza Meow API

## Project Overview

The **Pizzeria Orders Management API** is a RESTful Web API designed to manage orders, menu items, and customer information for a pizzeria. The API provides endpoints for order creation, updating order status, managing the menu, and handling customer data.

## Features

- 📦 **Order Management** – Create, update, and delete orders.
- 🍕 **Pizza Management** – Create, update, and delete pizzas.
- 🧑‍🍳 **Customer Handling** – Register new customers and manage their data.
- 📊 **Order Tracking** – Monitor order status and track their progress.
- 🔒 **Authentication & Authorization** – Secure endpoints with Cookies authentication.

## Technologies Used

- **ASP.NET Core** (Web API)

- **Entity Framework Core** (Database ORM)

- **MS SQL Server** (Database)

- **Cookies Authentication**

- **Fluent Validation**

- **Swagger** (API Documentation)

## Prerequisites

- .NET SDK 6.0 or later
- MS SQL Server
- Postman (or any API testing tool)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/pizzeria-api.git
   cd pizzeria-api
   ```
2. Set up the database connection in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=PizzaMeow;Trusted_Connection=True;"
   }
   ```
3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

## API Endpoints

### Orders (Requires authorization)

- **POST /orders** – Create a new order.
- **GET /orders/{id}** – Retrieve an order by ID.
- **PUT /orders/{id}** – Update an order's status.
- **DELETE /orders/{id}** – Cancel an order.
- **POST /orders/getToWork** Sends order to couriers group with a customer location

### Pizza
- **POST /pizza** – Create a new order.
- **GET /pizza/{id}** – Retrieve all pizzas available.
- **GET /pizza/{id}** – Retrieve an pizza by ID.
- **PUT /pizza/{id}** – Update an pizza status.
- **DELETE /pizza/{id}** – Deletes pizza.

## Authentication

- To access secured endpoints, Cookies auth provided:

  ```bash
  POST /login
  ```

- Running in Docker

1. Build and run the Docker container:
   ```bash
   docker build -t pizzeria-api .
   docker run -p 5000:5000 pizzeria-api
   ```

## Testing

- Use Postman or Swagger at `/swagger/index.html` to test API endpoints.

## Prepared user to login:
**Admin:**
Email: admin1@gmail.com
Password: Admin123

**Courier:**
Email: courier1.pizzameow@gmail.com
Password: Courier123

**User:**
Email: user1.pizzameow.stas@gmail.com
Password: User123

## Project not completed yet
**It can still have issues**


