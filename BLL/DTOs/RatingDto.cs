using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class RatingDto
{
    public int UserId { get; set; }
    public string VoterUsername { get; set; }
    public int PhotoId { get; set; }
    public string PhotoOwnerUsername { get; set; }
    [Range(1,5)]
    public int Value { get; set; }
}