using PMPoshanWithAngular.Server.Data.TypedConstants;

namespace PMPoshanWithAngular.Server.Data.CustomeModels.Reponse
{
    public class ResponseMessageWithData
    {
        public ResponseCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } = string.Empty;
        public object Data { get; set; }
        public ResponseMessageWithData(
            ResponseCode StatusCode
            , string Message
            , object data = null
            , string status = "")
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.Data = data;
            Status = status;
        }
    }
}
