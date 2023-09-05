using System;

namespace VideoAppCoreModels.Common
{
    public class AuditableBase
    {

        /// <summary>
        /// 생성시간
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 생성자
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 수정일
        /// </summary>
        public DateTime Modified { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
