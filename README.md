# Tacx Activities API

## Provisioning

This project uses a storage account and a cosmos database. You can provision them with templates stored in `ARMTemplates` directory. You need to provide globally unique storage account and cosmos account names and also your principal ID.

## Building and running

There are no surprises. You can build this project in an IDE or using command line:

```
dotnet build
```

Entry point is Tacx.Activities.Api project, which you can also run from IDE or CLI.
Remember to change values in appsettings.json

## Assignment vs production code

This is a recruitment assignment, not production code, therefore I decided to include as many different items and types of tests as possible at the cost of test coverage. There are also several aspects that would need to be addressed before calling this code production ready:

* There should be strongly typed logging code sending events to AppInsights
* There should be retry policy implemented for external dependencies (e.g. using Polly)
* There should be CI pipeline code
* There would probably be some authorization requirements
