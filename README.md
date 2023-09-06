# ZC-GPA-Calculator

## Table of contents
- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [License](#license)
## Introduction
ZC-GPA-Calculator is a desktop-based application written in .NET (C# WinForms) designed to provide users with a comprehensive tool for analyzing pre-downloaded academic transcripts in the form of HTML files. This application utilizes web scraping techniques to extract academic data from the HTML transcript file and presents it in a user-friendly interface. Users can easily manipulate the academic data to simulate the effect on their semester GPA and cumulative GPA (cGPA) by introducing changes to course grades.

## Features
- **Web Scraping**: Automatically extracts your academic data from HTML transcript files.
- **User-Friendly Interface**: Provides an intuitive and easy-to-use interface for viewing and editing academic data.
- **Grade Manipulation**: Allows users to modify course grades or add new courses to simulate the impact on semester GPA and cGPA.
- **Detailed Analysis**: Provides detailed breakdowns of GPA calculations, including semester and cumulative GPA.

## Getting Started
### Prerequisites
Before using ZC GPA Calculator, ensure that you have the following prerequisites installed on your system:
- **Operating System:** Windows 7 or later.
- **.NET Framework:** <a href="https://dotnet.microsoft.com/en-us/download/dotnet/7.0" target="_blank">.NET 7.0</a>.

Or simply accept the automatic download when launching the software, in case .NET 7.0 is not already installed on your machine.

> _**Please note that:** the application is specifically designed for Windows operating systems starting from Windows 7 and requires the specified version of the .NET Framework to run successfully._

---

### Installation

#### Option 1: Build and Run from Source Code

1. Clone this GitHub repository to your local machine:

   ```bash
   git clone https://github.com/MoEssamKhattab/ZC-GPA-Calculator.git
   ```

2. Navigate to the project directory:
    ```bash
    cd ZC-GPA-Calculator
    ```

3. Build and run the application using Visual Studio.

> _**Note that:** this option reuires both Visual Studio IDE and .NET desktop development package to be installed on your machine._

#### Option 2: Use Executable from Release

1. Go to the Releases section of this GitHub repository.

2. Download the latest release executable file.

3. Run the downloaded executable file to launch <img src="./ZC-GPA Calculator/Logos/zewail logo.png" width="15" height="20" alt="ZC GPA Calculator Logo"> **ZC-GPA Calculator.exe**

> _By using Option 2, you can quickly get started with the application without the need to build it from the source code._

## Usage

1. Download Your Transcript as an HTML File:

    - Open your transcript on the <a href="https://sisselfservice.zewailcity.edu.eg/PowerCampusSelfService/Home/LogIn?ReturnUrl=%2FPowerCampusSelfService%2FGrades%2FUnofficialTranscrip" target="_blank">SelfService</a> platform.


    - Right-click on the page &rarr; save as.

    -  In the "**Save As**" dialog box, choose the third option, which is "**_Webpage, Complete_**" from the "**Save as type**" dropdown menu.

2. Launch ZC GPA Calculator excutable file <img src="./ZC-GPA Calculator/Logos/zewail logo.png" width="15" height="20" alt="ZC GPA Calculator Logo"> **ZC-GPA Calculator.exe**

3. Open your HTML transcript file by clicking on the **Open Transcript** button.

4. The application will automatically scrape and display the academic data.

5. Use the interface:

    * to make changes to course grades as needed using the corresponding dropdown list.

    * to add new courses:

        - Click on the **Add New Semester** button.

        - Then click on **Add New Course** button.
    
    * to delete a newly-added course:

        - Right click on the course row on the table &rarr; delete course

6. The application will update semester GPA and cGPA in real-time.

## License

This project is licensed under the [MIT License](LICENSE) - see the [LICENSE](LICENSE) file for details.