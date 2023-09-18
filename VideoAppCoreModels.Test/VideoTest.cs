using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAppCoreModels.Test
{
    [TestClass]
    public class VideoTest
    {
        [TestMethod]
        public async Task AddVideo()
        {
            //DbContextOptionsBuilder를 사용하여 인-메모리 데이터베이스 정보를 DbContext에 전달
            var options = new DbContextOptionsBuilder<VideoDbContext>().UseInMemoryDatabase(databaseName: "AddVIdeo").Options;

            //컨텍스트 객체 생성

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                Video video = new Video {  Company = "test", Url = "www.naver.com", CreateBy = "홍길동" };
                repository.AddVideoAsync(video);
            }

            using (var context = new VideoDbContext(options))
            {

                Assert.AreEqual(1, context.Videos.Count());
                Assert.AreEqual("www.naver.com", context.Videos.Where(x => x.Id == 1).SingleOrDefault().Url);
            }
        }

        [TestMethod]
        public async Task GetVideos()
        {
            //DbContextOptionsBuilder를 사용하여 인-메모리 데이터베이스 정보를 DbContext에 전달
            var options = new DbContextOptionsBuilder<VideoDbContext>().UseInMemoryDatabase(databaseName: "GetVIdeos").Options;

            //컨텍스트 객체 생성

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                await repository.AddVideoAsync(new Video { Title = "Test", Company = "test", Url = "www.naver.com", CreateBy = "홍길동" });
                await repository.AddVideoAsync(new Video { Title = "Test1", Company = "test", Url = "www.naver.com", CreateBy = "홍길동1" });
                await repository.AddVideoAsync(new Video { Title = "Test2", Company = "test", Url = "www.naver.com", CreateBy = "홍길동2" });
            }

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                var videos = await repository.GetVideosAsync();

                Assert.AreEqual(3, context.Videos.Count());
                Assert.AreEqual("홍길동", context.Videos.Where(x => x.Id == 1).SingleOrDefault().CreateBy);
            }
        }


        [TestMethod]
        public async Task GetVideo()
        {
            //DbContextOptionsBuilder를 사용하여 인-메모리 데이터베이스 정보를 DbContext에 전달
            var options = new DbContextOptionsBuilder<VideoDbContext>().UseInMemoryDatabase(databaseName: "GetVIdeo").Options;

            //컨텍스트 객체 생성

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                await repository.AddVideoAsync(new Video { Title = "Test", Company = "test", Url = "www.naver.com", CreateBy = "홍길동" });
                await repository.AddVideoAsync(new Video { Title = "Test1", Company = "test", Url = "www.naver.com", CreateBy = "홍길동1" });
                await repository.AddVideoAsync(new Video { Title = "Test2", Company = "test", Url = "www.naver.com", CreateBy = "홍길동2" });
            }

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                var video = await repository.GetVideoByIdAsync(1);

                Assert.AreEqual("www.naver.com1", video.Url);
                Assert.AreEqual("홍길동", context.Videos.Where(x => x.Id == 1).SingleOrDefault().CreateBy);
            }
        }



        [TestMethod]
        public async Task DeleteVideo()
        {
            //DbContextOptionsBuilder를 사용하여 인-메모리 데이터베이스 정보를 DbContext에 전달
            var options = new DbContextOptionsBuilder<VideoDbContext>().UseInMemoryDatabase(databaseName: "DeleteVideo").Options;

            //컨텍스트 객체 생성

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                await repository.AddVideoAsync(new Video { Title = "Test", Company = "test", Url = "www.naver.com", CreateBy = "홍길동" });
                await repository.AddVideoAsync(new Video { Title = "Test1", Company = "test", Url = "www.naver.com", CreateBy = "홍길동1" });
                await repository.AddVideoAsync(new Video { Title = "Test2", Company = "test", Url = "www.naver.com", CreateBy = "홍길동2" });
            }

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                await repository.RemoveVideoAsync(1);
                var vidoes = await repository.GetVideosAsync();

                Assert.AreEqual(2, vidoes.Count());
                Assert.IsNull(vidoes.Where(x => x.Id == 1).SingleOrDefault());
            }
        }



        [TestMethod]
        public async Task UpdateVideo()
        {
            //DbContextOptionsBuilder를 사용하여 인-메모리 데이터베이스 정보를 DbContext에 전달
            var options = new DbContextOptionsBuilder<VideoDbContext>().UseInMemoryDatabase(databaseName: "UpdateVideo").Options;

            //컨텍스트 객체 생성

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                await repository.AddVideoAsync(new Video { Title = "Test", Company = "test", Url = "www.naver.com", CreateBy = "홍길동" });
                await repository.AddVideoAsync(new Video { Title = "Test1", Company = "test", Url = "www.naver.com", CreateBy = "홍길동1" });
                await repository.AddVideoAsync(new Video { Title = "Test2", Company = "test", Url = "www.naver.com", CreateBy = "홍길동2" });
            }

            using (var context = new VideoDbContext(options))
            {
                var repository = new VideoRepositoryErCoreAsync(context);
                var video = await repository.GetVideoByIdAsync(1);

                video.CreateBy = "T1";
                await repository.UpdateVideoAsync(video);
                var updateVideo = await repository.GetVideoByIdAsync(1);

                Assert.AreEqual("T1", updateVideo.CreateBy);
            }
        }

    }
}
