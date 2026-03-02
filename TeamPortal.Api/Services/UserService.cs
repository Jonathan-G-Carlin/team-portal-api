
using TeamPortal.Api.Dtos;
using TeamPortal.Data;
using TeamPortal.Data.Entities;

using BRc = BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace TeamPortal.Api.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context; 
            // Table: Users
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                MiddleName = userDto.MiddleName,
                FamilyName = userDto.FamilyName,
                Email = userDto.Email,
                PasswordHash = BRc.BCrypt.HashPassword(userDto.Password),
                Role = "User",
                IsActive = true
            };

            _context.Users.Add(user);

            //  Personal Note: user.Id is initialized to 0 by default. Add() tell EF tracker 'pending insert'

            await _context.SaveChangesAsync();

            //  Personal Note Cont': EF executes INSERT... and then, reads its back in a select statement
            // "EF immediately queries back the generated Id and any DB-defaulted columns (CreatedOnUtc) so the entity in memory stays in sync with what's in the database."

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedOnUtc = user.CreatedOnUtc
            };
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedOnUtc = user.CreatedOnUtc
            };
        }

        public async Task<List<UserResponseDto>> GetUsersAsync() 
        {
            return await _context.Users
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    FamilyName = user.FamilyName,
                    Email = user.Email,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    CreatedOnUtc = user.CreatedOnUtc
                })
                .ToListAsync();

        }

        public async Task<UserResponseDto?> UpdateUserAsync(int Id, UpdateUserDto updateDto)
        {
            var user = await _context.Users.FindAsync(Id);

            if (user is null)
                return null;

            user.FirstName = updateDto.FirstName;
            user.MiddleName = updateDto.MiddleName;
            user.FamilyName = updateDto.FamilyName;
            user.Email = updateDto.Email;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedOnUtc = user.CreatedOnUtc
            };
        }

        public async Task<bool> UpdatePasswordAsync(int Id, UpdatePasswordDto updateDto)
        {
            var user = await _context.Users.FindAsync(Id);

            if (user is null)
                return false;

            bool isPasswordMatch = BRc.BCrypt.Verify(updateDto.CurrentPassword, user.PasswordHash);

            if (!isPasswordMatch)
                return false;

            user.PasswordHash = BRc.BCrypt.HashPassword(updateDto.NewPassword);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
