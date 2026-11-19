namespace DNSLab.Web.API.Authentication.DTOs;

public record ConfirmOtpRequest(
    string Token,
    string Otp);
