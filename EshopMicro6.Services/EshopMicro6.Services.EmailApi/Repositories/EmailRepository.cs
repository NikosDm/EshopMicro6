using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopMicro6.Services.EmailApi.Data;
using EshopMicro6.Services.EmailApi.Entities;
using EshopMicro6.Services.EmailApi.Interfaces;
using EshopMicro6.Services.EmailApi.Messages;
using Microsoft.EntityFrameworkCore;

namespace EshopMicro6.Services.EmailApi.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<AppDbContext> _dbContext; 
        
        public EmailRepository(DbContextOptions<AppDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            //implement an email sender or some other class library 

            EmailLog emailLog = new()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Order - {message.OrderID} has been created successfully."
            };

            await using var _db = new AppDbContext(_dbContext);

            _db.EmailLogs.Add(emailLog);

            await _db.SaveChangesAsync();
        }
    }
}