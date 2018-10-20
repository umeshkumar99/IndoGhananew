using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using OpenPop.Mime;
using OpenPop.Pop3;

namespace EmailApplicationSend
{
    public partial class frmEmailRecieve : Form
    {
        EmailPopUpaccount emailaccount = new EmailPopUpaccount();
        bool flag;
        public frmEmailRecieve()
        {
            InitializeComponent();
        }



        private void getemailaccountdata()
        {




            string connStr = ConfigurationManager.ConnectionStrings["IndoGhanaConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("usp_EmailAccountsGet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlParameter para1 = new SqlParameter("branchid", 1);
            SqlParameter para2 = new SqlParameter("companyid", 1);
            cmd.Parameters.Add(para1);
            cmd.Parameters.Add(para2);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    emailaccount.EAccountID = Convert.ToInt32(dr["EAccountID"].ToString());
                    emailaccount.emailid = dr["emailid"].ToString();
                    emailaccount.password = dr["password"].ToString();
                    emailaccount.POP3 = dr["pop3"].ToString();
                    emailaccount.POP3port = dr["pop3Port"].ToString();
                    emailaccount.IsSecured = Convert.ToBoolean(dr["IsSecured"].ToString());



                }

            }
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }


        private void InsertEmailMessages(string MessageNum,string Subject, DateTime DateSent, string MessageFrom, string EmailBody)
        {


            try {
                if (MessageNum == null) MessageNum = "NA";
                if (Subject == null) Subject = "No Subject ";
                if (EmailBody == null) EmailBody = "No Content";
                string connStr = ConfigurationManager.ConnectionStrings["IndoGhanaConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand cmd = new SqlCommand("usp_EmailIncomingMessagesInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
                
                    SqlParameter para1 = new SqlParameter("MessageNum", MessageNum);
                SqlParameter para2 = new SqlParameter("Subject", Subject);
            SqlParameter para3 = new SqlParameter("DateSent", DateSent);
            SqlParameter para4 = new SqlParameter("MessageFrom", MessageFrom);
            SqlParameter para5 = new SqlParameter("EmailBody", EmailBody);
            SqlParameter para6 = new SqlParameter("CreatedBy", 1);
            SqlParameter para7 = new SqlParameter("BranchID", 1);
            SqlParameter para8 = new SqlParameter("CompanyID", 1);
            cmd.Parameters.Add(para1);
            cmd.Parameters.Add(para2);
            cmd.Parameters.Add(para3);
            cmd.Parameters.Add(para4);
            cmd.Parameters.Add(para5);
            cmd.Parameters.Add(para6);
            cmd.Parameters.Add(para7);
                cmd.Parameters.Add(para8);
                int iRowCount = cmd.ExecuteNonQuery();
            //if (iRowCount > 0)
            //    MessageBox("");
            if (conn.State == ConnectionState.Open)
                conn.Close();
            }
            catch (Exception ex) { throw ex; }
        }
        private void frmEmailRecieve_Load(object sender, EventArgs e)
        {
            getemailaccountdata();
            ReadMail();
        }

        public void ReadMail()
        {


        try {

                Pop3Client pop3Client;

                            pop3Client = new Pop3Client();
                            pop3Client.Connect(emailaccount.POP3, Convert.ToInt32(emailaccount.POP3port), emailaccount.IsSecured);
                            pop3Client.Authenticate(emailaccount.emailid, emailaccount.password);

               // int MessageNum;
                            int count = pop3Client.GetMessageCount();
                            var Emails = new List<POPEmail>();
                            int counter = 0;
                            for (int i = count; i >= 1; i--)
                            {
                                OpenPop.Mime.Message message = pop3Client.GetMessage(i);
                                POPEmail email = new POPEmail()
                                {
                                   // MessageNumber = i,
                                    MessageNumber=  message.Headers.MessageId,
                                    //MessageNum=MessageNumber,
                                    Subject = message.Headers.Subject,
                                    DateSent = message.Headers.DateSent,
                                    From= message.Headers.From.Address,
                                    //From = string.Format("<a href = 'mailto:{1}'>{0}</a>", message.Headers.From.DisplayName, message.Headers.From.Address),
                                };
                                MessagePart body = message.FindFirstHtmlVersion();
                                if (body != null)
                                {
                                    email.Body = body.GetBodyAsText();
                                }
                                else
                                {
                                    body = message.FindFirstPlainTextVersion();
                                    if (body != null)
                                    {
                                        email.Body = body.GetBodyAsText();
                                    }
                                }
                                List<MessagePart> attachments = message.FindAllAttachments();

                                foreach (MessagePart attachment in attachments)
                                {
                                    email.Attachments.Add(new Attachment
                                    {
                                        FileName = attachment.FileName,
                                        ContentType = attachment.ContentType.MediaType,
                                        Content = attachment.Body
                                    });
                                }
                                InsertEmailMessages(email.MessageNumber, email.Subject, email.DateSent, email.From, email.Body);
                                Emails.Add(email);
                                counter++;
                                //if (counter > 2)
                                //{
                                //    break;
                                //}
                            }
                            var emails = Emails;
            }
            catch (Exception ex) { throw ex; }
        }
            
            }




}

    public class EmailPopUpaccount
    {

        public int EAccountID { get; set; }
        public string emailid { get; set; }
        public string password { get; set; }
        public string POP3 { get; set; }
        public string POP3port { get; set; }
        public Boolean IsSecured { get; set; }
    }

    
    public class POPEmail
    {
        public POPEmail()
        {
            this.Attachments = new List<Attachment>();
        }
        public string MessageNumber { get; set; }
        //[AllowHtml]
        public string From { get; set; }
        //[AllowHtml]
        public string Subject { get; set; }
        //[AllowHtml]
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        //[AllowHtml]
        public List<Attachment> Attachments { get; set; }
    }
  
    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }

