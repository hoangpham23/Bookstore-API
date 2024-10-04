using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class BookLanguage : BaseEntity
{
    [Column("language_code")]
    [StringLength(8)]
    public required string LanguageCode  { get; set; }
    
    [Column("language_name")]
    [StringLength(40)]
    public required string LanguageName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
