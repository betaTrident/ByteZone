using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;


namespace E_CommerceASP.Controllers
{
    public class HomeController : Controller
    {
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dennis\source\repos\E-CommerceASP\E-CommerceASP\App_Data\byteZone.mdf;Integrated Security=True";
        string connStr1 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dennis\source\repos\E-Commerce_Project\E-Commerce_Project\App_Data\StudentInformationSystem.mdf;Integrated Security=True";
        string connStr2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dennis\source\repos\E-Commerce_Project\E-Commerce_Project\App_Data\Phonebook.mdf;Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult studAccData()
        {
            var studData = new List<object>();

            var idNum = Request["studentID"];
            var firstName = Request["fName"];
            var lastName = Request["lName"];
            var gender = Request["gendr"];
            var courseCode = Request["crsCode"];
            var courseName = Request["crsName"];
            var numSubjects = Request["subject"];
            var totalUnits = Request["units"];
            var prelimsPymt = Request["prelim"];
            var midtermsPymt = Request["midterm"];
            var semisPymt = Request["semis"];
            var finalsPymt = Request["finals"];
            var totalFees = Request["total"];
            var paymentMode = Request["paymentM"];
            var amountPaid = Request["amtPaid"];
            var amtToPay = Request["amontToPay"];
            var change = Request["totalChange"];

            studData.Add(new
            {
                mess = 1
            });

            return Json(studData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cusAddToCart()
        {
            return View();
        }
        public ActionResult customers()
        {
            return View();
        }
        public ActionResult retrieveFinals()
        {
            return View();
        }
        public ActionResult cusProducts()
        {
            return View();
        }

        public ActionResult practiceFinals()
        {
            return View();
        }

        public ActionResult Student_Acc_Sys()
        {
            return View();
        }
        public ActionResult Homepage()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult cusRegistration()
        {
            return View();
        }
        public ActionResult cusLogin()
        {
            return View();
        }
        public ActionResult cusHomepage()
        {
            return View();
        }
        public ActionResult ProductEntryForm()
        {
            return View();
        }
        public ActionResult adminHomepage()
        {
            return View();
        }
        public ActionResult update()
        {
            return View();
        }
        public ActionResult createFinals()
        {
            return View();
        }
        public ActionResult retrievFinal()
        {
            return View();
        }
        public ActionResult retrieveFinal()
        {
            return View();
        }
        public ActionResult updatePhone()
        {
            return View();
        }
        public ActionResult updateProduct()
        {
            return View();
        }
        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Category { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string Image { get; set; }
            public DateTime CreatedAt { get; set; }
        }


        [HttpGet]
        public ActionResult GetSortedProducts(string sortBy)
        {
            try
            {
                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;

                        // Modify the SQL query based on the sortBy parameter
                        cmd.CommandText = $"SELECT * FROM PRODUCTS ORDER BY {sortBy}";

                        using (var reader = cmd.ExecuteReader())
                        {
                            var products = new List<dynamic>();

                            while (reader.Read())
                            {
                                var imgFilename = reader["prod_img"].ToString();
                                var imgSrc = Url.Action("Image", "Home", new { filename = imgFilename });

                                var createDate = "";
                                if (DateTime.TryParse(reader["prod_createDate"].ToString(), out DateTime date))
                                {
                                    createDate = date.ToString("MM-dd-yyyy");
                                }
                                else
                                {
                                    createDate = reader["prod_createDate"].ToString();
                                }

                                products.Add(new
                                {
                                    prod_ID = reader["prod_ID"],
                                    prod_name = reader["prod_name"],
                                    prod_brand = reader["prod_brand"],
                                    prod_category = reader["prod_category"],
                                    prod_price = reader["prod_price"],
                                    prod_description = reader["prod_description"],
                                    prod_quantity = reader["prod_quantity"],
                                    prod_img = imgSrc, // Use the generated image source
                                    prod_createDate = createDate // Use the formatted created date
                                });
                            }

                            return Json(products, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        public ActionResult Search(string searchQuery)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dennis\source\repos\E-CommerceASP\E-CommerceASP\App_Data\byteZone.mdf;Integrated Security=True";
            List<Product> products = new List<Product>();

            using (var db = new SqlConnection(connStr))
            {
                db.Open();

                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM PRODUCTS WHERE prod_name LIKE @searchQuery";
                    cmd.CommandText = @"
                    SELECT * FROM PRODUCTS 
                    WHERE 
                        prod_name LIKE @searchQuery 
                        OR prod_ID LIKE @searchQuery
                        OR prod_brand LIKE @searchQuery
                        OR prod_category LIKE @searchQuery";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ID = (int)reader["prod_ID"],
                                    Name = reader["prod_name"].ToString(),
                                    Brand = reader["prod_brand"].ToString(),
                                    Category = reader["prod_category"].ToString(),
                                    Quantity = (int)reader["prod_quantity"],
                                    Price = (decimal)reader["prod_price"],
                                    Image = reader["prod_img"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["prod_createDate"])
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
            }

            return View(products);
        }


        [HttpGet]
        public ActionResult GetProductData(int prod_ID)
        {
            try
            {
                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Products WHERE prod_ID = @prod_ID";
                        cmd.Parameters.Add(new SqlParameter("@prod_ID", SqlDbType.Int) { Value = prod_ID });

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var product = new
                                {
                                    prod_ID = reader["prod_ID"],
                                    prod_name = reader["prod_name"],
                                    prod_brand = reader["prod_brand"],
                                    prod_category = reader["prod_category"],
                                    prod_price = reader["prod_price"],
                                    prod_description = reader["prod_description"],
                                    prod_quantity = reader["prod_quantity"],
                                    prod_img = reader["prod_img"],
                                };

                                return Json(new { success = true, product = product }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, message = "Product not found" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult UpdateProduct(FormCollection collection, HttpPostedFileBase prodImg)
        {
            try
            {
                var prodID = int.Parse(collection["prodID"]);
                var prodName = collection["prodName"];
                var prodBrand = collection["prodBrand"];
                var prodCategory = collection["prodCategory"];
                var prodQuantity = int.Parse(collection["prodQuantity"]);
                var prodPrice = decimal.Parse(collection["prodPrice"]);
                var prodDesc = collection["prodDesc"];

                string imgPath = "";
                if (prodImg != null && prodImg.ContentLength > 0)
                {
                    string imag = Path.GetFileName(prodImg.FileName);
                    string logpath = @"D:\\Images";
                    string filepath = Path.Combine(logpath, imag);
                    prodImg.SaveAs(filepath);
                    imgPath = imag;
                }

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE Products SET " +
                                          "prod_name = @prodName, " +
                                          "prod_brand = @prodBrand, " +
                                          "prod_category = @prodCategory, " +
                                          "prod_quantity = @prodQuantity, " +
                                          "prod_price = @prodPrice, " +
                                          "prod_description = @prodDesc";

                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            cmd.CommandText += ", prod_img = @imgPath";
                        }

                        cmd.CommandText += " WHERE prod_ID = @prodID";

                        cmd.Parameters.AddWithValue("@prodID", prodID);
                        cmd.Parameters.AddWithValue("@prodName", prodName);
                        cmd.Parameters.AddWithValue("@prodBrand", prodBrand);
                        cmd.Parameters.AddWithValue("@prodCategory", prodCategory);
                        cmd.Parameters.AddWithValue("@prodQuantity", prodQuantity);
                        cmd.Parameters.AddWithValue("@prodPrice", prodPrice);
                        cmd.Parameters.AddWithValue("@prodDesc", prodDesc);

                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            cmd.Parameters.AddWithValue("@imgPath", imgPath);
                        }

                        var rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Failed to update product" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating product: " + ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetCustomerData()
        {
            try
            {
                string customerFullName = Session["UserName"]?.ToString();
                if (string.IsNullOrEmpty(customerFullName))
                {
                    return Json(new { success = false, message = "Customer full name not found in session" }, JsonRequestBehavior.AllowGet);
                }

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM CustomerAccount WHERE cus_fullname = @customerFullName";
                        cmd.Parameters.Add(new SqlParameter("@customerFullName", SqlDbType.NVarChar) { Value = customerFullName });

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var customer = new
                                {
                                    full_name = reader["cus_fullname"],
                                    email = reader["cus_email"],
                                    address = reader["cus_address"],
                                    phone_number = reader["cus_phonenum"],
                                    birthdate = Convert.ToDateTime(reader["cus_birthdate"]).ToString("yyyy-MM-dd")
                                };

                                return Json(new { success = true, customer = customer }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, message = "Customer not found" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult UpdateCustomerData(string full_name, string email, string address, string phone_number, string birthdate, string password, string confirm_password)
        {
            try
            {
                string customerFullName = Session["UserName"]?.ToString();
                if (string.IsNullOrEmpty(customerFullName))
                {
                    return Json(new { success = false, message = "Customer full name not found in session" });
                }

                // Check if passwords match
                if (password != confirm_password)
                {
                    return Json(new { success = false, message = "Passwords do not match" });
                }

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE CustomerAccount SET cus_email = @email, cus_address = @address, cus_phonenum = @phone_number, cus_birthdate = @birthdate, cus_password = @password WHERE cus_fullname = @customerFullName";
                        cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar) { Value = email });
                        cmd.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar) { Value = address });
                        cmd.Parameters.Add(new SqlParameter("@phone_number", SqlDbType.NVarChar) { Value = phone_number });
                        cmd.Parameters.Add(new SqlParameter("@birthdate", SqlDbType.Date) { Value = birthdate });
                        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar) { Value = password });
                        cmd.Parameters.Add(new SqlParameter("@customerFullName", SqlDbType.NVarChar) { Value = customerFullName });

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Failed to update customer information" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }




        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM Products WHERE prod_ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    var ctr = cmd.ExecuteNonQuery();
                    if (ctr > 0)
                    {
                        Response.Write("<script>alert('Product Successfully Deleted!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Product Deletion Failed.')</script>");
                    }
                }
            }

            return RedirectToAction("Products");
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {
                            
                            using (var cmd = db.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "DELETE FROM CART WHERE cus_ID = @id";
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }

                            
                            using (var cmd = db.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "DELETE FROM CustomerAccount WHERE cus_ID = @id";
                                cmd.Parameters.AddWithValue("@id", id);
                                var ctr = cmd.ExecuteNonQuery();
                                if (ctr > 0)
                                {
                                    TempData["Message"] = "Customer Account Successfully Deleted!";
                                }
                                else
                                {
                                    TempData["Message"] = "Customer Account Deletion Failed.";
                                }
                            }

                            
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            
                            transaction.Rollback();
                            TempData["Message"] = "An error occurred: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("customers");
        }


        [HttpGet]
        public FileResult Image(string filename)
        {
            var folder = "";
            var filepath = "";

            try
            {
                folder = @"D:\\Images";
                filepath = Path.Combine(folder, filename);
                if (!System.IO.File.Exists(filepath))
                {
                    Response.Write("<script>alert('Image not found')</script>");
                }
            }
            catch (Exception)
            {

            }
            var mime = System.Web.MimeMapping.GetMimeMapping(Path.GetFileName(filepath));
            Response.Headers.Add("content-disposition", "inline");
            return new FilePathResult(filepath, mime);
        }


        [HttpGet]
        public ActionResult GetImageFile(string filename)
        {
            try
            {
                var folder = @"D:\\Images";
                var filepath = Path.Combine(folder, filename);

                if (!System.IO.File.Exists(filepath))
                {

                    return HttpNotFound("Image not found");
                }

                var mime = System.Web.MimeMapping.GetMimeMapping(filename);

                Response.Headers.Add("content-disposition", "inline");
                return File(filepath, mime);
            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Home", "GetImageFile"));
            }
        }


        [HttpPost]
        public ActionResult ProductEntryForm(FormCollection collection, HttpPostedFileBase prodImg)
        {
            string imag = Path.GetFileName(prodImg.FileName);
            var extension = Path.GetExtension(prodImg.FileName).ToLower();
            int filesize = prodImg.ContentLength;

            string logpath = "D:\\Images";
            string filepath = Path.Combine(logpath, imag);
            prodImg.SaveAs(filepath);

            string prodName = collection["prodName"];
            string prodBrand = collection["prodBrand"];
            string prodCategory = collection["prodCategory"];
            string prodPrice = collection["prodPrice"];
            string prodDesc = collection["prodDesc"];
            string prodQuantity = collection["prodQuantity"];


            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Products (prod_name, prod_brand, prod_category, prod_price, prod_description, prod_quantity, prod_img, prod_createDate) " +
                                      "VALUES (@prodName, @prodBrand, @prodCategory, @prodPrice, @prodDesc, @prodQuantity, @prodImg, @createdAt)";

                    cmd.Parameters.AddWithValue("@prodName", prodName);
                    cmd.Parameters.AddWithValue("@prodBrand", prodBrand);
                    cmd.Parameters.AddWithValue("@prodCategory", prodCategory);
                    cmd.Parameters.AddWithValue("@prodPrice", prodPrice);
                    cmd.Parameters.AddWithValue("@prodDesc", prodDesc);
                    cmd.Parameters.AddWithValue("@prodQuantity", prodQuantity);
                    cmd.Parameters.AddWithValue("@prodImg", imag);
                    cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);

                    var ctr = cmd.ExecuteNonQuery();
                    if (ctr > 0)
                    {
                        Response.Write("<script>alert('Product Successfully Created!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Product Creation Failed.')</script>");
                    }
                }
            }

            return View();
        }


        public ActionResult RegisterUser()
        {
            var data = new List<object>();

            string username = Request["username"];
            string password = Request["password"];
            string firstName = Request["firstName"];
            string lastName = Request["lastName"];
            string email = Request["email"];
            string phoneNumber = Request["phoneNumber"];
            string dateOfBirth = Request["birthDate"];
            string address = Request["address"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO UserAccounts (Username, Password, FirstName, LastName, EmailAddress, PhoneNumber, BirthDate, Address, CreatedAt, IsAdmin) " +
                                      "VALUES (@username, @password, @firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @address, @createdAt, @isAdmin)";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@dateOfBirth", string.IsNullOrEmpty(dateOfBirth) ? (object)DBNull.Value : DateTime.Parse(dateOfBirth));
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@isAdmin", false);

                    var ctr = cmd.ExecuteNonQuery();

                    if (ctr > 0)
                    {
                        data.Add(new { mess = 1, message = "Registered Successfully!" });
                    }
                    else
                    {
                        data.Add(new { mess = 0, error = "Registration failed. Please try again later." });
                    }
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OtherPage()
        {

            string userName = Session["UserName"] as string;


            return View(userName);
        }

        [HttpPost]
        public ActionResult LoginUser(string login, string password)
        {
            try
            {
                int cus_ID = 0;
                string userName = null;

                if (IsAdmin(login, password))
                {
                    
                    return Json(new { success = true, message = "Admin login successful!", redirectUrl = "/Home/adminHomepage", isAdmin = true });
                }
                else
                {
                    using (var db = new SqlConnection(connStr))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT cus_ID, cus_fullname FROM CustomerAccount WHERE (cus_username = @login OR cus_email = @login) AND cus_password = @password";
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@password", password);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    
                                    cus_ID = Convert.ToInt32(reader["cus_ID"]);
                                    userName = reader["cus_fullname"].ToString();
                                }
                            }
                        }
                    }

                    
                    if (!string.IsNullOrEmpty(userName) && cus_ID != 0)
                    {
                        Session["UserName"] = userName;
                        Session["cus_ID"] = cus_ID;
                        return Json(new { success = true, message = "Customer login successful!", redirectUrl = "/Home/cusHomepage", isAdmin = false });
                    }
                    else
                    {
                       
                        return Json(new { success = false, message = "Invalid login credentials." });
                    }
                }
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        private bool IsAdmin(string login, string password)
        {
            return login.ToLower() == "admin" && password == "byteZoneAdmin2@";
        }

        [HttpGet]
        public ActionResult GetUserInformation()
        {
            try
            {
                string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dennis\source\repos\E-CommerceASP\E-CommerceASP\App_Data\byteZone.mdf;Integrated Security=True";

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                       
                        cmd.CommandText = "SELECT cus_fullname, cus_email, cus_address, cus_phoneNumber, cus_birthdate FROM CustomerAccount WHERE cus_username = @username";
                        cmd.Parameters.AddWithValue("@username", Session["UserName"]);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                return Json(new
                                {
                                    success = true,
                                    fullname = reader["cus_fullname"].ToString(),
                                    email = reader["cus_email"].ToString(),
                                    address = reader["cus_address"].ToString(),
                                    phoneNumber = reader["cus_phoneNumber"].ToString(),
                                    birthdate = reader["cus_birthdate"].ToString()
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                return Json(new { success = false, message = "User information not found." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public JsonResult SignUp()
        {
            string username = Request["username"];
            string email = Request["email"];
            string fullName = Request["fullName"];
            string address = Request["address"];
            string phoneNumber = Request["phoneNumber"];
            string birthdate = Request["birthdate"];
            string password = Request["password"];
            string confirmPassword = Request["confirmPassword"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO CustomerAccount (cus_username, cus_email, cus_fullname, cus_address, cus_phonenum, cus_birthdate, cus_password, cus_confirmpass, cus_createdate) " +
                                      "VALUES (@username, @email, @fullName, @address, @phoneNumber, @birthdate, @password, @confirmpass, @createdate)";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@birthdate", string.IsNullOrEmpty(birthdate) ? (object)DBNull.Value : DateTime.Parse(birthdate));
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@confirmpass", confirmPassword);
                    cmd.Parameters.AddWithValue("@createdate", DateTime.UtcNow);


                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Json(new { success = true, message = "Account created successfully!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Account creation failed. Please try again." });
                    }
                }

            }

        }
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            try
            {
                // Retrieve cus_ID from session
                int cus_ID = Convert.ToInt32(Session["cus_ID"]);

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        // Check if the product is already in the cart for the customer
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT COUNT(*) FROM CART WHERE cus_ID = @cus_ID AND prod_ID = @prod_ID";
                        cmd.Parameters.AddWithValue("@cus_ID", cus_ID);
                        cmd.Parameters.AddWithValue("@prod_ID", productId);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Product is already in the cart
                            return Json(new { success = false, message = "Product is already in the cart." });
                        }

                        // Get product price
                        cmd.CommandText = "SELECT prod_price FROM Products WHERE prod_ID = @prod_ID";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@prod_ID", productId);

                        var price = (decimal)cmd.ExecuteScalar();

                        // Insert into cart
                        cmd.CommandText = "INSERT INTO CART (cus_ID, prod_ID, Quantity, Price, Status) VALUES (@cus_ID, @prod_ID, @Quantity, @Price, @Status)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@cus_ID", cus_ID);
                        cmd.Parameters.AddWithValue("@prod_ID", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Status", "Pending");

                        cmd.ExecuteNonQuery();
                    }
                }

                return Json(new { success = true, message = "Product added to cart successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to add product to cart: " + ex.Message });
            }
        }
        public ActionResult GetCartItems()
        {
            try
            {
                // Retrieve cus_ID from session
                int cus_ID = Convert.ToInt32(Session["cus_ID"]);

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        // Retrieve cart items for the current customer
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT p.prod_name, p.prod_img, c.Price, c.Quantity, c.Total " +
                                          "FROM CART c " +
                                          "INNER JOIN Products p ON c.prod_ID = p.prod_ID " +
                                          "WHERE c.cus_ID = @cus_ID";
                        cmd.Parameters.AddWithValue("@cus_ID", cus_ID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            // Generate HTML markup for cart items table rows
                            StringBuilder sb = new StringBuilder();
                            while (reader.Read())
                            {
                                int quantity = (int)reader["Quantity"];
                                decimal price = (decimal)reader["Price"];
                                decimal total = price * quantity;

                                string formattedPrice = price.ToString("#,##0.00");
                                string formattedTotal = total.ToString("#,##0.00");

                                sb.Append("<tr>");
                                sb.Append("<td>").Append(reader["prod_name"]).Append("</td>");
                                // Fetch the image filename
                                string filename = reader["prod_img"].ToString();
                                // Append the image tag with the src attribute pointing to the controller action
                                sb.Append("<td><img src='/Home/Image?filename=").Append(filename).Append("' alt='Product Image' width='100'></td>");
                                sb.Append("<td>").Append(formattedPrice).Append("</td>");

                                sb.Append("<td>");
                                sb.Append("<div class='input-group' style='width: fit-content;'>");
                                sb.Append("<div class='input-group-prepend'>");
                                sb.Append("<button class='btn btn-outline-primary btn-sm btn-decrement' type='button' onclick='decrementQuantity(this)'>-</button>");
                                sb.Append("</div>");
                                sb.Append("<input type='number' class='form-control form-control-sm input-quantity text-center quantityInput' value='").Append(quantity).Append("' min='1' style='width: 3em; padding: 0;' onkeydown='return false'>");
                                sb.Append("<div class='input-group-append'>");
                                sb.Append("<button class='btn btn-outline-primary btn-sm btn-increment' type='button' onclick='incrementQuantity(this)'>+</button>");
                                sb.Append("</div>");
                                sb.Append("</div>");
                                sb.Append("</td>");
                                sb.Append("<td>").Append(reader["Total"]).Append("</td>");
                                sb.Append("<td><button class='btn btn-danger btn-sm remove-btn'>Remove</button></td>");
                                sb.Append("</tr>");
                            }

                            return Content(sb.ToString(), "text/html");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
        [HttpPost]
        public ActionResult RemoveCartItem(string productName)
        {
            try
            {
                // Retrieve cus_ID from session
                int cus_ID = Convert.ToInt32(Session["cus_ID"]);

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        // Remove the item from the cart based on the product name
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM CART WHERE cus_ID = @cus_ID AND prod_ID IN (SELECT prod_ID FROM Products WHERE prod_name = @prod_name)";
                        cmd.Parameters.AddWithValue("@cus_ID", cus_ID);
                        cmd.Parameters.AddWithValue("@prod_name", productName);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Item successfully removed
                            return Json(new { success = true, message = "Item removed from cart successfully!" });
                        }
                        else
                        {
                            // Item not found in the cart
                            return Json(new { success = false, message = "Item not found in the cart." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to remove item from cart: " + ex.Message });
            }
        }





    }
}