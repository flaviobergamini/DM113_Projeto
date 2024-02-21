namespace Core.Entities.AddressAggregate;

public class Address : BaseEntity
{
    public string Country { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Uf { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string? Complement { get; set; }
    public string District { get; set; } = null!;

    public List<Address> Addresses { get; } = new();
}