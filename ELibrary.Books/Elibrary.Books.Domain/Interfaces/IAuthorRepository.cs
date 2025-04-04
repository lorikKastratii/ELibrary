﻿using ELibrary.Books.Domain.Entity;

namespace ELibrary.Books.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> CreateAsync(Author author);
        Task<List<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<bool> UpdateAuthorAsync(Author author);
    }
}
