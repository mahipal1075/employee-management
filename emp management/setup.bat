@echo off
echo ========================================
echo Employee Management System Setup
echo ========================================
echo.

echo Step 1: Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 goto error
echo.

echo Step 2: Building project...
dotnet build
if %errorlevel% neq 0 goto error
echo.

echo Step 3: Creating database migration...
dotnet ef migrations add InitialCreate
echo.

echo Step 4: Creating database...
dotnet ef database update
if %errorlevel% neq 0 goto error
echo.

echo ========================================
echo Setup Complete!
echo ========================================
echo.
echo To run the application, execute:
echo   dotnet run
echo.
echo Then open your browser to:
echo   https://localhost:5001
echo.
echo Login credentials:
echo   Username: admin
echo   Password: Admin@123
echo.
pause
goto end

:error
echo.
echo ========================================
echo ERROR: Setup failed!
echo ========================================
echo Please check the error messages above.
pause

:end
