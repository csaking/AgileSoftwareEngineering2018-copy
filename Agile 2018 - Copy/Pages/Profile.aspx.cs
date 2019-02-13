using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace Agile_2018.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        //Declaring instance of ProfileManager to use the getUserInfo to return the current user's info into the thisProfile database. 
        ProfileManager pm = new ProfileManager();
        DataTable thisProfile = null;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // On load, the users information will be presented to them on the web form. 
                thisProfile = pm.getUserInfo(Convert.ToInt32(Session["uID"]));
                foreach (DataRow dr in thisProfile.Rows)
                {
                    username.Value = dr["StaffID"].ToString();
                    email.Value = dr["Email"].ToString();
                    firstname.Value = dr["Forename"].ToString();
                    lastname.Value = dr["Surname"].ToString();


                }
            }
            

        }

         protected void Save_Click(object sender, EventArgs e)
        {
            thisProfile = pm.getUserInfo(Convert.ToInt32(Session["uID"]));
            //Check if user is changing top half
            bool changingTop;
            object existingForename = thisProfile.Rows[0]["Forename"];
            object existingSurname = thisProfile.Rows[0]["Surname"];
            object existingEmail = thisProfile.Rows[0]["Email"];


            if ((firstname.Value != existingForename.ToString()) || (lastname.Value != existingSurname.ToString()) || (email.Value != existingEmail.ToString()))
            {
                changingTop = false;
            }
            else
            {
                changingTop = true;
            }

            //Check if current password is correct for this user.
            if (currentpassword.Value == thisProfile.Rows[0]["Pass"].ToString())
            {
                //Check if email value is valid
                if (IsValidEmail(email.Value) == true)
                {
                    //update forename, surname, email
                    pm.updateTop(Convert.ToInt32(Session["uID"]), firstname.Value, lastname.Value, email.Value);
                    if (newpassword.Value == null)
                    {
                        Response.Redirect("/2017-agile/team5/Pages/AllProjects");
                    }
                }
                else
                {
                    //update forename, surname, email (using previous email)
                    pm.updateTop(Convert.ToInt32(Session["uID"]), firstname.Value, lastname.Value, thisProfile.Rows[0].Field<string>(6));
                    errorLabel.Text = "Email entered is invalid. This must use the following format: 'johnsmyth@mail.com'.";

                }



                //Check if user wants to change password or not.
                if (newpassword.Value != "") //User wants to change password
                {
                    //If new password and confirm password are the same
                    if (newpassword.Value == confirmpassword.Value)
                    {
                        //update the 
                        pm.updateBot(Convert.ToInt32(Session["uID"]), Convert.ToString(newpassword.Value));
                        if (IsValidEmail(email.Value) == true)
                        {
                            Response.Redirect("/2017-agile/team5/Pages/AllProjects");
                        }

                        if(changingTop == false)
                        {
                            Response.Redirect("/2017-agile/team5/Pages/AllProjects");
                        }
                    }
                    else
                    {
                        errorLabel.Text = "New Password and Confirm Password fields must be equal.";
                    }
                }
            }
            //Else if password is not correct
            else
             {
                errorLabel.Text = "Current Password is not correct for this user profile.";

            }
            //Response.Redirect("/2017-agile/team5/Pages/AllProjects");

        }



        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}