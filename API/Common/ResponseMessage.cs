namespace Epam.Xmp.Vts.Server.Common
{
    public class ResponseMessage
    {
        public string Result { get; set; }
        public int ResultCode { get; set; }
    }

    public sealed class ResponseListResultMessage : ResponseMessage
    {
        public VacationRequest[] ListResult { get; set; }
    }

    public sealed class ResponseItemResultMessage : ResponseMessage
    {
        public VacationRequest ItemResult { get; set; }
    }
}