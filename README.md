# Example Code
Work from various classes at Columbus State Community College, uploaded as example code

## C Sharp Programming I

A First Level (but not introductory) course to C Sharp. Class expects an understanding of basic programing concepts (variables, loops, methods, and other generic programing concepts)

### Burger Picker

This Windows Form app was to show understanding of using Groups, Radio Buttons, and Checkboxes.

This app simulates a 'Self Ordering' console at a fast food resturant. Allows the user to pick from various condiments and extras, patties, and other options. Prints out a sample reciept.

### Code Breaker

This windows form app takes a phrase you want to encode, and a key phrase, and generates a very basic (not true) Play Fair array to generate an encypted line. It can also show the play fair array on command

### Flashcards

This is a simple flashcard app, randomizing 5 images and matching the answers. Uses a Fisher Yates shuffle.

### Mortgage Calculator

A basic calculator using windows forms. Second pass of this project added basic custom exceptions.



## C# II

This class will be going into much more in depth development with C#, including, but not limited to, ASP.net, MVCs, regex, linq, and many other aspects of full stack development along the axis of C#.

A note about the teaching style of this class:

This class was taught more in a 'learn together' manner. The teacher had projects and goals to make, but we worked through them as a group, but on our own versions. We had direction and help, and were encouraged to take them our own way.


### Readability

This was a simple MVC app to take in a section of text and figure out the Readability level of it. There are a few iterations in the project, changing simple details as we attempted to work into more detailed iterations of the assignment

### Free Weather

This mvc app was to take a JSON provided by an API, parse the information, get what we needed out of it, and display it. I did this app on my own, outside of class, though it was guided in general by the professor.

### DatabaseADOTest and EntityFramework

These were apps working with AWS RDS server hosting a msSQL server. Connecting to, building basic scaffalolded views, and performing very basic (auto generated) CRUD operations.

Note: the AWS server was on a classroom account, that has been since shut down. The app will not run successfully now.

###csci2630FinalProject

As noted above, the class was very laid back. This was our semester project. We didn't hae time to finish it. As of this upload, I haven't had time to go back and finish it either, though I have a thought to do so.

We were to take csv file, parse the information, and upload it to tables on the RDS using Entity Framework. Then create a single view CRUD page (rather than the automatically generated CRUD pages)

Again, this was mostly done on my own time outside of class, though I had plenty of guidance. We were only required to use the file provided, and be able to add at least 5 columns to the databases, plus the Professors (in a seperate table). 

My version was able to successfully insert the data, check to make sure it wasn't inserting the same information, provide it back in a much easier to read view (Accordion Cards, using Bootstrap). I submitted the data into 3 seperate tables, though considered more for 'Future proofing'. 

I was not able to successfully get the single view CRUD to work properly before this version of the app and the end of class. 

## Java Programing I

A First Level java programing course. Assumes only basic understanding of programing concepts, but pushes more into depth as time goes on.

It is important to note that most of the labs were provided with either some starting code, or strict parameters on what methods to use and how to call them, what they should return. Most of these are far more broken out than my typcial approach, but one must adhere to the parameters of the project, unless there is ample reason to do otherwise (and in class, there is little reason not to).

* Each lab has a corresponding Doc file, that has the parameters and restrictions of the lab, as well as example outputs


### Lab 1
 * A - Console: Making use of Scanner for input 
 * B - Console: Generic Splash Screen
 
### Lab 2
 * A - Console: Example shipping calculator
 * B - Console: Option based currency convertor


### Lab 3
 * A - Console: Basic payroll calculator
 * B - Console: Small 3 instancte character bubble sort.

### Lab 4
 * A - Console: Loops, keeps track of the number of positive, negative, and zero's entered.
 * B - Console: Find Highest (slight error here. An initial negative number can cause an issue)

### Lab 5

* A - Applet, energy calculator
* B - Applet, using Swing.

### Lab 6 
* A - Console: making use of Methods
* B - Applet, using swing: make use of methods. (This lab was given to us as a skeleton, we only had to do the methods)

### Lab 7
* A - Console: Avg an array with a Method, as well as overloading methods
* B - Console: Basic linear search using a method. 

### simpleRectangle
* In class exercise in Object / Class creation

### Lab 8
* Create a class called Car that takes a model year and a make, and can show the car accellerating and breaking.
* Console: Create the main class that uses methods to error check for model year and nonempty make string, then accelarates the car 5 times and breaks 5 time.

### Lab 9
* BankAcctDemo.java provided by instructor
* Console: Create the external class CheckAcct that has various Accessors, Mutators, and a static variable that needs its own accessor and mutator.

### Lab 10
* Exception Handling
* Swing Applet to handle commissions and sales using exception handling for error catching.

### Lab 11
* Additional Exception Handling, in Console
* Note, Main() Method and Method names provided by professor (I would have prefered for output not to require any paramaters, and call the functions inside it)

### Lab 12 - Final
* TestMultiply.java - this was a debug assignment. Had to debug and add exception handling. The original code is found in Initial Code to Debug.txt
* FinalATG - A console program that uses the methods getNum1(), getNum2(), that both return non 0 integers, with exception handling, and an Output() method that displays arithmetic functions as done by java.



# Capstone

## Not the full Project

Files within this directory are various aspects of the Capstone Project. The full project will be located in a different git when it is finished/in progress. These files are personal test cases I developed for working through various problems of the project before implimenting them in the full project git.


## Full Project git

see https://github.com/sbinmakhashen/CSCC-Flowers-Poject for the full git of the capstone project.

### TestSQL Connection

Working through connecting to a SQL database, getting information from it, and inserting new information. It is very dirty and ugly, but it uses the MySQLClient Library, in order to connect, draw data into a dataTable for display, and insert additional data.

This was effectively a proof of concept for our project as well - if we couldn't get this done, we couldn't do any of the rest.

**Note** This app currently uses a connection to a free sql hosting site that deletes databases after 30 days of non use. As it was for a proof of concept, I didn't need the database to stay around long. As such it probably won't work without updating the connection string information, and rebuilding the table on a new database.