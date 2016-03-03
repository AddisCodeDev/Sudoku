using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Addiscode.SudokuSolver
{
    public interface IHasResponseStatus
    {
        ResponseStatus ResponseStatus { get; set; }
    }
    public class ResponseStatus
    {
        /// <summary>
        /// Instantiates a Response Status with an Errors list
        /// of Count zero i.e. no errors
        /// </summary>
        public ResponseStatus()
        {
            Message = ResponseStatusMessage.Successful;
            Errors = new List<ApiError>();
        }
        public ResponseStatus(ResponseStatus responseStatus)
        {
            Errors = responseStatus.Errors;
        }
        
        public void AddResponseStatus(ResponseStatus status)
        {
            Errors.AddRange(status.Errors);
            if (status.Errors.Any())
            {
                Message = ResponseStatusMessage.Unsuccessful;
            }
        }

        public void AddErrors(Exception exception)
        {
            this.Errors.Add(new ApiError { Message = exception.Message, Source = exception.Source });
            if (exception.InnerException != null)
                AddErrors(exception.InnerException);
            Message = ResponseStatusMessage.Unsuccessful;
        }


        /// <summary>
        /// Every Error Recorded its description is added as a string.
        /// Hence if this list's count is zero then it means there are no errors
        /// </summary>
        public List<ApiError> Errors { get; set; }

        /// <summary>
        /// A human friendly error message
        /// </summary>
        public string Message { get; set; }
        

    }
    public class ApiError
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public string Source { get; set; }
    }
}