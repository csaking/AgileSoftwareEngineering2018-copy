using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agile_2018
{
    public partial class ViewProject : Page
    {
        private int numOfFiles;
        public int NumOfFiles { get { return numOfFiles; } }

        public int statusCode;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                this.Title = Session["Title"].ToString();
                ProjectManager pm = new ProjectManager();
                DataTable pn = pm.viewProjectInfo(Int32.Parse(Session["projectID"].ToString()));
                DataTable pf = pm.viewProjectFiles(Int32.Parse(Session["projectID"].ToString()));

                Files.DataSource = pf;
                Files.DataBind();

                numOfFiles = pf.Rows.Count;

                ProjectName.DataSource = pn;
                ProjectName.DataBind();

                comments.DataSource = pn;
                comments.DataBind();

                statusCode = Int32.Parse(pn.Rows[0]["StatusCode"].ToString());
            }
            catch (Exception) { Response.Redirect("Login"); }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    String fileName = Path.GetFileName(file.FileName);
                    DatabaseFileHandler dfh = new DatabaseFileHandler();
                    dfh.UploadFile(Int32.Parse(Session["projectID"].ToString()), file.InputStream, fileName);
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void Download_Click(object sender, EventArgs e)
        {
            string[] args = ((LinkButton)sender).CommandArgument.ToString().Split('|');
            DatabaseFileHandler dfh = new DatabaseFileHandler();
            byte[] blob = dfh.GetFile(Int32.Parse(args[0]));
            try
            {
                if (args[1].EndsWith(".xlxs") || args[1].EndsWith(".xls"))
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }
                else
                {
                    Response.ContentType = "application/octet-stream";
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + args[1] + "\"");
                Response.OutputStream.Write(blob, 0, blob.Length);
                Response.Flush();
            }
            catch (Exception) { }
        }

        public void Sign_Click(object sender, EventArgs e)
        {
            Project p = new Project();
            int id = Int32.Parse(Session["pID"].ToString());
            int projectID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
            int owner = p.GetProjectOwner(projectID);

            string projectName = p.GetProjectName(projectID);
            switch (id)
            {
                case 0:
                    p.ResearcherSign(projectID, Session["uID"].ToString());
                    HttpContext.Current.Response.Redirect(Request.RawUrl);
                    break;
                case 1:
                    p.RISSign(projectID, Session["uID"].ToString());
                    ComfirmationEmail(owner, "An RIS Staff Member", projectName);
                    HttpContext.Current.Response.Redirect("AllProjects");
                    break;
                case 2:
                    p.AssocDeanSign(projectID, Session["uID"].ToString());
                    ComfirmationEmail(owner, "The Associate Dean", projectName);
                    HttpContext.Current.Response.Redirect("AllProjects");
                    break;
                case 3:
                    p.DeanSign(projectID, Session["uID"].ToString());
                    CompleteEmail(owner, projectName);
                    CompleteEmail(p.GetRISSignID(projectID), projectName);
                    HttpContext.Current.Response.Redirect("AllProjects");
                    break;
                default:
                    break;
            }
        }

        protected void DeleteProject_Click(object sender, EventArgs e)
        {
            int projectID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
            Project p = new Project();
            p.DeleteProject(projectID);
            HttpContext.Current.Response.Redirect("AllProjects#");
        }

        protected void DeleteFile_Click(object sender, EventArgs e)
        {
            int fileID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
            DatabaseFileHandler dfh = new DatabaseFileHandler();
            dfh.DeleteFile(fileID);
            HttpContext.Current.Response.Redirect(Request.RawUrl);
        }

        protected void Reject_Click(object sender, EventArgs e)
        {
            Project p = new Project();
            int id = Int32.Parse(Session["pID"].ToString());
            int projectID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
            int owner = p.GetProjectOwner(projectID);
            
            string projectName = p.GetProjectName(projectID);
            switch (id)
            {
                case 0:
                    p.ResearcherReject(projectID);
                    HttpContext.Current.Response.Redirect(Request.RawUrl);
                    break;
                case 1:
                    p.RISReject(projectID);
                    RejectEmail(owner, "An RIS Staff Member", projectName);
                    break;
                case 2:
                    p.AssocDeanReject(projectID);
                    RejectEmail(owner, "The Associate Dean", projectName);
                    break;
                case 3:
                    p.DeanReject(projectID);
                    RejectEmail(owner, "The Dean", projectName);
                    break;
                default:
                    break;
            }
        }

        protected void RejectEmail(int owner, string who, string projectName)
        {
            AutomaticEmail ae = new AutomaticEmail();
            string email = ae.getUserEmail(owner);
            ae.SendEmail(email, "Project Rejected", who + " has rejected your project (" + projectName + ").");
        }

        protected void ComfirmationEmail(int owner, string who, string projectName)
        {
            AutomaticEmail ae = new AutomaticEmail();
            string email = ae.getUserEmail(owner);
            ae.SendEmail(email, "Project Confirmed", who + " has confirmed your project (" + projectName + ").");
        }

        protected void CompleteEmail(int owner, string projectName)
        {
            AutomaticEmail ae = new AutomaticEmail();
            string email = ae.getUserEmail(owner);
            ae.SendEmail(email, "Project Completed", projectName + " has been fully signed.");
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
        }

        protected void Post_Comments(object sender, EventArgs e)
        {
            int pID = Int32.Parse(Session["projectID"].ToString());
            string comment = inputComment.Text;
            Project newProject = new Project();
            newProject.PostComment(comment, pID);
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
        }
    }
}