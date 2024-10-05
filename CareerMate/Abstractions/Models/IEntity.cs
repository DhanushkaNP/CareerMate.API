using System;

namespace CareerMate.Abstractions.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime? ModifiedAt { get; set; }
    }
}
