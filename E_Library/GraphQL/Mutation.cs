using E_Library.Data;
using E_Library.GraphQL;
using E_Library.Models;

namespace E_Library.GraphQL {


    public class Mutation {

        [UseDbContext(typeof(LibraryContext))]
        public async Task<AddBookPayload> AddBookAsync(AddBookInput input, [ScopedService] LibraryContext context) {

            var book = new Book {
                Title = input.Title,
                Year = input.Year,
                AuthorId = input.AuthorId,
                Isbn = input.Isbn,
                Price = input.Price,
                Edition_statement = input.Edition_statement,
                Item_call_number = input.Item_call_number,
                Publisher_code = input.Publisher_code,
                Number_of_copies = input.Number_of_copies
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return new AddBookPayload(book);

        }

        [UseDbContext(typeof(LibraryContext))]
        public async Task<AddBookPayload> UpdateBookAsync(UpdateBookInput input, [ScopedService] LibraryContext context) {

            var book = context.Books.Single(b => b.Id == input.Id);

            book.Item_call_number = input.Item_call_number;
            book.Number_of_copies = input.Number_of_copies;
            book.Price = input.Price;
            book.Publisher_code = input.Publisher_code;
            book.Edition_statement = input.Edition_statement;

            await context.SaveChangesAsync();

            return new AddBookPayload(book);

        }

        [UseDbContext(typeof(LibraryContext))]
        public async Task<int> DeleteBookAsync(int id, [ScopedService] LibraryContext context) {

            var itemToRemove = context.Books.SingleOrDefault(b => b.Id == id); //returns a single item.

            if (itemToRemove != null) {
                context.Books.Remove(itemToRemove);
                await context.SaveChangesAsync();
                return 200;
            } else {
                return 500;
            }

        }

        [UseDbContext(typeof(LibraryContext))]
        public async Task<AddLoanPayload> AddLoanAsync(AddLoanInput input, [ScopedService] LibraryContext context) {

            var loan = new Loan {
                User_id = input.User_id,
                Book_id = input.Book_id,
            };

            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            return new AddLoanPayload(loan);

        }

        [UseDbContext(typeof(LibraryContext))]
        public async Task<int> DeleteLoanAsync(int id, [ScopedService] LibraryContext context) {

            var itemToRemove = context.Loans.SingleOrDefault(l => l.Id == id); //returns a single item.

            if (itemToRemove != null) {
                context.Loans.Remove(itemToRemove);
                await context.SaveChangesAsync();
                return 200;
            } else {
                return 500;
            }

        }

    }

}


