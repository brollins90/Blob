namespace Blob.Contracts.ServiceContracts
{
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Request;
    using Response;

    [ServiceContract]
    public interface IDeviceStatusService
    {
        [OperationContract]
        Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest dto);

        [OperationContract]
        Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto);

        [OperationContract]
        Task AddStatusRecordAsync(AddStatusRecordRequest dto);

        [OperationContract]
        Task AddPerformanceRecordAsync(AddPerformanceRecordRequest dto);
    }
}