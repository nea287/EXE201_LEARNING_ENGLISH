using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Common
{
    public class Constraints
    {
        #region Addition
        public const string NOT_FOUND_INFO = "Không tìm thấy thông tin tương ứng!";
        public const string EMPTY_INFO = "Thông tin hiện đang trống!";
        public const string LOAD_INFO_FAILED = "Thông tin loading thất bại!";
        public const string EXISTED_INFO = "Thông tin đã tồn tại!";
        public const string CREATE_INFO_SUCCESS = "Tạo thông tin thành công!";
        public const string CREATE_INFO_FAILED = "Tạo thông tin thất bại!";
        public const string UPDATE_INFO_SUCCESS = "Cập nhật thông tin thành công!";
        public const string UPDATE_INFO_FAILED = "Cập nhật thông tin thất bại!";
        public const string DELETE_INFO_SUCCESS = "Xóa thông tin thành công!";
        public const string DELELTE_INFO_FAILED = "Xóa thông tin thất bại!";
        #endregion

        #region Validate
        public const string NUMBER_PHONE_VALIDATE = "Số điện thoại không hợp lệ!";
        #endregion

        #region Page
        public const int DefaultPaging = 50;
        public const int LimitPaging = 500;
        public const int DefaultPage = 1;
        #endregion
    }
}
