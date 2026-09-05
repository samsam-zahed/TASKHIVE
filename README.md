# TASKHIVE

**TASKHIVE** is a Windows desktop application built with **C# and .NET 10 / WPF**, designed to bring task management, customer and company management, communication tools, reporting, device connectivity, and everyday business utilities into a single application.

The project focuses on providing an integrated desktop workspace for managing tasks and business-related information while keeping commonly used tools accessible from one interface.

## ✨ Features

* 📋 **Task Management**

  * Create and manage tasks
  * Track ongoing and completed tasks
  * View task-related information and notifications

* 👥 **Customer & Applicant Management**

  * Manage customer/applicant information
  * Store contact and company-related details
  * Access related communication options

* 🏢 **Company Management**

  * Manage company information
  * Store address, phone, email, website, services and notes

* 👤 **User & Security Management**

  * User management
  * Login/security mode
  * Application-level access controls

* 🔔 **Reminders & Notifications**

  * Task reminders
  * Application notifications
  * Status monitoring

* 🌐 **Integrated Web Applications**

  * Email
  * WhatsApp
  * Telegram
  * Website
  * Google Maps
  * Microsoft Office / Office 365 web services

* 📱 **Android Device Connectivity**

  * Device connection and interaction through **ADB**
  * Uses SharpAdbClient for Android device communication

* 📄 **PDF & Excel**

  * PDF generation and reporting
  * Excel-related functionality using ClosedXML

* 💾 **Data Management**

  * SQLite database support
  * Backup and restore functionality

* 🌍 **Multi-language Support**

  * English
  * Finnish (Finland)

* 🌤️ **Additional Utilities**

  * Internet connection monitoring
  * Weather information
  * Navigation
  * File sharing / transfer utilities

## 🛠️ Technologies

| Technology             | Usage                        |
| ---------------------- | ---------------------------- |
| **C#**                 | Main programming language    |
| **.NET 10**            | Application framework        |
| **WPF**                | Windows desktop UI           |
| **MahApps.Metro**      | Modern Windows UI components |
| **SQLite**             | Local data storage           |
| **ClosedXML**          | Excel processing             |
| **QuestPDF**           | PDF generation               |
| **Microsoft WebView2** | Embedded web applications    |
| **SharpAdbClient**     | Android / ADB communication  |
| **Newtonsoft.Json**    | JSON serialization           |
| **SharpVectors**       | SVG support                  |

## 🖥️ Application Architecture

TASKHIVE is implemented as a Windows desktop application using **WPF and XAML**, with application logic written in C#.

The project is organized into several areas including:

* `Classes` – application classes and shared functionality
* `Data` – application data and local storage
* `Reports` – reporting and document generation
* `HtmlEditor` – web/HTML-related functionality
* `Strings` – localization resources
* WPF/XAML views and application components

## 🌍 Localization

TASKHIVE includes localization support and currently provides:

* 🇺🇸 English
* 🇫🇮 Finnish

The application can switch language from its user interface and stores the selected language for subsequent application sessions.

## 🚀 Getting Started

### Requirements

* Windows
* Visual Studio
* .NET 10 SDK
* WebView2 Runtime
* Android ADB support (required only for device-related features)

### Run the Project

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Build the project.
5. Run the application.

```bash
dotnet restore
dotnet build
dotnet run
```

## 📦 Main NuGet Packages

TASKHIVE uses several open-source libraries, including:

* MahApps.Metro
* Microsoft.Data.Sqlite
* Microsoft.Web.WebView2
* ClosedXML
* QuestPDF
* SharpAdbClient
* Newtonsoft.Json
* SharpVectors.Wpf

## 🔐 Data & Privacy

TASKHIVE is designed as a desktop application and can work with locally stored application data.

When using integrated web services such as Email, WhatsApp, Telegram, Google Maps, or Office 365, the privacy and authentication policies of those services also apply.

## 🚧 Project Status

TASKHIVE is an actively developed personal software project.

New features, improvements, UI refinements, and application capabilities may be added over time.

## 🎯 Project Goals

The main goal of TASKHIVE is to create an **all-in-one Windows desktop workspace** that combines:

**Task Management + Customer Management + Company Management + Communication + Reporting + Device Connectivity**

into a single application.

---

### Author

Developed with **C# / .NET / WPF**.

⭐ If you find the project interesting, feel free to explore the source code and follow its development.
