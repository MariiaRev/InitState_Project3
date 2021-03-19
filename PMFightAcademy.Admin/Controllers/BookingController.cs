using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PMFightAcademy.Admin.Contract;
using PMFightAcademy.Admin.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Booking controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [SwaggerTag("BookingController for check books for admin and remove some")]
    public class BookingController : ControllerBase
    {
        /// <summary>
        /// Constructor for booking
        /// </summary>
        public BookingController()
        {

        }

        /// <summary>
        /// Return all booked services
        /// </summary>
        ///  <param name="pageSize">The count of books to return at one time.</param>
        /// <param name="page">The current page number.</param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what can be  booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Return all booked services 
        /// if notFounded return NF
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{pageSize}/{page}")]
        [ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServices([FromRoute] int pageSize,[FromRoute] int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// select booked services on person
        /// </summary>
        /// <param name="client"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what is booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots
        /// </returns>
        /// <remarks>
        /// Return list about booked info for Client
        /// not founded if no Client 
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(typeof(List<BookingContract>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForClient(int id)
        {
            throw new NotImplementedException();
        }






        /// <summary>
        /// Select booked services on coach
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return list of slots what booked
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots
        /// </returns>
        /// <remarks>
        /// Return list about booked info for coach
        /// not founded if no coaches 
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("coach/{coachId}")]
        [ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookedServiceForCoach(int coachId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return if book is successful deleted
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Use for delete book , using post method
        /// return ok if successes
        /// return Not found if not founded 
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("delete")]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteBook([FromBody]BookingContract bookId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update file
        /// </summary>
        /// <param name="newBook"></param>
        /// <returns>
        /// <see cref="HttpStatusCode.OK"/>return if book is successful deleted
        /// <see cref="HttpStatusCode.NotFound"/> not founded slots</returns>
        /// <remarks>
        /// Send with the same Id new values
        /// and they will be updated
        /// </remarks>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateBook(BookingContract newBook)
        {
            throw new NotImplementedException();
        }



        ///// <Not useble part for create book>
        ///// Get book services 
        ///// </summary>
        ///// <param name="train"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return ok if its booked
        ///// <see cref="HttpStatusCode.NotFound"/> return not founded slots
        ///// </returns>
        ///// <remarks>
        ///// Return a list of (DTO) if OK
        ///// Not found is some not founded to book
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpPost("book")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> BookService([FromBody]BookingContract train)
        //{
        //    throw  new NotImplementedException();
        //}
        ///// <summary>
        ///// Get book services 
        ///// </summary>
        ///// <param name="train"></param>
        ///// <returns>
        ///// <see cref="HttpStatusCode.OK"/>return ok if its booked
        ///// <see cref="HttpStatusCode.NotFound"/> return not founded slots
        ///// </returns>
        ///// <remarks>
        ///// Return a list of (DTO) if OK
        ///// Not found is some not founded to book
        ///// </remarks>
        ///// <exception cref="NotImplementedException"></exception>
        //[HttpPost]
        //[ProducesResponseType(typeof(List<BookingContract>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> CompactBookService([FromBody] BookingContract train)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
