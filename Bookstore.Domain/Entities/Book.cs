using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Core.Base;

namespace Bookstore.Domain.Entities;

public class Book : BaseEntity
{
    public required string Title { get; set; }
    [StringLength(13)]
   
    public required string Isbn13 { get; set; }

    [Column("language_id")]
    public required string LanguageId { get; set; }
    
    [Column("num_pages")]
    public required int NumPages { get; set; }

    [Column("publication_date")]
    public DateOnly? PublicationDate{ get; set; }
   
    [Column("publisher_id")]
    public required string PublisherId { get; set; }
    
    public virtual BookLanguage? Language { get; set; }
    public virtual Publisher? Publisher{ get; set; }
    public virtual ICollection<Author> Authors {get; set;} = new List<Author>();
    public virtual ICollection<OrderLine> OrderLines {get; set;} = new List<OrderLine>();
}
