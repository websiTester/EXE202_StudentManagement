namespace EXE202_StudentManagement.ViewModels
{
    public class PeerReviewViewModel
    {

        public int AssignmentId { get; set; }
        public int GroupId { get; set; }
        public string ReviewerId { get; set; }
        public string RevieweeId { get; set; }
        public string Comment { get; set; }
        public decimal Score { get; set; }

    }
}
