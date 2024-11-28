# **Group 9 Final Project**

## **Project Overview**
This project is a web-based platform designed to showcase products, allow users to view product details, manage orders, and complete checkout seamlessly. It includes an admin management system to facilitate product CRUD (Create, Read, Update, Delete) operations. The project is built using modern web development practices and technologies.

---

## **Project Features**
### **Public Pages**
1. **Home Page**  
   - Displays all products grouped by categories.
   - Products are showcased using a card layout.
   - Users can navigate to product details from this page.

2. **Product Detail Page**  
   - Displays detailed information about a single product.
   - Includes product image, name, price, description, and other relevant details.

3. **Order Page**  
   - Allows users to view their current orders.
   - Displays order status and history.

4. **Checkout Page**  
   - Enables users to complete their purchases.
   - Includes payment and shipping information.

### **Admin Management**
1. **Admin Dashboard**  
   - Accessible only by logged-in admin users.
   - Provides product management capabilities.

2. **Product CRUD**  
   - Admin can add, edit, view, and delete products.
   - Includes features for uploading product images.

---

## **Team Responsibilities**

### **Yunxiang**
- **Home Page**:
  - Displays all products grouped by categories.
  - Implements dynamic card layout with links to product detail pages.
- **Admin Management Page**:
  - Implements product CRUD functionality.
  - Develops admin login and authentication system.

### **Gurleen**
- **Product Detail Page**:
  - Develops detailed product view.
  - Integrates data from the backend to display product-specific information.

### **Ritk**
- **Order Page**:
  - Designs and implements user order management.
  - Displays order history and current order status.

### **David**
- **Checkout Page**:
  - Implements the checkout process.
  - Includes payment gateway integration and shipping information.

---

## **Technologies Used**
- **Backend**: ASP.NET Core 8
- **Frontend**: Razor Pages with Bootstrap 5
- **Database**: Microsoft SQL Server
- **Tools**: Visual Studio 2022, Entity Framework Core
- **Version Control**: Git and GitHub

---

## **Setup Instructions**

1. **Clone the repository**:
   ```bash
   git clone https://github.com/WebDev-24Fall/Group9_FinalProject.git
   ```

2. **Configure the database**:
   - Update the connection string in `appsettings.json` to point to your SQL Server instance.
   - Run migrations to create the database:
     ```bash
     Add-Migration InitialData
     Update-Database
     ```

3. **Run the application**:
   ```bash
   dotnet run
   ```

4. **Access the application**:
   - Public pages: `https://localhost:7032/`
   - Admin dashboard: `https://localhost:7032/admin/products` (requires admin login)

---

## **Future Enhancements**
- Add user authentication and authorization.
- Implement search and filter functionality.
- Enhance UI/UX with more dynamic elements.
- Add analytics for admin insights.

---

## **Contributors**
- **Yunxiang**: [Yunxiang Gu](https://github.com/guyunxiang)
- **Gurleen**: 
- **Ritk**: 
- **David**: 
