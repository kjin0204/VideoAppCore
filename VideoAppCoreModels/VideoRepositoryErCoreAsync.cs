using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAppCoreModels
{
    /// <summary>
    /// [4] 리포지토리 클래스 Er Core를 사용 하여 CRUD 구현
    /// </summary>
    public class VideoRepositoryErCoreAsync : IVideoRepositoryAsync
    {
        private readonly VideoDbContext context;

        public VideoRepositoryErCoreAsync(VideoDbContext context)
        {
            this.context = context;
        }

        public async Task<Video> AddVideoAsync(Video model)
        {
            context.Videos.Add(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            return  await context.Videos.FindAsync(id);
        }

        public async Task<List<Video>> GetVideosAsync()
        {
            return await context.Videos.ToListAsync();
        }

        public async Task RemoveVideoAsync(int id)
        {
            var video = await context.Videos.FindAsync(id);
            if(video != null)
            {
                context.Videos.Remove(video);
            }
            await context.SaveChangesAsync();
        }

        public async Task<Video> UpdateVideoAsync(Video model)
        {
            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return model;
        }
    }
}
