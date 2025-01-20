# E-Commerce Web API

This is a scalable and modular **E-Commerce Web API** built using **.NET 8** and designed with a **N-Layer Architecture** for efficient code organization and maintainability.

---

## **Features**
- **Authentication & Authorization**:  
  - JWT-based authentication for secure user access.
  - Role-based authorization for user and admin functionalities.

- **User Management**:  
  - User registration, login, and profile management.  
  - Password recovery functionality.

- **Product Management**:  
  - CRUD operations for managing product inventory.  
  - Integration of Redis caching for faster product retrieval.
  - Discount service of products.

- **Order Management**:  
  - Order creation, viewing, and management by users.  
  - Automatic price fetching during order creation.

- **Warehouse Management**:  
  - Control stocks of products based on warehouses.
  - Transfer stocks between warehouses 

•	**Data Management**: Utilizes Entity Framework Core for database interactions with support for SQL Server.

•	**Caching**: Implements caching using StackExchange.Redis and In-Memory Caching.

•	**Messaging**: Integrates with RabbitMQ for message queuing.

•	**Payment Processing**: Supports PayPal for payment processing.

•	**Testing**: Includes xUnit for unit testing and Moq for mocking dependencies.

•	**Web API**: Provides a client for interacting with Web APIs.

## **Technologies Used**
- **Framework**: .NET 8 Web API
- **Architecture**: N-Layered (API, Service, Repository, Core)
- **Database**: SQL Server (with Entity Framework Core)
- **Message Queue**: RabbitMQ (CloudAMQP)
- **Caching**: Redis, In-Memory
- **Authentication**: JWT Tokens
- **API Documentation**: Swagger

## **Project Structure**
```plaintext
E-Commerce/
├── API/                  # API layer for exposing endpoints

├── Service/              # Business logic and service layer

├── Repository/           # Data access layer (EF Core and database operations)

├── Core/                 # Shared models and interfaces

└── Redis & RabbitMQ/     # Redis and RabbitMQ implementations
```


Endpoints on Swagger UI

![image](https://github.com/user-attachments/assets/cdee30c5-d0b2-46b2-85f0-ead52a0e9122)
![image](https://github.com/user-attachments/assets/1f77aa47-5416-4f30-9f7d-b16101d85c5b)
![image](https://github.com/user-attachments/assets/88931294-dd41-4def-9d2f-9dbfbcc2270a)
![image](https://github.com/user-attachments/assets/def9c9b3-5d10-487e-bb88-3339b998522f)




 
