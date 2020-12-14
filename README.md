# GloboTicket Example Application

Example app for the course "Fundamentals of Distributed Systems" on Pluralsight, by Michael L Perry.

## Setting Up a Mac

To run on a Mac, you will need to run SQL Server in a Docker container.
Install Docker Desktop for the Mac, and then pull the base image using `Scripts/pull.sh`.
Then start up a container using `Scripts/startsql.sh`.

To connect to that instance of SQL Server, you will need to change the connection string.
The easiest way to do this is with User Secrets.
Manage the user secrets for GloboTicket.Promotion.csproj.
You can use the [.NET Core User Secrets](https://marketplace.visualstudio.com/items?itemName=adrianwilczynski.user-secrets) extension by Adrian Wilczyński.
Set your user secrets file like this:

```json
{
    "ConnectionStrings": {
        "PromotionContext": "Data Source=.;Database=GloboTicket-promotion;User ID=sa;Password=Pass@word1;MultipleActiveResultSets=true"
    }
}
```

## Creating the Database

Install the EF command-line tools in order to work with the application database.
Run this command:

```bash
dotnet tool install --global dotnet-ef
```

Initialize the application database by running migrations.
Use the following command:

```bash
dotnet ef database update --project GloboTicket.Promotion/
```

## Running the App

Start up the Promotion Web application with this command:

```bash
dotnet run --project GloboTicket.Promotion
```

Or run GloboTicket.Promotion from Visual Studio.

## Running the Emailer and Indexer

The Emailer is a mock service that stands in for a process that emails about new shows.
The Indexer simulates a service that indexes shows for search.
They both use MassTransit to manage RabbitMQ.
To start RabbitMQ, create a Docker container.
To start it in a Docker container, run the shell script:

```bash
Scripts/startrabbitmq.sh
```

Then start the Emailer and Indexer console apps and schedule a show.
