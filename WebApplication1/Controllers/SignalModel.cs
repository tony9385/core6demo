namespace WebApplication1.Controllers
{
    public class SignalModel
    {
        public int BatchIndex { get; set; }
        public int SignalId { get; set; }
        public string SignalValue { get; set; }
        public DateTime ReceivedTime { get; set; }
    }
}