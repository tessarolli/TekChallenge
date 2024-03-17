# Tek Challenge

## 1. Requirements
This challenge must include the following:
1.1. Create a REST API using the most dominant language and framework.
1.2. Document the API using Swagger.
1.3. Apply SOLID principles and Clean Code.
1.4. Implement the solution using TDD (Test-Driven Development).
1.5. Use good patterns for request validations, and also consider HTTP status codes for each request made.
1.6. Structure the project into N layers.
1.7. Upload the project to GitHub publicly.
1.8. Specify in the project's README:
   - 1.8.1. Justify the choice of cloud resources and their scalability.
   - 1.8.2. Justify the monitoring strategy.
   - 1.8.3. Brief description of the patterns used.
   - 1.8.4. Architecture diagram used in the project.
   - 1.8.5. Infrastructure diagram.
   - 1.8.6. Instructions for running the project locally.
   - 1.8.7. Any additional information that the candidate considers relevant.

## 2. Statement
Currently, there is the web part of a product registration system, and it is desired to create an API service that supports the following functionalities:
2.1. Perform Insert(POST), Update(PUT), and GetById(GET) operations for a product master.
2.2. Log the response time of each request made in a plain text file.
2.3. Cache a dictionary of product statuses for 5 minutes, whose values are shown in the following table:
   - Status (key) StatusName (value)
     - 1 Active
   - 0 Inactive
   Standard Cache, Lazy Cache, or any other type of cache deemed appropriate can be used.
2.4. Persist product information locally using any type of persistence. 
   - Mandatory fields are: ProductId, Name, Status(0 or 1), Stock, Description, and Price. 
   - Additional fields can be added if the evaluated person considers them relevant.
2.5. The GetById method must return a product with the following fields:
   - ProductId
   - Name
   - StatusName This field is obtained from the cache created in 2.3 based on the "Status".
   - Stock
   - Description
   - Price
   - Discount Discount percentage [0-100] obtained from an external service based on the ProductId. This service can be https://mockapi.io/ or another.
   - FinalPrice Price * (100 - Discount) / 100
   - ... Other fields defined by the candidate

## 3. Deployment
For deploying the solution, the following must be considered:
3.1. Deploy on any of the 3 clouds: AWS, Azure, GCP.
3.2. It must be demonstrated that the solution supports a defined number of users by the candidate.
3.3. Add service monitoring.
