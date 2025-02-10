﻿using Microsoft.EntityFrameworkCore;

namespace Auth.DataAccess.Interfaces
{
    /// <summary>
    /// Конфигуратор контекста.
    /// </summary>
    public interface IDbContextOptionsConfigurator<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Выполняет конфигурацию контекста.
        /// </summary>
        /// <param name="options">настройки.</param>
        void Configure(DbContextOptionsBuilder<TContext> options);
    }
}
