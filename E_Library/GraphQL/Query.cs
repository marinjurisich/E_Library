using bl;
using E_Library.Data;
using E_Library.Models;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HotChocolate.Data.Sorting.Expressions;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Data.Projections.Expressions;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace E_Library.GraphQL {

    public class Query {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public Query(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }


        [UsePaging(MaxPageSize = 1001)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Author> GetAuthor([ScopedService] LibraryContext context) {

            return context.Authors;

        }

        [AccessData]
        [UsePaging(MaxPageSize = 1001)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [AccessData]
        public IQueryable<Book> GetBook([ScopedService] LibraryContext context) {

            var user_id = _session.GetInt32("userid");

            //var res = context.Books.Where(l => l.Loan.User_id == user_id || l.Loan.User_id == null);
            var res = context.Books.AsQueryable();

            //IQueryable<Book> books = context.Books.AsQueryable();

            //var allMiddlewareApplied = books
            //    .Sort((HotChocolate.Resolvers.IResolverContext) context)
            //    .Filter((HotChocolate.Resolvers.IResolverContext) context)
            //    .Project((HotChocolate.Resolvers.IResolverContext) context);


            return res;

        }

        [Authorize]
        [UsePaging(MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Loan> GetLoan([ScopedService] LibraryContext context) {

            var user_id = _session.GetInt32("userid");

            var data = context.Loans.Where(e => e.User_id == user_id);

            return data;

        }

        [Authorize(Roles = new[] { "admin" })]
        [UsePaging(MaxPageSize = 1001)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<User>? GetUser([ScopedService] LibraryContext context) {

            var user_id = _session.GetInt32("userid");

            return context.Users.Where(e => e.Id == user_id); ;

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
            //claims.Add(new Claim("guid", user.Guid));


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "https://localhost:5000",
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        //public async Task<Book> GetFilteredLoans(BookBatchDataLoader dataLoader) {
        //    var book = await dataLoader.LoadAsync();
        //    return (Book) book;
        //}

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

    //public class BookBatchDataLoader : BatchDataLoader<string, Book> {
    //    private readonly LibraryContext _context;

    //    public BookBatchDataLoader(
    //        LibraryContext context, IBatchScheduler batchScheduler) : base(batchScheduler) {
    //        _context = context;
    //    }

    //    protected override async Task<IReadOnlyDictionary<string, Book>> LoadBatchAsync(
    //        IReadOnlyList<string> keys,
    //        CancellationToken cancellationToken) {
    //        // instead of fetching one person, we fetch multiple persons
    //        var persons = await _context.Books.;
    //        return persons.ToDictionary(x => x.Id);
    //    }
    //}

    public class AccessDataAttribute : ObjectFieldDescriptorAttribute {



        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member) {
            descriptor.Use(next => async context =>
            {
                var d = context.ContextData;
                // before the resolver pipeline
                await next(context);
                // after the resolver pipeline
                var dd = context.ContextData;

                if (context.Result is IQueryable<Book> query) {


                }
            });
        }
    }

}

