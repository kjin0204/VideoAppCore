using VideoAppCoreModels.Common;

namespace VideoAppCoreModels
{


    /// <summary>
    /// [2] 테이블 생성 후 대응 모델 생성.
    /// 통상적으로 모델 사용하는 명칭. 테이블명이 Videos일 경우
    /// video,videoModel,videoViewModel,videoBase,videoEntity,videoDto,....
    /// </summary>

    public class Video : AuditableBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        //public DateTimeOffset Created { get; set; }
        /// <summary>
        /// 강의 제목
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 동영상 URL 경로
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 생성자
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 회사
        /// </summary>
        public string Company { get; set; }
    }
}
