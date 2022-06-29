using cw8.DTOs.Requests;
using cw8.DTOs.Responses;
using cw8.Helpers;
using cw8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cw8.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        public DbService(MainDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<GetDoctorDtoResponse> GetDoctorAsync(int idDoctor)
        {
            var doctor = await _context.Doctor.Where(e => e.IdDoctor == idDoctor)
                                              .Select(z => new GetDoctorDtoResponse
                                              {
                                                  IdDoctor = idDoctor,
                                                  FirstName = z.FirstName,
                                                  LastName = z.LastName,
                                                  Email = z.Email
                                              }).FirstAsync();
            return doctor;
        }
        public async Task<bool> DeleteDoctorAsync(int idDoctor)
        {
            var doctorExists = await _context.Doctor.AnyAsync(e => e.IdDoctor == idDoctor);
            if (!doctorExists)
                return false;

            var doctor = new Doctor
            {
                IdDoctor = idDoctor
            };
            _context.Doctor.Attach(doctor);
            _context.Entry(doctor).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> PostDoctorAsync(PostDoctorDtoRequest doctor)
        {
            var doctorExists = await _context.Doctor.AnyAsync(e => e.Email == doctor.Email);
            if (doctorExists) 
            {
                return false;
            }
            var newDoctor = new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            await _context.Doctor.AddAsync(newDoctor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PutDoctorAsync(PostDoctorDtoRequest doctor, int idDoctor)
        {
            var doctorExists = await _context.Doctor.AnyAsync(e => e.IdDoctor == idDoctor);
            if (!doctorExists)
            {
                return false;
            }
            var newDoctor = new Doctor
            {
                IdDoctor = idDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email

            };
            _context.Doctor.Attach(newDoctor);
            _context.Entry(newDoctor).Property(nameof(Doctor.FirstName)).IsModified = true;
            _context.Entry(newDoctor).Property(nameof(Doctor.LastName)).IsModified = true;
            _context.Entry(newDoctor).Property(nameof(Doctor.Email)).IsModified = true;
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<GetPrescriptionDtoResponse> GetPrescriptionAsync(int idPrescription)
        {
            HashSet<GetPrescriptionDtoResponse> list = new();

            var prescription = await _context.Prescription.Where(z => z.IdPrescription == idPrescription)
                                                     .Include(e => e.Doctor)
                                                     .Include(n => n.Patient)
                                                     .Include(c => c.PrescriptionMedicaments)
                                                     .ThenInclude(c => c.Medicament)
                                                     .Select(c => new GetPrescriptionDtoResponse
                                                     {
                                                         IdPrescription = c.IdPrescription,
                                                         Date = c.Date,
                                                         DueDate = c.DueDate,
                                                         IdPatient = c.Patient.IdPatient,
                                                         PatientFirstName = c.Patient.FirstName,
                                                         PatientLastName = c.Patient.LastName,
                                                         PatientBirthDate = c.Patient.BirthDate,
                                                         IdDoctor = c.Doctor.IdDoctor,
                                                         DoctorFirstName = c.Doctor.FirstName,
                                                         DoctorLastName = c.Doctor.LastName,
                                                         DoctorEmail = c.Doctor.Email,
                                                         Medicaments = c.PrescriptionMedicaments.Select(c => new MedicamentDtoResponse
                                                         {
                                                             IdMedicament = c.Medicament.IdMedicament,
                                                             Name = c.Medicament.Name,
                                                             Description = c.Medicament.Description,
                                                             Type = c.Medicament.Type
                                                         })
                                                     }).FirstAsync();


            return prescription;
        }

        public async Task<bool> PostRegisterUserAsync(RegisterRequest model)
        {
            var user = await _context.User.AnyAsync(e => e.Login == model.Login);
            if (user)
                return false;

            var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);


            var newUser = new AppUser()
            {
                Email = model.Email,
                Login = model.Login,
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(1)
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<TokenDtoResponse> LoginAsync(LoginRequest loginRequest)
        {
            AppUser user = await _context.User.Where(u => u.Login == loginRequest.Login).FirstOrDefaultAsync();
            if (user == null)
            {
                return new TokenDtoResponse("1", "1");
            }

            string passwordHashFromDb = user.Password;
            string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);
            if (passwordHashFromDb != curHashedPassword)
            {
                return new TokenDtoResponse("2", "1");
            }


            Claim[] userclaim = new[] {
                    new Claim(ClaimTypes.Name, "s20296"),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Role, "admin")
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = user.RefreshToken;
            return new TokenDtoResponse(accessToken, refreshToken);
        }
        public async Task<TokenDtoResponse> NewTokenAsync(string token, RefreshTokenRequest refreshToken)
        {
            AppUser user = await _context.User.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            if (user.RefreshTokenExp < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh token expired");
            }
            var login = SecurityHelpers.GetUserIdFromAccessToken(token.Replace("Bearer ", ""), _configuration["SecretKey"]);

            Claim[] userclaim = new[] {
                    new Claim(ClaimTypes.Name, "s20296"),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Role, "admin")
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return new TokenDtoResponse(new JwtSecurityTokenHandler().WriteToken(jwtToken), user.RefreshToken);
        }


    }
}
