using System.Collections.Generic;
using System.Text;

namespace VideoAppCoreModels._
{
    /// <summary>
    /// [3] CRUD API 명세서 정의
    /// </summary>
    public interface IVideoRepository
    {
        Video AddVideo(Video model);        //입력 : T Add(T model);
        List<Video> GetVideos();            //출력 : List<T> GetAll();
        Video GetVideoById(int id);         //상세 : T GetById(int id);
        Video UpdateVideo(Video model);     //수정 : T Edit(T model);
        Video RemoveVideo(int id);          //삭제 : T VideoDelete(int id);
    }
}
