using CarPark.API.Common.Model;
using CarPark.API.Data.Entity;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Common
{
    public static class TokenHelper
    {

        private static IServiceProvider _applicationServices { get; set; }

        public static void Configure(IServiceProvider applicationServices)
        {
            _applicationServices = applicationServices;
        }

        public static string CreateToken(Personnel personnel, string userAuthCodeListAsString)
        {
            // https://github.com/jwt-dotnet/jwt#JwtNet

            // JwtSecret bilgisi config dosyasından okunuyor
            //string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            string secret = ConfigHelper.Jwt_Secret;
            int expiredTime = 60;

            var token = new JwtBuilder()
              .WithAlgorithm(new HMACSHA256Algorithm())
              .WithSecret(secret)
              .AddClaim("expireAsUnixSeconds", DateTimeOffset.UtcNow.AddMinutes(expiredTime).ToUnixTimeSeconds())
              .AddClaim("ID", personnel.Id)
              .AddClaim("name", personnel.Name)
              .AddClaim("lastName", personnel.LastName)
              .AddClaim("authCodeListAsString", userAuthCodeListAsString) // kullanıcı yetkileri de eklenir
              .AddClaim("tokenGuid", Guid.NewGuid().ToString())
              .Encode();

            return token;
        }


        public static string DecodeTokenAndGetTokenGuid(string jwtToken)
        {
            string result = "";
            string secret = ConfigHelper.Jwt_Secret;
            try
            {
                var payload = new JwtBuilder()
                     .WithAlgorithm(new HMACSHA256Algorithm())
                     .WithSecret(secret)
                     .MustVerifySignature()
                     .Decode<IDictionary<string, object>>(jwtToken);
                result = payload["TokenGuid"].ToString();

            }
            catch (TokenExpiredException tokenExpiredException)
            {
                //Console.WriteLine("Token has expired");

            }
            catch (SignatureVerificationException signatureVerificationException)
            {
                //Console.WriteLine("Token has invalid signature");
            }
            catch (Exception ex)
            {

            }


            return result;
        }

        public static TokenModel DecodeToken(string jwtToken)
        {
            TokenModel tokenModel = null;

            if (string.IsNullOrEmpty(jwtToken))
            {
                return tokenModel;
            }

            //
            string secret = ConfigHelper.Jwt_Secret;

            try
            {
                var payload = new JwtBuilder()
                     .WithAlgorithm(new HMACSHA256Algorithm())
                     .WithSecret(secret)
                     .MustVerifySignature()
                     .Decode<TokenModel>(jwtToken);
                tokenModel = payload;
            }
            catch (TokenExpiredException tokenExpiredException)
            {
                //Console.WriteLine("Token has expired");

            }
            catch (SignatureVerificationException signatureVerificationException)
            {
                //Console.WriteLine("Token has invalid signature");
            }
            catch (Exception ex)
            {

            }
            return tokenModel;
        }


        public static TokenModel DecodeTokenFromRequestHeader(HttpRequest httpRequest)
        {
            var authToken = "";
            if (httpRequest.Headers.ContainsKey("Authorization"))
            {
                authToken = httpRequest.Headers["Authorization"].FirstOrDefault();
                authToken = authToken.Replace("Bearer ", "");
            }
            return DecodeToken(authToken);
        }



    }
}
