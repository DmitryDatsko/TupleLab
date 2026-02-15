# TupleLab - C# Tuples in Production

A hands-on exploration of C# tuples through practical ASP.NET backend scenarios. This project demonstrates when and how to use tuples effectively in real-world applications.

## What I Built

Three practical implementations showing tuple usage patterns:

### 1. Parsing & Validation
**Connection String Parser** - Parse database connection strings with comprehensive validation
- Returns tuple: `(bool Success, string Host, int Port, string Database, string Error)`
- Handles edge cases: null input, invalid formats, malformed ports
- Uses dictionary-based parsing for reliability

**Key Features:**
- Type-safe enum-based validation
- Clear error messages
- No exceptions for control flow

### 2. Data Access Layer
**Product Search Service** - EF Core service with filtering and pagination
- Returns tuple: `(List<Product> Products, int TotalCount, int FilteredCount, string AppliedFilters, TimeSpan ExecutionTime)`
- Price range filtering with pattern matching
- Performance monitoring built-in

**Key Features:**
- Async/await throughout
- SQLite with EF Core
- Query optimization

### 3. Pattern Matching
**Access Control Service** - Role-based authorization using switch expressions
- Returns tuple: `(bool HasAccess, string Reason, int StatusCode)`
- Four roles: Guest, User, Moderator, Admin
- Four resources with granular permissions

**Key Features:**
- Modern C# switch expressions
- Exhaustive pattern matching
- Clear access denial reasons

## Running the Project

```bash
# Clone the repository
git clone https://github.com/DmitryDatsko/TupleLab.git 
cd TupleLab

# Create db with entries
dotnet ef database update

# Restore dependencies
dotnet restore

# Run the project
dotnet run
```

## Sample Output

```
---- Connection String Parser ---
Success: True
Host: localhost, Port: 5432, Database: mydb
*****
Success: False
Host: , Port: 0, Database:

---- Product Search ----
Found 3 products
Filters: searchTerm: s, minPrice: 1.5
Execution time: 00:00:00.1453665 ms

---- Access Control Service ----
For admin request: HasAccess: True, Reason: You're Admin, Status code: 200
For user request: HasAccess: False, Reason: You're User, Status code: 403
```
