# VACATION TRACKING SYSTEM API

For the Xamarin Mentoring Program, this server-side API may be used for local development.
No durable storage is involved, all the fake data is preserved in memory (server's restart resets its state).

To start the server on Windows or Mac, you will need to setup [.NET Core SDK](https://www.microsoft.com/net/download/core) and run the commands:
```
dotnet restore
dotnet build
dotnet run
```

(!) To access your running server from your Android emulator, you will need to explicitly specify your machine's IP address instead of the "localhost" because the emulator works in its own virtual subnet.

## Sign In operation -----------------

> (POST) localhost:5000/vts/signin

- Request content-type: `application/json`
- Request content

```
{ "login" : "ark", "password" : "123" }
```

As a result, a json message is provided

```
{
  "result": "OK",
  "resultCode": 0
}
```

And in the `token` header, an authenticated token is included

```
token → d97b97e0-b73d-4534-8cf2-3ae80bd3f1e6
```

Since this operation is just for the demo, this token is not preserved on the server and no validation is performed further.

## Entities and Value types

For reference, check the [XMP Server Common sources](https://git.epam.com/mobile/xmp-server/tree/master/Common).

### VacationRequest entity
```
public class VacationRequest
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public VacationType VacationType { get; set; }
    public VacationStatus VacationStatus { get; set; }

    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
}
```

### VacationStatus enum
```
public enum VacationStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    InProgress = 3,
    Closed = 4
};
```

### VacationType enum
```
public enum VacationType
{
    Undefined = 0,
    Regular = 1,
    Sick = 2,
    Exceptional = 3,
    LeaveWithoutPay = 4,
    Overtime = 5
};
```

## Get a list of Vacation Requests -----------------

> (GET) localhost:5000/vts/workflow

As a result, a json message is provided with the **List** result

```
{
  "listResult": [
    {
      "id": "35055cea-59a5-43df-9d84-9f2bc8401008",
      "start": "2017-03-20T00:00:00",
      "end": "2017-03-30T00:00:00",
      "vacationType": 1,
      "vacationStatus": 2,
      "createdBy": "Someone",
      "created": "2017-01-04T12:27:20.902425Z"
    },
    {
      "id": "16954f5a-87cf-4030-8897-c8abe2e6d516",
      "start": "2016-12-26T00:00:00",
      "end": "2016-12-30T00:00:00",
      "vacationType": 1,
      "vacationStatus": 4,
      "createdBy": "Someone",
      "created": "2017-01-04T12:27:20.902484Z"
    }
  ],
  "result": "OK",
  "resultCode": 0
}
```

## Get a specific Vacation Request -----------------

> (GET) localhost:5000/vts/workflow/35055cea-59a5-43df-9d84-9f2bc8401008

As a result, a json message is provided with an **Item** result

```
{
  "itemResult": {
    "id": "35055cea-59a5-43df-9d84-9f2bc8401008",
    "start": "2017-03-20T00:00:00",
    "end": "2017-03-30T00:00:00",
    "vacationType": 1,
    "vacationStatus": 2,
    "createdBy": "Someone",
    "created": "2017-01-04T12:27:20.902425Z"
  },
  "result": "OK",
  "resultCode": 0
}
```

If no items found for the ID specified or ID provided is not parsed successfully as Guid

```
{
  "result": "Not found",
  "resultCode": -1
}
```

## Create or Update a specific Vacation Request -----------------

> (POST) localhost:5000/vts/workflow/

- Request content-type: `application/json`
- Request content

```
{
	"id": "35055cea-59a5-43df-9d84-9f2bc8401008",
	"start": "2017-03-20T00:00:00",
	"end": "2017-03-30T00:00:00",
	"vacationType": 1,
	"vacationStatus": 2,
	"createdBy": "TEST",
	"created": "2017-01-04T12:27:20.902425Z"
}
```

As a result, a json message is provided with an **Item** result stored on the server

```
{
  "itemResult": {
    "id": "35055cea-59a5-43df-9d84-9f2bc8401008",
    "start": "2017-03-20T00:00:00",
    "end": "2017-03-30T00:00:00",
    "vacationType": 1,
    "vacationStatus": 2,
    "createdBy": "TEST",
    "created": "2017-01-04T12:27:20.902425Z"
  },
  "result": "OK",
  "resultCode": 0
}
```

Required fields are: `Start`, `End` and `CreatedBy`. If `Id` and `Created` are not provided by the client, they're initialized automatically.
If a required value is missed, a json message is sent to the client

```
{
  "itemResult": null,
  "result": "To create or update a Vacation Request, fill its required fields: Start, End, CreatedBy",
  "resultCode": -1
}
```

## Delete a specific Vacation Request -----------------

> (DELETE) localhost:5000/vts/workflow/35055cea-59a5-43df-9d84-9f2bc8401008

As a result, a json message is provided

```
{
  "result": "OK",
  "resultCode": 0
}
```

If an item is not found or ID provided is not parsed successfully as Guid

```
{
  "result": "Not found",
  "resultCode": -1
}
```
