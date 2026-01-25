# ByteZone E-Commerce Platform

![ByteZone](E-CommerceASP/Scripts/images/OfficialLogo_ByteZone.png)

A full-featured e-commerce web application built with ASP.NET MVC, designed for managing online product sales with separate interfaces for customers and administrators.

## 🚀 Features

### Customer Features
- **User Registration & Authentication** - Secure customer account creation and login
- **Product Browsing** - View and search through available products
- **Shopping Cart** - Add products to cart and manage quantities
- **Product Details** - Detailed product information and specifications
- **Responsive Design** - Mobile-friendly interface using Bootstrap 5

### Admin Features
- **Admin Dashboard** - Centralized management interface
- **Product Management** - Add, edit, and remove products from inventory
- **Customer Management** - View and manage customer accounts
- **Product Entry Forms** - Streamlined product data entry

## 🛠️ Technology Stack

- **Framework:** ASP.NET MVC 5.2.9
- **Language:** C# (.NET Framework 4.7.2)
- **Database:** SQL Server LocalDB (MSSQLLocalDB)
- **Frontend:** 
  - Bootstrap 5.2.3
  - jQuery 3.7.0
  - Modern CSS3
- **Data Access:** ADO.NET with SqlClient
- **Package Manager:** NuGet

## 📋 Prerequisites

Before running this application, ensure you have the following installed:

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or higher
- SQL Server LocalDB (comes with Visual Studio)
- IIS Express (comes with Visual Studio)

## 🔧 Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/betaTrident/ByteZone.git
   cd ByteZone
   ```

2. **Open the solution**
   - Open `E-CommerceASP.sln` in Visual Studio

3. **Update Database Connection**
   - Navigate to [HomeController.cs](E-CommerceASP/Controllers/HomeController.cs)
   - Update the connection string to match your local setup:
     ```csharp
     string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\byteZone.mdf;Integrated Security=True";
     ```

4. **Restore NuGet Packages**
   - Right-click on the solution in Solution Explorer
   - Select "Restore NuGet Packages"

5. **Build the solution**
   - Press `Ctrl + Shift + B` or select Build → Build Solution

6. **Run the application**
   - Press `F5` or click the IIS Express button
   - The application will launch in your default browser

## 📁 Project Structure

```
E-CommerceASP/
├── App_Data/                  # Database files
│   ├── byteZone.mdf          # Main database
│   └── byteZone_log.ldf      # Database log file
├── App_Start/                 # Configuration files
│   ├── BundleConfig.cs       # Script and style bundling
│   ├── FilterConfig.cs       # Global filters
│   └── RouteConfig.cs        # URL routing rules
├── Controllers/               # MVC Controllers
│   └── HomeController.cs     # Main application controller
├── Models/                    # Data models
├── Views/                     # Razor views
│   ├── Home/                 # Home controller views
│   │   ├── Homepage.cshtml
│   │   ├── cusHomepage.cshtml
│   │   ├── adminHomepage.cshtml
│   │   ├── cusLogin.cshtml
│   │   ├── cusRegistration.cshtml
│   │   ├── Products.cshtml
│   │   ├── cusProducts.cshtml
│   │   ├── cusAddToCart.cshtml
│   │   └── ProductEntryForm.cshtml
│   └── Shared/               # Shared layouts
│       └── _Layout.cshtml
├── Content/                   # CSS files
├── Scripts/                   # JavaScript files
├── Style/                     # Custom stylesheets
└── Web.config                # Application configuration
```
## 🗄️ Database

The application uses SQL Server LocalDB with the following database:
- **Database Name:** byteZone
- **Location:** `App_Data/byteZone.mdf`

**Note:** The database file is not included in the repository. You'll need to create the necessary tables based on your application requirements.

## 📦 Dependencies

```xml
- ASP.NET MVC 5.2.9
- Bootstrap 5.2.3
- jQuery 3.7.0
- jQuery Validation 1.19.5
- Newtonsoft.Json 13.0.3
- Modernizr 2.8.3
- Microsoft.AspNet.Web.Optimization 1.1.3
```

## 🎨 UI/UX

- **Responsive Design:** Built with Bootstrap 5 for mobile-first responsiveness
- **Modern Interface:** Clean and intuitive user interface
- **Custom Styling:** Additional custom CSS for brand identity
- **Client-side Validation:** jQuery validation for better user experience

## 🔐 Security Considerations

- Implement proper authentication and authorization
- Use parameterized queries to prevent SQL injection
- Hash passwords before storing (not implemented yet - recommended)
- Implement HTTPS in production
- Add CSRF protection for forms
- Validate all user inputs

## 🚧 Future Enhancements

- [ ] Implement Entity Framework for data access
- [ ] Add order management system
- [ ] Implement payment gateway integration
- [ ] Add product reviews and ratings
- [ ] Implement email notifications
- [ ] Add advanced search and filtering
- [ ] Create RESTful API
- [ ] Implement admin analytics dashboard
- [ ] Add inventory management
- [ ] Multi-image upload for products

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

## 👤 Author

**betaTrident**
- GitHub: [@betaTrident](https://github.com/betaTrident)
- Repository: [ByteZone](https://github.com/betaTrident/ByteZone)

## 📧 Support

For support, please open an issue in the GitHub repository or contact the development team.

---

**Built with ❤️ by Kentsuuii using ASP.NET MVC**
