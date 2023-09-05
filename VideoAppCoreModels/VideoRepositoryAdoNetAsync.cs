using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace VideoAppCoreModels
{
    /// <summary>
    /// [4] 리포지토리 클래스 Ado.Net을 사용 하여 CRUD 구현
    /// </summary>
    public class VideoRepositoryAdoNetAsync : IVideoRepositoryAsync
    {
        private readonly string connectionString;

        public VideoRepositoryAdoNetAsync(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public async Task<Video> AddVideoAsync(Video model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //select cast(SCOPE_IDENTITY() As Int)은 add 할때 자동으로 들어간 id값을 가지고 오기 위함.
                const string query =
                    @"insert into Videos(Title, Url,Name, Company, CreateBy) Values(@Title,@Url,@Name,@Company,@CreateBy);
                        select cast(SCOPE_IDENTITY() As Int);";
                SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Url", model.Url);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Company", model.Company);
                cmd.Parameters.AddWithValue("@CreateBy", model.CreateBy);

                con.Open();
                object result = await cmd.ExecuteScalarAsync();
                if (int.TryParse(result.ToString(), out int id))
                {
                    model.Id = id;
                }
                con.Close();
            }
            return model;
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            Video video = new Video();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                const string query = "select * from Videos where id = {@id}";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();

                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                if (dr.Read())
                {
                    video.Id = dr.GetInt32(0);
                    video.Title = dr["title"].ToString();
                    video.Url = dr["url"].ToString();
                    video.Name = dr["name"].ToString();
                    video.Company = dr["Company"].ToString();
                    video.CreateBy = dr["CreateBy"].ToString();
                    video.Created = Convert.ToDateTime(dr["created"]);
                }
                con.Close();
            }
            return video;
        }

        public async Task<List<Video>> GetVideosAsync()
        {
            List<Video> videos = new List<Video>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                const string query = "select * from Videos ";

                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                con.Open();

                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (dr.Read())
                {
                    Video video = new Video();
                    video.Id = dr.GetInt32(0);
                    video.Title = dr["title"].ToString();
                    video.Url = dr["url"].ToString();
                    video.Name = dr["name"].ToString();
                    video.Company = dr["Company"].ToString();
                    video.CreateBy = dr["CreateBy"].ToString();
                    video.Created = Convert.ToDateTime(dr["created"]);
                    videos.Add(video);
                }
                con.Close();
            }
            return videos;
        }

        public async Task RemoveVideoAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                const string query = "delete Videos where Id = @id";

                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("id",id);

                con.Open();
                await cmd.ExecuteNonQueryAsync();

                con.Close();
            }

        }

        public async Task<Video> UpdateVideoAsync(Video model)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                const string query = @"update Videos 
                                        set 
                                            Title = @Title,
                                            Url = @Url,
                                            Name = @Name,
                                            Company = @Company,
                                            ModifiedBy = @ModifiedBy
                                        where Id = @id";

                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Url", model.Url);
                cmd.Parameters.AddWithValue("@Company", model.Company);
                cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);

                con.Open();
                await cmd.ExecuteNonQueryAsync();

                con.Close();
            }

            return model;
        }
    }
}
