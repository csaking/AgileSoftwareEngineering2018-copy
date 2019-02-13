using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agile_2018
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    username.Value = Request.Cookies["UserName"].Value;
                    password.Value = Request.Cookies["Password"].Value;
                    checkbox.Checked = true;
                }
            }

            password.Attributes["type"] = "password";
        }

        protected void LoginControl_Authenticate(object sender, EventArgs e)
        {
            bool authenticated = false;
            string uID = this.ValidateCredentials(username.Value.ToString(), password.Value.ToString());
            if (uID != null)
            {
                authenticated = true;
            }

            if (authenticated)
            {
                MySqlCommand cmd;
                ConnectionClass.OpenConnection();
                cmd = ConnectionClass.con.CreateCommand(); //New Connection object
                cmd.CommandText = "SELECT Position FROM logindetails WHERE UserID = @uID";
                // Populate SQl query values
                cmd.Parameters.AddWithValue("@uID", uID);
                // Execute Query
                MySqlDataReader reader = cmd.ExecuteReader();
                String pID = "";
                while (reader.Read())
                {
                    pID = reader.GetString("Position");
                }
                reader.Close();

                Session["uID"] = uID;
                Session["pID"] = pID;
                FormsAuthentication.RedirectFromLoginPage(username.Value.ToString(), false);

                if (checkbox.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["UserName"].Value = username.Value.ToString();
                Response.Cookies["Password"].Value = password.Value.ToString();

                Response.Redirect("AllProjects");
            }
            errorLabel.Text = "Invalid username or password.";
        }

        public bool IsAlphaNumeric(string text)
        {
            return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
        }

        private string ValidateCredentials(string userName, string password)
        {
            if (this.IsAlphaNumeric(userName) && userName.Length <= 50 && password.Length <= 50)
            {
                LoginClass lc = new LoginClass();
                return lc.ValidateLoginDetails(userName, password);
            }
            else
            {
                return null;
            }
        }
    }
}