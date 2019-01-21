# Time Tracking Web Application

**Time Tracking** is a standalone (micro)service, which contains web api for time tracking requests processing.
It used for time tracking purposes, users can track time what they spent on specific issue.

## Tech Stack
* [Asp.Net Core 2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)
* [EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)
* [F#](https://fsharp.org/)
* [PostgreSQL](https://www.postgresql.org/)
* [flyway](https://flywaydb.org/)
* [Docker](https://www.docker.com/)
* [Powershell](https://docs.microsoft.com/en-us/powershell/scripting/overview?view=powershell-6)

## Solution Structure

```
/TimeTracking
├── FP.TimeTracking.Core                     # Contains common functionality of the system, contracts
├── FP.TimeTracking.Domain                   # Contains business entities and EF DbContext
├── FP.TimeTracking.Service                  # Contains business logic and time tracking validation rules
├── FP.TimeTracking.Test                     # Contains Unit Test for all layers
├── FP.TimeTracking.Web                      # Contains Api contracts and enpoinds implementation
├── time-tracking                            # Contains flyway migration SQL scripts
├── FP_TimeTracking.postman_collection.json  # Postman collection with API request smaples
├── flyway-migrate.ps1                       # Powershell flyway migration script
├── runflyway.bat                            # Migration runner
```

## Start Project
1. [Configure PostgreSQL and Flyway](https://github.com/khdevnet/postgres-tools/tree/master/flyway)
3. Run flyway migrations
```
$ runflyway.bat
```
4. Run Application    
Use **TimeTracking.sln** solution to run using docker or IIS Express

## REST API
### Create Time Track
#### Request

Type: POST

URL: https://[domain-name]/api/timetracking

Body:

| Property name       | Type           | Description  |
| ------------------- |:-------------- |:-----|
| IssueId             | string         | Issue id |
| createdBy           | string         | User Email |
| description         | string         | Issue description |
| timeMinutes         | number         | Time spent on issue in minutes |
| reportedDate        | string         | Time track date |

Body Sample:
```
{
  "IssueId": "18009ca1-f0d4-419e-8ff6-0d356ba71541",
  "createdBy": "anton@gmail.com",
  "description": "issue description",
  "timeMinutes": 180,
  "reportedDate": "2018-11-13T10:57:52Z"
}
```
#### Response

| Status Code | Description  |
| -------------|:-----|
| 200     | Success.|
| 400     | Bad Request. Time track information not valid |

Body Sample:
```
{
    "data": {
        "id": "a03baa9a-ccab-476c-a033-6fa8b51e932c",
        "issueId": "18009ca1-f0d4-419e-8ff6-0d356ba71541",
        "timeMinutes": 180,
        "createdAt": "2019-01-13T15:47:23.4227892+02:00",
        "reportedDate": "2018-11-13T10:57:52Z",
        "createdBy": "xantshc",
        "description": "time report description"
    },
    "messages": [
        {
            "message": "Time track a03baa9a-ccab-476c-a033-6fa8b51e932c was added.",
            "levelType": "Info"
        }
    ]
}
```
### Get Issue Time tracking
#### Request
Type: GET

URL: https://[domain-name]/api/timetracking/{issueId}

#### Response
| Status Code | Description  |
| -------------|:-----|
| 200     | Success. |
| 500     | Server exception. |

Body Sample:
```
{
  "data": [
    {
      "id": "7f8308b1-577c-4eb9-849c-8fda9491c2f6",
      "issueId": "18009ca1-f0d4-419e-8ff6-0d356ba71541",
      "timeMinutes": 180,
      "createdAt": "2019-01-13T15:56:11.771083+02:00",
      "reportedDate": "2018-11-13T10:57:52Z",
      "createdBy": "xantshc",
      "description": "time report description"
    }
  ],
  "messages": []
}
```

### Delete Issue Time
#### Request
Type: Delete

URL: https://[domain-name]/api/timetracking/{id}

#### Response
| Status Code | Description  |
| -------------|:-----|
| 200     | Success. |
| 500     | Server exception. |

Body Sample:
```
{
  "id": "7f8308b1-577c-4eb9-849c-8fda9491c2f6",
  "issueId": "18009ca1-f0d4-419e-8ff6-0d356ba71541",
  "timeMinutes": 180,
  "createdAt": "2019-01-13T15:56:11.771083+02:00",
  "reportedDate": "2018-11-13T10:57:52Z",
  "createdBy": "xantshc",
  "description": "time report description"
}
```
