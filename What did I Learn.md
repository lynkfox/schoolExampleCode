# Purpose of this File

This file is for leaving myself notes about what some of the projects in here taught me. What problem I was trying to work out, some of the thoughts I went through when trying to fix whatever problem it was, and some of the lessons I learned in the process.


## Flashcards 

### * Class: C#1 
### * Problem: Fisher Yates Shuffle

I tried for many hours to figure out how to shuffle the flash cards while keeping the answers and the images connected. I thought about using a matrix, but the assignment actuall pushed us not to use one. I could shuffle each array (of pictures, and of answers) seperately with ease. I had a brute force method for doing them together, but it was clunky, ugly, and generally bad code. It worked, but I was not happy with it.

Another student in the class posted asking for some help in a different part of the app. His code used a Fisher Yates shuffle. I had never heard of it before. I looked it up, went through the concept for a while. It made perfect sense.  I built a console app to watch how it worked, debugging out each individual step and the current array (and the source array) to see each step in action so I understood the concept better, and to make sure my code was correct.

This opened my eyes to the fact there are so many solutions already out there. I knew of course that I could google problems and find answers, or at least tidbits to lead me to my own answers. This was different. This was a gutpunch to remind me that most of the hard mathimatical problems of programing have already been done by mathematicians. This was a reminder that I didn't have to brute force my way through problems like shuffling, or sorting data, or discovering paths. This had already been done by others in a mathematical sense, and it could easily be done in code using the formulas and methods they derived. 

## TestSQL Connection 

### * Class: Capstone
### * Problem: Sql Connection through c#, as a proof of concept
### * Problem 2: import.data.SqlClient library just did not want to work.

This was a bit of a dosey. I spent 4 hours trying to get import.data.SqlClient library to connect to a server. I could connect through a powershell. I could connect manually. I tried an established SQL server I had already on a website. I tried a free sql host (which the current version is connected to atm). I tried setting up my own mySQL server on another computer in my LAN and connecting. With all of them I could use third party programs to connect to the server, but the connection string just *would not* connect.

Either it gave a connection time out error, server does not exist error, or a 'Fatal Internal Error'.  I tried 3 different servers, i tried doing it from my desktop to the sql server on my desktop (using localhost). I tried all of the above in a console and a windows form app.

Four hours I tried. Eventually I stumbled upon a stack overflow post saying that, for mySQL their connection string was wrong. it then used a MySqlConnectionStringBuilder to build the connection string. It took me a bit to figure out which library that was, but when I did, I switched immediately and it worked on the first try.

As of this writing, and the test app creation, I do not know why the original connection string was not working. Maybe I did not have something setup properly in VisualStudio 2019. (Event Handler for databases not setup?) Maybe it had something to do with just how the program was being compiled. 

What I did learn through this experiment app was that libraries can be fickle. I gained a bit of an understanding of what libraries really are that I hadn't had before. Conceptually I understood the library idea. I even made use of them, often of course. There are may 'using' lines in my code.  But I had not truely understood what using a library, getting it through NuGet was about until this point.  I probably still have a lot to understand, but the method, the use, and the general understanding of the entire idea of libraries is much more solidified in my mind now.


