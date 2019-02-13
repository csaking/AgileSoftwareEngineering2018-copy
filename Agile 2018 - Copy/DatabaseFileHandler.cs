using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Agile_2018
{
    public class DatabaseFileHandler
    {
        /// <summary>
        /// Deletes any existing files in the project with the same name then reads the bytes from the pasted stream and inputs them into the database. 
        /// </summary>
        /// <param name="id">Project ID that the file belongs to</param>
        /// <param name="stream">Stream of the file you are uploading</param>
        /// <param name="fileName">File name to display to the user</param>
        /// <returns>Number of rows effected</returns>
        public int UploadFile(int id, Stream stream, string fileName)
        {
            //Convert file to bytes
            byte[] file;
            using (var reader = new BinaryReader(stream))
            {
                file = reader.ReadBytes((int)stream.Length);
            }

            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("deleteFileWhereProjectIDAndFileName", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new MySqlParameter("?id", id));
            comm.Parameters.Add(new MySqlParameter("?fileName", fileName));
            comm.ExecuteNonQuery();

            //Insert bytes into the storedfiles table
            comm = new MySqlCommand("createNewFile", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("?id", id);
            comm.Parameters.AddWithValue("?fileName", fileName);
            comm.Parameters.Add("?fileData", MySqlDbType.LongBlob, file.Length).Value = file;
            int i = comm.ExecuteNonQuery();
            ConnectionClass.CloseConnection();

            return i;
        }

        /// <summary>
        /// Takes a file primary key (fileID) and deletes all rows from storedfiles with that key.
        /// </summary>
        /// <param name="fileID">Database primary key that you want to delete</param>
        /// <returns>Number of rows effected</returns>
        public int DeleteFile(int fileID)
        {
            //Deletes all rows with primary key = fileID
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("deleteFile", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new MySqlParameter("?id", fileID));
            int i = comm.ExecuteNonQuery();
            ConnectionClass.CloseConnection();

            return i;
        }

        /// <summary>
        /// Downloads all files with the passed project ID into the path location passed to it
        /// </summary>
        /// <param name="id">Project ID to fetch files from</param>
        /// <param name="path">Path to download folder</param>
        /// <returns>List of paths to the files downloaded</returns>
        public List<String> DownloadAllFiles(int id, string path)
        {
            List<String> fileList = new List<string>();
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("selectAllFilesWithProjectID", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
            {
                if (sqlQueryResult.HasRows)
                {
                    //Loop for all files
                    while (sqlQueryResult != null && sqlQueryResult.Read())
                    {
                        byte[] blob = new Byte[(sqlQueryResult.GetBytes(sqlQueryResult.GetOrdinal("FileData"), 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(sqlQueryResult.GetOrdinal("FileData"), 0, blob, 0, blob.Length);
                        
                        //Manage file name duplication filename(count).filetype
                        String fileName = sqlQueryResult["FileName"].ToString();
                        String fullPath = System.IO.Path.Combine(path, fileName);
                        int count = 1;
                        while (File.Exists(fullPath))
                        {
                            string[] split = fileName.Split('.');
                            fullPath = System.IO.Path.Combine(path, split[0] + "(" + count + ")." + split[1]);
                            count++;
                        }

                        using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(blob, 0, blob.Length);
                            fileList.Add(fullPath);
                        }
                    }
                }
            }
            ConnectionClass.CloseConnection();
            return fileList;
        }

        /// <summary>
        /// Downloads file with given file ID
        /// </summary>
        /// <param name="id">File ID to fetch files from</param>
        /// <param name="path">Path to download folder</param>
        /// <returns>List of paths to the file downloaded</returns>
        public List<String> DownloadFile(int id, string path)
        {
            List<String> fileList = new List<string>();
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("selectFileWithFileID", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
            {
                if (sqlQueryResult.HasRows)
                {
                    //Loop for all files
                    while (sqlQueryResult != null && sqlQueryResult.Read())
                    {
                        byte[] blob = new Byte[(sqlQueryResult.GetBytes(sqlQueryResult.GetOrdinal("FileData"), 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(sqlQueryResult.GetOrdinal("FileData"), 0, blob, 0, blob.Length);

                        //Manage file name duplication filename(count).filetype
                        String fileName = sqlQueryResult["FileName"].ToString();
                        String fullPath = System.IO.Path.Combine(path, fileName);
                        int count = 1;
                        while (File.Exists(fullPath))
                        {
                            string[] split = fileName.Split('.');
                            fullPath = System.IO.Path.Combine(path, split[0] + "(" + count + ")." + split[1]);
                            count++;
                        }

                        using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(blob, 0, blob.Length);
                            fileList.Add(fullPath);
                        }
                    }
                }
            }
            ConnectionClass.CloseConnection();
            return fileList;
        }

        public byte[] GetFile(int id)
        {
            ConnectionClass.OpenConnection();
            MySqlCommand comm = new MySqlCommand("selectFileWithFileID", ConnectionClass.con);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader sqlQueryResult = comm.ExecuteReader())
            {
                if (sqlQueryResult.HasRows)
                {
                    //Loop for all files
                    while (sqlQueryResult != null && sqlQueryResult.Read())
                    {
                        byte[] blob = new Byte[(sqlQueryResult.GetBytes(sqlQueryResult.GetOrdinal("FileData"), 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(sqlQueryResult.GetOrdinal("FileData"), 0, blob, 0, blob.Length);
                        ConnectionClass.CloseConnection();
                        return blob;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Opens a file dialog allowing the user to select a file.
        /// </summary>
        /// <returns>Path to selected file</returns>
        public String SelectFile()
        {
            String path = "";
            Stream myStream = null;
            DatabaseFileHandler dfh = new DatabaseFileHandler();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            path = openFileDialog1.FileName;
                        }
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    MessageBox.Show("Could not read file from disk", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return path;
        }
    }
}