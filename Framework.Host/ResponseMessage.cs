namespace Framework.Host
{
    public class ResponseMessage
    {
        public ResponseMessage(string message)
        {
            this.message = message;
        }

        public string message { get; set; }
    }
}