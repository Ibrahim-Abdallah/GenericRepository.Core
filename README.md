# GenericRepository.Core

A lightweight and reusable **Generic Repository** implementation built with **.NET 9.0** and **Entity Framework Core** to streamline data access logic and promote **Clean Architecture** practices.

## ✅ Features

- Generic CRUD operations
- Pagination support (`PagedResult<T>`)
- AsNoTracking toggle for queries
- `ExistsAsync` and `IsUniqueAsync` helpers
- Plug-and-play via DI extension method
- Clean, minimal, and testable design

---

## 📦 Installation

1. Clone or add this project as a Git submodule.
2. Reference it from your main solution.

---

## 🛠️ Usage

### 1. Register the Repository

In your main project’s `Program.cs` or `Startup.cs`:
```csharp
services.AddDbContext<YourDbContext>(options => /* configure db */);
services.AddGenericRepository();

public class ProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAll().ToListAsync();
    }
}
```

---

## 📁 Project Structure
```bash
GenericRepository.Core/
│
├── Interfaces/
│   └── IRepository<T>
│
├── Implementations/
│   └── GenericRepository<T>
│
├── Pagination/
│   ├── PagedResultBase
│   └── PagedResult<T>
│
├── DependencyInjection/
│   └── ServiceCollectionExtensions.cs
│
├── GlobalUsings.cs
├── LICENSE
└── README.md
```

---

## 📋 Requirements

- .NET 9.0
- Entity Framework Core 9.0

---

## 🤝 Contributing

Feel free to fork and contribute via PRs. Suggestions and improvements are welcome!

---

## 📄 License

MIT License
