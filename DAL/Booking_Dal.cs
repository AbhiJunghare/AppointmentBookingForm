using AppointmentForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AppointmentForm.DAL
{
    public class Booking_Dal
    {
        string conStr = ConfigurationManager.ConnectionStrings["Connectionstr"].ToString();
        public DataSet GetUsp(string usp)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = usp;
                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    con.Open();
                    dsAdapter.Fill(ds);
                    con.Close();

                }
            }
            catch (Exception ex)
            {

            }

            return ds;  
        }
        public List<Booking> GetList()
        {
            DataSet dataSet = new DataSet();
            List<Booking> list = new List<Booking>();
            try
            {
                dataSet = GetUsp("usp_patient_bk_list");
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    list.Add(new Booking
                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DateOfBirth = dr["DateOfBirth"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        MobileNo = dr["MobileNo"].ToString(), //Convert.ToInt32(dr["MobileNo"]),
                        DoctorName = dr["DoctorName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        AppointmentSlot = dr["AppointmentSlot"].ToString()

                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }
        public DataSet InsertData(Booking bk)
        {
            DataSet ds = new DataSet(); 
            int count = 0;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_insert_patient_info";
                cmd.Parameters.AddWithValue("@FirstName",bk.FirstName);
                cmd.Parameters.AddWithValue("@LastName", bk.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", bk.DateOfBirth);
                cmd.Parameters.AddWithValue("@EmailId", bk.EmailId);
                cmd.Parameters.AddWithValue("@MobileNo", bk.MobileNo);
                cmd.Parameters.AddWithValue("@Age", bk.Age);
                cmd.Parameters.AddWithValue("@DoctorName", bk.DoctorName);
                cmd.Parameters.AddWithValue("@Gender", bk.Gender);
                cmd.Parameters.AddWithValue("@AppointmentSlot", bk.AppointmentSlot);
                SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);

                con.Open();

                //count = cmd.ExecuteNonQuery();
                dsAdapter.Fill(ds);
                con.Close();
                return ds;
            }
        }
        public List<DoctorList> GetDoctorList() 
        {
            try
            {
               var list = new List<DoctorList>();
                
                DataSet ds = GetUsp("usp_drp_DrName");
                foreach (DataRow dr in ds.Tables[0].Rows) 
                {
                    list.Add(new DoctorList
                    {
                        Name = dr["Name"].ToString(),
                        Id = dr["Id"].ToString()
                    });

                }
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        
        
        }
        public List<AppointMentSlot> GetSlotTime(int slotid)
        {
            try
            {
                var list = new List<AppointMentSlot>();

                DataSet ds = new DataSet();

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_appointment_slottime";
                    cmd.Parameters.AddWithValue("@slotid", slotid);

                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);

                    con.Open();
                    dsAdapter.Fill(ds);
                    con.Close();

                }

               // DataSet ds = GetUsp("usp_appointment_slottime");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new AppointMentSlot
                    {
                        slot = dr[0].ToString(),
                        slotid = dr[0].ToString()
                    });

                }
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<Booking> GetListById(int id)
        {
            try
            {
                DataSet ds = new DataSet();

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Get_Patientinfoby_id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);

                    con.Open();
                    dsAdapter.Fill(ds);
                    con.Close();
                }
                List<Booking> list = new List<Booking>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new Booking
                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DateOfBirth = dr["DateOfBirth"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        MobileNo = dr["MobileNo"].ToString(), //Convert.ToInt32(dr["MobileNo"]),
                        DoctorName = dr["DoctorName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        AppointmentSlot = dr["AppointmentSlot"].ToString()

                    });
                }
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataSet DeleteById(int id )
        {
            try
            {
                DataSet ds = new DataSet();

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_delete_patient_info";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);

                    con.Open();
                    dsAdapter.Fill(ds);
                    con.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}