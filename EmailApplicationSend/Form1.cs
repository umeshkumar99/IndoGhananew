using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;


namespace EmailApplicationSend
{
    public partial class frmEmail : Form
    {
        Emailaccount emailaccount = new Emailaccount();
        bool flag;
        public frmEmail()
        {
            InitializeComponent();
        }

        private void frmEmail_Load(object sender, EventArgs e)
        {
            
            label1.Width = 700;
            getemailaccountdata();
            label1.Text = "";
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
                    emailaccount.smtp = dr["smtp"].ToString();
                    emailaccount.smtpport = dr["smtpport"].ToString();
                    emailaccount.IsSecured = Convert.ToBoolean( dr["IsSecured"].ToString());



                }

            }
            if (conn.State == ConnectionState.Open)
                conn.Close();

        }

        private void getEmailMessageData()
        {
            flag = true;
            int MessageID = 0;
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["IndoGhanaConnectionString"].ToString();
                SqlDataReader dr;
                SqlConnection conn = new SqlConnection(connStr);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("usp_EmailMessagesGet", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlParameter para1 = new SqlParameter("branchid", 1);
                SqlParameter para2 = new SqlParameter("companyid", 1);
                cmd.Parameters.Add(para1);
                cmd.Parameters.Add(para2);
                 dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    SmtpClient SmtpServer = new SmtpClient(emailaccount.smtp);
                    SmtpServer.Port = Convert.ToInt32(emailaccount.smtpport);
                    SmtpServer.Credentials = new System.Net.NetworkCredential(emailaccount.emailid, emailaccount.password);
                    SmtpServer.EnableSsl = emailaccount.IsSecured;
                    while (dr.Read())
                    {
                        if (dr["EmailTo"].ToString() != null && dr["EmailTo"].ToString().Trim() != "")
                        {
                            MailMessage mail = new MailMessage();



                            System.Windows.Forms.Application.DoEvents();

                            label1.Text = "Processing Email Id:" + dr["EmailTo"].ToString();
                            mail.From = new MailAddress(emailaccount.emailid);
                            MessageID = Convert.ToInt32(dr["MessageID"].ToString());

                            if (dr["EmailTo"].ToString() != null && dr["EmailTo"].ToString().Trim() != "")
                            {
                                char[] semiSeparator = new char[] { ';' };
                                string[] strTo = dr["EmailTo"].ToString().Split(semiSeparator);
                                foreach (string result in strTo)
                                {
                                    mail.To.Add(result);
                                }
                            }


                            //   mail.To.Add(dr["EmailTo"].ToString());//;umesh_kumar1988@yahoo.com

                            if (dr["EmailCC"].ToString() != null && dr["EmailCC"].ToString().Trim() != "")
                            {
                                char[] semiSeparator = new char[] { ';' };
                                string[] strCC = dr["EmailCC"].ToString().Split(semiSeparator);
                                foreach (string result in strCC)
                                {
                                    mail.CC.Add(result);
                                }
                            }
                            if (dr["EmailBCC"].ToString() != null && dr["EmailBCC"].ToString().Trim() != "")
                            {

                                char[] semiSeparator = new char[] { ';' };
                                string[] strCC = dr["EmailBCC"].ToString().Split(semiSeparator);
                                foreach (string result in strCC)
                                {
                                    mail.Bcc.Add(result);

                                }

                            }

                            mail.Subject = dr["Subject"].ToString();
                            mail.Body = dr["MessageBody"].ToString();
                            
                            // Attachment attachment = new Attachment(filename);
                            // mail.Attachments.Add(attachment);
                            mail.IsBodyHtml = true;
                            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;


                            try
                            {
                                SmtpServer.Send(mail);
                            }
                            catch (Exception ex)
                            {
                                label1.Text = "Error occured while processing email:" + dr["EmailTo"].ToString();
                                EmailsentError(MessageID, ex.Message.ToString());
                                continue;
                            }
                            EmailSuccess(MessageID);



                        }
                    }

                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                btnStart.Enabled = true;
                flag = false;
                label1.Text = "All Emails processed";
            }
            catch (Exception ex)
            {
                EmailsentError(MessageID, ex.Message.ToString());
                flag = false;
               // continue;
                //throw ex;
            }

        }



        private void EmailsentError(int messageid,string msgError)
        {
            string connStr = ConfigurationManager.ConnectionStrings["IndoGhanaConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                
                
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("usp_EmailMessagesErrorUpdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlParameter para1 = new SqlParameter("MessageID", messageid);
                SqlParameter para2 = new SqlParameter("errorMsg", msgError);
                cmd.Parameters.Add(para1);
                cmd.Parameters.Add(para2);
                int result = cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();


            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                throw;
            }

        }

        private void EmailSuccess(int messageid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["IndoGhanaConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
            

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("usp_EmailMessagesSuccessUpdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlParameter para1 = new SqlParameter("MessageID", messageid);
                
                cmd.Parameters.Add(para1);
               
                int result = cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                throw;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag == false)
            {
                label1.Text = "Starting process";
                btnStart.Enabled = false;
           //     getemailaccountdata();
                getEmailMessageData();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            label1.Text = "Starting process";
            btnStart.Enabled = false;
            getemailaccountdata();
            getEmailMessageData();
           

        }

        private void label1_Resize(object sender, EventArgs e)
        {

        }
    }

    public class Emailaccount
    {

        public int EAccountID { get; set; }
        public string emailid { get; set; }
        public string password { get; set; }
        public string smtp { get; set; }
        public string smtpport { get; set; }
        public Boolean IsSecured { get; set; }
    }

}
