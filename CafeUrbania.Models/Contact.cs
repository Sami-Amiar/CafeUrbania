﻿using System.ComponentModel.DataAnnotations;

namespace CafeUrbania.Models;

public class Contact
{
    [Required]
    public string Nom { get; set; }

    public string Telephone { get; set; }

    public string Courriel { get; set; }

    public string Message { get; set; }

    public DateTime DateHeureCreation { get; set; }
}
