using benhxaCA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace benhxaCA.Database
{
    public class DatabaseHelper
    {
        //ConnectString
        private string serverName = @"DESKTOP-1A4L33T\SQLEXPRESS";
        private string uid = "mvchuong";
        private string pwd = "371850899";
        private string DBName = "benhxaCA";
        private SqlConnection conn;
        //Mở kết nối đến database
        public bool OpenConnection()
        {
            string cString = $"server={serverName};Uid={uid};Pwd={pwd};Database={DBName}";
            try
            {
                conn = new SqlConnection(cString);
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Đóng kết nối đến database
        public bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------SELECT--------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //Lấy danh sách tên đơn vị
        public List<string> Get_donvi()
        {
            try
            {
                if (OpenConnection())
                {
                    //Gọi Procedure trong SQLServer
                    SqlCommand cmd = new SqlCommand("GET_TENDONVI", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader r = cmd.ExecuteReader();
                    // Tạo list để lấy danh sách dữ liệu ra
                    List<string> dv = new List<string>();
                    while (r.Read())
                    {
                        dv.Add(r[0].ToString());
                    }
                    CloseConnection(); // Đóng kết nối với SQL Server
                    return dv;
                }
                else
                {
                    //Không có dữ liệu thì trả về sanh sách rỗng
                    return new List<string>();
                }
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Không lấy được đơn vị");
            }


        }
        //Lấy mã đơn vị từ tên đơn vị
        public string Get_madonvi(string tendv)
        {
            if (OpenConnection() && !tendv.Equals(string.Empty))
            {
                //Gọi Procedure trong SQLServer
                SqlCommand cmd = new SqlCommand("GET_MADONVI", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ten",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tendv
                    }
                );
                SqlDataReader r1 = cmd.ExecuteReader();
                string ma = "";
                while (r1.Read())
                {
                    ma = r1[0].ToString();
                }
                CloseConnection(); // Đóng kết nối với SQL Server
                return ma;
            }
            else
            {
                //Không có dữ liệu thì trả về sanh sách rỗng
                return null;
            }

        }
        //Lấy danh sách đợt khám sức khỏe
        public List<dotkhamsuckhoe> Get_dotkhamsuckhoe()
        {
            if (OpenConnection())
            {
                //Gọi Proc trong SQLServer
                SqlCommand cmd = new SqlCommand("GET_DOTKHAMSUCKHOE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader r = cmd.ExecuteReader();
                List<dotkhamsuckhoe> dv = new List<dotkhamsuckhoe>();
                while (r.Read())
                {
                    dotkhamsuckhoe dksk = new dotkhamsuckhoe();

                    dksk.dksk_stt = (int)r[0];
                    dksk.dksk_ngaykham = r[1].ToString();
                    dksk.dksk_madv = r[2].ToString();
                    dksk.dksk_loaikham = r[3].ToString();
                    dksk.dksk_macb = r[4].ToString();
                    dksk.dksk_ghichu = r[5].ToString();

                    dv.Add(dksk);
                }
                CloseConnection(); // Đóng kết nối với SQL Server
                return dv;
            }
            else
            {
                //Không có dữ liệu thì trả về sanh sách rỗng
                return new List<dotkhamsuckhoe>();
            }

        }
        //Lay danh sach can bo da kham
        public List<thongtincanbo> Get_dscb_dakham(string ngaydotkham, string dv)
        {
            List<thongtincanbo> dscb = new List<thongtincanbo>();
            try
            {
                //Tạo list chứa danh sách mã cán bộ đã khám từ Get_danhsachmacanbodakham(ngaydotkham, dv)
                List<string> listmacb = Get_danhsachmacanbodakham(ngaydotkham, dv);
                if (listmacb.Count > 0)
                {
                    foreach (string idcb in listmacb)
                    {
                        OpenConnection(); // //Mở kết nối với SQL Server
                        //Gọi Procedure trong SQLServer
                        SqlCommand cmd = new SqlCommand("GET_DSCANBODAKHAM", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(
                            new SqlParameter()
                            {
                                ParameterName = "@macb",
                                SqlDbType = SqlDbType.NVarChar,
                                Value = idcb
                            }
                        );
                        cmd.Parameters.Add(
                            new SqlParameter()
                            {
                                ParameterName = "@ngaykham",
                                SqlDbType = SqlDbType.NVarChar,
                                Value = ngaydotkham
                            }
                        );
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            thongtincanbo ttcb = new thongtincanbo();
                            ttcb.ttcb_id = r[0].ToString();
                            ttcb.ttcb_madv = r[1].ToString();
                            ttcb.ttcb_macv = r[2].ToString();
                            ttcb.ttcb_macb = r[3].ToString();
                            ttcb.ttcb_hoten = r[4].ToString();
                            ttcb.ttcb_gioitinh = r[5].ToString();
                            ttcb.ttcb_ngaysinh = r[6].ToString();
                            ttcb.ttcb_cmnd_or_cccd = r[7].ToString();
                            ttcb.ttcb_hokhauthuongtru = r[8].ToString();
                            ttcb.ttcb_choohiennay = r[9].ToString();
                            ttcb.ttcb_dantoc = r[10].ToString();
                            ttcb.ttcb_sodienthoai = r[11].ToString();
                            ttcb.ttcb_hinhanh = r[12].ToString();
                            dscb.Add(ttcb);
                        }
                    }
                    CloseConnection(); // Đóng kết nối với SQL Server
                }
                else
                {
                    //Không có dữ liệu thì trả về danh sách rỗng
                    return new List<thongtincanbo>();
                }
            }
            catch (Exception)
            {
                throw new NullReferenceException("Không có dữ liệu");
            }
            return dscb;

        }
        //Lay danh sach can bo cho kham
        public List<thongtincanbo> Get_dscb_chokham(string ngaydotkham, string dv)
        {
            string ma = Get_madonvi(dv); //Lấy mã đơn vị từ tên đơn vị được gửi từ Controller sang
            List<thongtincanbo> cb = new List<thongtincanbo>(); // Tạo list để lấy danh sách dữ liệu ra
            bool check = true; 
            if (Get_danhsachmacanbodakham(ngaydotkham, dv).Count > 0) check = false;
            if (ngaydotkham.Equals(string.Empty)) check = false;
            if (Get_stt_dksk(ma, ngaydotkham).Equals(string.Empty)) check = false;
            try
            {
                if (check)
                {
                    OpenConnection(); // //Mở kết nối với SQL Server //Mở kết nối với SQL Server
                    //Gọi Procedure trong SQLServer
                    SqlCommand cmd = new SqlCommand("GET_DSCANBOCHOKHAM", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                        new SqlParameter()
                        {
                            ParameterName = "@madv",
                            SqlDbType = SqlDbType.NVarChar,
                            Value = ma
                        }
                    );
                    cmd.Parameters.Add(
                       new SqlParameter()
                       {
                           ParameterName = "@ngaykham",
                           SqlDbType = SqlDbType.NVarChar,
                           Value = ngaydotkham
                       }
                   );
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        thongtincanbo ttcb = new thongtincanbo();
                        ttcb.ttcb_id = r[0].ToString();
                        ttcb.ttcb_madv = r[1].ToString();
                        ttcb.ttcb_macv = r[2].ToString();
                        ttcb.ttcb_macb = r[3].ToString();
                        ttcb.ttcb_hoten = r[4].ToString();
                        ttcb.ttcb_gioitinh = r[5].ToString();
                        ttcb.ttcb_ngaysinh = r[6].ToString();
                        ttcb.ttcb_cmnd_or_cccd = r[7].ToString();
                        ttcb.ttcb_hokhauthuongtru = r[8].ToString();
                        ttcb.ttcb_choohiennay = r[9].ToString();
                        ttcb.ttcb_dantoc = r[10].ToString();
                        ttcb.ttcb_sodienthoai = r[11].ToString();
                        ttcb.ttcb_hinhanh = r[12].ToString();
                        cb.Add(ttcb);
                    }
                    CloseConnection(); // Đóng kết nối với SQL Server
                }
                else
                {
                    return new List<thongtincanbo>();
                }
            }
            catch (Exception)
            {
                throw new System.NullReferenceException("Không có dữ liệu");
            }
            return cb;
        }
        //Lay ma danh sach can bo da kham
        public List<string> Get_danhsachmacanbodakham(string ngaydotkham, string tendonvi)
        {
            OpenConnection(); // //Mở kết nối với SQL Server
            bool check = true;
            string sttdk = "";
            List<string> macb = new List<string>();
            if (Get_madonvi(tendonvi).Equals(string.Empty))
            {
                check = false;
            }
            else
            {
                string ma = Get_madonvi(tendonvi);
                sttdk = Get_stt_dksk(ma, ngaydotkham);
                if (sttdk.Equals(string.Empty)) check = false;
                if (Get_dotkhamsuckhoe_by(ngaydotkham, ma).Count() == 0) check = false;
                if (ngaydotkham.Equals(string.Empty)) check = false;
            }
            if (check)
            {
                OpenConnection(); // //Mở kết nối với SQL Server
                SqlCommand cmd = new SqlCommand("GET_DSMACANBODAKHAM", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@stt",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = sttdk
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ngaykham",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = ngaydotkham
                    }
                );
                SqlDataReader r1 = cmd.ExecuteReader();

                while (r1.Read())
                {
                    macb.Add(r1[0].ToString());
                }
                CloseConnection(); // Đóng kết nối với SQL Server
                return macb;
            }
            else
            {
                return new List<string>(); //Không có dữ liệu thì trả về danh sách rỗng
            }
        }
        //Lấy ra thông tin khám sức khỏe tổng hợp của cán bộ
        public DataSet Get_tong_hop(string macb)
        {

            if (OpenConnection() && !macb.Equals(string.Empty))
            {
                DataSet ds = new DataSet(); //Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_TONGHOP", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@macb",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = macb
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet(); //Không có dữ liệu thì trả về danh sách rỗng
            }
        }
        //Lấy ra danh sách thông tin cán bộ khám sức khỏe tự phát ĐÃ KHÁM theo ngày
        public List<thongtincanbo> Get_dscb_dakham_tuphat(string ngaydotkham) 
        {
            List<thongtincanbo> cb = new List<thongtincanbo>(); // Tạo list để lấy danh sách dữ liệu ra
            bool check = true;
            if (ngaydotkham.Equals(string.Empty)) check = false;
            try
            {
                if (check)
                {
                    OpenConnection(); // //Mở kết nối với SQL Server
                    SqlCommand cmd = new SqlCommand("GET_DSCANBODAKHAM_TUPHAT", conn); //Gọi Procedure trong SQLServer
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                       new SqlParameter()
                       {
                           ParameterName = "@ngaykham",
                           SqlDbType = SqlDbType.NVarChar,
                           Value = ngaydotkham
                       }
                   );
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        thongtincanbo ttcb = new thongtincanbo(); // //Tạo object để chứa từng Row dữ liệu khi foreach
                        ttcb.ttcb_id = r[0].ToString();
                        ttcb.ttcb_madv = r[1].ToString();
                        ttcb.ttcb_macv = r[2].ToString();
                        ttcb.ttcb_macb = r[3].ToString();
                        ttcb.ttcb_hoten = r[4].ToString();
                        ttcb.ttcb_gioitinh = r[5].ToString();
                        ttcb.ttcb_ngaysinh = r[6].ToString();
                        ttcb.ttcb_cmnd_or_cccd = r[7].ToString();
                        ttcb.ttcb_hokhauthuongtru = r[8].ToString();
                        ttcb.ttcb_choohiennay = r[9].ToString();
                        ttcb.ttcb_dantoc = r[10].ToString();
                        ttcb.ttcb_sodienthoai = r[11].ToString();
                        ttcb.ttcb_hinhanh = r[12].ToString();
                        cb.Add(ttcb); //Thêm dữ liệu vào danh sách cb từ object
                    }
                    CloseConnection(); // Đóng kết nối với SQL Server
                }
                else
                {
                    return new List<thongtincanbo>(); //Không có dữ liệu thì trả về danh sách rỗng
                }
            }
            catch (Exception)
            {
                throw new System.NullReferenceException("Không có dữ liệu");
            }
            return cb;

        }
        //Lấy ra danh sách thông tin cán bộ khám sức khỏe tự phát CHỜ KHÁM theo ngày
        public List<thongtincanbo> Get_dscb_chokham_tuphat(string ngaydotkham)
        {
            List<thongtincanbo> cb = new List<thongtincanbo>(); // Tạo list để lấy danh sách dữ liệu ra
            bool check = true;
            if (ngaydotkham.Equals(string.Empty)) check = false;
            if (Get_dscb_dakham_tuphat(ngaydotkham).Count != 0) check = false;
            try
            {
                if (check)
                {
                    OpenConnection(); // //Mở kết nối với SQL Server
                    SqlCommand cmd = new SqlCommand("GET_DSCANBOCHOKHAM_TUPHAT", conn); //Gọi Procedure trong SQLServer
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                       new SqlParameter()
                       {
                           ParameterName = "@ngaykham",
                           SqlDbType = SqlDbType.NVarChar,
                           Value = ngaydotkham
                       }
                   );
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        thongtincanbo ttcb = new thongtincanbo(); //Tạo object để chứa từng Row dữ liệu khi foreach
                        ttcb.ttcb_id = r[0].ToString();
                        ttcb.ttcb_madv = r[1].ToString();
                        ttcb.ttcb_macv = r[2].ToString();
                        ttcb.ttcb_macb = r[3].ToString();
                        ttcb.ttcb_hoten = r[4].ToString();
                        ttcb.ttcb_gioitinh = r[5].ToString();
                        ttcb.ttcb_ngaysinh = r[6].ToString();
                        ttcb.ttcb_cmnd_or_cccd = r[7].ToString();
                        ttcb.ttcb_hokhauthuongtru = r[8].ToString();
                        ttcb.ttcb_choohiennay = r[9].ToString();
                        ttcb.ttcb_dantoc = r[10].ToString();
                        ttcb.ttcb_sodienthoai = r[11].ToString();
                        ttcb.ttcb_hinhanh = r[12].ToString();
                        cb.Add(ttcb); //Thêm oject vào list cb
                    }
                    CloseConnection(); // Đóng kết nối với SQL Server
                }
                else
                {
                    return new List<thongtincanbo>(); //Không có dữ liệu thì trả về danh sách rỗng
                }
            }
            catch (Exception)
            {
                throw new System.NullReferenceException("Không có dữ liệu");
            }
            return cb;

        }
        //Lấy ra toàn bộ thông tin của cán bộ với tham số là mã cán bộ
        public DataSet Get_thong_tin_cb(string macb)
        {
            if (OpenConnection() && !macb.Equals(string.Empty))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("GET_THONGTINCANBO", conn);  //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@macb",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = macb
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds); //Đổ dữ liệu vào DataSet ds
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet(); //Không có dữ liệu thì trả về danh sách rỗng
            }


        }
        //Lấy ra thông tin đợt khám với tham số là mã đơn vị và ngày khám
        public List<dotkhamsuckhoe> Get_dotkhamsuckhoe_by(string ngaykham, string madv)
        {
            bool check = true;
            if (ngaykham.Equals(string.Empty)) check = false;
            if (madv.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                SqlCommand cmd = new SqlCommand("GET_DOTKHAMSUCKHOE_BY", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@madv",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = madv
                    }
                );
                cmd.Parameters.Add(
                     new SqlParameter()
                     {
                         ParameterName = "@ngaykham",
                         SqlDbType = SqlDbType.NVarChar,
                         Value = ngaykham
                     }
                );
                SqlDataReader r = cmd.ExecuteReader();
                List<dotkhamsuckhoe> dv = new List<dotkhamsuckhoe>(); // Tạo list để lấy danh sách dữ liệu ra
                while (r.Read())
                {
                    dotkhamsuckhoe dksk = new dotkhamsuckhoe(); //Tạo object để chứa từng Row dữ liệu khi foreach

                    dksk.dksk_stt = (int)r[0];
                    dksk.dksk_ngaykham = r[1].ToString();
                    dksk.dksk_madv = r[2].ToString();
                    dksk.dksk_loaikham = r[3].ToString();
                    dksk.dksk_macb = r[4].ToString();
                    dksk.dksk_ghichu = r[5].ToString();
                    dv.Add(dksk); //Thêm object vào danh sách dv
                }
                CloseConnection(); // Đóng kết nối với SQL Server
                return dv;
            }
            else
            {
                return new List<dotkhamsuckhoe>(); //Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        //Lấy ra số mã đợt khám từ tham só mã đơn vị và ngày khám 
        public string Get_stt_dksk(string madv, string ngaykham)
        {
            bool check = true;
            if (ngaykham.Equals(string.Empty)) check = false;
            if (madv.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                SqlCommand cmd = new SqlCommand("GET_STT_DKSK", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@madv",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = madv
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ngaykham",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = ngaykham
                    }
                );
                SqlDataReader r1 = cmd.ExecuteReader();
                string stt = "";
                while (r1.Read())
                {
                    stt = r1[0].ToString();
                }
                CloseConnection(); // Đóng kết nối với SQL Server
                return stt;
            }
            else
            {
                return ""; //Không có dữ liệu thì trả về danh sách rỗng
            }


        }
        //Lấy dữ liệu khám sưc khỏe tổng hợp từ với lời gọi từ Controller : baocaokhamsuckhoeController
        public DataSet Get_tong_hop_ksk(string tungay, string denngay)
        {
            bool check = true;
            if (tungay.Equals(string.Empty)) check = false;
            if (denngay.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                DataSet ds = new DataSet(); //Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_TONGHOPKSK", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tungay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tungay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@denngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = denngay
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds); //Đổ dữ liệu vào DataSet
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet(); //Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        //Lấy dữ liệu báo cáo khám sức khỏe theo loại
        public DataSet Get_tonghopphanloai_ksk(string tungay, string denngay)
        {
            bool check = true;
            if (tungay.Equals(string.Empty)) check = false;
            if (denngay.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                DataSet ds = new DataSet(); //Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_TONGHOP_PHANLOAI_KSK", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tungay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tungay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@denngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = denngay
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);//Đổ dữ liệu vào DataSet
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet(); //Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        //Lấy dữ liệu báo cáo khám sức khỏe định kỳ
        public DataSet Get_baocaoksk_theodot_dky(string tungay, string denngay)
        {
            bool check = true;
            if (tungay.Equals(string.Empty)) check = false;
            if (denngay.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                DataSet ds = new DataSet();//Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_KSK_THEODOT_DINHKY", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tungay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tungay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@denngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = denngay
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);//Đổ dữ liệu vào DataSet
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet(); //Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        //Lấy dữ liệu báo cáo khám sức khỏe tự phát
        public DataSet Get_baocaoksk_theodot_tuphat(string tungay, string denngay)
        {
            bool check = true;
            if (tungay.Equals(string.Empty)) check = false;
            if (denngay.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                DataSet ds = new DataSet(); //Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_KSK_THEODOT_TUPHAT", conn);//Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tungay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tungay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@denngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = denngay
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);//Đổ dữ liệu vào DataSet
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet();//Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        //Lấy dữ liệu báo cáo theo loại khám
        public DataSet Get_baocaoksk_theoloaikham(string tungay, string denngay)
        {
            bool check = true;
            if (tungay.Equals(string.Empty)) check = false;
            if (denngay.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                DataSet ds = new DataSet();//Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_TONGHOP_LOAIKHAM", conn);//Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tungay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tungay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@denngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = denngay
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);//Đổ dữ liệu vào DataSet
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet();//Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        //Lấy dữ liệu báo cáo khám sức khỏe theo từng đơn vị
        public DataSet Get_baocaoksk_theodonvi(string tungay, string denngay, string donvi)
        {
            bool check = true;
            if (tungay.Equals(string.Empty)) check = false;
            if (denngay.Equals(string.Empty)) check = false;
            if (donvi.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                DataSet ds = new DataSet();//Tạo DataSet chứa dữ liệu
                SqlCommand cmd = new SqlCommand("GET_TONGHOP_THEODONVI", conn);//Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tungay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tungay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@denngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = denngay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@donvi",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = donvi
                    }
                );
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);//Đổ dữ liệu vào DataSet
                CloseConnection(); // Đóng kết nối với SQL Server
                return ds;
            }
            else
            {
                return new DataSet();//Không có dữ liệu thì trả về danh sách rỗng
            }

        }
        public List<thongtincanbo> get_canbo(string madv)
        {
            List<thongtincanbo> cb = new List<thongtincanbo>();// Tạo list để lấy danh sách dữ liệu ra
            try
            {
                if (!madv.Equals(string.Empty))
                {
                    OpenConnection(); // //Mở kết nối với SQL Server
                    SqlCommand cmd = new SqlCommand("GET_DSCANBO_BYMADV", conn);//Gọi Procedure trong SQLServer
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                       new SqlParameter()
                       {
                           ParameterName = "@dv",
                           SqlDbType = SqlDbType.NVarChar,
                           Value = madv
                       }
                   );
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        thongtincanbo ttcb = new thongtincanbo(); //Tạo object để chứa từng Row dữ liệu khi foreach
                        ttcb.ttcb_id = r[0].ToString();
                        ttcb.ttcb_madv = r[1].ToString();
                        ttcb.ttcb_macv = r[2].ToString();
                        ttcb.ttcb_macb = r[3].ToString();
                        ttcb.ttcb_hoten = r[4].ToString();
                        ttcb.ttcb_gioitinh = r[5].ToString();
                        ttcb.ttcb_ngaysinh = r[6].ToString();
                        ttcb.ttcb_cmnd_or_cccd = r[7].ToString();
                        ttcb.ttcb_hokhauthuongtru = r[8].ToString();
                        ttcb.ttcb_choohiennay = r[9].ToString();
                        ttcb.ttcb_dantoc = r[10].ToString();
                        ttcb.ttcb_sodienthoai = r[11].ToString();
                        ttcb.ttcb_hinhanh = r[12].ToString();
                        cb.Add(ttcb); //Thêm oject vào list cb
                    }
                    CloseConnection(); // Đóng kết nối với SQL Server
                }
                else
                {
                    return new List<thongtincanbo>(); //Không có dữ liệu thì trả về danh sách rỗng
                }
            }
            catch (Exception)
            {
                throw new System.NullReferenceException("Không có dữ liệu");
            }
            return cb;
        }
        //Lấy mã cán bộ từ tên cán bộ
        public string Get_macb(string tencb)
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("GET_MACANBO_BYTENCB", conn);//Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ten",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = tencb
                    }
                );
                SqlDataReader r1 = cmd.ExecuteReader();
                string stt = "";
                while (r1.Read())
                {
                    stt = r1[0].ToString();
                }
                CloseConnection(); // Đóng kết nối với SQL Server
                return stt;
            }
            else
            {
                return "";//Không có dữ liệu thì trả về danh sách rỗng
            }


        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------INSERT----------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        //Thêm đợt khám sức khỏe định kỳ
        public void Insert_dotkhamsuckhoe_dinhky(string tendonvi, string ngay, string loai, string ghichu)
        {
            bool check = true;
            string ma = "";
            if (tendonvi.Equals(string.Empty))
            { check = false; }
            else
            { ma = Get_madonvi(tendonvi); } //Lấy mã đơn vị khi tên đơn vị khác rỗng
            if (ngay.Equals(string.Empty)) check = false;
            if (loai.Equals(string.Empty)) check = false;
            if (OpenConnection() && check)
            {
                SqlCommand cmd = new SqlCommand("SET_DOTKHAMSUCKHOE_DINHKY", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = ngay
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ma",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = ma
                    }
                );
                cmd.Parameters.Add(
                   new SqlParameter()
                   {
                       ParameterName = "@loai",
                       SqlDbType = SqlDbType.NVarChar,
                       Value = loai
                   }
               );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ghichu",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = ghichu
                    }
                );
                cmd.ExecuteNonQuery();
                CloseConnection(); // Đóng kết nối với SQL Server
            }
            else
            {
                throw new System.ArgumentException("Không thể thêm đợt khám sức khỏe");
            }

        }
        //Thêm đợt khám sức khỏe tự phát
        public void Insert_dotkhamsuckhoe_tuphat(string tendonvi, string ngaykham, string loaikham, string idcanbo, string note)
        {
            bool check = true;
            string madv = "";
            if (tendonvi.Equals(string.Empty))
            { check = false; }
            else
            { madv = Get_madonvi(tendonvi); }//Lấy mã đơn vị khi tên đơn vị khác rỗng
            if (ngaykham.Equals(string.Empty)) check = false;
            if (loaikham.Equals(string.Empty)) check = false;
            if (idcanbo.Equals(string.Empty)) check = false;
            if (OpenConnection() && check) //Mở kết nối với SQL Server và kiểm tra biến check 
            {
                SqlCommand cmd = new SqlCommand("SET_DOTKHAMSUCKHOE_TUPHAT", conn); //Gọi Procedure trong SQLServer
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ngay",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = ngaykham
                    }
                );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ma",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = madv
                    }
                );
                cmd.Parameters.Add(
                   new SqlParameter()
                   {
                       ParameterName = "@loai",
                       SqlDbType = SqlDbType.NVarChar,
                       Value = loaikham
                   }
               );
                cmd.Parameters.Add(
                   new SqlParameter()
                   {
                       ParameterName = "@macb",
                       SqlDbType = SqlDbType.NVarChar,
                       Value = idcanbo
                   }
               );
                cmd.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@ghichu",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = note
                    }
                );
                cmd.ExecuteNonQuery();
                CloseConnection(); // Đóng kết nối với SQL Server
            }
            else
            {
                throw new System.ArgumentException("Không thể thêm đợt khám sức khỏe");
            }
        }
    }
}