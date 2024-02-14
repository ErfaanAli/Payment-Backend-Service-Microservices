namespace Payment_Backend_Service.Common
{
    public class PaymentResponse<T>
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
