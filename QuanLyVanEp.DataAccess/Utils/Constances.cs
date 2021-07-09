using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyVanEp.DataAccess.Utils
{
    public static class Constances
    {
        /// <summary>
        /// Số lượng đầu trắng bỏ trong cđ sokumen
        /// </summary>
        public const int SL_DAU_TRANG_SOKUMEN = 2;

        /// <summary>
        /// Số tấm kiban tối đã chạy cho 1 lot
        /// </summary>
        public const int MAXIMUM_KIBAN_PER_LOT = 700;

        /// <summary>
        /// Số làm tròn định mức
        /// </summary>
        public const int DM_ROUND_NUMBER = 9;

        /// <summary>
        /// Số làm tròn
        /// </summary>
        public const int ROUND_NUMBER = 4;

        /// <summary>
        /// Làm tròn nhỏ
        /// </summary>
        public const int SMALL_ROUND_NUMBER = 2;

        /// <summary>
        /// Khối lượng chênh lệch tối đa khi lấy mực
        /// </summary>
        public const float PASTE_DIFF_LIMIT = 0.3f;

        /// <summary>
        /// Số lần dùng mực tối đa trước khi cảnh báo
        /// </summary>
        public const int PASTE_USE_LIMIT = 8;

        /// <summary>
        /// Số lượng chênh lệch tối đa khi trả mực so với kl hũ
        /// </summary>
        public const float PASTE_RETURN_DIFF_LIMIT = 5;

        /// <summary>
        /// Số lượng chênh lệch tối đa giữa kl lý thuyết và kl thực tế
        /// </summary>
        public const float PASTE_USE_DIFF_LIMIT = 10;

        /// <summary>
        /// Khối lượng chênh lệch tối đã đối với khối lượng trộn và khối lượng mực sử dụng
        /// </summary>
        public const float PASTE_MIX_DIFF_LIMIT = 5;

        /// <summary>
        /// Khối lượng chênh lệch tối đã đối với khối lượng tách và khối lượng mực sử dụng
        /// </summary>
        public const float PASTE_SPLIT_DIFF_LIMIT = 5;

        /// <summary>
        /// Khối lượng chênh lệch tối đã đối với khối lượng chiết và khối lượng mực nhận chiết
        /// </summary>
        public const float PASTE_CHIET_DIFF_LIMIT = 5;


        /// <summary>
        /// Nếu chênh lệch khi trả mực nằm trong khoảng này thì coi như chưa sử dụng
        /// </summary>
        public const float PASTE_UNCHANGE_LIMIT = 0.3f;
    }
}
