using System;

namespace Twileloop.UOW.MongoDB.Support
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
