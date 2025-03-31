using Base;

namespace Schema;

public class CustomerAddressRequest : BaseRequest
{
    public long CustomerId { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}

public class CustomerAddressResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}