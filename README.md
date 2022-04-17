---
languages:
- csharp
page_type: sample
description: "The project should be started by selecting multiple startup projects from the Solution properties and then selecting the apies options."
products:
- aspnet-core-api
- aspnet-core
- refit
- rabbitMQ
- mongoDB
- redis
- excelCreater
- swagger
---

# ASP.NET Core API and Microservice project
The project should be started by selecting multiple startup projects from the Solution properties and then selecting the apies options.

- Adding, deleting, updating contacts should be done using ContactApi.
- Afterwards, a report request should be sent using the report service.
- This service first sends this data to the **SendQueue** method of the service (QueueService) using RabbitMQ, by sending a request to the **ContactAll** method, going to the Contact service and pulling the Contact information, and it is put into the queue. The Queue service creates a queue and makes a request to the **CreateReport** method in the JobService layer with the incoming json data.
- As stated in Assesment, JobService creates a report information with the incoming data and saves it in the folder it is in.
- If there is no error during excel creation, it returns true.
- Our Queue service consumes this value with **ConsumeQueue** method and consumer it as reportCreated.

* Method names are shown in bold.

## License

See [LICENSE](https://github.com/oguzhanyikilmaz/AssessmentPhoneDirectory/blob/master/LICENSE.md).

## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [oguzhanyklmz27@gmail.com](mailto:oguzhanyklmz27@gmail.com) with any additional questions or comments.
