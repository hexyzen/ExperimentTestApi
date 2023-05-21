# ExperimentTestApi

## Project Description

This project is aimed at managing and analyzing experiments conducted on different devices. It provides an API to retrieve experiment data and statistics.

The project consists of the following components:

1. Controllers: The [ExperimentsController](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Web/Controllers/ExperimentController.cs) class handles HTTP requests related to experiments. It includes endpoints for retrieving experiment data, such as button color and price, based on device tokens.

2. Models: The [Experiment](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Common/Models/Experiment.cs) class represents an experiment and contains properties for the experiment key, device token, and experiment value. The [ExperimentStatistics](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Common/Models/Statistic.cs) class represents statistics for an experiment, including the experiment key, device count, experiment value, and option count.

3. Database: The project utilizes a SQL database to store experiment data. The [Experiment](https://github.com/hexyzen/ExperimentTestApi/blob/main/Database.txt) table has columns for the experiment ID (auto-incremented), device token, key, and value.

4. Dapper: Dapper is used as the ORM (Object-Relational Mapping) tool to simplify database operations. It provides a convenient way to query and map results to the corresponding model classes.

5. Unit Of Work + Repository: [The Unit of Work (UOW)](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Infrastructure/UnitOfWork.cs) + [Repository](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Infrastructure/Repositories/ExperimentRepository.cs) pattern is a design pattern commonly used in software development to manage data access and provide a layer of abstraction between the application and the underlying data storage. It promotes separation of concerns and improves code organization, reusability, and testability. The UOW represents a single transaction or a unit of work that encompasses multiple operations.

6. ExperimentRepository: The [ExperimentRepository](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Infrastructure/Repositories/ExperimentRepository.cs) class encapsulates the logic for retrieving experiment data and statistics. It interacts with the database using Dapper and returns the results in the desired format.

7. [NLog + MiddlewareException](https://github.com/hexyzen/ExperimentTestApi/blob/main/ExperimentApp.Web/Program.cs): Implemented middlewer exception for better exception handler via NLog.

Overall, this project allows clients to retrieve experiment data and obtain statistics about the experiments conducted on different devices. It provides a scalable and efficient solution for managing and analyzing experiments.
