using bl;
using E_Library.Data;
using E_Library.Models;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Library.GraphQL {

    public class Query {


        [UsePaging(MaxPageSize = 1001)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Author> GetAuthor([ScopedService] LibraryContext context) {
             
            return context.Authors;

        }

        
        [UsePaging(MaxPageSize = 1001)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> GetBook([ScopedService] LibraryContext context) {

            return context.Books;

        }

        [Authorize]
        [UsePaging(MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Loan> GetLoan([ScopedService] LibraryContext context) {

            return context.Loans;

        }

        [Authorize(Roles = new[] { "admin" })]
        [UsePaging(MaxPageSize = 1001)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<User>? GetUser([ScopedService] LibraryContext context) {


                return context.Users;


        }

        [Authorize]
        public IQueryable<User>? GetMe(string userGuid, [ScopedService] LibraryContext context) {

            return context.Users.Where(x => x.Guid.Equals(userGuid));

        }


        public static string GetJWTAuthKey(User user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey"));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            claims.Add(new Claim("role", user.Role));
            

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "https://localhost:5000",
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }





        protected void Configure(IObjectTypeDescriptor descriptor) {
            descriptor
                .Field("book")
                .UsePaging()
                .Resolve(context =>
                {
                    var repository = context.Service<LibraryContext>();

                    return repository.Books;
                });
        }
    }

}

