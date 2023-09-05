using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCoreModels
{
    public class VideoRepositoryDapperAsync : IVideoRepositoryAsync
    {
        private readonly SqlConnection db;

        public VideoRepositoryDapperAsync(string connectionString)
        {
            this.db = new SqlConnection( connectionString);
        }

        public async Task<Video> AddVideoAsync(Video model)
        {
            const string query =
                @"insert into Videos(Title, Url,Name, Company, CreateBy) Values(@Title,@Url,@Name,@Company,@CreateBy);
                        select cast(SCOPE_IDENTITY() As Int);";

            int id = await db.ExecuteScalarAsync<int>(query,model);

            model.Id = id;

            return model;
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            const string query = "select * from Videos where Id = @id";

            var video = await db.QueryFirstOrDefaultAsync<Video>(query,new {id}, commandType : System.Data.CommandType.Text);

            return video;
        }

        public async Task<List<Video>> GetVideosAsync()
        {
            const string query = "select * from Videos";

            var videos = await db.QueryAsync<Video>(query);

            return videos.ToList();
        }

        public async Task RemoveVideoAsync(int id)
        {
            const string query = "delete  Videos where Id = @id";

            await db.ExecuteAsync(query, new { id }, commandType: System.Data.CommandType.Text);
        }

        public async Task<Video> UpdateVideoAsync(Video model)
        {
            const string query = @"update Videos 
                                        set 
                                            Title = @Title,
                                            Url = @Url,
                                            Name = @Name,
                                            Company = @Company,
                                            ModifiedBy = @ModifiedBy
                                        where Id = @id";

            await db.ExecuteAsync(query, model);

            return model;
        }
    }
}
