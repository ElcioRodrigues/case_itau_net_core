namespace CaseItau.Web.Models
{
    public class Result
    {
        public Result()
        {
            Messages = [];
            SuccessMessage = string.Empty;
        }

        public List<string> Messages { get; }
        public string SuccessMessage { get; set; }

        public bool Success { get { return Messages.Count == 0; } }
    }
}
