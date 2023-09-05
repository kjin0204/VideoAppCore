using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAppCoreModels
{
    /// <summary>
    /// [3] CRUD API 명세서 정의
    /// </summary>
    public interface IVideoRepositoryAsync
    {
        Task<Video> AddVideoAsync(Video model);        //입력 : T Add(T model);
        Task<List<Video>> GetVideosAsync();            //출력 : List<T> GetAll();
        Task<Video> GetVideoByIdAsync(int id);         //상세 : T GetById(int id);
        Task<Video> UpdateVideoAsync(Video model);     //수정 : T Edit(T model);
        Task RemoveVideoAsync(int id);          //삭제 : T VideoDelete(int id);
    }
}
