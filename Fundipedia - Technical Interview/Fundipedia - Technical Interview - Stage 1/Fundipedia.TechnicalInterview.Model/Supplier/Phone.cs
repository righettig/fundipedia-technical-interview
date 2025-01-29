using System;
using System.ComponentModel.DataAnnotations;

namespace Fundipedia.TechnicalInterview.Model.Supplier;

public class Phone
{
    /// <summary>
    /// Gets or sets the phone id
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the phone number
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 digits.")]
    [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Phone number must contain only numeric digits.")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email is the preferred one or not
    /// </summary>
    public bool IsPreferred { get; set; }
}