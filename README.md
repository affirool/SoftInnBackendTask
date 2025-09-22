# TestAPI for Backend Task
Information as requested

## How to Run

### Restore NuGet packages
`dotnet restore`

### Build the project
`dotnet build`

### Run the project
Please select the `IIS Express` instead of `https` at Visual Studio `Run` options

### Design Decision
Almost all section was written according to request.
- Requested
  - List all available rooms 
  - Book a room for a guest
  - Get all bookings
- Added
  - Get booking by roomID
  - Add booking only if no conflict and return conflicting booking


## Reason (if any) of not using the suggested options
All request is completed according to specs.


## Bonus (Optional but Appreciated) 
- Used Dependency Injection for services
- Used Clean Architecture principles (Controllers, Services, Repositories)
- Added unit tests using xUnit, FluentAssertion(v7.0.0), FakeItEasy.
- Swagger for API documentation included.
- Suggested feature.
  - Get booking by room id and return all booking
  - I didnt filter to return only active booking as the sample data is inconsistent.
