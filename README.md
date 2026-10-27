How to login  
![Login](docs/images/screenshot_login.png)
```
1. Go to Login.
2. Type your username and password.
3. Click "Login".
```

How to add a car  
![Cars](docs/images/cars_screenshot.png)  
![AddCars](docs/images/create_car_screenshot.png)  
![CarAdded](docs/images/car_created_screenshot.png)  

```
1. Make sure you are logged in.
2. Go to Cars or My cars.
3. Click "Add car".
4. Type your information about the car.
5. Click "Add".
```

![MyCars](docs/images/my_cars_screenshot.png)
![EditCar](docs/images/edit_car_screenshot.png)  
![CarEdited](docs/images/car_edited_screenshot.png)

How to edit a car
1. Make sure you are logged in.
2. Go to My cars.
3. Click "Edit" on the car you want to edit.
4. Edit some of the fields.
5. Click "Save changes".
```

![DeleteCar](docs/images/delete_car_screenshot.png)
![CarDeleteConfirmation](docs/images/car_delete_confirmation.png)

How to delete a car
```
1. Make sure you are logged in.
2. Go to My cars.
3. Click "Delete" on the car you want to delete.
4. On the confirmation page click on "Yes"
```


## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach.

Connection string is configured in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=Cars4U;Trusted_Connection=True;Encrypt=false"
}
```

To create and seed the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## ⚙️ Configuration

Key settings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Cars4U;Trusted_Connection=True;Encrypt=false"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```


## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add some feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 📬 Contact

**Your Name** – [ackodavidoff-png](https://github.com/ackodavidoff-png/)

Project Link: [https://github.com/ackodavidoff-png/Cars4U](https://github.com/ackodavidoff-png/Cars4U)

---

*Built as part of the **ASP.NET Fundamentals** course.*
