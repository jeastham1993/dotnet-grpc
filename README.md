# GRPC on .NET Core

Playing around with using GRPC for inter-service communication in a typical microservices architecture.

The solution contains two simple projects:

1. JEasthamDev.Grpc.Orders - A GRPC Server that has two service methods, one to get a specific order and one to list all orders for a given customer. Both of which just create objects in memory and return them.
2. JEasthamDev.Grpc.RestProxy - A REST API with HTTP GET endpoints for retrieving customer orders and a specific order. The HTTP endpoints pass the request directly on to the GRPC server and return the GRPC result.

The proto also include one depreciated property to give an example of the compiler warnings.

## Local Execution

Open two command windows and navigate into the root directories for the two services. Run

``` bash
dotnet run
```

in both. The HTTP API will start on http://localhost:5010. You can then make requests to:

- http://localhost:5010/order/test@test.com
- http://localhost:5010/order/test@test.com/order/anyrandomstring

## Further Developments

- Add Adapters to translate between the GRPC response and a 'OrderDTO' object. Condenses all of the coupling into one place.
- Investigate how to use DI with a GrpcClient, can it be created in REST startup?