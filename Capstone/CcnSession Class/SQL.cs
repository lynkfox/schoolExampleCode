/* This class is designed to hold all the information needed to access a mySQL database in the background for a 
 * session. This includes the permission level of the user (currently Employee or Manager based on the table)
 * it also holds the connection data, so that it is always the same.
 * 
 * each function that can retrieve or send data to the mySQL database deals with the connection internally
 * it opens and closes it to make sure there are no extra open connections floating around
 * 
 * a note on exceptions. 
 * 
 * Due to the limited time in the summer semester for putting together a project of this magnitude, I have
 * elected to go with a cheap and dirty exception method: just continue to throw the ex upward.
 * 
 * in the main program I handle catches with message boxes to display what went wrong. No, it is not ideal, 
 * and no it is not necessarily the best practice, but it works and gets the job done, plus
 * when I break something in the RDS I can see the message easier than having to make a console testbed for it.
 * 
 * Designed by: Anthony Goh (lynkfox on GitHub)
 * For: Columbus State Community College, CSCI-2999 capstone, su2019.
 */

using CcnSession.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CcnSession
{
    public class SQL
    {
        //public properties - used to check permissions and defaults

        // Consider changing IsManager to an int? For more than just 2 levels of permission? Future proofing.
        static public bool IsManager { get; set; }
        static public bool PwCorrect { get; set; }
        static public string DefaultStore { get; set; }
        public static string Username { get; set; }
        public static bool CurEmp { get; set; }
        public static int FailPWAttempts { get; set; }
        public static int LoggedInEmpNum { get; set; }

        //Private properties, variables

        // these remain private, as they are not really needed outside of the class

        private static string address, database, userid, password;

        private static MySqlConnectionStringBuilder cnnStr = new MySqlConnectionStringBuilder
        {

        };
        /* Public Functions */

        /* Housekeeping Functions
         * 
         * these functions are setup and cleanup of the static object
         *
         * SQL.Setup(username, password) - No Overloads
         * 
         * Initializes session.
         * 
         * takes parameters of the username and the password
         * 
         * sets the Username inside the static object, to be used within various other functions used during
         * the session
         * 
         * sets the permission level (IsManager true/false)
         * 
         * checks to make sure the password is correct, sets a bool for later (so as not to store pw)
         * 
         * stores the DefaultStore string for use in the main program.
         * 
         * 
         * 
         * this should be called immediately after login, and ONLY after login. Never again.
         */

        static public void Setup(string user, string password)
        {
            try
            {
                SetupConnection();

                Console.WriteLine("CallingCheckUsername");
                if (CheckUsername(user))
                {
                    Username = user;
                }
                else
                {
                    throw new Exception("Those records do not match our database.");
                }

                Console.WriteLine("Calling ChkPassword");
                if (ChkPassword(password))
                {
                    PwCorrect = true;
                    Console.WriteLine("Calling Permission");
                    Permission();
                    Console.WriteLine("Calling GetStore");
                    DefaultStore = GetStore();
                    Console.WriteLine("Calling GetEmpNum");
                    LoggedInEmpNum = GetEmpNum(Username);
                    Console.WriteLine("PWCorrect: " + PwCorrect + " | isManager: " + IsManager + " | DefaultStore: " + DefaultStore + "| empNum: " + LoggedInEmpNum + " | username: " + Username);

                }
                else
                {
                    IsManager = false;
                    CurEmp = false;
                    PwCorrect = false;
                    LoggedInEmpNum = 0;
                    throw new Exception("Those records do not match our database. \nThis was attempt #" + (FailPWAttempts + 1) + " of 3.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SQL.Cleanup() - No overloads.
         * 
         * Safety function. Cleans up the static class for logout (but not program exit). 
         * 
         * Needs to be called at every position that can log out. Setup **should** overwrite for a new user, but
         * its better to be safe and call this.
         * 
         */
        static public void Cleanup()
        {
            Username = null;
            PwCorrect = false;
            IsManager = false;
            DefaultStore = null;
            CurEmp = false;
            LoggedInEmpNum = 0;
        }

        /* User (Employee Database) functions
         * 
         * these functions are for creating, and updating new user entries in the employee database
         */

        /* SQL.ChkPassword(password) - 1 overload (string, vs Hash+salt)
         * 
         * Checks the password against the database, and returns true if valid, false if not.
         * 
         * to do - add exception handling.
         * 
         * Passwords are stored with a 24bit Hash and a 24bit salt. We will pull the salt and store
         * it in a string, then generate the hash from the inputed pw and the salt, comparing that to the hash
         * (without ever storing it in a variable of its own other than the reader).
         */
        public static bool ChkPassword(string pw)
        {
            try
            {
                if (PwAttempts(GetEmpNum(Username)) >= 3)
                {
                    throw new Exception("Exceeded Login Attempts. Please wait at least 15 mins before trying again.");
                }
                byte[] salt, hash;
                var saltData = new DataTable();
                var hashData = new DataTable();
                Console.WriteLine("Getting Salt.");
                saltData = GetColumn("employee", "salt", "username", Username);

                Console.WriteLine("Getting Hash.");
                hashData = GetColumn("employee", "password", "username", Username);

                salt = Convert.FromBase64String(saltData.Rows[0]["salt"].ToString());
                hash = Convert.FromBase64String(hashData.Rows[0]["password"].ToString());
                if (PasswordHash.VerifyPassword(pw, salt, hash))
                {
                    return true;
                }
                else
                {
                    PWAttemptFail(GetEmpNum(Username));
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* ChkPasswordNoFail(password) - no overloads
         * 
         * Same as above method, but does not deal with the Failed pw - for use in Changing Passwords
         */

        public static bool ChkPasswordNoFail(string pw)
        {
            try
            {
                byte[] salt, hash;
                var saltData = new DataTable();
                var hashData = new DataTable();
                Console.WriteLine("Getting Salt.");
                saltData = GetColumn("employee", "salt", "username", Username);

                Console.WriteLine("Getting Hash.");
                hashData = GetColumn("employee", "password", "username", Username);

                salt = Convert.FromBase64String(saltData.Rows[0]["salt"].ToString());
                hash = Convert.FromBase64String(hashData.Rows[0]["password"].ToString());
                if (PasswordHash.VerifyPassword(pw, salt, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SQL.CreateUser(fname, lname, desiredPW) - no overloads
         * 
         * CreateUser takes the employees first and last name and desired pw.
         * 
         * will automatically generate a unique username (first initial, last name + numbers as needed)
         * 
         * will generate a salt for the pw, and store the salt and the resulting hash in the database, along with the firstname/lastname
         * username. Additional information will need to be stored by other means
         * 
         * returns the generated username as a string.
         * 
         * 
         * if this errors out in some way, it will should return null - check when using that username !null.
         */
        public static bool ChkOldPW()
        {
            try
            {

                // no user inputed data in this string, so we dont need to worry about injection attacks, and dateTime is finicky.
                string sql = "SELECT COUNT(id) FROM pw_history WHERE emp_num = '" + LoggedInEmpNum + "' AND change_date < now() -interval 90 day";

                var data = SelectQry(sql);

                int.TryParse(data.Rows[0][0].ToString(), out int i);
                if (i == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CreateUser(string fName, string lName, string pw)
        {
            //remove whitespace from last name
            lName = lName.Trim();
            //remove possible special characters
            lName = lName.Trim(new Char[] { '\'', '.', '>', '<', '/', '\\', '"', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '"', '`', '~', '|', ',', ':', ';' } );
            // automatically create the username as first initial, last name - all lowercase
            string username = fName[0] + lName;
            username = username.ToLower();

            /* if there happen to be 2 people with the same first initial/last name combo
             * then this section will add a number to the end of the username.
             */

            int i = 1;

            while (CheckUsername(username))
            {

                if (i > 1 && i < 10)
                {
                    /* if there happen to be more than 2 people with the same first initial, last name
                     * then we remove the 1 (the last char fo the string) and add the new incrimimented i
                     * to the username (so username2, then username3, ect)
                     */
                    username = username.Substring(0, username.Length - 1);
                }
                else if (i >= 10)
                {
                    /* Let's be real here. If there are more than 10 people with the exact same first
                     * initial and last name, there there is either nepotism or something very weird going on
                     * but just in case, we're removing 2 numbers if it gets above 10 for i.
                     * 
                     * we're not going to check for 3 numbers. Something is messed up, contact IT
                     */
                    username = username.Substring(0, username.Length - 2);
                }
                username += i; // add the iteration number (starting at 1!!!) to the end of the preset username.
                i++;
            }

            //Generate the salt and the hash
            var passwordSalt = PasswordHash.GenerateSalt();
            var passwordHash = PasswordHash.ComputeHash(pw, passwordSalt);

            //convert the hash and salt to a string for database storage
            string saltString = Convert.ToBase64String(passwordSalt);
            string hashString = Convert.ToBase64String(passwordHash);

            //setup the sql string for insertion
            try
            {
                var cmd = new MySqlCommand
                {
                    CommandText = "INSERT INTO employee (first_name, last_name, username, password, salt, hired, location) VALUES(@first_name, @last_name, @username, @password, @salt, @hired, @location)"
                };
                cmd.Parameters.AddWithValue("@first_name", fName);
                cmd.Parameters.AddWithValue("@last_name", lName);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashString);
                cmd.Parameters.AddWithValue("@salt", saltString);
                //cmd.Parameters.AddWithValue("@hired", today);
                cmd.Parameters.Add("@hired", MySqlDbType.Timestamp).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@location", DefaultStore);

                if (SendQry(cmd))
                {
                    return username;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SQL.CheckUsername(username) - no overloads
         * 
         * returns bool if username exists (good for creating new user to prevent duplicate usernames and error in the
         * sql insert or to make sure the username exists before calling the database for the passwordhash
         */
        public static bool CheckUsername(string username)
        {
            try
            {
                if (GetColumn("employee", "username", username).Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SQL.ChangePassword(newPW) - no overloads
         * 
         * Method for changing the pw in the database.
         * 
         * Pulls the username from the current session. Overloaded version takes a username as a second argument.
         * 
         * Note. This method JUST changes the pw (and the hash and the salt, of course).
         * 
         * Be sure to ChkPassword BEFORE calling this function.
         * 
         * throws exceptions upward for use in error messages
         */

        public static void ChangePassword(string newPW)
        {
            var salt = PasswordHash.GenerateSalt();
            var hash = PasswordHash.ComputeHash(newPW, salt);

            string saltString = Convert.ToBase64String(salt);
            string hashString = Convert.ToBase64String(hash);

            try
            {
                // this method throws errors upward, so if we get one it will exit out without finishing the rest
                // of this pw change
                PWProtocol(LoggedInEmpNum, newPW);

                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET password = @hash, salt = @salt WHERE username = @username;" +
                    "INSERT pw_history (`emp_num`, `hash`, `salt`) VALUES (@empNum, @hash, @salt);"
                };

                cmd.Parameters.AddWithValue("@hash", hashString);
                cmd.Parameters.AddWithValue("@salt", saltString);
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@empNum", LoggedInEmpNum);

                /* the SQL query here updates the Employee table with the current new pw for checking, and adds the now
                 * confirmed to be protocol safe new pw to the pw_history database for checking against the next pw change.
                 */

                if (!SendQry(cmd))
                {
                    throw new Exception("Error connecting to the Database. Please contact IT.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SQL.GetEmpNumber(username)
         * 
         * returns an employee number. 
         * 
         * important because for safety reasons (to prevent errors or miss data) we use the emp number for all
         * update and modification functions. The empNum is the Primary Key in the database, unique, and so much
         * less trouble than possibly mispelling usernames.
         *
         * 
         */
        public static int GetEmpNum(string user)
        {
            try
            {
                int.TryParse(GetColumn("employee", "emp_num", "username", user).Rows[0]["emp_num"].ToString(), out int empNum);

                if (empNum == 0)
                {
                    throw new Exception("No employee found.");
                }
                else
                {
                    return empNum;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Update Functions
         * 
         * These functions handle the various fields that might need to be updated in the employee database
         * 
         * they are individual because only one may need to be updated at a time, and due to time constraints it was easier
         * to build them like this and check if a field has changed in the main program rather than doing an entire black box
         * check in the background.
         * 
         */
        public static void UpdateFname(int empNum, string fname)
        {

            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET first_name = @fname WHERE emp_num = @empNum;"
                };
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateLname(int empNum, string lname)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET last_name = @lname WHERE emp_num = @empNum;"
                };
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateStreet(int empNum, string street)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET street = @street WHERE emp_num = @empNum;"
                };

                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateCity(int empNum, string city)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET city = @city WHERE emp_num = @empNum;"
                };

                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateState(int empNum, string st)
        {

            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET state = @state WHERE emp_num = @empNum; "

                };
                cmd.Parameters.AddWithValue("@state", st);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateZip(int empNum, int zip)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET zip = @zip WHERE emp_num = @empNum;"
                };
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Promote(int empNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET type = 'Manager' WHERE emp_num = @empNum;"
                };
                cmd.Parameters.AddWithValue("@empNum", empNum);


                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Demote(int empNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                { CommandText = "UPDATE employee SET type = 'Employee' WHERE emp_num = @empNum;" };

                cmd.Parameters.AddWithValue("@empNum", empNum);


                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Changestore(int empNum, string store)
        {
            try
            {
                var cmd = new MySqlCommand()
                { CommandText = "UPDATE employee SET location = @store WHERE emp_num = @empNum;" };

                cmd.Parameters.AddWithValue("@store", store);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ChangePay(int empNum, double newPay)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE employee SET pay = @newPay WHERE emp_num = @empNum;"
                };
                cmd.Parameters.AddWithValue("@newPay", newPay);
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Terminate(int empNum)
        {
            try
            {
                /*
                var cmd = new MySqlCommand
                {
                    CommandText = "UPDATE `employee` SET `type` = 'Terminated', `terminated` = NOW() WHERE (`emp_num` = @empNum);"
                };
                cmd.Parameters.AddWithValue("@empNum", empNum);
                */

                /* This is ... not proper, but not to bad.
                 * we are not taking directly the empNum from a user.  empNumber is derived from other commands, and therefore can't be altered by
                 * an SQL injection attack.
                 * 
                 * Given that I was having trouble getting NOW() to work with the MySqlCommand paramaters, this seemed to work just fine.
                 */

                string sql = "UPDATE `employee` SET `type` = 'Terminated', `terminated` = NOW() WHERE(`emp_num` = " + empNum + ");";
                SendQry(new MySqlCommand(sql));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /* GetGemployee(empNum) 
         * 
         * This function pulls up a single row DataTable of the employee based on their empNum
         * 
         * this row contains everything needed: fname, lname, address, uname, pwhash, salthash, payrate, start date, end date
         * 
         * 
         */
        public static DataTable GetEmployee(int empNum)
        {
            try
            {
                var data = GetTable("employee", "emp_num", empNum.ToString());
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Order Database Functions
         * 
         * these functions all have to do with retrieving and viewing orders
         * 
         */

        /* SQL.GetOrders( ... several overloads)
         * 
         * at its basic returns orders for the default store of the user.
         * 
         * overloads can return for a different store, or for specific customers, or even specific orders
         */

        /*Default version - defaults to ALL orders from USERS DEFAULT STORE
         * 
         */
        public static DataTable GetOrders()
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT acct_num, del_addy, trans_date, del_date, total, pay_rec, order_status, order_num  FROM order_history WHERE location = @store ORDER BY del_date DESC;"
                };

                cmd.Parameters.AddWithValue("@store", DefaultStore);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Overload 1 - Allows to select ALL orders from a SPECIFIC STORE
         */
        public static DataTable GetOrders(string store)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT acct_num, del_addy, trans_date, del_date, pay_rec, total, order_status, order_num FROM order_history WHERE location = @store ORDER BY del_date DESC;"
                };

                cmd.Parameters.AddWithValue("@store", store);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Overload 2 - All of a single users orders (by email address) from a specific store
         */
        public static DataTable GetOrders(string store, string email)
        {
            try
            {
                string acctNum = GetAcctNum(email).ToString();
                int.TryParse(acctNum, out int custNum);

                return GetOrders(store, custNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Overload 3 ALL of a users orders (by acct_num) in ANY store
         */
        public static DataTable GetOrders(int custNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT acct_num, del_addy, trans_date, del_date, total, pay_rec, order_status, order_num FROM order_history WHERE acct_num = @acctNum ORDER BY del_date DESC;"
                };

                cmd.Parameters.AddWithValue("@acctNum", custNum);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /* Overload 4 - returns all of a customers orders at a specific store, searched by cust num (called in the one searched by email)
         */
        public static DataTable GetOrders(string store, int custNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT acct_num, del_addy, trans_date, del_date, total, pay_rec, order_status, order_num FROM order_history WHERE location = @store AND acct_num = @custNum ORDER BY del_date DESC;"
                };

                cmd.Parameters.AddWithValue("@store", store);
                cmd.Parameters.AddWithValue("@custNum", custNum);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Overload 5  - all of a customers order, for a specific store, after a certain date
         */
        public static DataTable GetOrders(string store, int custNum, DateTime date)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT acct_num, del_addy, trans_date, del_date, total, pay_rec, order_status, order_num FROM order_history WHERE DATE(trans_date) > @date AND  location = @store AND acct_num = @custNum ORDER BY del_date DESC;"
                };
                cmd.Parameters.AddWithValue("@custNum", custNum);
                cmd.Parameters.AddWithValue("@store", store);
                cmd.Parameters.Add("@date", MySqlDbType.Timestamp).Value = date;

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* GetOrderItems(ordernum) - no overloads - for pulling up the items from an order to the new window.
         * 
         * for retrieving the line items out of the order_items database.
         * 
         * only used in the GetOrder functions - no use for just the line items without the additional
         * order information in the order_history database
         * 
         */

        public static DataTable GetOrderItems(int orderNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT h.order_num, v.item_name, i.qty, h.total, h.pay_rec, h.del_date, h.order_status, h.del_addy, h.emp_num FROM order_history h JOIN order_items i ON i.order_num = h.order_num AND h.order_num = @orderNum JOIN items v ON v.item_id = i.item_id ORDER by h.order_num"
                };

                cmd.Parameters.AddWithValue("@orderNum", orderNum);
                //string sql = "SELECT h.order_num, v.item_name, i.qty, h.total, h.pay_rec, h.del_date, h.order_status, h.del_addy FROM order_history h JOIN order_items i ON i.order_num = h.order_num AND h.order_num = "+orderNum+" JOIN items v ON v.item_id = i.item_id ORDER by h.order_num";

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         * valid status:
         * 
         * Ordered, Recieved, Processed, Out for Delivery, Delivered, Canceled
         * 
         * Paid, Not Paid
         */
        public static void ChangeStatus(int orderNum, string status)
        {
            try
            {

                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE order_history SET order_status = @status, emp_num=@empNum WHERE order_num = @orderNum;"
                };

                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@orderNum", orderNum);
                cmd.Parameters.AddWithValue("@empNum", LoggedInEmpNum);

                SendQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ChangeStatus(int orderNum, int empNum)
        {
            if (empNum == 0)
            {
                //order number is not user inputed on the top app, so this is a safe string.
                //this is if a user is unclaiming the order, so emp_num is set back to 0.
                string sql = "UPDATE order_history SET order_status = 'Ordered', emp_num='0' WHERE order_num = " + orderNum + ";";
                SendQry(sql);
            }
            else
            {
                //order number is not user inputed on the top app, so this is a safe string.
                // the empNum is generated from a username or firstname, and not directly entered by the user so not a problem for SQL injection attacks either.
                string sql = "UPDATE order_history SET order_status = 'Received', emp_num=" + empNum + " WHERE order_num = " + orderNum + ";";
                SendQry(sql);
            }
        }

        public static void ChangeDate(int orderNum, string date)
        {

        }

        public static void ChangeDelAddy(int orderNum, string address)
        {

        }

        public static void AddItem(int orderNum, int itemNum, int qty)
        {

        }

        public static void AdjustQty(int orderNum, int itemNum, int qty)
        {

        }

        public static int GetAcctNum(string email)
        {
            try
            {
                int.TryParse(GetColumn("customer", "acct_num", "email", email).Rows[0]["acct_num"].ToString(), out int acctNum);


                if (acctNum == 0)
                {
                    throw new Exception("No employee found.");
                }
                else
                {
                    return acctNum;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int GetAcctNum(string fName, string lName)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT acct_num FROM customer WHERE first_name =@name AND last_name = @lname;"
                };

                cmd.Parameters.AddWithValue("@fname", fName);
                cmd.Parameters.AddWithValue("@lname", lName);

                //string sql = "SELECT acct_num FROM customer WHERE first_name ='" + fName + "' AND last_name = '" + lName + "';";
                int.TryParse(SelectQry(cmd).Rows[0]["acct_num"].ToString(), out int acctNum);


                if (acctNum == 0)
                {
                    throw new Exception("No employee found.");
                }
                else
                {
                    return acctNum;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Store Inventory Functions
         * 
         * these functions deal with the inventory database. 
         * 
         * they DO NOT deal with the item database - that stuff is NOT adjusted by the store app
         * and at this point is to be dealt with by HQ when new items are brought to sale.
         * 
         */

        /* SQL.GetInventory( ... several overloads)
         * 
         * at its basic returns orders for the default store of the user.
         * 
         * overloads can return for a different store, or for specific items
         */
        public static DataTable GetInventory()
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT items.item, items.item_name, items.purchase_cost, items.sell_cost, items.category, store_inventory.qty FROM items  INNER JOIN store_inventory WHERE store_inventory.item_id = items.item_id AND location = @store; "
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetInventory(string store)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT items.item, items.item_name, items.purchase_cost, items.sell_cost, items.category, store_inventory.qty FROM items  INNER JOIN store_inventory WHERE store_inventory.item_id = items.item_id AND location = @store; "
                };

                cmd.Parameters.AddWithValue("@store", store);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetInventory(string store, int itemNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT items.item, items.item_name, items.purchase_cost, items.sell_cost, items.category, store_inventory.qty FROM items  INNER JOIN store_inventory WHERE store_inventory.item_id = items.item_id AND location = @store AND items.item_id = @itemNum; "
                };
                cmd.Parameters.AddWithValue("@store", store);
                cmd.Parameters.AddWithValue("@itemNum", itemNum);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetItemId(string itemName)
        {
            try
            {
                var data = new DataTable();

                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT item_id FROM items WHERE item_name =@itemName;"
                };

                cmd.Parameters.AddWithValue("@itemName", itemName);

                //string sql = "SELECT item_id FROM items WHERE item_name ='" + itemName + "';";
                try
                {
                    data = SelectQry(cmd);
                }
                catch
                {
                    throw new Exception("No item found with that name");
                }
                int.TryParse(data.Rows[0]["item_id"].ToString(), out int item_id);

                return item_id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*CheckStock(itemNum)
         * 
         * returns either an int of the current stock in a store, or with an overload of a store name, an array of 2 ints
         * where index 0= DefaultStore stock and 1= dest store stock
         */
        public static int[] CheckStock(int itemNum, string store)
        {
            int[] stock = new int[2];
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT qty FROM store_inventory WHERE location=@store AND item_id =@item"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);
                cmd.Parameters.AddWithValue("@item", itemNum);

                var data = SelectQry(cmd);

                int.TryParse(data.Rows[0]["qty"].ToString(), out stock[0]);
                Console.WriteLine("Get Stock from default store | amt is " + stock[0]);

                cmd.Parameters["@store"].Value = store;

                data = SelectQry(cmd);

                int.TryParse(data.Rows[0]["qty"].ToString(), out stock[1]);
                Console.WriteLine("Get Stock from dest store | amt is " + stock[1]);
                return stock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int CheckStock(int itemNum)
        {
            int stock;
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT qty FROM store_inventory WHERE location=@store AND item_id =@item"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);
                cmd.Parameters.AddWithValue("@item", itemNum);

                var data = SelectQry(cmd);

                int.TryParse(data.Rows[0]["qty"].ToString(), out stock);
                Console.WriteLine("Get Stock from default store | amt is " + stock);

                return stock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SQL.ChangeQty(itemNum, qty)
         * 
         * this updates the item qty at the store - positive or negative - and spits out an error message if the
         * item is going to go negative.
         * 
         * autmatically uses the users default store. CANNOT MODIFY OTHER STORES INVENTORY
         */
        public static void ChangeQty(int itemNum, int qty)
        {

            var cmd = new MySqlCommand()
            {
                CommandText = "UPDATE store_inventory SET qty = @qty WHERE item_id=@itemNum AND location = @store;"
            };

            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@itemNum", itemNum);
            cmd.Parameters.AddWithValue("@store", DefaultStore);

            try
            {
                SendQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /* SQL.ItemChgStore(itemNum, store, amount)
         * 
         * moves an amount of item from default store to new store
         * 
         */

        public static void ItemChgStore(int itemNum, string store, int amt)
        {
            int[] stock = new int[2];
            //get current value of item qty in Default Store

            stock = CheckStock(itemNum, store);

            try
            {
                //remove an amount of item from default store
                Console.WriteLine("Removing " + amt + " of item " + itemNum + " from " + DefaultStore);
                ChangeQty(itemNum, stock[0] - amt);

                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE store_inventory SET qty = @qty WHERE item_id=@itemNum AND location = @store;"
                };

                cmd.Parameters.AddWithValue("@qty", amt + stock[1]);
                cmd.Parameters.AddWithValue("@itemNum", itemNum);
                cmd.Parameters.AddWithValue("@store", store);

                Console.WriteLine("Adding " + amt + " of item " + itemNum + " to " + store);
                SendQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /* Accounting methods
         * 
         * these methods handle the Accts Rec, Accts Payable, and basic financial reporting tools for the
         * default store. They ONLY affect the default store, and cannot be used to see other stores information
         */

        /*SQL.GetAcRec(... several overloads
         * 
         * this method retrieves the accounts recievables for the store, or a specific one, or a specific users)
         */
        public static DataTable GetAcRec()
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM acct_rec WHERE location =@store;"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetAcRecDetail(int invoiceNum)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT r.invoice_num, r.order_num, r.acct_num, r.amt, r.amt_paid, r.remainder, r.due_date, r.paid_date, h.order_status, h.pay_type, h.trans_date, c.first_name, c.last_name, c.email, c.street, c.city, c.state, c.zip  FROM acct_rec r JOIN order_history h ON h.order_num = r.order_num  JOIN customer c ON c.acct_num = r.acct_num WHERE invoice_num = @invoice ;"
                };
                cmd.Parameters.AddWithValue("@invoice", invoiceNum);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AccRecPmt(int invoiceNum, double pmt)
        {
            try
            {
                var data = new DataTable();
                double totalPaid = 0;

                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT remainder, amt_paid, order_num FROM acct_rec WHERE invoice_num = @id;"
                };
                cmd.Parameters.AddWithValue("id", invoiceNum);

                data = SelectQry(cmd);
                double.TryParse(data.Rows[0]["remainder"].ToString(), out double remain);
                double.TryParse(data.Rows[0]["amt_paid"].ToString(), out double amtPaid);
                int.TryParse(data.Rows[0]["order_num"].ToString(), out int orderNum);

                totalPaid = pmt + amtPaid;

                Console.WriteLine("Remainder: " + remain + " | Already Paid: " + amtPaid + " | Payment: " + pmt + " | TotalPaid: " + totalPaid + " | OrderNum: " + orderNum);

                if (remain == 0)
                {
                    throw new Exception("This invoice is already paid off.");
                }

                if (remain - pmt < 0)
                {
                    throw new Exception("You are attempting to record a payment of more than what is owed. Please contact Accounting to approve this.");
                }

                if (remain == pmt)
                {

                    var cmd2 = new MySqlCommand()
                    {
                        CommandText = "UPDATE acct_rec SET amt_paid = @paid, paid_date = NOW() WHERE invoice_num=@id;" +
                        "UPDATE order_history SET pay_rec ='Paid' WHERE order_num=@orderNum;"
                    };
                    cmd2.Parameters.AddWithValue("id", invoiceNum);
                    cmd2.Parameters.AddWithValue("paid", totalPaid.ToString());
                    cmd2.Parameters.AddWithValue("orderNum", orderNum.ToString());

                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);
                    SendQry(cmd2);

                }
                else if (remain - pmt > 0)
                {
                    var cmd2 = new MySqlCommand()
                    {
                        CommandText = "UPDATE acct_rec SET amt_paid = @paid WHERE invoice_num=@id;"
                    };

                    cmd2.Parameters.AddWithValue("id", invoiceNum);
                    cmd2.Parameters.AddWithValue("paid", totalPaid.ToString());
                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);
                    SendQry(cmd2);
                }
                else // um. Should be caught but... if remain-totalPaid < 0
                {
                    throw new Exception("Error. Contact IT (code NegPay) ");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable AcctPay()
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM acct_pay WHERE location =@store;"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable AcctPay(int id)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM acct_pay WHERE id = @id;"
                };
                cmd.Parameters.AddWithValue("id", id);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AcctPayPmt(int idNum, double pmt)
        {
            try
            {
                var data = new DataTable();
                double totalPaid = 0;

                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT remainder, amt_paid FROM acct_pay WHERE id = @id;"
                };
                cmd.Parameters.AddWithValue("id", idNum);

                data = SelectQry(cmd);
                double.TryParse(data.Rows[0]["remainder"].ToString(), out double remain);
                double.TryParse(data.Rows[0]["amt_paid"].ToString(), out double amtPaid);

                totalPaid = pmt + amtPaid;

                Console.WriteLine("Remainder: " + remain + " | Already Paid: " + amtPaid + " | Payment: " + pmt + " | TotalPaid: " + totalPaid);

                if (remain == 0)
                {
                    throw new Exception("This invoice is already paid off.");
                }

                if (remain - pmt < 0)
                {
                    throw new Exception("You are attempting to record a payment of more than what is owed. Please contact Accounting to approve this.");
                }

                if (remain == pmt)
                {
                    var cmd2 = new MySqlCommand()
                    {
                        CommandText = "UPDATE acct_pay SET amt_paid = @paid, paid_date = NOW() WHERE id=@id;"
                    };
                    cmd2.Parameters.AddWithValue("id", idNum);
                    cmd2.Parameters.AddWithValue("paid", totalPaid.ToString());

                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);
                    SendQry(cmd2);

                }
                else if (remain - pmt > 0)
                {
                    var cmd2 = new MySqlCommand()
                    {
                        CommandText = "UPDATE acct_pay SET amt_paid = @paid WHERE id=@id;"
                    };

                    cmd2.Parameters.AddWithValue("id", idNum);
                    cmd2.Parameters.AddWithValue("paid", totalPaid.ToString());
                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);
                    SendQry(cmd2);
                }
                else // um. Should be caught but... if remain-totalPaid < 0
                {
                    throw new Exception("Error. Contact IT (code NegPay) ");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* still Accounting Methods - but these are the Report Generation Methods
         * 
         * again - these all only deal with the current store of the logged in manager.
         * 
         * this information can be provided to corporate account department for corporate level reports
         */

        public static DataTable GeneralLedger()
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM financials WHERE location = @store"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);

                return SelectQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GeneralLedger(string date)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM financials WHERE location = @store AND entry_date >= @date AND entry_date < @date +interval 1 month ORDER BY entry_date;"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);
                cmd.Parameters.AddWithValue("@date", date);

                return SelectQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GeneralLedger(int year)
        {
            try
            {
                string date = year + "-01-01";
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM financials WHERE location = @store AND entry_date >= @date AND entry_date < @date +interval 1 year ORDER BY entry_date;"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);
                cmd.Parameters.AddWithValue("@date", date);

                return SelectQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //returns current month as default
        public static PLState ProfitLossReport()
        {
            var cmd = new MySqlCommand()
            {
                CommandText = "SELECT trans_type, amt, particular FROM financials WHERE location = @store ORDER BY entry_date;"
            };
            cmd.Parameters.AddWithValue("@store", DefaultStore);

            try
            {
                return ProfitRead(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static PLState ProfitLossReport(string date, int month)
        {
            var profitLoss = new PLState();

            var cmd = new MySqlCommand()
            {
                CommandText = "SELECT trans_type, amt, particular FROM financials WHERE location = @store AND entry_date >= @date AND entry_date < @date +interval 1 month ORDER BY entry_date;"
            };
            cmd.Parameters.AddWithValue("@store", DefaultStore);
            cmd.Parameters.AddWithValue("@date", date);
            try
            {
                profitLoss = ProfitRead(cmd);
                string trimDate = date.Trim(new char[] { ' ', '-' });

                int.TryParse(trimDate.Substring(0, 4), out int year);
                profitLoss.SetMonth(month);
                profitLoss.Year = year;
                return profitLoss;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static PLState ProfitLossReport(int year)
        {
            var profitLoss = new PLState();

            string date = year + "-01-01";
            profitLoss.Year = year;
            var cmd = new MySqlCommand()
            {
                CommandText = "SELECT trans_type, amt, particular FROM financials WHERE location = @store AND entry_date >= @date AND entry_date < @date +interval 1 year ORDER BY entry_date;"
            };
            cmd.Parameters.AddWithValue("@store", DefaultStore);
            cmd.Parameters.AddWithValue("@date", date);
            try
            {
                profitLoss = ProfitRead(cmd);

                profitLoss.Year = year;
                return profitLoss;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /* CashFlowReport 
         * 
         * um. These two reports seem so damn similar to me, just leaving out a few things from one to the other
         * 
         * so... i just recalled the functions. The output side of the app only uses what ... needed...(?) but
         * its all there if I got them backwards!
         * 
         */
        public static PLState CashFlowReport()
        {
            return ProfitLossReport();
        }
        public static PLState CashFlowReport(string date, int month)
        {
            return ProfitLossReport(date, month);
        }
        public static PLState CashFlowReport(int year)
        {
            return ProfitLossReport(year);
        }



        public static DataTable BalanceSheetReport()
        {
            var cmd = new MySqlCommand()
            {
                CommandText = "Select trans_type AS 'Transaction Type', particular AS Detail, amt AS Amount, entry_date As 'Date Entered' from financials WHERE location = @Store AND trans_type - 'Sales' OR trans_type = 'Payroll';" +
                "SELECT 'AcctRec' As 'Transaction Type', CONCAT('Account Receving, Order # ', order_num) AS 'Detail',  amt_paid AS Amount,  due_date As 'Entry Date' FROM acct_rec WHERE location = @Store;" +
                "SELECT 'AcctPay' AS 'Transaction Type', CONCAT('Acct Payable, to ', vendor, ' Invoice #, ', invoice_num) AS Detail, amt_paid*-1 AS Amount ,  due_date as 'EntryDate' FROM capstoneFlowers.acct_pay WHERE location =@Store;"
            };
            cmd.Parameters.AddWithValue("@Store", DefaultStore);

            return BalanceReader(cmd);

        }
        public static DataTable BalanceSheetReport(string date)
        {
            var cmd = new MySqlCommand()
            {
                CommandText = "Select trans_type AS 'Transaction Type', particular AS Detail, amt AS Amount, entry_date As 'Date Entered' from financials WHERE location = @Store AND (trans_type = 'Sales' OR trans_type = 'Payroll') AND (entry_date >= @date AND entry_date < @date + interval 1 month);" +
                "SELECT 'AcctRec' As 'Transaction Type', CONCAT('Account Receving, Order # ', order_num) AS 'Detail',  amt_paid AS Amount,  due_date As 'Entry Date' FROM acct_rec WHERE location = @Store  AND (due_date >= @date AND due_date < @date + interval 1 month);" +
                "SELECT 'AcctPay' AS 'Transaction Type', CONCAT('Acct Payable, to ', vendor, ' Invoice #, ', invoice_num) AS Detail, amt_paid*-1 AS Amount ,  due_date as 'EntryDate' FROM capstoneFlowers.acct_pay WHERE location =@Store  AND (due_date >= @date AND due_date < @date + interval 1 month);"
            };
            cmd.Parameters.AddWithValue("@Store", DefaultStore);
            cmd.Parameters.AddWithValue("@date", date);

            return BalanceReader(cmd);

        }
        public static DataTable BalanceSheetReport(int year)
        {
            string date = year + "-01-01";
            var cmd = new MySqlCommand()
            {
                CommandText = "Select trans_type AS 'Transaction Type', particular AS Detail, amt AS Amount, entry_date As 'Date Entered' from financials WHERE location = @Store AND (trans_type = 'Sales' OR trans_type = 'Payroll') AND (entry_date >= @date AND entry_date < @date + interval 1 year);" +
                "SELECT 'AcctRec' As 'Transaction Type', CONCAT('Account Receving, Order # ', order_num) AS 'Detail',  amt_paid AS Amount,  due_date As 'Entry Date' FROM acct_rec WHERE location = @Store  AND (due_date >= @date AND due_date < @date + interval 1 year);" +
                "SELECT 'AcctPay' AS 'Transaction Type', CONCAT('Acct Payable, to ', vendor, ' Invoice #, ', invoice_num) AS Detail, amt_paid*-1 AS Amount ,  due_date as 'EntryDate' FROM capstoneFlowers.acct_pay WHERE location =@Store  AND (due_date >= @date AND due_date < @date + interval 1 year);"
            };
            cmd.Parameters.AddWithValue("@Store", DefaultStore);
            cmd.Parameters.AddWithValue("@date", date);

            return BalanceReader(cmd);
        }

        /* FutureRec(Date)
         * 
         * this function pulls up the Accts Recievable that are still Outstanding and DUE after the current date for the
         * Balance Sheet Report
         * 
         */
        public static double FutureRec(string date)
        {
            var cmd = new MySqlCommand()
            {

                CommandText = "SELECT SUM(remainder) AS total FROM acct_rec WHERE paid_date = 0 AND due_date >= @date + interval 1 month AND location = @store;"
            };

            cmd.Parameters.AddWithValue("@store", DefaultStore);
            cmd.Parameters.AddWithValue("@date", date);

            var data = new DataTable();
            data = SelectQry(cmd);

            double.TryParse(data.Rows[0]["Total"].ToString(), out double x);

            return x;
        }

        /*BalanceCombile(DataTable)
         * 
         * this function takes the data table and calculates all the necessary values that the Balance Sheet 
         * will need, storing them in the class BalanceData
         * 
         */
        public static balanceData BalanceCompile(DataTable dt)
        {
            var b = new balanceData();

            foreach (DataRow row in dt.Rows)
            {
                if (row[1].ToString() == "Sales")
                {
                    double.TryParse(row[3].ToString(), out double d);
                    b.Cash += d;
                }
                if (row[1].ToString() == "Payroll")
                {
                    double.TryParse(row[3].ToString(), out double d);
                    b.Payroll += -d;
                }
                if (row[1].ToString() == "AcctRec")
                {
                    double.TryParse(row[3].ToString(), out double d);
                    b.Receivable += -d;
                }
                if (row[1].ToString() == "AcctPay")
                {
                    double.TryParse(row[3].ToString(), out double d);
                    b.Payable += -d;
                }
            }

            var cmd = new MySqlCommand()
            {
                CommandText = "SELECT SUM(s.qty * i.purchase_cost) AS Total FROM store_inventory s JOIN items i ON i.item_id = s.item_id WHERE s.location=@store;"
            };
            cmd.Parameters.AddWithValue("@store", DefaultStore);

            var data = new DataTable();
            data = SelectQry(cmd);

            double.TryParse(data.Rows[0]["Total"].ToString(), out double x);
            b.Inventory = x;

            b.Taxes = b.Cash * .055;
            b.Insurance = b.Cash * .001 + b.Taxes * .001;
            b.Trademark = b.Cash * .07 + b.Insurance * .39;
            b.Other = (b.Cash / 50) * .1;
            return b;
        }

        public static void NewLedgerEntry(string type, string particular, double amt)
        {
            /* Valid Types:
             * ENUM('Payroll', 'Inventory', 'Utilities', 'Sales', 'Depreciation', 'Expense', 'Correction')
             * 
             * will be passed from a drop down menu, so will be proper
             */
            try
            {

                var cmd = new MySqlCommand()
                {
                    CommandText = "INSERT INTO `financials` (`location`, `trans_type`, `particular`, `amt`) VALUES ('@store', '@type', '@particular', '@amt');"
                };
                cmd.Parameters.AddWithValue("@store", DefaultStore);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@amt", amt);
                cmd.Parameters.AddWithValue("@particular", particular);

                SendQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* LedgerUpdate--x--
         * 
         * various Ledger Update fields.
         * 
         */
        public static void LedgerUpdateType(int id, string type)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE financials SET type=@type WHERE money_id=@id"
                };
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@id", id);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LedgerUpdateAmt(int id, double amt)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE financials SET amt=@amt WHERE money_id=@id"
                };
                cmd.Parameters.AddWithValue("@amt", amt);
                cmd.Parameters.AddWithValue("@id", id);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void LedgerUpdatePar(int id, string particular)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "UPDATE financials SET particular=@particular WHERE money_id=@id"
                };
                cmd.Parameters.AddWithValue("@particular", particular);
                cmd.Parameters.AddWithValue("@id", id);

                SendQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Private Internal Functions */

        /* PWProtocol(empNum, newPW)
         * 
         * this method will take a desired pw and check it against the pw_history table. It is looking for the following.
         * 
         * a) No pw changes in the last 24 hours for the selected empNum
         * 
         * b) The PW desired does not exist in the last 15 used pws
         * 
         *  throw's exception messages for the app to catch and display as error windows in the pw change prw
         * 
         */
        private static void PWProtocol(int empNum, string newPW)
        {


            try
            {
                if (ChkPasswordNoFail(newPW)) // this checks to see if the pw is the same as the current pw.
                {
                    throw new Exception("Cannot change a password to your current password");
                    /* I don't believe this is a security risk? if they are able to change the pw they already
                     * know the current pw based on how it is set up
                     */
                }






                byte[] prevSalt, prevHash;

                var pwData = new DataTable();
                var lastPWDate = new DateTime();
                var now = DateTime.Now;

                Console.WriteLine("Getting Previous 15 Hashes");
                // get the Table pw_history of all values where empNum is the employee number
                pwData = GetTable("pw_history", "emp_num", empNum.ToString());

                int rows = pwData.Rows.Count;

                if (rows != 0) // if there is any data in the table that is returned - 
                {
                    DateTime.TryParse(pwData.Rows[rows - 1]["change_date"].ToString(), out lastPWDate);

                    /* Another lazy hack. Server time is 4 hours ahead of where we are using this.
                     * 
                     * really should get a global server time variable set up for use ANYWHERE in the world...
                     */
                    var past24 = now.AddHours(-20);

                    Console.WriteLine("Row Count: " + rows + " | 24 Hours ago (servertime):  " + past24 + " | Last PW Change (servertime): " + lastPWDate);

                    if (lastPWDate >= past24)
                    {
                        // if the last pw change was less than 24 hours ago, throw an error
                        throw new Exception("You must wait at least 24 hours before changing your password again");
                    }
                } // else, there has not been any pw changes for this empNum, and we don't have to check last 24hrs

                if (rows > 15)
                {
                    for (int i = 1; i <= 15; i++)
                    {
                        /* this will count down from the last row of the pw_history that was returned, until we
                         * check 15 rows - the pw protocol requirement of not being able to reuse one of the last
                         * 15 pws
                         * 
                         *  we start i at 1 because rows is a Count, and therefore needs to be one smaller to get the
                         *  last row index.  Starting i at 1 just removes the need for a -1 anywhere
                         */


                        // convert the salt and hash into a Byte for the pwHash functions
                        prevSalt = Convert.FromBase64String(pwData.Rows[rows - i]["salt"].ToString());
                        prevHash = Convert.FromBase64String(pwData.Rows[rows - i]["hash"].ToString());



                        if (PasswordHash.VerifyPassword(newPW, prevSalt, prevHash))
                        {
                            /* if the two hashs (one created with this row's salt and the new pw, the other created from
                             * whatever that pw was + the same hash - so if they are the same, they are the same pw) are same
                             * return false
                             */

                            DateTime.TryParse(pwData.Rows[rows - i]["change_date"].ToString(), out DateTime prevPWDate);

                            throw new Exception("You cannot use a pw you have used within the last 15 password changes.\n You last used that password on " + prevPWDate.ToString("yyyy-MM-dd") + ".");
                        }

                    }
                    /* if we get through the last 15 pw possiblities, and have not thrown an error yet, we can simply exit
                     * pw isn't in the system yet. 
                     */

                }
                else // if there are currently less than 15 pw changes
                {
                    for (int i = 1; i <= rows; i++)
                    {
                        /* this will count down from the last row of the pw_history that was returned, until we hit
                         * the number of rows in the table, preventing an out of index error.
                         * 
                         * we start i at 1 because rows is a Count, and therefore needs to be one smaller to get the
                         *  last row index.  Starting i at 1 just removes the need for a -1 anywhere
                         */
                        var prevSaltString = pwData.Rows[rows - i]["salt"].ToString();
                        var prevHashString = pwData.Rows[rows - i]["hash"].ToString();

                        Console.WriteLine("Row#: " + (rows - i) + " | Prev Hash: " + prevHashString + " | Prev Salt: " + prevSaltString + " | New PW: " + newPW);





                        // convert the salt and hash into a Byte for the pwHash functions
                        prevSalt = Convert.FromBase64String(pwData.Rows[rows - i]["salt"].ToString());
                        prevHash = Convert.FromBase64String(pwData.Rows[rows - i]["hash"].ToString());

                        string newHashString = Convert.ToBase64String(PasswordHash.ComputeHash(newPW, prevSalt));
                        Console.WriteLine("PrevHash | NewHash (Bytes) - " + prevHashString + " | " + newHashString);

                        if (PasswordHash.VerifyPassword(newPW, prevSalt, prevHash))
                        {

                            Console.WriteLine("Match Found");

                            /* if the two hashs (one created with this row's salt and the new pw, the other created from
                             * whatever that pw was + the same hash - so if they are the same, they are the same pw) are same
                             * throw an error
                             */
                            DateTime.TryParse(pwData.Rows[rows - i]["change_date"].ToString(), out DateTime prevPWDate);

                            throw new Exception("You cannot use a pw you have used within the last 15 password changes.\n You last used that password on " + prevPWDate.ToString("yyyy-MM-dd") + ".");
                        }

                        Console.WriteLine("No Match Found on Row " + (rows - i));
                    }
                    /* if we get through the last 15 pw possiblities, and have not thrown an error, we can exit gracefully because the
                     * pw isn't in the system yet. 
                     */

                }


            }
            catch (Exception ex)
            {
                //continue to pass the error upwards.
                throw ex;
            }



        }

        /*PWAttemptFail(empNum) - no overlaods, internal use only
         * 
         * if the pw was incorrect, this method adds a value to the pw_fail_attempts table to record the failed attempts
         */

        private static void PWAttemptFail(int empNum)
        {
            try
            {
                Console.WriteLine("PW Incorrect, entering into pw_fail_attempts table");
                var cmd = new MySqlCommand()
                {
                    CommandText = "INSERT pw_fail_attempts (`emp_num`) VALUE (@empNum);"
                };
                cmd.Parameters.AddWithValue("@empNum", empNum);

                SendQry(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /* PwAttempts(empNum) - no overloads, internal use only
         * 
         * this method checks to see if there are more than 3 attempts in the last 15 mins, and sets a property 
         * in this class for use elsewhere
         */

        private static int PwAttempts(int empNum)
        {
            try
            {
                var now = DateTime.Now;

                //the database server time is 4 hours ahead of EST. This is a hack. I should be checking against
                // the server time, defining it as a global variable against DateTime.Now no matter where the program
                // is run

                var timePass = now.AddMinutes(-15).AddHours(4).ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine("15 Mins ago (server time)  is: " + timePass + " | Current Failed Attempts before Check for more: " + FailPWAttempts);

                /* neither of these two parts of the string are being accepted from user input (so safe against SQL injection) and DateTime is so stupidly finicky
                 * that it is just so much easier to pass it as a string
                 */
                string sql = "SELECT COUNT(`id`) FROM `pw_fail_attempts` WHERE emp_num = '" + GetEmpNum(Username) + "' AND fail_time >='" + timePass + "'";
                var cmd = new MySqlCommand(sql);


                // we could use another way, because we are JUST getting back a count, but this function is already
                // written and I'm lazy :)
                var data = SelectQry(cmd);
                int.TryParse(data.Rows[0][0].ToString(), out int f);
                FailPWAttempts = f; // set the property for use elsewhere if needed

                Console.WriteLine("Failed Attempts after checking Database " + FailPWAttempts);
                return f; // return the value, not the property, just in case.

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* BalanceReader - no overloads, internal use only
         * 
         * Takes the incoming command, which is a serious of SQL statements, and inputs them all into a 
         * unified table (so there are no blank spaces) that can be displayed in a datagridview
         * 
         */
        private static DataTable BalanceReader(MySqlCommand cmd)
        {
            var data = new DataTable("Balance Report");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            data.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TransactionType";
            column.AutoIncrement = false;
            column.Caption = "Transaction";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            data.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Details";
            column.AutoIncrement = false;
            column.Caption = "Details";
            column.ReadOnly = false;
            column.Unique = false;
            data.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Amount";
            column.AutoIncrement = false;
            column.Caption = "Amount";
            column.ReadOnly = false;
            column.Unique = false;
            data.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "EntryDate";
            column.AutoIncrement = false;
            column.Caption = "Date";
            column.ReadOnly = false;
            column.Unique = false;
            data.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = data.Columns["id"];
            data.PrimaryKey = PrimaryKeyColumns;

            int i = 0; //counter

            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {

                MySqlDataReader rdr = null;
                try
                {
                    cmd.Connection = cnn;
                    cnn.Open();

                    Console.WriteLine("Connection:  {0}", cnn.State);
                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);

                    rdr = cmd.ExecuteReader();
                    while (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            row = data.NewRow();
                            row["id"] = i;
                            row[1] = rdr.GetString(0);
                            row[2] = rdr.GetString(1);
                            row[3] = rdr.GetDouble(2);
                            row[4] = rdr.GetString(3);
                            data.Rows.Add(row);
                            i++;

                        }
                        rdr.NextResult();
                    }

                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /* PorfitRead() No overloads, interal use only
         * 
         * this is the function that reads in the data from the General Ledger to create the Profit Loss
         * sheet and the Cashflow sheet, adding the data into a custom class
         * 
         */

        private static PLState ProfitRead(MySqlCommand cmd)
        {
            var profitLoss = new PLState();

            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {
                MySqlDataReader rdr = null;
                try
                {
                    cmd.Connection = cnn;
                    cnn.Open();

                    Console.WriteLine("Connection:  {0}", cnn.State);
                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr.GetString(0) == "Payroll")
                        {
                            profitLoss.Payroll += rdr.GetDouble(1);

                        }
                        else if (rdr.GetString(0) == "Utilities")
                        {
                            profitLoss.Utilities += rdr.GetDouble(1);

                        }
                        else if (rdr.GetString(0) == "Inventory")
                        {
                            profitLoss.Inventory += rdr.GetDouble(1);

                        }
                        else if (rdr.GetString(0) == "Expense")
                        {
                            if (rdr.GetString(2).Substring(0, 5) == "Vendo")
                            {
                                profitLoss.Vendor += rdr.GetDouble(1);
                            }
                            else if (rdr.GetString(2).Substring(0, 5) == "Marke" || rdr.GetString(2).Substring(0, 5) == "Addit")
                            {
                                profitLoss.Marketing += rdr.GetDouble(1);

                            }
                            else if (rdr.GetString(2).Substring(0, 5) == "Petty")
                            {
                                profitLoss.Expenses += rdr.GetDouble(1);
                            }
                            else
                                profitLoss.Expenses += rdr.GetDouble(1);

                        }
                        else if (rdr.GetString(0) == "Sales")
                        {
                            if (rdr.GetString(2).Substring(0, 4) == "Acct")
                            {
                                profitLoss.Receivable += rdr.GetDouble(1);
                            }
                            else
                                profitLoss.Sales += rdr.GetDouble(1);
                        }
                    }

                    return profitLoss;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /* GetStore() - no overloads - private - internal use Only
         * 
         * used to get the store of the logged in account.
         */
        private static string GetStore()
        {
            try
            {
                var data = new DataTable();
                data = GetColumn("employee", "location", "username", Username);

                string store = data.Rows[0]["location"].ToString();
                return store;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SetupConnection() - Internal, Private, No Overloads
         * 
         * This function reads in the connection data from the file in the resources and adds its information to the 
         * properties needed for the connection builder.
         * 
         * to do: encrypt and decrypt the file for additional protection
         */

        private static void SetupConnection()
        {

            string[] rows = Resources.connection.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            /* I acknowledge this is a dirty way to do this. There is no error checking to make sure that the
             * file is in the correct order, nor is it a deliniated list. This is fast and effective for the moment
             * mostly being used as a current method until I figure out how to use the config file properly.:/
             */
            address = rows[0];
            database = rows[1];
            userid = rows[2];
            password = rows[3];

            MySqlConnectionStringBuilder cnn = new MySqlConnectionStringBuilder
            {

                Server = address,
                Database = database,
                UserID = userid,
                Password = password,
                Port = 3306,
                AllowZeroDateTime = true

            };

            cnnStr = cnn;
        }

        /* Permission() - No Overloads - Private - Internal use Only
         * 
         * This function can be used to determine if the current employee has permission to access
         * whatever it is being asked of. 
         * 
         * checks the password
         * 
         * checks for if the user has the Manager access level, and sets appropriately.
         * 
         * 
         * 
         * This may change in the future.
         */
        private static void Permission()
        {

            var cmd = new MySqlCommand()
            {
                CommandText = "SELECT type FROM employee WHERE username = @user;"
            };

            cmd.Parameters.AddWithValue("@user", Username);

            //string sql = "SELECT type FROM employee WHERE username = '" + Username + "';";
            MySqlDataReader rdr = null;
            int i = 0;

            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {
                try
                {
                    cmd.Connection = cnn;
                    cnn.Open();

                    Console.WriteLine("Connection:  {0}", cnn.State);
                    Console.WriteLine("Sending Command: {0}", cmd.CommandText);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Console.WriteLine("Checking Data. Username: " + Username + " | AcctType: " + rdr.GetString(i));
                        i++;
                        // this bit is just in case the command somehow draws back more than one username of the same name. 
                        // the username col in this table is set to unique, so this shouldn't happen. 

                        if (rdr.GetString(0) == "Manager")
                        {

                            IsManager = true;
                            CurEmp = true;

                        }
                        else if (rdr.GetString(0) == "Terminated")
                        {
                            CurEmp = false;
                            IsManager = false;
                        }
                        else
                        {
                            IsManager = false;
                            CurEmp = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }

            }
        }
        /* Generic SQL functions to Follow
         * 
         * The goal is to not use these functions outside of the class. Because these are some of the generic connection
         * types were going to need over and over, then they are functions. 
         * 
         *
         * 
         * they are available for public use for edge cases however
         */


        /* SQL.GetTable(... Multiple overloads)
         * 
         * This function is designed to get any table from the connection.
         * 
         * Overloaded version can order the table by a custom ORDER BY
         * 
         * Overloaded version can be ordered by, where col = value
         * 
         * Both functions return NULL data if they catch an exception, and log it to the console
         * 
         * in live, check for Null when using these functions, if return null then throw an error.
         * 
         * does where val does not currently work with BETWEEN
         * 
         * some of these  use what appears to be vulnerable to an SQL Injection attack. Due to the nature in which we
         * are using them in the app, they are actually secure - for most only the "Search" value (where = value)" is
         * entered by the user. Everything else is predetermined.
         */
        static public DataTable GetTable(string tableName)
        {
            try
            {
                string sql = "SELECT * FROM " + tableName;
                return SelectQry(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static public DataTable GetTable(string tableName, string orderBy)
        {
            try
            {
                string sql = "SELECT * FROM " + tableName + "ORDER BY" + orderBy + ";";
                return SelectQry(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static public DataTable GetTable(string tableName, string whCol, string whVal)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT * FROM " + tableName + " WHERE " + whCol + " = @value;"
                };


                cmd.Parameters.AddWithValue("@value", whVal);

                //string sql = "SELECT * FROM " + tableName + " WHERE " + whCol + "=" + whVal + ";";
                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* same as GetTable, except takes the table name and the column name as arguments, and only returns
         * that single column
         * 
         * single overload with a third string, WHERE query
         * 
         * session.GetColumn(tablename, columnName, valueOfCol)
         * 
         * session.GetColumn(ACCT_REC, acct_num, "10003")
         * 
         * does not currently work with BETWEEN query (tho you could jury rig it with
         * FROM table BETWEEN value=start, value=end; as one string in the tableName.
         * 
         * don't do this. just use sendqry instead.
         * 
         * some of these  use what appears to be vulnerable to an SQL Injection attack. Due to the nature in which we
         * are using them in the app, they are actually secure - for most only the "Search" value (where = value)" is
         * entered by the user. Everything else is predetermined.
         */
        static public DataTable GetColumn(string tableName, string colName)
        {
            try
            {
                string sql = "SELECT " + colName + " FROM " + tableName + ";";
                return SelectQry(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static public DataTable GetColumn(string tableName, string colName, string whereVal)
        {
            try
            {
                var cmd = new MySqlCommand()
                {
                    CommandText = "SELECT " + colName + " FROM " + tableName + " WHERE " + colName + "= @value ;"
                };


                cmd.Parameters.AddWithValue("@value", whereVal);
                //string sql = "SELECT " + colName + " FROM " + tableName + " WHERE " + colName + "= '" + whereVal + "' ;";
                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public DataTable GetColumn(string tableName, string colName, string whereCol, string whereVal)
        {
            try
            {
                string sql = "SELECT " + colName + " FROM " + tableName + " WHERE " + whereCol + "=@value ;";
                var cmd = new MySqlCommand()
                {
                    CommandText = sql
                };

                cmd.Parameters.AddWithValue("@value", whereVal);

                return SelectQry(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* SendQry(2 overloads)
         * 
         * 
         * For Executing non table getting queries (INSERT, ALTER, UPDATE)
         * 
         * takes a string, and puts it into a MySqlCommand 
         * 
         * or
         *
         * takes a MySqlCommand - built with the MySqlCommand.CommandText functions
         * 
         * using something like:
         * 
         *          cmd.CommandText = "INSERT INTO testTable(ID, FIRST_NAME, LAST_NAME, EMAIL) VALUES(?ID, ?FIRST_NAME, ?LAST_NAME, ?EMAIL)";
                    cmd.Parameters.Add("?ID", MySqlDbType.Int32).Value = ID;
                    cmd.Parameters.Add("?FIRST_NAME", MySqlDbType.Text).Value = firstName;
                    cmd.Parameters.Add("?LAST_NAME", MySqlDbType.Text).Value = lastName;
                    cmd.Parameters.Add("?EMAIL", MySqlDbType.Text).Value = email;

            * Alternatively, it the  SQL Query to be sent does not need to be rebuilt like this often,
            * simply store it as a string 
           

            returns a boolean, of True if the command is successful (ie, returns more than 0 rows), or false
            if the command is not successful (returns 0 rows)
         */
        static public bool SendQry(MySqlCommand cmd)
        {

            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {

                try
                {

                    // assigns the connection to the command, so when executeNonQry fires it knows what connection to use
                    cmd.Connection = cnn;
                    cnn.Open();

                    //Sends the command, as defined by the string builder externally.

                    /* return true/false is for edge cases where we need to know if 0 lines were affected - and hence the data didn't update
                     */
                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    //mostly for debugging at this point.
                    Console.WriteLine("Error: {0}", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }
            }
        }

        static public bool SendQry(string sql)
        {

            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {

                try
                {

                    // assigns the connection to the command, so when executeNonQry fires it knows what connection to use
                    var cmd = new MySqlCommand(sql, cnn);
                    cnn.Open();

                    //Sends the command, as defined by the string builder externally.
                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    //mostly for debugging at this point.
                    Console.WriteLine("Error: {0}", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }
            }
        }

        /* SelectQry( ... 2 overloads
         * 
         * this is the most basic function here, returns a datatable from a Select string.
         * 
         * all GetTable and GetColumn functions use it, they just build the string for you
         * 
         * this is public in case need something that doesn't fit in the GetTable, GetColumn overloads
         */

        static public DataTable SelectQry(MySqlCommand cmd)
        {
            var tableData = new DataTable();
            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {

                try
                {

                    cmd.Connection = cnn;
                    cnn.Open();

                    using (MySqlDataAdapter data = new MySqlDataAdapter(cmd))
                    {
                        data.Fill(tableData);
                    }

                    return tableData;
                }
                catch (Exception ex)
                {
                    //mostly for debugging at this point.
                    Console.WriteLine("Error: {0}", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }

            }
        }

        static public DataTable SelectQry(string sql)
        {
            var tableData = new DataTable();
            using (var cnn = new MySqlConnection(cnnStr.ConnectionString))
            {

                try
                {

                    var cmd = new MySqlCommand(sql, cnn);
                    cnn.Open();
                   
                    using (MySqlDataAdapter data = new MySqlDataAdapter(cmd))
                    {
                        data.Fill(tableData);
                    }

                    return tableData;
                }
                catch (Exception ex)
                {
                    //mostly for debugging at this point.
                    Console.WriteLine("Error: {0}", ex.Message);
                    throw ex;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }
            }
        }
    }
}

