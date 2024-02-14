namespace Payment_Backend_Service.Services
{
    public interface IRequestHashGeneratorService
    {
        string GenerateRequestHash(int employeeId, string month, string requestType);

    }
}
