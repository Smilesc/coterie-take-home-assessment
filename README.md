## What it does
Simple API that accepts a business type, amount of revenue, and a list of states, and calculates insurance quotes for the business per state.

## How to run it
A) In the `Coterie.Api` project directory, run `dotnet run`, or choose the `Coterie.Api` run configuration in your IDE
  - Hit the endpoint via Postman or cURL - `https://localhost:5001/quote`
  - View documentation at `https://localhost:5001/swagger`

B) Choose the `IIS Express` run configuration
  - Hit the endpoint via Postman or cURL - `https://localhost:44307/quote`
  - View documentation at `https://localhost:44307/swagger`

## Example request/response

#### Example request
```json
{
  "business": "Plumber",
  "revenue": 6000000,
  "states": [
    "TX",
    "OH",
    "FLORIDA"
  ]
}
```

#### Example response
```json
{
  "business": "Plumber",
  "revenue": 6000000,
  "premiums": [
    {
      "premium": 11316,
      "state": "TX"
    },
    {
      "premium": 12000,
      "state": "OH"
    },
    {
      "premium": 14400,
      "state": "FL"
    }
  ],
  "isSuccessful": true,
  "transactionId": "27373db4-56c3-4383-a2e1-f55c77b4aa3f"
}
```