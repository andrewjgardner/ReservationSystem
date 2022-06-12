using ReservationSystem.Utilities;

namespace ReservationSystem.Areas.Admin.Models.Sitting
{
    public class Index
    {
        public PaginatedList<Summary> Sittings { get; set; }
        public Close Close { get; set; }
        public string CurrentFilter { get; set; }
        public int CurrentSort { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
    }
}
