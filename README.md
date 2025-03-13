# Booking System Project

## Overview

This project consists of two startup applications:

- **Booking.Api** – The API project that depends on the Application, Data Access, and Domain layers.
- **Booking.Mbc** – The MVC project that acts as a stand-alone application and communicates with the API via AJAX calls.

Due to time constraints, the project includes only essential features. While many additional features were planned, the
focus is on providing a simple working implementation.

## Features

### API (Booking.Api)

- **Customers:**
    - A simple form to add a customer.
    - A view to display all customers.
- **Hotels:**
    - A simple form to add a hotel.
    - A view to display all hotels.
- **Bookings:**
    - A booking page that lists all bookings with pagination.
    - The booking add form allows the user to select:
        - A customer.
        - A hotel.
        - Check-in and check-out dates.
        - Number of rooms.
    - Collision checking: Prevents booking the same room more than once during the same period.
    - A discount of 5% is applied if the customer has previously booked a room in the selected hotel.

### MVC (Booking.Mbc)

- Stand-alone front-end application that communicates with the API via AJAX calls.
- Uses simple, responsive views:
    - A view to display all customers.
    - A view to display all hotels.
    - A booking page with pagination for bookings (pagination for hotels and customers is omitted due to the limited
      scope).

## Project Structure

- **Booking.Api**  
  Depends on:
    - Application
    - Data Access
    - Domain
- **Booking.Mbc**  
  A separate application that communicates with the API via AJAX.

## Setup and Running the Project

### Database Migration

Before starting the project, apply the migrations to update the database. Open your terminal and run:

```bash
dotnet ef database update -s Booking.Api -p Booking.DataAccess
```
- you can modify the connection string at Booking.Api/appsettings.json file

### Running The Api

```bash
dotnet run Booking.Api
```
### Running The Mvc

```bash
dotnet run Booking.Mvc
```
- you can change the api base url at Booking.Mvc/appsettings.json
