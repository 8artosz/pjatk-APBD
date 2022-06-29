using cw8.DTOs.Requests;
using cw8.DTOs.Responses;
using cw8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw8.Services
{
    public interface IDbService
    {
        public Task<GetDoctorDtoResponse> GetDoctorAsync(int idDoctor);
        public Task<bool> DeleteDoctorAsync(int idDoctor);
        public Task<bool> PostDoctorAsync(PostDoctorDtoRequest doctor);
        public Task<bool> PutDoctorAsync(PostDoctorDtoRequest doctor, int idDoctor);
        public Task<GetPrescriptionDtoResponse> GetPrescriptionAsync(int idPrescription);
        public Task<bool> PostRegisterUserAsync(RegisterRequest model);
        public Task<TokenDtoResponse> LoginAsync(LoginRequest loginRequest);
        public Task<TokenDtoResponse> NewTokenAsync(string token, RefreshTokenRequest refreshToken);

    }
}
