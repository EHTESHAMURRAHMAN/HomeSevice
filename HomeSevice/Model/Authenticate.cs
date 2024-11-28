using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection.Metadata;
using static Dapper.SqlMapper;

namespace HomeSevice.Model
{
    public class Authenticate
    {
        public int id { get; set; } = 0;
        public string fname { get; set; } = "";
        public string phone { get; set; } = "";
        public string email { get; set; } = "";
        public string pass { get; set; } = "";
        public string dpImage { get; set; } = "";

        static string dataAccess = DataAccess.GetConnection();

        public RResponse CreateAccount(Authenticate authenticate)
        {
            RResponse response = new RResponse();
            HomeServicess homeServices = new HomeServicess();
            if (authenticate == null)
            {
                response.status = "failed";
                return response;
            }
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                sqlConnection.Open();

                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("fname", authenticate. fname);
                dynamicParameters.Add("phone", authenticate.phone);
                dynamicParameters.Add("email", authenticate.email);
                dynamicParameters.Add("pass", authenticate.pass);
                dynamicParameters.Add("dpImage", authenticate.dpImage);
                dynamicParameters.Add("@intResult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                SqlMapper.Query<int>(sqlConnection, "sp_register_insert", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                int result = dynamicParameters.Get<int>("@intResult");
                if (result > 0)
                {
                    homeServices.id = result;
                    response.account = homeServices;
                    response.status = "succeed";
                    response.message = "succeed";
                }
                else if (result == -1)

                {
                    response.status = "failed";
                    response.message = "This email already exits";
                }
                else
                {
                    response.status = "failed";
                    response.message = "Try after some time";
                }
            }
            return response;
        }
        public static ProfileResponse? GetProfileDetail(int userid)
        {
            ProfileResponse objresponse = new ProfileResponse();
            using (SqlConnection con = new SqlConnection(dataAccess))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", userid);
                objresponse.data = SqlMapper.Query<Profile>(con, "sp_register_view", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                objresponse.status = "succeed";
                objresponse.message = "succeed";
            }
            return objresponse;
        }

        public Result UpdateProfile(Authenticate objupdate)
        {
            Result objResponseResult = new Result();
            using (SqlConnection con = new SqlConnection(dataAccess))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", objupdate.id);
                parameter.Add("fname", objupdate. fname);
                parameter.Add("phone", objupdate.phone);
                parameter.Add("email", objupdate.email);
                parameter.Add("pass", objupdate.pass);
                parameter.Add("dpImage", objupdate.dpImage);

                parameter.Add("@intResult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                SqlMapper.Query<Authenticate>(con, "sp_register_update", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                int newid = parameter.Get<int>("@intResult");
                if (newid > 0)
                {

                    objResponseResult.status = "succeed";
                    objResponseResult.message = "Successful Updated";
                }
                else
                {
                    objResponseResult.status = "failed";
                    objResponseResult.message = "Try after some time";
                }
            }
            return objResponseResult;
        }
        public static Result DeleteProfileDetail(int userid)
        {
            Result objresponse = new Result();
            using (SqlConnection con = new SqlConnection(dataAccess))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", userid);
                SqlMapper.Query<Profile>(con, "sp_register_delete", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                objresponse.status = "succeed";
                objresponse.message = "succeed";
            }
            return objresponse;
        }
    }


    public class HomeServicess
        {
            public int id { get; set; } = 0;
        }

        public class RResponse
        {
            public HomeServicess account { get; set; }
            public string status { get; set; } = "";
            public string message { get; set; } = "";
        }


        public class Profile
        {
            public int id { get; set; } = 0;
            public string fname { get; set; } = "";
            public string phone { get; set; } = "";
            public string email { get; set; } = "";
            public string pass { get; set; } = "";
            public string dpImage { get; set; } = "";
        }

        public class ProfileResponse
        {
            public Profile data { get; set; }
            public string status { get; set; } = "";
            public string message { get; set; } = "";
        }
    }
