using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agile_2018;
using MySql.Data.MySqlClient;

namespace Agile_2018.Tests
{
    [TestClass]
    public class AddComment
    {
        [TestMethod]
        public void CommentAddTest()
        {
            //pass through string
            string comment = "This is my comment";
            int projectID = 530;
            Project myComment = new Project();
            Assert.AreEqual(1, myComment.PostComment(comment,projectID));
        }

        [TestMethod]
        public void GetCommentTest()
        {
            //return expected string
            //string comes from default id
            string comment = "This is my comment";
            int projectID = 530;
            Project myComment = new Project();
            Assert.AreEqual(comment, myComment.GetComment(projectID));
        }
    }
}
