# Tek Challenge Project

Tek Challenge is a Containerized Distributed System built for scaling using .NET Core and PostgreSQL as the primary data storage. 
It follows Clean Architecture, Domain-Driven Design (DDD) and CQRS (Commands and Queries Responsibility Seggregation) principles to ensure the code is organized, maintainable, and scalable.
The application provides an API for users to Products in a Catalog, along with Authentication and Authorization.

## Tech stack
- .NET Core: A free, open-source, cross-platform framework for building modern, cloud-based, internet-connected applications.
- Dapper: A simple, lightweight ORM (Object-Relational Mapping) that makes it easy to work with relational databases in .NET applications.
- Dapr: Distributed Application Runtime, is an open-source, portable runtime that simplifies building distributed applications with a focus on interoperability and abstraction of common infrastructure concerns.
- MediatR: A library that allows for the easy implementation of the Mediator pattern by providing a simple interface for sending and handling requests between objects, typically in a request-response fashion. This can be especially useful in large applications where there are many classes and dependencies, as it helps to simplify communication and reduce coupling between different parts of the codebase.
- Mapster: A fast, convention-based object-object mapper that allows you to easily convert objects of one type to another, with support for nested mapping and customization.
- PostgreSQL: A powerful, open-source object-relational database system.

## Available Features
- User authentication
- User management (CRUD)
- Product Catalog Management (CRUD)
- Role-based access control (user, manager, and admin)
- REST API for all actions

## System Architecture
The system architecture of Tek Challenge can be visualized as a set of microservices deployed within containers orchestrated by Kubernetes. 
Each microservice follows a modular architecture, leveraging the principles of Clean Architecture, Domain-Driven Design (DDD), and Command Query Responsibility Segregation (CQRS).

### Components:

1. **Authentication Service**: Responsible for managing user authentication and authorization. Utilizes JWT (JSON Web Tokens) for secure authentication.

2. **Authorization Service**: Implements role-based access control (RBAC), distinguishing between user roles such as regular users, managers, and administrators. Controls access to different parts of the system based on user roles and permissions.

3. **User Management Service**: Handles CRUD operations related to user entities, including creation, retrieval, updating, and deletion of user accounts.

4. **Product Catalog Service**: Manages the CRUD operations for products in the catalog. This includes functionalities for adding, updating, deleting, and retrieving product information.

5. **API Gateway**: Serves as the entry point for external clients to interact with the system. Routes incoming requests to the appropriate microservices based on the requested endpoint.

### Infrastructure:

- **Container Orchestration (Kubernetes)**: Manages the deployment, scaling, and monitoring of containerized microservices across a cluster of machines. Ensures high availability and fault tolerance.

- **Database (PostgreSQL)**: Provides persistent storage for application data, including user accounts, product information, and access control policies.

### Communication:

- **REST API**: Exposes a set of RESTful endpoints for external clients to interact with the system. Utilizes HTTP methods such as GET, POST, PUT, and DELETE for performing CRUD operations.

- **Dapr Service Invocation**: Facilitates communication between microservices using Dapr Service Invocation.

### Deployment:

- **Docker Containers**: Each microservice is packaged as a lightweight, self-contained Docker container, ensuring consistency and portability across different environments.

- **Continuous Integration/Continuous Deployment (CI/CD)**: Implements automated pipelines for building, testing, and deploying microservices. Ensures rapid and reliable delivery of software updates.

### Scalability and Resilience:

- **Horizontal Scaling**: Utilizes Kubernetes to dynamically scale individual microservices based on resource utilization and incoming traffic.

- **Fault Tolerance**: Implements redundancy and failover mechanisms to ensure system resilience in the face of hardware failures or network issues.

### Monitoring and Logging:

- **Logging**: Generates logs for capturing application events, errors, and diagnostic information. Utilizes centralized logging solutions for aggregation and analysis.

- **Metrics**: Collects performance metrics such as response times, throughput, and error rates to monitor the health and performance of the system. Utilizes tools like Prometheus and Grafana for visualization and analysis.

### Security:

- **JWT Tokens**: Implements secure authentication using JSON Web Tokens (JWT), ensuring that only authenticated users can access protected resources.

- **Encryption**: Utilizes encryption mechanisms to secure sensitive data such as user passwords and authentication tokens.

### Conclusion:

The architecture of Tek Challenge emphasizes modularity, scalability, and maintainability, enabling the development of robust, distributed systems capable of handling complex business requirements. By adhering to best practices and leveraging modern technologies, Tek Challenge delivers a reliable and scalable solution for managing user authentication, user management, and product catalog management.

### Tests Layer
Although this project wasn't designed using Test-Driven-Development (TDD), it is highly testable, and implements several features that make it suitable for comprehensive testing. For instance, the Tests Layer can include a suite of unit tests that cover the core functionality of the application. These tests can be designed to validate the behavior of individual components and functions and can be run quickly and easily during the development process.

## Installation and Setup
To run the application locally, follow these steps:

1. Clone the repository:

`git clone https://github.com/tessarolli/TekChallenge.Services.ProductService.git`

2. Install dependencies:

`dotnet restore`

3. Execute the User-Secrets.cmd script located in the TekChallenge.Services.ProductService.API folder, for registering sensitive information on local store.

4. Start the application:

`dotnet run`

## API Documentation
The API documentation can be found in the Swagger UI, which is available at `http://localhost:5000/swagger/index.html` when the application is running. The documentation includes information about each endpoint and its parameters.

## Contributors
- Luiz Tessarolli (@tessarolli)
