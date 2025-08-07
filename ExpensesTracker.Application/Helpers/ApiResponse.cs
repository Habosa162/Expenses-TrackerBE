using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Application.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static IActionResult SuccessResponse(T data, string message = "Success")
        {
            return new OkObjectResult(new ApiResponse<T>(true, message, data));
        }

        public static IActionResult NotFoundResponse(string message = "Not Found")
        {
            return new NotFoundObjectResult(new ApiResponse<T>(false, message));
        }

        public static IActionResult FailureResponse(string message = "Something went wrong")
        {
            return new BadRequestObjectResult(new ApiResponse<T>(false, message));
        }
    }
}
