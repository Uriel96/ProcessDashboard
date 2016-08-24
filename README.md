# Process Dashboard

Process Dashboard is a software program developed using C# 6.0 and .NET Framework 4.6.1. 
The Database side was developed using SQLite and Entity Frameowrk Core 7.0.0-beta8. Also, to extract Zip Files it was neccesary to install DoNetZip Library. Link: https://dotnetzip.codeplex.com/

The purpose of this project was to speed up the process of reading each student's projects and have a place where important data was shown and stored. 
Therefore, the main objective of this project was to, through a software program, ease up the data of each student's projects in a way which data could be easily seen and handled for further analysis.

All the Students Projects are in a Directory.

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17915806/47f8e074-6973-11e6-9da2-4ea5c25c603c.png)

And each student's project consists of a Zip File which contains all the data for the corresponding project.

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17915814/58655028-6973-11e6-8f22-92af9effee66.png)

## 'Process Dashboard'
This Zip File is generated by a software program called 'Process Dashboard' which collects multiple metrics done by the students through the project. This metrics are used by the Personal Software Process (PSP) or the Team Software Process (TSP) metodology.

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17915823/634bab36-6973-11e6-9793-5539ba93d903.png)
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17915819/5e8c5604-6973-11e6-8958-1c01ef693404.png)

A project is separated into programs, where the student must work within one program before given up. Then the other program is done and given up, this means that programs are acumulating through the project.

Each project contains Time Log metrics, Defect Log metrics, Proxy Based Estimated Methods (PROBE), Size Estimated Template (SET) parts, Phases metrics, and Summaries of each program. All these metrics are contained within the Zip File.

## Program Specifications
This software is responsible of generating an Excel File where all important data is contained. Also it is capable of storing this information in a SqLite Database (local database), this way all data for different students throughout the years are stored, and can be analysed.
This is how the program looks like:

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17934268/dfc6dff0-69dc-11e6-9b05-2a0c5d7f38d7.png)

**Dashboard Directory:** The main directory where all the folders with each students information is.

**Excel File:** The Excel Template File that contains the format in which information will be presented.

**Prefix:** The first Words to identify the Zip File. e.g. if provided 0dsh as prefix, then it will search for a Zip File starting with 0dsh

**Generate Excels:** Whether or not Excels for each student will be generated.

**Save in Database:** Whether or not data will be stored in the local database.

**Delete Old Excels:** Whether or not Excels contained in the student's folder will be deleted.

**Generate Only One:** Whether or not information will be generated for only a particular student.

**Acumulated Excel:** Whether or not another Excel will be generated with all the information of each student.

**Generate Automatically:** If the last program of each student information is not known.

**Generate Until:** If the last program of each student is known. This Program must be chosen in the Combobox.

**Progress Bar:** Shows how much progress is done on generating all the information.

**Excecute:** Excecutes the Process Generation.

**Cancel:** Stops the process of generation.

**Show Database:** Shows a new Window with a query to find the information needed in the database.

Data Window lets you query basic information from the Database, such as student, table, until program and show previous versions:

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17935913/b1647a0e-69e2-11e6-8bc2-b133c66a2aae.png)

**Student:** The Name of the Student. Can be chosen from the Combobox or written.

**Table:** The type of Data to show (Defect Logs, Time Logs, Phases, Summaries, PROBE and SET)

**Until Program:** The Programs that want to be shown. e.g. if Program 3 is chosen means Program 1, 2 and 3 will be shown.

**Show previous versions:** Whether previous versions of the student will be shown (previous generated data for the student).

## Excel Files
Generated Excel Files are the must important part of this software, because it will allow data to be analysed. This analysis will not be shown because of confidentiality purposes, but the generated data will. 

When the Process is finished Excels of each Student will be generated in their given directory.

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17936697/4294ae48-69e5-11e6-8bc7-2ca496418a1d.png)

And the Information of the Student's project will look something like this:
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937094/8a495ecc-69e6-11e6-8bd6-4b89834dc4b2.png)
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937091/8a423354-69e6-11e6-9f01-a758189b10f8.png)
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937093/8a48a4c8-69e6-11e6-9b31-0073c266034b.png)
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937092/8a448230-69e6-11e6-8376-75b8cb1f2ca1.png)
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937095/8a4ed42e-69e6-11e6-904d-9bf5250cacc1.png)
![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937096/8a50d49a-69e6-11e6-8bde-f69609fb0755.png)

Lastly an Acumulated Excel can be also generated, it stores all the Information of every student.

![alt tag](https://cloud.githubusercontent.com/assets/6753760/17937439/b299ca1e-69e7-11e6-9767-3196d903d42b.png)

