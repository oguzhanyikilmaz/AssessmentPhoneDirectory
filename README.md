---
languages:
- csharp
page_type: sample
description: "The project should be started by selecting multiple startup projects from the Solution properties and then selecting the Case Study.Api and CaseStudy.Web options."
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
- This service firstly goes to the Contact service and retrieves Contact information, sends this data to the service using RabbitMQ and queues it up. Queue service creates a queue and requests the incoming json data to the JobService layer.
- As stated in Assesment, JobService creates a report information with the incoming data and saves it in the folder it is in.
- If there is no error during excel creation, it returns true.
- Our Queue service consumes this value and consumes it as reportCreated.

* Method names are shown in bold.

## License

See [LICENSE](https://github.com/oguzhanyikilmaz/AssessmentPhoneDirectory/blob/master/LICENSE.md).

## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [oguzhanyklmz27@gmail.com](mailto:oguzhanyklmz27@gmail.com) with any additional questions or comments.
