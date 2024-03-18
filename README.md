# Tek Challenge Project

Tek Challenge is a Cloud Native Containerized Distributed System built for scaling using .NET Core and PostgreSQL as the primary data storage. 
It follows Clean Architecture, Domain-Driven Design (DDD), and CQRS (Commands and Queries Responsibility Segregation) principles to ensure the code is organized, maintainable, and scalable.
The application provides an API for users to Products in a Catalog, along with Authentication and Authorization.

## Tech stack
1. **.NET Core**: A free, open-source, cross-platform framework for building modern, cloud-based, internet-connected applications.
2. **Dapper**: A simple, lightweight ORM (Object-Relational Mapping) that makes it easy to work with relational databases in .NET applications.
3. **Dapr**: Distributed Application Runtime, is an open-source, portable runtime that simplifies building distributed applications with a focus on interoperability and abstraction of common infrastructure concerns.
4. **Envoy**: Envoy is a high-performance, open-source edge, and service proxy designed for modern, cloud-native architectures. It offers advanced features such as load balancing, service discovery, dynamic routing, observability, and security. Envoy is widely used in microservices-based applications to handle network communication between services efficiently and reliably. Its extensible architecture and robust features make it a popular choice for building scalable and resilient distributed systems.
5. **MediatR**: A library that allows for the easy implementation of the Mediator pattern by providing a simple interface for sending and handling requests between objects, typically in a request-response fashion. This can be especially useful in large applications where there are many classes and dependencies, as it helps to simplify communication and reduce coupling between different parts of the codebase.
6. **Mapster**: A fast, convention-based object-object mapper that allows you to easily convert objects of one type to another, with support for nested mapping and customization.
7. **PostgreSQL**: A powerful, open source object-relational database system.
8. **Redis**: An open-source, high-performance, in-memory data store used for caching, state management, and message broker purposes.

## Applied Architectural Patterns, Software Design Principles and or standards

Throughout the distributed application source code, you will encounter several architectural/design patterns and standards/principles. Here are some of them:

### Architectural Patterns

- **Clean Architecture**: Clean Architecture emphasizes separating concerns into layers, with clear boundaries and dependencies flowing inward. This architectural pattern prioritizes business logic and domain models at the core, surrounded by layers such as applications, interface adapters, and frameworks, each with specific responsibilities. By following Clean Architecture principles, the application remains highly maintainable, testable, and adaptable to evolving requirements.

- **CQRS (Command Query Responsibility Segregation)**: CQRS is a design pattern that separates the responsibility for reading and writing data into separate models. It distinguishes between commands (actions that change state) and queries (actions that retrieve data). By segregating these concerns, CQRS enables the optimization of read and write operations independently, leading to improved scalability, performance, and maintainability.

- **Dependency Injection**: Dependency Injection (DI) is a design pattern used to implement inversion of control (IoC) in software development. It allows components to be loosely coupled by injecting dependencies into a class from an external source rather than creating them internally. DI promotes modularity, testability, and maintainability by making it easier to replace dependencies and manage component lifecycles.

- **Factory Pattern**: The Factory Pattern is a creational design pattern that provides an interface for creating objects without specifying their concrete classes. It defines a method for creating objects based on certain conditions or parameters, allowing for flexibility and encapsulation of object creation logic. By using the Factory Pattern, the application can decouple object creation from the client code, making it easier to extend and maintain.

- **Fluent Validation**: Fluent Validation is an architectural pattern used to define and enforce validation rules within software systems in a fluent and declarative manner. Instead of scattering validation logic throughout the codebase, fluent validation allows developers to define validation rules in a centralized and expressive way. This typically involves chaining methods or using a fluent interface to specify validation constraints for different data fields or objects. By separating validation concerns from business logic, fluent validation promotes code reuse, maintainability, and testability while ensuring data integrity and consistency across the application.

- **Fluent Results**: Fluent Results is an architectural pattern used in software design to handle the outcome of operations or functions fluently and expressively. It involves encapsulating the result of an operation, typically in the form of a success or failure, within a specialized data structure or object. By using fluent interfaces or method chaining, developers can compose operations sequentially and handle success and failure cases more elegantly. This pattern promotes the readability, maintainability, and composability of code by providing a consistent and intuitive way to handle outcomes throughout the application.

- **Microservices**: Microservices is an architectural style that structures an application as a collection of loosely coupled services, each responsible for a specific business function. Each microservice is developed, deployed, and scaled independently, often using lightweight communication protocols such as HTTP or messaging queues. Microservices promote flexibility, scalability, and resilience, as well as enable continuous delivery and easier maintenance of complex systems.

- **MVC (Model-View-Controller)**: MVC is a software architectural pattern commonly used for developing user interfaces. It divides an application into three interconnected components: Model, View, and Controller. The Model represents the application's data and business logic, the View displays the user interface elements, and the Controller handles user input, updating the Model and View accordingly. MVC promotes separation of concerns, making it easier to maintain and extend applications.

- **Password Hashing**: Password Hashing is a security-focused architectural pattern used to protect user credentials by securely storing passwords in a hashed and salted format. This design principle involves using cryptographic hashing algorithms, such as bcrypt or Argon2, to convert plain-text passwords into irreversible hash values before storing them in a database. By incorporating password hashing into software systems, developers can mitigate the risk of password-related vulnerabilities, such as password leaks or brute-force attacks. Additionally, salting techniques add an extra layer of security by appending a unique random value to each password before hashing, further enhancing protection against dictionary attacks and rainbow table-based cracking methods. Password hashing promotes data confidentiality and integrity, safeguarding sensitive user information from unauthorized access or exploitation.

- **Repository Pattern**: The Repository Pattern provides an abstraction layer between the application's business logic and data access mechanisms, such as databases or web services. It centralizes data access logic within repositories, allowing for better separation of concerns and improved testability. By utilizing the Repository Pattern, the application can achieve a cleaner architecture and reduce coupling with specific data storage implementations.

- **RESTful architecture**: REST (Representational State Transfer) is an architectural style for designing networked applications. RESTful architecture emphasizes using HTTP methods (such as GET, POST, PUT, and DELETE) to perform CRUD (Create, Read, Update, Delete) operations on resources. It leverages stateless communication between client and server, where each request from the client contains all the necessary information for the server to fulfill it. RESTful APIs are typically designed to be simple, scalable, and loosely coupled.

### Design Principles and Standards

- **API Contracts**: API (Application Programming Interface) contracts are formal agreements that define the interactions and expectations between different software components or systems. These contracts outline the methods, parameters, data formats, authentication mechanisms, and behavior that developers can rely on when integrating with an API. API contracts serve as blueprints for building and consuming APIs, ensuring consistency, interoperability, and compatibility between different software modules, services, or platforms. They help establish clear guidelines for developers, streamline development efforts, and mitigate potential integration issues by specifying how data should be exchanged and processed. API contracts can take various forms, including OpenAPI specifications, Swagger documents, RAML files, or custom documentation formats tailored to the specific requirements of the API.

- **Caching**: Caching is a design principle used to improve the performance and scalability of software systems by storing frequently accessed data in a temporary storage location, known as a cache. This architectural pattern involves leveraging caching mechanisms, such as in-memory caches or distributed caches, to reduce latency and alleviate the load on backend resources. By caching data at various levels of the application stack, developers can accelerate response times and enhance scalability by minimizing the need for repeated computations or data retrieval from backend services. Effective caching strategies ensure data consistency, handle cache invalidation and prevent stale or outdated information from being served to users, thereby optimizing system performance and resource utilization.

- **Clean Code**: Clean Code principles focus on writing code that is easy to read, understand, and maintain. It emphasizes concepts such as meaningful naming, small functions, single responsibility, and minimal duplication. By writing clean code, developers can reduce bugs, improve collaboration, and enhance the overall quality of the software.

- **Domain-Driven Design (DDD)**: Domain-Driven Design is an approach to software development that focuses on understanding and modeling the core domain of a system. It encourages collaboration between domain experts and developers to create a shared understanding of the domain's complexities. DDD emphasizes structuring the codebase around the domain model, utilizing ubiquitous language, and defining clear boundaries between different domains within the system. This approach aims to improve the alignment between software design and business requirements, leading to more effective and maintainable solutions.

- **Exception Handling**: Exception Handling is a design principle used to manage and respond to unexpected or exceptional conditions that arise during the execution of software programs. This principle involves designing robust error-handling mechanisms to detect, handle, and recover from errors gracefully, ensuring system integrity and preventing catastrophic failures. By implementing techniques such as try-catch blocks, exception propagation, and error recovery strategies, developers can identify and mitigate errors effectively, minimizing service disruptions and providing a seamless user experience. Exception handling promotes code reliability, maintainability, and resilience by facilitating the detection and resolution of issues in a systematic and controlled manner.

- **JWT (JSON Web Token)**: JWT is a compact, URL-safe means of representing claims to be transferred between two parties. It's commonly used for securely transmitting information between parties as a JSON object. JWTs consist of three parts: a header, a payload, and a signature, each encoded in Base64. The header typically contains metadata about the token such as the type and signing algorithm, while the payload contains the claims (e.g., user ID, expiration time) being transmitted. The signature is created by combining the encoded header, payload, and a secret key and then hashed, ensuring the integrity of the token. JWTs are commonly used for authentication and authorization in web applications, enabling stateless communication and allowing servers to verify the authenticity of incoming requests without needing to maintain a session state.

- **Lazy Loading**: Lazy loading is a programming technique primarily used in software development to defer the loading of non-essential resources until the point at which they are needed. This approach optimizes performance and resource utilization by postponing the initialization of objects or components until they are required for execution. In practice, lazy loading can be implemented in various scenarios, such as loading images or data asynchronously when they come into view in a web application, or delaying the instantiation of objects in object-oriented programming until their methods are invoked. By incorporating lazy loading, developers can significantly enhance application responsiveness, reduce initial load times, and improve overall user experience, especially in resource-intensive environments or when dealing with large datasets.

- **Minimal API**: A minimal API refers to a programming interface designed with simplicity and efficiency in mind, stripping away unnecessary complexity and features to provide only the essential functionality required for a specific task or use case. Minimal APIs are often lightweight, easy to understand, and quick to implement, making them ideal for scenarios where simplicity and performance are prioritized over extensive functionality. These APIs typically focus on providing the core functionality needed to accomplish a specific task without adding unnecessary overhead or complexity, resulting in more streamlined and maintainable codebases.

- **ORM (Object-Relational Mapping)**: ORM is a programming technique that allows developers to map objects from the application domain to relational database tables and vice versa. It abstracts away the complexities of database interaction, providing a higher-level interface for data manipulation. By using an ORM framework, developers can write database-driven applications more efficiently and with less boilerplate code, increasing productivity and reducing development time.

- **Singleton Pattern**: The Singleton Pattern is a creational design pattern that ensures a class has only one instance and provides a global point of access to that instance. It involves defining a class with a method that either creates a new instance if one does not exist or returns the existing instance. The Singleton Pattern is commonly used for managing shared resources, configuration settings, or caching objects.

- **SOLID**: SOLID is an acronym representing five design principles: Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion. These principles aim to make software designs more understandable, flexible, and maintainable by promoting modular code structures, reducing dependencies, and enhancing code reusability. By adhering to SOLID principles, the application becomes more resilient to changes and easier to extend over time.

- **Test-Driven Development (TDD)**: Test-Driven Development is a software development approach where tests are written before the implementation code. It emphasizes creating automated tests that define the desired functionality of the code before writing the actual code. This iterative process helps ensure that the code meets the specified requirements and maintains desired functionality as it evolves. TDD improves code quality, encourages modular design, and provides a safety net for refactoring, resulting in more robust and maintainable software.

- **Vertical Slicing Architecture**: Vertical Slicing Architecture advocates for organizing software systems into vertical slices, each representing end-to-end functionality across all layers of the application. In this architectural paradigm, every slice encompasses user interfaces, business logic, and data access layers, facilitating a holistic view of system capabilities. Vertical slices prioritize delivering value incrementally by focusing on complete features rather than isolated components. By embracing Vertical Slicing Architecture, development teams can achieve enhanced collaboration, faster delivery cycles, and greater alignment with user needs.

## Architecture Diagram
Insert Architecture Diagram here

## Infrastructure Diagram
Insert Infrastructure Diagram here

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

1. **API Gateway**: Serves as the entry point for external clients to interact with the system. Routes incoming requests to the appropriate microservices based on the requested endpoint.

2. **Auth Service**: Responsible for managing user authentication and authorization. Utilizes JWT (JSON Web Tokens) for secure authentication. Implements role-based access control (RBAC), distinguishing between user roles such as regular users, managers, and administrators. Controls access to different parts of the system based on user roles and permissions. Handles CRUD operations related to user entities, including creation, retrieval, updating, and deletion of user accounts.

4. **Discount Service**: Act as a single source of truth for calculating product discount amounts.

5. **Product Catalog Service**: Manages the CRUD operations for products in the catalog. This includes functionalities for adding, updating, deleting, and retrieving product information.

### Communication:

- **REST API**: Exposes a set of RESTful endpoints for external clients to interact with the system. Utilizes HTTP methods such as GET, POST, PUT, and DELETE for performing CRUD operations.

- **Dapr Service Invocation**: Facilitates communication between microservices using Dapr Service Invocation.

### Deployment:

- **Docker Containers**: Each microservice is packaged as a lightweight, self-contained Docker container, ensuring consistency and portability across different environments.

- **Continuous Integration/Continuous Deployment (CI/CD)**: Implements automated pipelines for building, testing, and deploying microservices. Ensures rapid and reliable delivery of software updates.

### Scalability and Resilience:

- **Horizontal Scaling**: Utilizes Azure Container Apps to dynamically scale individual microservices based on resource utilization and incoming traffic.

- **Fault Tolerance**: Implements redundancy and failover mechanisms to ensure system resilience in the face of hardware failures or network issues.

## Tests Layer
The Tests Layer is a crucial component aimed at ensuring the reliability, functionality, and correctness of our software system. It comprises a suite of automated tests developed using popular testing frameworks including Xunit, Nsubstitute, and Fluent Assertions. This documentation provides an overview of the purpose, structure, and usage of the Tests Layer.

### Purpose

The primary purpose of the Tests Layer is to validate the behavior of our software components across various scenarios and edge cases. By automating tests, we can systematically verify that each unit of code performs as expected, detects regressions, and maintains compatibility as the codebase evolves. This layer also aids in identifying and debugging issues early in the development cycle, promoting higher software quality and faster iteration.

### Components

#### 1. Xunit

[Xunit](https://xunit.net/) is a popular unit testing framework for .NET applications. It provides a simple, extensible architecture for writing and executing unit tests in C#. Xunit offers features such as parameterized tests, test categorization, and test parallelization, enabling efficient and comprehensive test coverage.

#### 2. Nsubstitute

[Nsubstitute](https://nsubstitute.github.io/) is a flexible mocking library for .NET. It allows developers to create mock objects and define their behaviors during test setup. Nsubstitute simplifies the process of simulating dependencies, enabling isolated unit testing and facilitating the testing of components in isolation from their collaborators.

#### 3. Fluent Assertions

[Fluent Assertions](https://fluentassertions.com/) is a fluent assertion library that enhances the readability and expressiveness of test assertions in C#. It provides a rich set of assertion methods and a fluent syntax for composing assertions in a natural language style. Fluent Assertions promotes clear, descriptive test code that is easier to understand and maintain.

### Structure

The Tests Layer is organized into separate test projects corresponding to the different layers and modules of our application architecture. Each test project contains test classes that target specific units of code, such as classes, methods, or functional units. Within these test classes, individual test cases are defined to cover different scenarios and behaviors.

### Usage

Developers can run the tests using integrated development environment (IDE) tools such as Visual Studio or via command-line interfaces using build automation tools like MSBuild or .NET CLI. To execute the tests, simply build the solution and run the test runner, which will execute all tests and report the results, including any failures or errors encountered during the test run.

## Running in Local Development Environment
To run the application locally, follow these steps:

### Environment Setup
First, you have to set up your local environment with some dependencies:

1. **Docker**:
    - Install Docker Desktop on your local environment, following the instructions
      here: [Docker Desktop Setup](https://docs.docker.com/get-docker/).

2. **.NET 8.0**:
    - Ensure you have .NET 8.0 installed. If you don't have it, you can download it
      here: [Dotnet 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0).

### Cloning and executing
After the above requirements are met, you can proceed with the following steps:

1. **Clone the repository:**

```shell
git clone https://github.com/tessarolli/TekChallenge.git`
```

2. **Enter the TekChallenge Directory:**

```shell
cd TekChallenge
```

3. **Install dependencies:**

```shell
dotnet restore TekChallenge.sln
```

4. **Run with docker-compose:**

```shell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build
```

### Accessing the services and Documentation

1. **API Gateway:**
For convenience, there is an Envoy API Gateway available at:

  (http://localhost:5000/)

You can use it to access all distributed services resources from a single base URL.
Available endpoints are:
- /authentication
- /discounts
- /products
- /users

2. **Authentication Service:**
    To access the Authentication Service Api Documentation, open a browser window at:

  (http://localhost:5010/swagger/)

3. **Discounts Service:**
    To access the Discounts Service Api Documentation, open a browser window at:

  (http://localhost:5020/swagger/)

4. **Products Service:**
    To access the Products Service Api Documentation, open a browser window at:

  (http://localhost:5030/swagger/)


## Azure Cloud Production Environment
We've chosen Azure Cloud for its robust infrastructure and comprehensive set of services that align well with our application's requirements. Azure provides scalable solutions for container orchestration, databases, authentication, and monitoring, allowing us to build and deploy our distributed system with ease.

Scalability: Azure offers auto-scaling capabilities through services like Azure Container Apps (ACA) and Redis cache. These services allow us to dynamically adjust resource allocation based on demand, ensuring optimal performance and availability as our user base grows.

Ps: Swagger is disabled in the production environment.

You can access your resources in Azure Public Cloud (for a short time) from these URLs:

1. **API Gateway:**
  (https://api-gateway-app.wonderfulwave-67ac4af3.eastus.azurecontainerapps.io/)

2. **Authentication Service:**
  (https://auth-app.wonderfulwave-67ac4af3.eastus.azurecontainerapps.io/)

3. **Discounts Service:**
  (https://discount-app.wonderfulwave-67ac4af3.eastus.azurecontainerapps.io/)

4. **Products Service:**
  (https://product-app.wonderfulwave-67ac4af3.eastus.azurecontainerapps.io/)

### Production Environment Monitoring Strategy

For monitoring our distributed system, we've chosen Azure Application Insights. Azure Application Insights provides powerful monitoring and diagnostics capabilities for our application, allowing us to collect telemetry data, track performance metrics, detect issues, and gain insights into user behavior. It integrates seamlessly with Azure services and offers features such as application performance monitoring, request tracking, exception logging, and dependency tracking, enabling us to monitor the health and performance of our application effectively.

In addition, we utilize Azure Container Apps to monitor our containerized microservices deployed within the Azure environment. Azure Container Apps offers built-in monitoring and observability features, including container health checks, logs aggregation, metrics visualization, and autoscaling based on resource utilization. By leveraging Azure Container Apps, we can monitor the performance and availability of our microservices, detect and troubleshoot issues in real time, and ensure the reliability and scalability of our distributed system.

## Conclusion:

The architecture of Tek Challenge emphasizes modularity, scalability, and maintainability, enabling the development of robust, distributed systems capable of handling complex business requirements. 
By adhering to best practices and leveraging modern technologies, Tek Challenge delivers a reliable and scalable solution for managing user authentication, user management, and product catalog management.

## Contributors
- Luiz Tessarolli (@tessarolli)
