# Nexora Technologies — ASP.NET Core + SQL Server Starter

This is a starter implementation for the Nexora Technologies public company website, role-protected Admin Panel, and Client Portal.

## Included in this starter
- Public Home, About, Services, Service Details, Contact, Privacy pages
- Contact/project enquiry form with server-side validation and SQL Server persistence
- ASP.NET Core Identity registration/login/logout
- `Admin` and `Client` roles
- Admin dashboard, enquiry listing and enquiry status update
- Client portal project listing (projects are linked to a client by `ClientUserId`)
- Seeded starter service catalog
- Responsive custom CSS and mobile navigation
- Starter schema creation on first run (`Database.EnsureCreatedAsync`)

## Recommended prerequisites
1. Install the .NET 10 SDK.
2. Install SQL Server or SQL Server Express and SQL Server Management Studio (SSMS).
3. Install Visual Studio 2026 with the ASP.NET and web development workload, or use VS Code with the C# Dev Kit.
4. Ensure the SQL Server instance is running and Windows authentication is permitted if using a trusted connection.

## Step 1 — Set the SQL Server connection
Open `appsettings.json`. Replace `YOUR_SQL_SERVER_NAME` with your actual SQL Server instance name.

Examples:
- Default SQL Server instance: `Server=localhost;Database=NexoraTechnologiesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`
- SQL Express: `Server=.\SQLEXPRESS;Database=NexoraTechnologiesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`
- LocalDB: `Server=(localdb)\MSSQLLocalDB;Database=NexoraTechnologiesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`

Do not use a connection string from another computer without confirming the SQL Server instance name.

## Step 2 — Configure a development Admin account
Open a terminal in this project folder and run:

```bash
dotnet user-secrets init
dotnet user-secrets set "AdminSeed:Email" "admin@yourdomain.com"
dotnet user-secrets set "AdminSeed:Password" "Choose-A-Strong-Password-123"
```

Use a unique strong password. These secrets are stored outside source control for local development. For production, use the hosting provider's secret configuration. Do not commit real passwords.

The application creates the Admin role and seeds the Admin user only when both settings are present. New registrations receive the Client role.

## Step 3 — Restore and run
```bash
dotnet restore
dotnet run
```

The first run creates the database schema if it does not exist and seeds the service catalog. Open the HTTPS URL shown in the terminal.

This starter uses `EnsureCreatedAsync` for an easier first run. It does not create migration history. Before production or future schema changes, switch to migrations deliberately: remove the `EnsureCreatedAsync` call, install the EF tool (`dotnet tool install --global dotnet-ef`), create a migration (`dotnet ef migrations add InitialCreate`), and apply it (`dotnet ef database update`). Do not use `EnsureCreated` and migrations interchangeably on the same database.

## Step 4 — Smoke test
1. Open Home and check Services.
2. Submit the Contact form with valid data.
3. Sign in to `/Account/Login` using the configured Admin credentials.
4. Open Admin Panel → Manage enquiries; change an enquiry status.
5. Register a separate test user. That user should receive the Client role and open Client Portal.
6. The Client Portal is empty until an Admin-side project creation workflow is implemented and a project is assigned to that user's Identity ID.

## Important before production
- The email notification service currently logs a message; it does NOT send real email. Connect a transactional email provider and verify sender/domain.
- Add rate limiting and a honeypot or CAPTCHA/Turnstile to the public form before exposing it widely.
- Review and replace the starter Privacy page with a complete, business-approved notice.
- Configure production secrets, HTTPS, backups, monitoring, logging retention, and deployment rollback.
- Add admin CRUD screens for Services and Client Projects, project updates/files, and audit logging before claiming full operational CRM functionality.
- Do not publish fake testimonials, client logos, certifications, or project outcomes.
- Run tests and security review before production.
