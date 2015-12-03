ECManager
===========

pl
----
ECManager jest częścią aplikacji do zarządzania sklepem internetowym.
Zawiera moduł obsługi dostaw oraz zarządzania produktami.

eng
----
ECManager is a part of e-commerce managament application. 
It contains delivery and product management modules.

How to re-create database?
---
Open Nuget Package Manager Console and enter as follows:
* Enable-Migrations -Project DataAccess
* Add-Migration init -Project DataAccess
* Update-Database -Project DataAccess

If you get an error with message: "Enable-Migrations : The term 'Enable-Migrations' is not recognized as the name of a cmdlet", just close Visual Studio and open project again.

You can also use a sql script with sample data. It's located in insert_data_sql folder. Run it as a 'New query' on created local database.

Technologies:
* .net c#
* wpf
* mvvm
* entity framework
* caliburn.micro https://github.com/Caliburn-Micro/Caliburn.Micro
* MahApps.Metro https://github.com/MahApps/MahApps.Metro

Video: https://www.youtube.com/watch?v=2Qyp49i-2Hk
