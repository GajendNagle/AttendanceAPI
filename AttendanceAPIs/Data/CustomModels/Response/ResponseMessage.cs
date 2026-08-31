
using PMPoshanWithAngular.Server.Data.TypedConstants;

namespace PMPoshanWithAngular.Server.Data.CustomeModels.Reponse
{
    public class ResponseMessage
    {
        public ResponseCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } = string.Empty;
        public ResponseMessage(
            ResponseCode StatusCode
            , string Message
            , string status = "")
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            Status = status;
        }

        //public ResponseMessage(ResponseCode oK, string gENERIC_SUCCESS, List<MonthRespons> months)
        //{
        //    this.oK = oK;
        //    this.gENERIC_SUCCESS = gENERIC_SUCCESS;
        //    this.months = months;
        //}
    }
}
