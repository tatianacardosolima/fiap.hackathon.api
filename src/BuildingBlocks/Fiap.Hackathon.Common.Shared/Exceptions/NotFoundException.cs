﻿namespace Fiap.Hackathon.Common.Shared.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException():base() { }
        public NotFoundException(string message):base(message) {}

        
        public static void ThrowWhenNullEntity(object? entity, string errorMessage)
        {
            if (entity is not null) return;
            throw new NotFoundException(errorMessage);
        }

        public static void ThrowWhenNullOrEmptyList(
            IEnumerable<object> list,
            string errorMessage)
        {
            if (list is not null && list.Any()) return;
            throw new NotFoundException(errorMessage);
        }
    }
}
