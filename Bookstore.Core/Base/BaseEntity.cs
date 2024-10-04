using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Core.Base;

public abstract class BaseEntity
{   
    [Key]
    public string Id { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}
