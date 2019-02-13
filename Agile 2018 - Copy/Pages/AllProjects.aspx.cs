using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agile_2018
{
    public partial class _Default : Page
    {
        public DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConnectionClass.OpenConnection();
            ViewProjects vp = new ViewProjects();
            dt = vp.ViewAllProjects((string)Session["uID"]);
            dt.Columns.Add("TimeAgo", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                dr["TimeAgo"] = Agile_2018.Time.TimeAgo(DateTime.Parse(dr["DateCreated"].ToString()));
            }
            Projects.DataSource = dt;
            Projects.DataBind();
        }

        protected void ViewProject_Click(object sender, EventArgs e)
        {
            string[] args = ((LinkButton)sender).CommandArgument.ToString().Split(null);
            Session["ProjectID"] = args[0];
            Session["Title"] = args[1];
            Response.Redirect("ViewProject");
        }

        protected void NewProject_Click(object sender, EventArgs e)
        {
            if (projectName.Value.ToString() != "" && projectName.Value.ToString() != null && Request.Files.Count > 0)
            {
                Project p = new Project();
                String pID = p.CreateProject(projectName.Value.ToString(), Int32.Parse(Session["uID"].ToString()));
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        String fileName = Path.GetFileName(file.FileName);
                        DatabaseFileHandler dfh = new DatabaseFileHandler();
                        int i = dfh.UploadFile(Int32.Parse(pID), file.InputStream, fileName);
                        Project p1 = new Project();
                        p1.ResearcherSign(Int32.Parse(pID), Session["uID"].ToString());
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
        }

        protected void DeleteProject_Click(object sender, EventArgs e)
        {
            int projectID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
            Project p = new Project();
            p.DeleteProject(projectID);
            Response.Redirect(Request.RawUrl);
        }

        protected void Sign_Click(object sender, EventArgs e)
        {
            ViewProject vp = new ViewProject();
            vp.Sign_Click(sender, e);
        }
    }
}