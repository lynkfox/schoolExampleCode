# CSCC-Capstone-2019-Flowers
Capstone Project, 2019 at Columbus State Community College, Take It Or Leaf It Flower Arrangements


# Session Class

Anthony Goh -  Developed this to be used as a static class behind the rest of our app. This class will hold all the current session information - the username, if the PW was correct (so as not to store the pw), the default store location, and if the user logged in is a Manager and can access manager level screens.

There are also various methods that handle the sending of sql queries to the database and retrieving data.

The goal was to keep as much of the SQL queries out of the App views as possible (following a MVC sort of framework, with the session object acting like the Controller). 

This is a work in progress at the moment. Current To Do List is to change the bool methods to throw exceptions instead of returning false.



It uses a connection file in resources that is set up as:

server-address
databasename
userid
password


this is currently a plain text file, and needs to be updated to be encrypted.


## Changes (from before upload to this git)

### Created Class

had a hard coded connection in the code, and only functions such as GetTable and SendQry

### Setup/Cleanup

Added a Setup and Cleanup Functions in order to keep things safe when using in the external program

### Added Password Hashing

adjusted the PW in the database to be a salted hash (24bit). Included in this was a ChangePW function, a CreateUser function, and updated the ChkPassword function to work with the hash.

Added a default-store property as well, for use in the app this goes with. Adjusted CreateUser to automatically set the store the same as the manager creating it.

### External Connection file

its currently a plaintext file stored in the resources of the dll. It needs to be encrypted i think... not sure. Its kinda iffy at the moment for security purposes, and I'm still looking into it :)