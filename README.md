# LibraryLoans

📚 **LibraryLoans** is a simple Web API project designed to manage books and loans in a library system.  
This project demonstrates fundamental software development concepts such as BDD (Behavior-Driven Development), unit testing, and clean architecture principles.

---

## 🚀 Features

- **Book Management**: Manage library books with CRUD operations.
- **Member Management**: Manage library members with CRUD operations.
- **Loan Management**: Handle book loans and returns seamlessly.
- **Dependency Injection**: Decouple components for better testability and maintainability.
- **Rest exceptions Management**: Centralize JSON error response management with middleware for better separation of concerns.

---

## 📖 User Stories

- ✅ [Book Reservation](https://github.com/niolikon/LibraryLoans/issues/1)

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 8 or later](https://dotnet.microsoft.com/download)
- A text editor or IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/))

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/niolikon/LibraryLoans.git
   cd LibraryLoans
   ```
   
2. Restore dependencies
   ```bash
   dotnet restore
   ```
   
4. Run the project
   ```bash
   dotnet run
   ```
   
### Deploy on container
   
1. Create build artifact
   ```bash
   dotnet publish LibraryLoans.Api/LibraryLoans.Api.csproj -c Development -o ./output
   ```
   
2. Create project image
   ```bash
   docker build -t libraryloans-api:latest .
   ```
   
3. Configure database password
   ```bash
   echo "SA_PASSWORD=YourStrong@Passw0rd" >.env
   ```
   
4. Compose docker container
   ```bash
   docker-compose up -d
   ```

---

## 📬 Feedback

If you have suggestions or improvements, feel free to open an issue or create a pull request. Contributions are welcome!

---

## 📝 License

This project is licensed under the MIT License.
