README

Assumption/Decision Made 
* Surcharges are returned as a list mapped to ProductTypeID - based on requirement and similar to product/productType design
* No duplicate surcharges are returned per ProductTypeID - not clear if this should be an error or could be intentional
* Surcharges are flat rates - could be % based or other
* Kept both CalculateInsurance and  CalculateInsuranceOrder - unclear if CalculateInsurance should be removed as a feature
* No security implemented - auth credentials and roles to be defined
* No auditing (over logging) implemented - unclear of audit requirements


API Project
* Logging: Log4Net
* HTTP Error codes used
* Added Swagger for API documentation
* ProblemDetails middleware used to return common errors with common data contract
* Exceptions are mapped to ProblemDetails
* Custom Exception used for common negative flows (i.e. Not Found)
* Exceptions are designed to be bubbled up to consuming layer - removes many if/else statements
* Dependency Injection used 
* DDD Used (DTOs, Repositories, Services, Factories, Builders, etc)
* DTO standard used for Requests and Responses and Mapping to External Services
* Validation: ModelState and Attributes with Filters to abstract usage
* Insurance Calculations: Implemented in property Get methods as simple calculations are used. 
Can be moved to a Calculation Service if becomes more advanced.
* Repositories: Used to communicate to external services
* Services: Business services used to contain logic - used for insurance calculation sequencing


Recommendations:
* Caching can be implemented to improve performance - common GET operations such as Surcharges and ProductTypes (look at distributed caching approaches such as Redis)
* Move common DTOs and objects into a package (Nuget) that can be shared between projects - removes additional duplicate code
* No Security implemented - Get operations and CalculateInsurance can be anonymous. CalculateShoppingCart can be behind LoggedIn user. 
CRUD operations (future) to be only accessed by Admin roles (claims based) - Bearer tokens as a start


Test Project
* Unit Tests: NUnit (test outputs)
* Mock Tests: Moq (test behaviours)
* Integration Tests: NUnit (test scenarios end to end)
* BDD Tests: Would have liked to use SpecFlow but requires additional setup (testrunner changes)
* Split between Unit Tests and Integration Tests


Test Issue:
* Currently have a multiple execution issue with port binding (did not have time to fix) 
- might experience issues when trying to run all integration tests


Test Recommendations:
* Improve test coverage - add integration tests for Repository and more unit tests
* Implement BDD/SpecFlow Tests
* Add Load Tests
* Add API based Tests
* Potentially use Fluent Builders for Test Data

