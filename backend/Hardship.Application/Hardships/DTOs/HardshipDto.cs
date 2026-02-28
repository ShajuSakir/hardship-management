namespace Hardship.Application.Hardships.DTOs
{
    public record HardshipDto(
     Guid Id,
     string CustomerName,
     DateTime DateOfBirth,
     decimal Income,
     decimal Expenses,
     string? HardshipReason
 );

}
