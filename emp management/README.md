# Employee Record Management System

A complete ASP.NET Core MVC application for managing employee records with SQLite database and cookie-based authentication.

## Features

? Employee CRUD operations (Create, Read, Update, Delete)  
? SQLite database with Entity Framework Core  
? Simple cookie-based authentication (no ASP.NET Identity)  
? Bootstrap 5 responsive UI  
? Sample data auto-seeding  
? Client-side and server-side validation  
? Success/Error notifications  

## Default Admin Credentials

- **Username:** `admin`
- **Password:** `Admin@123`

## Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code (optional)

## ?? Getting Started - Step by Step

### Step 1: Open Terminal/Command Prompt
Navigate to the project folder:
```bash
cd "emp management"
```

### Step 2: Add Migration
Run this command to create the database migration:
```bash
dotnet ef migrations add InitialCreate
```

**Expected Output:** You should see files created in a `Migrations` folder.

### Step 3: Create Database
Run this command to create the SQLite database:
```bash
dotnet ef database update
```

**Expected Output:** 
- A file named `employee.db` will be created in the project folder
- Sample data will be seeded automatically
- You'll see messages about applying migrations

### Step 4: Run the Application
Start the application:
```bash
dotnet run
```

**Expected Output:**
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

### Step 5: Open Your Browser
Navigate to: `https://localhost:5001` or `http://localhost:5000`

---

## ?? How to Use the Application

### **View Employees (No Login Required)**
1. Go to homepage - you'll see the employee list
2. Click "Details" to view full employee information

### **Create/Edit/Delete Employees (Login Required)**
1. Click "Login" in the navigation bar
2. Enter credentials: `admin` / `Admin@123`
3. After login, you'll see additional buttons:
   - **Create New Employee** - Add a new employee
   - **Edit** - Modify existing employee details
   - **Delete** - Remove an employee (with confirmation)

---

## ??? Project Structure

```
emp management/
??? Controllers/
?   ??? AccountController.cs       # Login/Logout
?   ??? EmployeesController.cs     # Employee CRUD operations
??? Data/
?   ??? ApplicationDbContext.cs    # EF Core DbContext
?   ??? SeedData.cs               # Sample data seeding
??? Models/
?   ??? Employee.cs               # Employee entity
?   ??? Salary.cs                 # Salary entity
?   ??? Attendance.cs             # Attendance entity
??? Views/
?   ??? Account/
?   ?   ??? Login.cshtml          # Login page
?   ??? Employees/
?   ?   ??? Index.cshtml          # Employee list
?   ?   ??? Create.cshtml         # Create employee form
?   ?   ??? Edit.cshtml           # Edit employee form
?   ?   ??? Details.cshtml        # Employee details
?   ?   ??? Delete.cshtml         # Delete confirmation
?   ??? Shared/
?       ??? _Layout.cshtml        # Main layout
?       ??? _ValidationScriptsPartial.cshtml
??? employee.db                   # SQLite database (created after migration)
??? Program.cs                    # Application entry point
```

---

## ?? Troubleshooting

### **Issue: "No such table: Employees"**
**Solution:** Run the database update command:
```bash
dotnet ef database update
```

### **Issue: "A database operation failed"**
**Solution:** Delete `employee.db` and recreate:
```bash
# Delete the database file
rm employee.db  # (Mac/Linux) or del employee.db (Windows)

# Recreate database
dotnet ef database update
```

### **Issue: "Create/Edit/Delete buttons not visible"**
**Solution:** Make sure you're logged in with admin credentials.

### **Issue: Changes not saving**
**Solution:** 
1. Check browser console for JavaScript errors
2. Ensure validation is passing (fill all required fields)
3. Check that the database file `employee.db` exists

---

## ?? Sample Data

After running `dotnet ef database update`, two sample employees are automatically created:

1. **John Doe** - IT Department, Software Engineer
2. **Jane Smith** - HR Department, HR Manager

---

## ?? Security Notes

?? **For Learning Purposes Only**
- Uses hard-coded credentials (not for production)
- Simple cookie authentication (no token refresh)
- No password hashing for admin account

For production applications, use:
- ASP.NET Core Identity
- JWT tokens or OAuth
- Secure password hashing
- Role-based authorization

---

## ?? Testing the Application

### Test Create:
1. Login as admin
2. Click "Create New Employee"
3. Fill all required fields
4. Click "Create"
5. ? Should redirect to employee list with success message

### Test Edit:
1. Login as admin
2. Click "Edit" on any employee
3. Modify some fields
4. Click "Save Changes"
5. ? Should redirect to employee list with success message

### Test Delete:
1. Login as admin
2. Click "Delete" on any employee
3. Confirm deletion
4. ? Should redirect to employee list with success message

---

## ??? Development Commands

```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Run with hot reload
dotnet watch run

# Create new migration
dotnet ef migrations add MigrationName

# Remove last migration
dotnet ef migrations remove

# Update database to specific migration
dotnet ef database update MigrationName
```

---

## ?? Common Validation Rules

- **First Name:** Required
- **Last Name:** Required
- **Email:** Required, must be valid email format
- **Phone:** Required, must be valid phone format
- **Address:** Required
- **Department:** Required
- **Designation:** Required
- **Date of Joining:** Required, must be a valid date

---

## Need Help?

If you encounter any issues:
1. Ensure .NET 8.0 SDK is installed: `dotnet --version`
2. Check that all NuGet packages are restored: `dotnet restore`
3. Verify the database exists: Look for `employee.db` file
4. Check the browser console for JavaScript errors (F12)
5. Review application logs in the terminal

Happy Coding! ??
